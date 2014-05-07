ngLogin.factory('loginService', ["$http", "$q", "$window", "configProvider", function ($http, $q, $window, configProvider) {
    var sessionApi = configProvider.getSettings().BlogRoot == "" ? window.blogConfiguration.blogRoot + "Profile/Authenticate" : configProvider.getSettings().BlogRoot + "Profile/Authenticate";
    var authApi = configProvider.getSettings().BlogApi == "" ? window.blogConfiguration.blogApi + "Authenticate" : configProvider.getSettings().BlogApi + "Authenticate";

    var apiAuthenticate = function (username, password) {
        var deferred = $q.defer();
        var credentials = {
            Username: username,
            Password: password
        };

        $http({
            url: authApi,
            method: "POST",
            data: credentials
        }).success(function (response) {
            deferred.resolve(response);
            $window.location.href = configProvider.getSettings().BlogRoot;
        }).error(function () {
            deferred.reject("Error authenticating in the API!");
        });

        return deferred.promise;
    };

    return {
        loginUser: function (username, password, rememberMe) {
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
                if (response.Session != null && response.User != null) {
                    apiAuthenticate(username, password).then(function (resp) {
                        deferred.resolve(response);
                    }, function (err) {
                        deferred.reject(err);
                    });
                } else {
                    deferred.reject("Username or password is invalid.");
                }
            }).error(function () {
                deferred.reject("Error communicating with login server!");
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
    };
}]);