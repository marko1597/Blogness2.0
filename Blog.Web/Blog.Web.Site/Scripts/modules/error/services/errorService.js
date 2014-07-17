ngError.factory('errorService', ["$location", "$rootScope", "$window", "configProvider", "authenticationService", "localStorageService",
    function ($location, $rootScope, $window, configProvider, authenticationService, localStorageService) {
        var error = {};

        var isAuthorized = function (d) {
            if (d.error == "invalid_grant") {
                return false;
            } else {
                return true;
            }
        };

        var logoutUser = function () {
            authenticationService.logout().then(function () {
                $window.location.href = configProvider.getSettings().BlogRoot + '/account';
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
                if (isAuthorized(d)) {
                    $location.path("/error");
                } else {
                    logoutUser();
                }
            },

            highlightField: function () {
                return "field-error";
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