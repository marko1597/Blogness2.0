ngError.factory('errorService', ["$location", "$rootScope", "$window", "configProvider", "loginService", "localStorageService",
    function ($location, $rootScope, $window, configProvider, loginService, localStorageService) {
        var error = {};

        var isAuthorized = function (d) {
            if (d.Message.Status == 401 || d.Message.Status == 403) {
                return false;
            } else {
                return true;
            }
        };

        var logoutUser = function() {
            var username = localStorageService.get("username");
            loginService.logout(username).then(function (resp) {
                if (resp == null || resp == "") {
                    loginService.logoutApi(username).then(function (apiResponse) {
                        if (apiResponse === "true") {
                            $window.location.href = configProvider.getSettings().BlogRoot + 'authentication';
                        } else {
                            $rootScope.$broadcast("displayError", apiResponse);
                            $location.path("/error");
                        }
                    }, function (apiError) {
                        $rootScope.$broadcast("displayError", apiError);
                        $location.path("/error");
                    });
                } else {
                    $rootScope.$broadcast("displayError", d);
                    $location.path("/error");
                }
            }, function (e) {
                $rootScope.$broadcast("displayError", e);
                $location.path("/error");
            });
        };
        
        return {
            displayError: function (d) {
                $rootScope.$broadcast("displayError", d);
            },

            displayErrorRedirect: function (d) {
                $rootScope.$broadcast("displayError", d);

                if (isAuthorized(d)) {
                    $location.path("/error");
                } else {
                    logoutUser();
                }
            },

            setError: function (e) {
                error = e;
            },

            getError: function () {
                return error;
            }
        };
    }
]);