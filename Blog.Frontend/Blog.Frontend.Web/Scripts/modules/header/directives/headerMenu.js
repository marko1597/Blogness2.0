ngHeader.directive('headerMenu', function () {
    var ctrlFn = function ($scope, snapRemote) {
        $scope.userLoggedIn = false;
        $scope.toggleClass = "nav-close";

        snapRemote.getSnapper().then(function (snapper) {
            var checkNav = function () {
                if ($scope.toggleClass == "nav-open") {
                    $scope.toggleClass = "nav-close";
                } else {
                    $scope.toggleClass = "nav-open";
                }
            };

            snapper.on('open', function () {
                checkNav();
            });

            snapper.on('close', function () {
                checkNav();
            });
        });
    };
    ctrlFn.$inject = ["$scope", "snapRemote"];


    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesUrl + "header/headerMenu.html",
        controller: ctrlFn
    };
});
