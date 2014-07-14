ngLogin.directive('loginForm', function () {
    var ctrlFn = function ($scope, $rootScope, $timeout, $window, errorService, localStorageService, configProvider, authenticationService, blockUiService) {
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

            authenticationService.login($scope.username, $scope.password).then(function (response) {
                if (response.error == undefined || response.error == null) {
                    localStorageService.add("username", $scope.username);
                    blockUiService.unblockIt();
                    $window.location.href = configProvider.getSettings().BlogRoot;
                } else {
                    blockUiService.unblockIt();
                    errorService.displayError({ Message: response.error_description });
                }
            }, function (error) {
                blockUiService.unblockIt();
                errorService.displayError(error);
            });
        };
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$timeout", "$window", "errorService", "localStorageService", "configProvider", "authenticationService", "blockUiService"];

    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "login/loginform.html",
        controller: ctrlFn
    };
});
