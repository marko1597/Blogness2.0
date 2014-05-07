ngPosts.factory('postsService', ["$http", "$q", "configProvider", function ($http, $q, configProvider) {
    var postsApi = configProvider.getSettings().BlogApi == "" ? window.blogConfiguration.blogApi + "Posts" : configProvider.getSettings().BlogApi + "Posts";

    return {
        getPopularPosts: function() {
            var deferred = $q.defer();

            $http({
                url: postsApi + "/popular/" + configProvider.getSettings().PostsThreshold,
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
                url: postsApi + "/recent/" + configProvider.getSettings().PostsThreshold,
                method: "GET"
            }).success(function(response) {
                deferred.resolve(response);
            }).error(function() {
                deferred.reject("An error occurred!");
            });

            return deferred.promise;
        }
    };
}]);