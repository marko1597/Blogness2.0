ngError.factory('errorService', ["$location", "$rootScope", "$window", "configProvider", "loginService", "localStorageService", "blockUiService",
    function ($location, $rootScope, $window, configProvider, loginService, localStorageService, blockUiService) {
        var error = {};
        
        return {
            displayErrorUnblock: function (d) {
                blockUiService.unblockIt();
                $rootScope.$broadcast("displayError", { Message: d.Message });
                if (isUnauthorized()) {
                    $location.path('/blog/profile/login');
                }
            },

            displayErrorRedirect: function (d) {
                blockUiService.unblockIt();
                $rootScope.$broadcast("displayError", { Message: d.Message });

                if (d.Status == 401) {
                    var  username = localStorageService.get("username");
                    loginService.logoutUser(username).then(function(resp) {
                        if (resp === "true") {
                            $window.location.href = configProvider.getSettings().BlogRoot + 'authentication';
                        } else {
                            $location.path("/error");
                        }
                    }, function(e) {
                        console.log(e);
                    });
                } else {
                    $location.path("/error");
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