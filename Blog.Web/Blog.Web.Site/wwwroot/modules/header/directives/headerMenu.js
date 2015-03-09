ngHeader.directive('headerMenu', ["$templateCache", function ($templateCache) {
    var ctrlFn = function ($scope, $location, $rootScope, snapRemote, $http, $window, configProvider, authenticationService) {
        $scope.userLoggedIn = false;

        $scope.toggleClass = "nav-close";

        $scope.addPostButtonVisible = true;

        $scope.showAddPostButton = function() {
            return $scope.addPostButtonVisible;
        };

        $scope.goAddNewPost = function () {
            $('#blog-header-collapsible').collapse("hide");
            $location.path("/post/create/new");
        };

        $scope.$on('toggleNavigation', function (ev, d) {
            snapRemote.toggle(d.direction, undefined);
        });

        $scope.testDisplayError = function () {
            $('#blog-header-collapsible').collapse("hide");
            $rootScope.$broadcast("displayError", { Message: "This is a test error message." });
        };

        $scope.getUserInfo = function () {
            $('#blog-header-collapsible').collapse("hide");

            authenticationService.getUserInfo().then(function (response) {
                if (response.Message != undefined || response.Message != null) {
                    $rootScope.$broadcast("launchLoginForm", { canClose: true });
                } else {
                    $rootScope.$broadcast("displayError", { Message: JSON.stringify(response)});
                }
            });
        };

        $scope.showLoginForm = function() {
            $rootScope.$broadcast("launchLoginForm", { canClose: true });
        };

        $scope.showCommunitySelection = function () {
            $scope.$broadcast("launchCommunitySelectionDialog", { canClose: true });
        };

        $scope.logout = function() {
            authenticationService.logout();
            $window.location.href = configProvider.getSettings().BlogRoot + "/account";
        };

        $scope.toggleSocketDebugger = function() {
            $rootScope.$broadcast("toggleSocketDebugger");
        };

        $scope.$on("doneSelectingCommunities", function(ev, data) {
            console.log(data);
        });

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
                $scope.addPostButtonVisible = false;
            });

            snapper.on('close', function () {
                checkNav();
                $scope.addPostButtonVisible = true;
            });
        });
    };
    ctrlFn.$inject = ["$scope", "$location", "$rootScope", "snapRemote", "$http", "$window", "configProvider", "authenticationService"];

    var linkFn = function () {
    };

    return {
        link: linkFn,
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        template: $templateCache.get("header/headerMenu.html"),
        controller: ctrlFn
    };
}]);