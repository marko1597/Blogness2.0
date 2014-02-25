loginModule.factory('loginService', ["$http", "$q", "$timeout", "$cookies", "$window", "configProvider", function ($http, $q, $timeout, $cookies, $window, configProvider) {
    var sessionApi = "";
    $timeout(function () {
        sessionApi = configProvider.getSettings().BlogRoot + "Profile/Authenticate";
    }, 1000);

    return {
        loginUser: function(username, password) {
            var deferred = $q.defer();
            var credentials = {
                Username: username,
                Password: password,
                RememberMe: null
            };

            $http({
                url: sessionApi,
                method: "POST",
                data: credentials
            }).success(function(response) {
                //$cookies.username = response.User.UserName;
                //$cookies.sessionId = response.Session.Token;
                $window.location.href = configProvider.getSettings().BlogRoot;
                deferred.resolve(response);
            }).error(function() {
                deferred.reject("An error occurred!");
            });

            return deferred.promise;
        },

        logoutUser: function(username) {
            var deferred = $q.defer();
            var credentials = {
                Username: username
            };

            $http({
                url: sessionApi,
                method: "DELETE",
                data: credentials
            }).success(function(response) {
                deferred.resolve(response);
            }).error(function() {
                deferred.reject("An error occurred!");
            });

            return deferred.promise;
        }
    };
}]);