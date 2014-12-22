ngPosts.factory('postsService', ["$http", "$q", "blogSocketsService", "configProvider", "dateHelper",
    function ($http, $q, blogSocketsService, configProvider, dateHelper) {
        var baseApi = configProvider.getSettings().BlogApi === "" ?
            window.blogConfiguration.blogApi : configProvider.getSettings().BlogApi;
        var postsApi = baseApi + "Posts/";

        var addPostViewData = function (post) {
            post.DateDisplay = dateHelper.getDateDisplay(post.CreatedDate);
            post.Url = "/#/post/" + post.Id;

            return post;
        };

        var cachedPostsList = [];

        var getCachedPostId = function (currentPostId, isNext) {
            if (cachedPostsList && cachedPostsList.length > 0) {
                var cachedPostIds = _.pluck(cachedPostsList, 'Id');
                var isCurrentPostInCache = _.contains(cachedPostIds, currentPostId);

                if (!isCurrentPostInCache) return null;
                
                var index = _.indexOf(cachedPostIds, currentPostId);
                if (index < 0) return cachedPostIds[0];

                if (index === 0 && !isNext) {
                    return null;
                } else {
                    index = isNext ? index + 1 : index - 1;

                    if (cachedPostIds.length < index + 1) {
                        return null;
                    }

                    return cachedPostIds[index];
                }
            } else {
                return null;
            }
        };

        return {
            getPost: function (id) {
                var deferred = $q.defer();

                $http({
                    url: postsApi + id,
                    method: "GET"
                }).success(function (response) {
                    var post = addPostViewData(response);
                    deferred.resolve(post);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            getRelatedPosts: function (id) {
                var deferred = $q.defer();

                $http({
                    url: postsApi + id + "/related",
                    method: "GET"
                }).success(function (response) {
                    _.each(response.PostsByUser, function (p) {
                        addPostViewData(p);
                    });
                    _.each(response.PostsByTags, function (p) {
                        addPostViewData(p);
                    });

                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            getPopularPosts: function () {
                var deferred = $q.defer();

                $http({
                    url: postsApi + "popular",
                    method: "GET"
                }).success(function (response) {
                    _.each(response, function (p) {
                        addPostViewData(p);
                    });
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            getRecentPosts: function () {
                var self = this;
                var deferred = $q.defer();

                $http({
                    url: postsApi + "recent",
                    method: "GET"
                }).success(function (response) {
                    _.each(response, function (p) {
                        addPostViewData(p);
                        self.addToCachedPostsList([p]);
                    });
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            getByCommunity: function (id) {
                var self = this;
                var deferred = $q.defer();

                $http({
                    url: baseApi + "community/" + id + "/posts",
                    method: "GET"
                }).success(function (response) {
                    _.each(response, function (p) {
                        addPostViewData(p);
                        self.addToCachedPostsList([p]);
                    });
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            getMoreByCommunity: function (id, skip) {
                var self = this;
                var deferred = $q.defer();

                $http({
                    url: baseApi + "community/" + id + "/posts/more/" + skip,
                    method: "GET"
                }).success(function (response) {
                    _.each(response, function (p) {
                        addPostViewData(p);
                        self.addToCachedPostsList([p]);
                    });
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            getMoreRecentPosts: function (currentPostsCount) {
                var self = this;
                var deferred = $q.defer();

                if (!currentPostsCount || currentPostsCount === 0) currentPostsCount = cachedPostsList.length;

                $http({
                    url: postsApi + "recent/more/" + currentPostsCount,
                    method: "GET"
                }).success(function (response) {
                    _.each(response, function (p) {
                        addPostViewData(p);
                        self.addToCachedPostsList([p]);
                    });
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            getPostsByUser: function (userId) {
                var userPostsUrl = configProvider.getSettings().BlogApi == "" ?
                    window.blogConfiguration.blogApi + "user/" :
                    configProvider.getSettings().BlogApi + "user/";

                var deferred = $q.defer();

                $http({
                    url: userPostsUrl + userId + "/posts",
                    method: "GET"
                }).success(function (response) {
                    _.each(response, function (p) {
                        addPostViewData(p);
                    });
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            getMorePostsByUser: function (userId, skip) {
                var userPostsUrl = configProvider.getSettings().BlogApi == "" ?
                    window.blogConfiguration.blogApi + "user/" :
                    configProvider.getSettings().BlogApi + "user/";

                var deferred = $q.defer();

                $http({
                    url: userPostsUrl + userId + "/posts/more/" + skip,
                    method: "GET"
                }).success(function (response) {
                    _.each(response, function (p) {
                        addPostViewData(p);
                    });
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            subscribeToPost: function (id) {
                blogSocketsService.emit(configProvider.getSocketClientFunctions().unsubscribeViewPost, { postId: id });
                blogSocketsService.emit(configProvider.getSocketClientFunctions().subscribeViewPost, { postId: id });
            },

            addPost: function (post) {
                var deferred = $q.defer();

                $http({
                    url: postsApi,
                    method: "POST",
                    data: post
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            updatePost: function (post) {
                var deferred = $q.defer();

                $http({
                    url: postsApi,
                    method: "PUT",
                    data: post
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            likePost: function (postId, username) {
                var deferred = $q.defer();

                $http({
                    url: postsApi + "likes?username=" + username + "&postId=" + postId,
                    method: "POST"
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            addToCachedPostsList: function (postsList) {
                var cachedPostIds = _.pluck(cachedPostsList, 'Id');

                _.each(postsList, function (post) {
                    if (!_.contains(cachedPostIds, post.Id)) {
                        cachedPostsList.push(post);
                        return;
                    }
                });
            },

            getNextPostIdFromCache: function (currentPostId) {
                return getCachedPostId(currentPostId, true);
            },

            getPreviousPostIdFromCache: function (currentPostId) {
                return getCachedPostId(currentPostId, false);
            },
        };
    }
]);