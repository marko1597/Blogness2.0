ngLogin.directive('loginForm', function () {
    var ctrlFn = function ($scope, $rootScope, $timeout, $window, errorService, localStorageService, configProvider, authenticationService, blockUiService) {
        $scope.username = "";
        $scope.password = "";
        $scope.rememberMe = false;
        $scope.errorMessage = "";

        $scope.showErrorMessage = function() {
            if ($scope.errorMessage == "") {
                return false;
            }
            return true;
        };

        $scope.hasError = function () {
            if ($scope.errorMessage == "") {
                return "";
            }
            return "has-error";
        };

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

                    if ($scope.modal == undefined) {
                        $window.location.href = configProvider.getSettings().BlogRoot;
                    } else {
                        $rootScope.$broadcast("hideLoginForm");
                        $rootScope.$broadcast("userLoggedIn");
                    }
                } else {
                    blockUiService.unblockIt();
                    $scope.errorMessage = response.error_description;
                }
            }, function (error) {
                blockUiService.unblockIt();
                $scope.errorMessage = error.Message;
            });
        };
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$timeout", "$window", "errorService", "localStorageService", "configProvider", "authenticationService", "blockUiService"];

    return {
        restrict: 'EA',
        scope: { modal: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "login/loginform.html",
        controller: ctrlFn
    };
});
