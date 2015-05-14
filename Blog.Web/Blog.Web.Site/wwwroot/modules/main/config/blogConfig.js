blog.config(["$routeProvider", "$httpProvider", "$provide", "$stateProvider",
    "$urlRouterProvider",
    function ($routeProvider, $httpProvider, $provide, $stateProvider, $urlRouterProvider) {
        $provide.factory('httpInterceptor', ["$q", "$location", "blockUiService", function ($q, $location, blockUiService) {
            return {
                request: function (config) {
                    blockUiService.blockIt();

                    return config || $q.when(config);
                },

                requestError: function (rejection) {
                    blockUiService.blockIt();
                    return $q.reject(rejection);
                },

                response: function (response) {
                    blockUiService.unblockIt();

                    return response || $q.when(response);
                },

                responseError: function (rejection) {
                    blockUiService.unblockIt();

                    return $q.reject(rejection);
                }
            };
        }]);

        $httpProvider.interceptors.push('httpInterceptor');
        $httpProvider.interceptors.push('authenticationInterceptorService');

        $urlRouterProvider.otherwise("/");

        $stateProvider
            .state('posts', {
                url: "/",
                controller: 'postsController',
                templateProvider: function ($templateCache) {
                    return $templateCache.get('posts/mainPagePosts.html');
                }
            })
            .state('viewpost', {
                url: "/post/:postId",
                controller: 'postsViewController',
                templateProvider: function ($templateCache) {
                    return $templateCache.get('posts/postView.html');
                }
            })
                .state('viewpost.gallery', {
                    url: "/gallery",
                    controller: 'mediaGalleryController'
                })
            .state('friends', {
                url: "/friends",
                templateProvider: function ($templateCache) {
                    return $templateCache.get('friends.html');
                }
            })
            .state('communities', {
                url: "/communities",
                controller: 'communitiesListController',
                templateProvider: function ($templateCache) {
                    return $templateCache.get('communities/communities.html');
                }
            })
            .state('viewcommunity', {
                url: "/community/:communityId",
                controller: 'communityViewController',
                templateProvider: function ($templateCache) {
                    return $templateCache.get('communities/communityView.html');
                }
            })
            .state('newcommunity', {
                url: "/community/create/new",
                controller: 'communityModifyController',
                templateProvider: function ($templateCache) {
                    return $templateCache.get('communities/communityEdit.html');
                }
            })
            .state('editcommunity', {
                url: "/community/edit/:communityId",
                controller: 'communityModifyController',
                templateProvider: function ($templateCache) {
                    return $templateCache.get('communities/communityEdit.html');
                }
            })
            .state('events', {
                url: "/events",
                templateProvider: function ($templateCache) {
                    return $templateCache.get('events.html');
                }
            })
            .state('newpost', {
                url: "/post/create/new",
                controller: 'postsModifyController',
                templateProvider: function ($templateCache) {
                    return $templateCache.get('posts/postUpdate.html');
                }
            })
            .state('editpost', {
                url: "/post/edit/:postId",
                controller: 'postsModifyController',
                templateProvider: function ($templateCache) {
                    return $templateCache.get('posts/postUpdate.html');
                }
            })
            .state('ownprofile', {
                url: "/user",
                controller: 'userProfileController',
                'abstract': true,
                templateProvider: function ($templateCache) {
                    return $templateCache.get('user/userView.html');
                }
            })
                .state('ownprofile.details', {
                    url: '',
                    controller: 'userProfileController',
                    templateProvider: function ($templateCache) {
                        return $templateCache.get('user/userProfileDetails.html');
                    }
                })
                .state('ownprofile.posts', {
                    url: '/posts',
                    controller: 'userProfilePostsController',
                    templateProvider: function ($templateCache) {
                        return $templateCache.get('user/userProfilePosts.html');
                    }
                })
                .state('ownprofile.comments', {
                    url: '/comments',
                    controller: 'userProfileCommentsController',
                    templateProvider: function ($templateCache) {
                        return $templateCache.get('user/userProfileComments.html');
                    }
                })
                .state('ownprofile.favorites', {
                    url: '/favorites',
                    controller: 'userProfileFavoritesController',
                    templateProvider: function ($templateCache) {
                        return $templateCache.get('user/userProfileFavorites.html');
                    }
                })
                .state('ownprofile.media', {
                    url: '/media',
                    controller: 'userProfileMediaController',
                    templateProvider: function ($templateCache) {
                        return $templateCache.get('user/userProfileMedia.html');
                    }
                })
                    .state('ownprofile.media.gallery', {
                        url: '/gallery/:albumName',
                        controller: 'mediaGalleryController'
                    })
            .state('othersprofile', {
                url: "/user/:username",
                controller: 'userProfileController',
                'abstract': true,
                templateProvider: function ($templateCache) {
                    return $templateCache.get('user/userView.html');
                }
            })
                .state('othersprofile.details', {
                    url: '',
                    controller: 'userProfileController',
                    templateProvider: function ($templateCache) {
                        return $templateCache.get('user/userProfileDetails.html');
                    }
                })
                .state('othersprofile.posts', {
                    url: '/posts',
                    controller: 'userProfilePostsController',
                    templateProvider: function ($templateCache) {
                        return $templateCache.get('user/userProfilePosts.html');
                    }
                })
                .state('othersprofile.comments', {
                    url: '/comments',
                    controller: 'userProfileCommentsController',
                    templateProvider: function ($templateCache) {
                        return $templateCache.get('user/userProfileComments.html');
                    }
                })
                .state('othersprofile.favorites', {
                    url: '/favorites',
                    controller: 'userProfileFavoritesController',
                    templateProvider: function ($templateCache) {
                        return $templateCache.get('user/userProfileFavorites.html');
                    }
                }).state('othersprofile.media', {
                    url: '/media',
                    controller: 'userProfileMediaController',
                    templateProvider: function ($templateCache) {
                        return $templateCache.get('user/userProfileMedia.html');
                    }
                })
                    .state('othersprofile.media.gallery', {
                        url: '/gallery/:albumName',
                        controller: 'mediaGalleryController'
                    })
            .state('error', {
                url: "/error",
                controller: 'errorPageController',
                templateProvider: function ($templateCache) {
                    return $templateCache.get('errorpage.html');
                }
            });
    }
]);