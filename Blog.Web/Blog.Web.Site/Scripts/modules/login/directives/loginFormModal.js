ngLogin.directive('loginFormModal', function () {
    var ctrlFn = function ($scope, $rootScope, $modal) {
        $scope.loginModal = $modal({
            template: window.blogConfiguration.templatesModulesUrl + "login/loginformmodal.html",
            show: false,
            keyboard: false,
            backdrop: 'static'
        });

        $rootScope.$on("launchLoginForm", function () {
            $scope.loginModal.$promise.then($scope.loginModal.show);
        });

        $rootScope.$on("hideLoginForm", function() {
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
