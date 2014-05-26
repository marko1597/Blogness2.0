ngHeader.directive('headerMenu', function () {
    var ctrlFn = function ($scope, $location, $rootScope, snapRemote, $http, configProvider) {
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

        $scope.yetAnotherTestDisplayError = function () {
            $('#blog-header-collapsible').collapse("hide");

            var api = configProvider.getSettings().BlogApi == "" ? window.blogConfiguration.blogApi + "debug/sendmessage" :
                configProvider.getSettings().BlogApi + "debug/sendmessage";

            $http({
                url: api + "/test-message",
                method: "GET"
            }).success(function (response) {
                $rootScope.$broadcast("displayError", { Message: response });
            }).error(function (error) {
                $rootScope.$broadcast("displayError", { Message: error.Message });
            });
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
    ctrlFn.$inject = ["$scope", "$location", "$rootScope", "snapRemote", "$http", "configProvider"];

    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesUrl + "header/headerMenu.html",
        controller: ctrlFn
    };
});
