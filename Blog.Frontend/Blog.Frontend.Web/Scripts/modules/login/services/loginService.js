loginModule.factory('loginService', ["$http", "$q", "$timeout", "$cookies", "$window", "configProvider", function ($http, $q, $timeout, $cookies, $window, configProvider) {
    var sessionApi = "";
    var authApi = "";

    $timeout(function () {
        sessionApi = configProvider.getSettings().BlogRoot + "Profile/Authenticate";
        authApi = configProvider.getSettings().BlogApi + "Authenticate";
    }, 1000);

    var apiAuthenticate = function(username, password) {
        var deferred = $q.defer();
        var credentials = {
            Username: username,
            Password: password
        };

        $http({
            url: authApi,
            method: "POST",
            data: credentials
        }).success(function(response) {
            deferred.resolve(response);
            $window.location.href = configProvider.getSettings().BlogRoot;
        }).error(function() {
            deferred.reject("An error occurred!");
        });

        return deferred.promise;
    };

    return {
        loginUser: function(username, password, rememberMe) {
            var deferred = $q.defer();
            var credentials = {
                Username: username,
                Password: password,
                RememberMe: rememberMe
            };

            $http({
                url: sessionApi,
                method: "POST",
                data: credentials
            }).success(function (response) {
                deferred.resolve(response);
                apiAuthenticate(username, password);
            }).error(function () {
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