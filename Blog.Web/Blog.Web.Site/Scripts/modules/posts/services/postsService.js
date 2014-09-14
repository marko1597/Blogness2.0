ngPosts.factory('postsService', ["$http", "$q", "blogSocketsService", "configProvider", "dateHelper",
    function ($http, $q, blogSocketsService, configProvider, dateHelper) {
        var postsApi = configProvider.getSettings().BlogApi == "" ?
            window.blogConfiguration.blogApi + "Posts/" :
            configProvider.getSettings().BlogApi + "Posts/";

        var addPostViewData = function(post) {
            post.DateDisplay = dateHelper.getDateDisplay(post.CreatedDate);
            post.Url = "/#/post/" + post.Id;

            return post;
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
                    _.each(response.PostsByUser, function(p) {
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
                var deferred = $q.defer();

                $http({
                    url: postsApi + "recent",
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

            getMoreRecentPosts: function (c) {
                var deferred = $q.defer();

                $http({
                    url: postsApi + "recent/more/" + c,
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

            getPostsByUser: function(userId) {
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
            }
        };
    }
]);