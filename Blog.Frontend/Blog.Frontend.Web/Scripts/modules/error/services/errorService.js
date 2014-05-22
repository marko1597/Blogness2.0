ngError.factory('errorService', ["$location", "$rootScope", "$window", "configProvider", "loginService", "localStorageService",
    function ($location, $rootScope, $window, configProvider, loginService, localStorageService) {
        var error = {};

        var isAuthorized = function (d) {
            if (d.Status == 401 || d.Status == 403) {
                return false;
            } else {
                return true;
            }
        };

        var logoutUser = function() {
            var username = localStorageService.get("username");
            loginService.logout(username).then(function (resp) {
                if (resp === "true") {
                    $window.location.href = configProvider.getSettings().BlogRoot + 'authentication';
                } else {
                    $location.path("/error");
                }
            }, function () {
                $location.path("/error");
            });
        };
        
        return {
            displayError: function (d) {
                $rootScope.$broadcast("displayError", { Message: d.Message });
            },

            displayErrorRedirect: function (d) {
                $rootScope.$broadcast("displayError", { Message: d.Message });

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