blog.config(["$routeProvider", "$httpProvider", "$provide", "$stateProvider", "$urlRouterProvider",
    function ($routeProvider, $httpProvider, $provide, $stateProvider, $urlRouterProvider) {
        $provide.factory('httpInterceptor', ["$q", function ($q) {
            return {
                request: function (config) {
                    return config || $q.when(config);
                },

                requestError: function (rejection) {
                    return $q.reject(rejection);
                },

                response: function (response) {
                    return response || $q.when(response);
                },

                responseError: function (rejection) {
                    return $q.reject(rejection);
                }
            };
        }]);

        $httpProvider.interceptors.push('httpInterceptor');
        $httpProvider.interceptors.push('authenticationInterceptorService');

        $urlRouterProvider.otherwise("/error");

        $stateProvider
            .state('account', {
                url: "/"
            })
            .state('posts', {
                url: "/",
                templateUrl: window.blogConfiguration.templatesUrl + 'posts.html',
                controller: 'postsController'
            })
            .state('viewpost', {
                url: "/post/:postId",
                templateUrl: window.blogConfiguration.templatesUrl + 'viewpost.html',
                controller: 'postsViewController'
            })
            .state('friends', {
                url: "/friends",
                templateUrl: window.blogConfiguration.templatesUrl + 'friends.html'
            })
            .state('groups', {
                url: "/groups",
                templateUrl: window.blogConfiguration.templatesUrl + 'groups.html'
            })
            .state('events', {
                url: "/events",
                templateUrl: window.blogConfiguration.templatesUrl + 'events.html'
            })
            .state('newpost', {
                url: "/post/create/new",
                templateUrl: window.blogConfiguration.templatesUrl + 'modifypost.html',
                controller: 'postsModifyController'
            })
            .state('editpost', {
                url: "/post/edit/:postId",
                templateUrl: window.blogConfiguration.templatesUrl + 'modifypost.html',
                controller: 'postsModifyController'
            })
            .state('ownprofile', {
                url: "/user",
                templateUrl: window.blogConfiguration.templatesUrl + 'user.html',
                controller: 'userProfileController',
                'abstract': true
            })
                .state('ownprofile.details', {
                    url: '',
                    templateUrl: window.blogConfiguration.templatesUrl + 'modules/user/userprofiledetails.html',
                    controller: 'userProfileController'
                })
                .state('ownprofile.posts', {
                    url: '/posts',
                    templateUrl: window.blogConfiguration.templatesUrl + 'modules/user/userprofileposts.html',
                    controller: 'userProfilePostsController'
                })
                .state('ownprofile.comments', {
                    url: '/comments',
                    templateUrl: window.blogConfiguration.templatesUrl + 'modules/user/userprofilecomments.html',
                    controller: 'userProfileCommentsController'
                })
                .state('ownprofile.media', {
                    url: '/media',
                    templateUrl: window.blogConfiguration.templatesUrl + 'modules/user/userprofilemedia.html',
                    controller: 'userProfileMediaController'
                })
                .state('ownprofile.favorites', {
                    url: '/favorites',
                    templateUrl: window.blogConfiguration.templatesUrl + 'modules/user/userprofilefavorites.html',
                    controller: 'userProfileFavoritesController'
                })
            .state('othersprofile', {
                url: "/user/:username",
                templateUrl: window.blogConfiguration.templatesUrl + 'user.html',
                controller: 'userProfileController',
                'abstract': true
            })
                .state('othersprofile.details', {
                    url: '',
                    templateUrl: window.blogConfiguration.templatesUrl + 'modules/user/userprofiledetails.html',
                    controller: 'userProfileController'
                })
                .state('othersprofile.posts', {
                    url: '/posts',
                    templateUrl: window.blogConfiguration.templatesUrl + 'modules/user/userprofileposts.html',
                    controller: 'userProfilePostsController'
                })
                .state('othersprofile.comments', {
                    url: '/comments',
                    templateUrl: window.blogConfiguration.templatesUrl + 'modules/user/userprofilecomments.html',
                    controller: 'userProfileCommentsController'
                })
                .state('othersprofile.media', {
                    url: '/media',
                    templateUrl: window.blogConfiguration.templatesUrl + 'modules/user/userprofilemedia.html',
                    controller: 'userProfileMediaController'
                })
                .state('othersprofile.favorites', {
                    url: '/favorites',
                    templateUrl: window.blogConfiguration.templatesUrl + 'modules/user/userprofilefavorites.html',
                    controller: 'userProfileFavoritesController'
                })
            .state('error', {
                url: "/error",
                templateUrl: window.blogConfiguration.templatesUrl + 'errorpage.html',
                controller: 'errorPageController'
            });
    }
]);