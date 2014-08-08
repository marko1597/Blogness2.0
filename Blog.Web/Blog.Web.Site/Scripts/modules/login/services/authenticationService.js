ngLogin.factory('authenticationService', ['$http', '$q', 'configProvider', 'localStorageService',
    function ($http, $q, configProvider, localStorageService) {
        var authenticationApi = configProvider.getSettings().BlogApi == "" ?
            window.blogConfiguration.blogApi + "Account/" : configProvider.getSettings().BlogApi + "Account/";

        var authentication = {
            isAuthenticated: false,
            username: ""
        };

        return {
            saveRegistration: function (registerInfo) {
                this.logout();

                var deferred = $q.defer();

                $http({
                    url: authenticationApi + "register",
                    method: "POST",
                    data: registerInfo
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (err) {
                    deferred.reject(err);
                });

                return deferred.promise;
            },

            getUserInfo: function () {
                var deferred = $q.defer();

                $http({
                    url: authenticationApi + "userinfo",
                    method: "GET"
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (error) {
                    deferred.reject(error);
                });

                return deferred.promise;
            },

            getAuthenticationData: function () {
                return authentication;
            },

            createAuthenticationData: function () {
                var authData = localStorageService.get('authorizationData');
                if (authData) {
                    authentication.isAuthenticated = true;
                    authentication.username = authData.userName;
                }
            },

            login: function (username, password) {
                var deferred = $q.defer();
                var credentials = "grant_type=password&Username=" + username + "&Password=" + password;

                $http.post(authenticationApi + "login", credentials, {
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                }).success(function (response) {
                    localStorageService.add("username", username);
                    localStorageService.set('authorizationData', {
                         token: response.access_token, username: username
                    });

                    authentication.isAuthenticated = true;
                    authentication.username = username;

                    deferred.resolve(response);
                }).error(function (response) {
                    deferred.reject({ Message: response.error_description });
                });

                return deferred.promise;
            },

            logout: function () {
                localStorageService.remove('username');
                localStorageService.remove('authorizationData');
                authentication.isAuthenticated = false;
                authentication.username = "";
            }
        };
    }
]);