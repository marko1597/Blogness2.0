ngError.factory('errorService', ["$location", "$rootScope", "$window", "configProvider", "authenticationService",
    function ($location, $rootScope, $window, configProvider, authenticationService) {
        var error = {};

        var isAuthorized = function (d) {
            if (d.error === "invalid_grant" || d.Message === "Authorization has been denied for this request.") {
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
            });
        };

        var getModelStateErrors = function (d) {
            if (d.ModelState) {
                var errorDisplayMessage = "";

                for (var property in d.ModelState) {
                    if (d.ModelState.hasOwnProperty(property)) {
                        _.each(d.ModelState[property], function (err) {
                            errorDisplayMessage += err + " ";
                        });
                    }
                }

                return errorDisplayMessage;
            } else {
                return d;
            }
        };

        return {
            displayError: function (d) {
                error = d;
                if (isAuthorized(d)) {
                    $rootScope.$broadcast("displayError", getModelStateErrors(d));
                }
            },

            displayErrorRedirect: function (d) {
                error = d;
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