ngLogin.factory('loginService', ["$http", "$q", "$window", "configProvider", function ($http, $q, $window, configProvider) {
    var sessionApi = configProvider.getSettings().BlogRoot == "" ? window.blogConfiguration.blogRoot + "Authentication" : configProvider.getSettings().BlogRoot + "Authentication";
    var authApi = configProvider.getSettings().BlogApi == "" ? window.blogConfiguration.blogApi + "Authenticate" : configProvider.getSettings().BlogApi + "Authenticate";

    var apiSignin = function (username, password) {
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
        }).error(function () {
            deferred.reject("Error authenticating in the API!");
        });

        return deferred.promise;
    };

    var apiSignout = function (username) {
        var deferred = $q.defer();
        var credentials = {
            Username: username
        };

        $http({
            url: authApi,
            method: "PUT",
            data: credentials
        }).success(function (response) {
            deferred.resolve(response);
        }).error(function () {
            deferred.reject("Error logging out in the API!");
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
                url: sessionApi + "/Login",
                method: "POST",
                data: credentials
            }).success(function (response) {
                if (response.Session != null && response.User != null) {
                    apiSignin(username, password).then(function (resp) {
                        if (resp === "true") {
                            deferred.resolve(response);
                        } else {
                            deferred.reject("Username or password is invalid.");
                        }
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
                url: sessionApi + "/Logout",
                method: "POST",
                data: credentials
            }).success(function (response) {
                if (response === "true") {
                    apiSignout(username).then(function (resp) {
                        if (resp === "true") {
                            deferred.resolve(response);
                        } else {
                            deferred.reject("Username or password is invalid.");
                        }
                    }, function (err) {
                        deferred.reject(err);
                    });
                } else {
                    deferred.reject("User has no valid session.");
                }
            }).error(function () {
                deferred.reject("Error communicating with login server!");
            });

            return deferred.promise;
        }
    };
}]);