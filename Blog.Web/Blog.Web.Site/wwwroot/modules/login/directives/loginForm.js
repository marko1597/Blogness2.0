ngLogin.directive('loginForm', ["$templateCache", function ($templateCache) {
    var ctrlFn = function ($scope, $rootScope, $timeout, $location, $window, errorService, localStorageService, configProvider, authenticationService) {
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
            authenticationService.login($scope.username, $scope.password).then(function (response) {
                if (response.error == undefined || response.error == null) {

                    if (!$scope.isModal()) {
                        $window.location.href = configProvider.getSettings().BlogRoot;
                    } else {
                        $rootScope.$broadcast("hideLoginForm");
                        $rootScope.$broadcast("userLoggedIn", { username: $scope.username });
                        $location.path("/");
                    }
                } else {
                    $scope.errorMessage = response.error_description;
                }
            }, function (error) {
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
    ctrlFn.$inject = ["$scope", "$rootScope", "$timeout", "$location", "$window", "errorService", "localStorageService", "configProvider", "authenticationService"];

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
        template: $templateCache.get("login/loginForm.html"),
        controller: ctrlFn
    };
}]);
