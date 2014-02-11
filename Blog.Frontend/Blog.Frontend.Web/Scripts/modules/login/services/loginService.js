loginModule.factory('loginService', ["$http", "$q", "$timeout", "$cookies", "configProvider", function ($http, $q, $timeout, $cookies, configProvider) {
    var sessionApi = "";
    $timeout(function () {
        sessionApi = configProvider.getSettings().BlogApi + "Session";
    }, 1000);

    return {
        loginUser: function (username, password) {
            var deferred = $q.defer();
            var credentials = {
                Username: username,
                Password: password
            };

            $http({
                url: sessionApi,
                method: "POST",
                data: credentials
            }).success(function (response) {
                $cookies.username = response.User.UserName;
                $cookies.sessionId = response.Session.Token;
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