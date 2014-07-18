ngLogin.directive('loginFormModal', function () {
    var ctrlFn = function ($scope, $rootScope, $modal) {
        $scope.loginModal = $modal({
            template: window.blogConfiguration.templatesModulesUrl + "login/loginformmodal.html",
            show: false,
            keyboard: false,
            backdrop: 'static'
        });

        $rootScope.$on("launchLoginForm", function (ev, data) {
            if (data.canClose) {
                $scope.loginModal.$options.keyboard = true;
                $scope.loginModal.$options.backdrop = true;
            } else {
                $scope.loginModal.$options.keyboard = false;
                $scope.loginModal.$options.backdrop = 'static';
            }
            $scope.loginModal.$promise.then($scope.loginModal.show);
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
});
