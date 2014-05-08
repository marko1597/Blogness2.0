ngHeader.directive('headerMenu', function () {
    var ctrlFn = function ($scope, snapRemote) {
        $scope.userLoggedIn = false;
        $scope.toggleClass = "nav-close";

        snapRemote.getSnapper().then(function (snapper) {
            var checkNav = function () {
                if ($scope.toggleClass == "open") {
                    $scope.toggleClass = "nav-close";
                } else {
                    $scope.toggleClass = "nav-open";
                }
            };

            snapper.on('open', function () {
                checkNav();
                console.log("nav opened");
            });

            snapper.on('close', function () {
                checkNav();
                console.log("nav closed ");
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
