ngPosts.factory('postsService', ["$http", "$q", "configProvider", function ($http, $q, configProvider) {
    var postsApi = configProvider.getSettings().BlogApi == "" ? window.blogConfiguration.blogApi + "Posts" : configProvider.getSettings().BlogApi + "Posts";

    return {
        getPopularPosts: function() {
            var deferred = $q.defer();

            $http({
                url: postsApi + "/popular",
                method: "GET"
            }).success(function(response) {
                deferred.resolve(response);
            }).error(function() {
                deferred.reject("An error occurred!");
            });

            return deferred.promise;
        },

        getRecentPosts: function() {
            var deferred = $q.defer();

            $http({
                url: postsApi + "/recent",
                method: "GET"
            }).success(function(response) {
                deferred.resolve(response);
            }).error(function() {
                deferred.reject("An error occurred!");
            });

            return deferred.promise;
        },

        getMorePosts: function (c) {
            var deferred = $q.defer();

            $http({
                url: postsApi + "/more/" + c,
                method: "GET"
            }).success(function (response) {
                deferred.resolve(response);
            }).error(function () {
                deferred.reject("An error occurred!");
            });

            return deferred.promise;
        },

        savePost: function(post) {
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