ngLogin.directive('loginFormModal', ["$templateCache", function ($templateCache) {
    var ctrlFn = function ($scope, $rootScope, $modal) {
        $scope.loginModal = $modal({
            template: "login/loginFormModal.html",
            show: false,
            keyboard: false,
            backdrop: 'static'
        });

        $rootScope.$on("launchLoginForm", function (ev, data) {
            try {
                if ($scope.loginModal.$options.show) return;

                if (data.canClose) {
                    $scope.loginModal.$options.keyboard = true;
                    $scope.loginModal.$options.backdrop = true;
                } else {
                    $scope.loginModal.$options.keyboard = false;
                    $scope.loginModal.$options.backdrop = 'static';
                }
                $scope.loginModal.$promise.then($scope.loginModal.show);
            } catch (ex) {
                $scope.loginModal.$options.keyboard = false;
                $scope.loginModal.$options.backdrop = 'static';
                $scope.loginModal.show();
            }
        });

        $rootScope.$on("hideLoginForm", function () {
            $scope.loginModal.hide();
        });
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$modal"];

    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        controller: ctrlFn
    };
}]);
