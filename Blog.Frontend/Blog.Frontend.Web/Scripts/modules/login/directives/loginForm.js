ngLogin.directive('loginForm', function () {
    var ctrlFn = function ($scope, $rootScope, $timeout, localStorageService, configProvider, loginService, blockUiService) {
        $scope.username = "";
        $scope.password = "";
        $scope.rememberMe = false;
        $scope.alert = { Title: "Oops!", Message: "", Show: false };

        $scope.login = function () {
            blockUiService.blockIt(
                '<h4>' +
                    '<img src="../content/images/loader-girl.gif" height="128" />' +
                '</h4>', {
                    border: 'none',
                    padding: '5px',
                    backgroundColor: '#000',
                    opacity: .5,
                    color: '#fff'
                });

            $scope.response = loginService.loginUser($scope.username, $scope.password, $scope.rememberMe).then(function (resp) {
                localStorageService.add("username", resp.User.UserName);
                console.log(resp);
                blockUiService.unblockIt();
            }, function (errorMsg) {
                $scope.alert.Message = errorMsg;
                $scope.alert.Show = true;
                blockUiService.unblockIt();

                $timeout(function () {
                    $scope.alert.Show = false;
                }, configProvider.getSettings().AlertTimer);
            });
        };

        $scope.closeAlert = function () {
            $scope.alert.Show = false;
        };
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$timeout", "localStorageService", "configProvider", "loginService", "blockUiService"];

    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesUrl + "login/loginform.html",
        controller: ctrlFn
    };
});
