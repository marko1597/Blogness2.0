postsModule.factory('postsService', ["$http", "$q", "$timeout", "configProvider", function ($http, $q, $timeout, configProvider) {
    var postsApi = "";
    $timeout(function () {
        postsApi = configProvider.getSettings().BlogApi + "Posts";
    }, 1000);

    return {
        getPopularPosts: function () {
            var deferred = $q.defer();

            $http({
                url: postsApi,
                method: "GET"
            }).success(function (response) {
                $cookies.username = response.User.UserName;
                $cookies.sessionId = response.Session.Token;
                $window.location.href = "http://localhost/blog/";
                deferred.resolve(response);
            }).error(function () {
                deferred.reject("An error occurred!");
            });

            return deferred.promise;
        },

        logoutUser: function (username) {
            var deferred = $q.defer();
            var credentials = {
                Username: username
            };

            $http({
                url: sessionApi,
                method: "DELETE",
                data: credentials
            }).success(function (response) {
                deferred.resolve(response);
            }).error(function () {
                deferred.reject("An error occurred!");
            });

            return deferred.promise;
        }
    }
}]);