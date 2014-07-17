ngHeader.directive('headerMenu', function () {
    var ctrlFn = function ($scope, $location, $rootScope, snapRemote, $http, $window, configProvider, authenticationService) {
        $scope.userLoggedIn = false;
        $scope.toggleClass = "nav-close";

        $scope.goAddNewPost = function () {
            $('#blog-header-collapsible').collapse("hide");
            $location.path("/post/new");
        };

        $scope.testDisplayError = function () {
            $('#blog-header-collapsible').collapse("hide");
            $rootScope.$broadcast("displayError", { Message: "This is a test error message." });
        };

        $scope.getUserInfo = function () {
            $('#blog-header-collapsible').collapse("hide");

            authenticationService.getUserInfo().then(function (response) {
                if (response.Message != undefined || response.Message != null) {
                    $rootScope.$broadcast("launchLoginForm");
                } else {
                    $rootScope.$broadcast("displayError", { Message: JSON.stringify(response)});
                }
            }, function () {
                $rootScope.$broadcast("launchLoginForm");
            });
        };

        $scope.showLoginForm = function() {
            $rootScope.$broadcast("launchLoginForm");
        };

        $scope.logout = function() {
            authenticationService.logout();
            $window.location.href = configProvider.getSettings().BlogRoot + "/account";
        };

        snapRemote.getSnapper().then(function (snapper) {
            var checkNav = function () {
                if ($scope.toggleClass == "nav-open") {
                    $scope.toggleClass = "nav-close";
                } else {
                    $scope.toggleClass = "nav-open";
                }
                $('#blog-header-collapsible').collapse("hide");
            };

            snapper.on('open', function () {
                checkNav();
            });

            snapper.on('close', function () {
                checkNav();
            });
        });
    };
    ctrlFn.$inject = ["$scope", "$location", "$rootScope", "snapRemote", "$http", "$window", "configProvider", "authenticationService"];

    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "header/headerMenu.html",
        controller: ctrlFn
    };
});
