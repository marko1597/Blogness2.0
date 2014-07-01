ngPosts.factory('postsService', ["$http", "$q", "configProvider", "dateHelper",
    function ($http, $q, configProvider, dateHelper) {
        var postsApi = configProvider.getSettings().BlogApi == "" ?
            window.blogConfiguration.blogApi + "Posts/" :
            configProvider.getSettings().BlogApi + "Posts/";

        return {
            getPost: function (id) {
                var deferred = $q.defer();

                $http({
                    url: postsApi + id,
                    method: "GET"
                }).success(function (response) {
                    response.DateDisplay = dateHelper.getDateDisplay(response.CreatedDate);
                    deferred.resolve(response);
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
                        p.DateDisplay = dateHelper.getDateDisplay(p.CreatedDate);
                        p.Url = "/blog/#/post/" + p.PostId;
                    });
                    _.each(response.PostsByTags, function (p) {
                        p.DateDisplay = dateHelper.getDateDisplay(p.CreatedDate);
                        p.Url = "/blog/#/post/" + p.PostId;
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
                        p.DateDisplay = dateHelper.getDateDisplay(p.CreatedDate);
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
                        p.DateDisplay = dateHelper.getDateDisplay(p.CreatedDate);
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
                        p.DateDisplay = dateHelper.getDateDisplay(p.CreatedDate);
                    });
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
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