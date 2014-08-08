ngLogin.directive('loginForm', function () {
    var ctrlFn = function ($scope, $rootScope, $timeout, $window, errorService, localStorageService, configProvider, authenticationService, blockUiService) {
        $scope.username = "";
        $scope.password = "";
        $scope.rememberMe = false;
        $scope.errorMessage = "";
        $scope.registerPopover = {
            title: "Don't have an account?",
            content: "Create an account with Bloggity so you bloggity-bliggity-blog away!"
        };

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
                    blockUiService.unblockIt();

                    if (!$scope.isModal()) {
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

        $scope.isModal = function() {
            if ($scope.modal == undefined) {
                return false;
            } else {
                return $scope.modal ? true : false;
            }
        };
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$timeout", "$window", "errorService", "localStorageService", "configProvider", "authenticationService", "blockUiService"];

    var linkFn = function(scope, elem) {
        scope.showRegisterForm = function() {
            $(elem).closest(".modal-body").addClass("hover");
        };
    };

    return {
        restrict: 'EA',
        scope: { modal: '=' },
        link: linkFn,
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "login/loginform.html",
        controller: ctrlFn
    };
});
