ngLogin.directive('loginForm', function () {
    var ctrlFn = function ($scope, $rootScope, $timeout, $window, errorService, localStorageService, configProvider, loginService, blockUiService) {
        $scope.username = "";
        $scope.password = "";
        $scope.rememberMe = false;

        $scope.login = function () {
            blockUiService.blockIt(
                '<h4>' +
                    '<img src="../blog/content/images/loader-girl.gif" height="128" />' +
                '</h4>', {
                    border: 'none',
                    padding: '5px',
                    backgroundColor: '#000',
                    opacity: .5,
                    color: '#fff'
                });

            loginService.login($scope.username, $scope.password, $scope.rememberMe).then(function (siteResponse) {
                loginService.loginApi($scope.username, $scope.password, $scope.rememberMe).then(function(apiResponse) {
                    if (apiResponse === "true") {
                        localStorageService.add("username", siteResponse.User.UserName);
                        blockUiService.unblockIt();
                        $window.location.href = configProvider.getSettings().BlogRoot;
                    } else {
                        errorService.displayError({ Message: "Error communicating with the login server." });
                    }
                }, function(error) {
                    errorService.displayError({ Message: error });
                });
            }, function (error) {
                errorService.displayError({ Message: error });
            });
        };
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$timeout", "$window", "errorService", "localStorageService", "configProvider", "loginService", "blockUiService"];

    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesUrl + "login/loginform.html",
        controller: ctrlFn
    };
});
