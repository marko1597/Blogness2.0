ngLogin.directive('loginForm', function () {
    var ctrlFn = function ($scope, $rootScope, $timeout, $window, errorService, localStorageService, configProvider, loginService, blockUiService) {
        $scope.username = "";
        $scope.password = "";
        $scope.rememberMe = false;

        $scope.login = function () {
            blockUiService.blockIt({
                html: '<h4><img src="content/images/loader-girl.gif" height="128" /></h4>',
                css: {
                    border: 'none',
                    padding: '5px',
                    backgroundColor: '#000',
                    opacity: .5,
                    color: '#fff'
                }
            });

            loginService.login($scope.username, $scope.password, $scope.rememberMe).then(function (siteResponse) {
                if (siteResponse.Error == undefined || siteResponse.Error == null) {
                    loginService.loginApi($scope.username, $scope.password, $scope.rememberMe).then(function (apiResponse) {
                        if (apiResponse === "true") {
                            localStorageService.add("username", siteResponse.User.UserName);
                            blockUiService.unblockIt();
                            $window.location.href = configProvider.getSettings().BlogRoot;
                        } else {
                            blockUiService.unblockIt();
                            errorService.displayError({ Message: "Error authenticating with Api" });
                        }
                    }, function (error) {
                        blockUiService.unblockIt();
                        errorService.displayError(error.Error);
                    });
                } else {
                    blockUiService.unblockIt();
                    errorService.displayError(siteResponse.Error);
                }
            }, function (error) {
                blockUiService.unblockIt();
                errorService.displayError(error.Error);
            });
        };
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$timeout", "$window", "errorService", "localStorageService", "configProvider", "loginService", "blockUiService"];

    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "login/loginform.html",
        controller: ctrlFn
    };
});
