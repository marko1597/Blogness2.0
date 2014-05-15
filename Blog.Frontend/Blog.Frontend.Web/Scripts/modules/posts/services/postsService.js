ngPosts.factory('postsService', ["$http", "$q", "configProvider", "postsStateService", "dateHelper",
    function ($http, $q, configProvider, postsStateService, dateHelper) {
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
                    response.CreatedDate = dateHelper.getDateDisplay(response.CreatedDate);
                    deferred.resolve(response);
                }).error(function () {
                    deferred.reject("An error occurred!");
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
                        p.CreatedDate = dateHelper.getDateDisplay(p.CreatedDate);
                    });
                    deferred.resolve(response);
                }).error(function () {
                    deferred.reject("An error occurred!");
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
                        p.CreatedDate = dateHelper.getDateDisplay(p.CreatedDate);
                    });
                    deferred.resolve(response);
                }).error(function () {
                    deferred.reject("An error occurred!");
                });

                return deferred.promise;
            },

            getMorePosts: function (c) {
                var deferred = $q.defer();

                $http({
                    url: postsApi + "more/" + c,
                    method: "GET"
                }).success(function (response) {
                    _.each(response, function (p) {
                        p.CreatedDate = dateHelper.getDateDisplay(p.CreatedDate);
                    });
                    deferred.resolve(response);
                }).error(function () {
                    deferred.reject("An error occurred!");
                });

                return deferred.promise;
            },

            savePost: function (post) {
                var deferred = $q.defer();

                $http({
                    url: postsApi,
                    method: "POST",
                    data: post
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function () {
                    deferred.reject("An error occurred!");
                });

                return deferred.promise;
            }
        };
    }]);