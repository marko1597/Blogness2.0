ngLogin.factory('loginService', ["$http", "$q", "$window", "configProvider", function ($http, $q, $window, configProvider) {
    var sessionApi = configProvider.getSettings().BlogRoot == "" ? window.blogConfiguration.blogRoot + "Authentication" : configProvider.getSettings().BlogRoot + "Authentication";
    var authApi = configProvider.getSettings().BlogApi == "" ? window.blogConfiguration.blogApi + "Authenticate" : configProvider.getSettings().BlogApi + "Authenticate";
    
    return {
        login: function (username, password, rememberMe) {
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
                    deferred.resolve(response);
                } else {
                    deferred.reject("Username or password is invalid.");
                }
            }).error(function () {
                deferred.reject("Error communicating with login server!");
            });

            return deferred.promise;
        },

        logout: function (username) {
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
                    deferred.resolve(response);
                } else {
                    deferred.reject("User has no valid session.");
                }
            }).error(function () {
                deferred.reject("Error communicating with login server!");
            });

            return deferred.promise;
        },

        loginApi: function (username, password, rememberMe) {
            var deferred = $q.defer();
            var credentials = {
                Username: username,
                Password: password,
                RememberMe: rememberMe
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
        },

        logoutApi: function (username) {
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
        }
    };
}]);