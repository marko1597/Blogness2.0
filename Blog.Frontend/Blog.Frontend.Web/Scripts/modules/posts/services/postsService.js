postsModule.factory('postsService', ["$http", "$q", "$timeout", "configProvider", function ($http, $q, $timeout, configProvider) {
    var postsApi = "";
    $timeout(function () {
        postsApi = configProvider.getSettings().BlogApi + "Posts";
    }, 1000);

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