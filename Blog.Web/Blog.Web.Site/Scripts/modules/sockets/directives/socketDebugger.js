ngSockets.directive("socketDebugger", [
    function () {
        var ctrlFn = function ($scope, $rootScope) {
            $scope.messages = [];
            $scope.show = true;
            
            for (var i = 0; i < 10; i++) {
                $scope.messages.push({
                    fn: "function_" + i,
                    data: "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque"
                });
            }

            $rootScope.$on("toggleSocketDebugger", function() {
                $scope.show = !$scope.show;
            });
        };
        ctrlFn.$inject = ["$scope", "$rootScope"];

        return {
            controller: ctrlFn,
            restrict: 'EA',
            replace: true,
            templateUrl: window.blogConfiguration.templatesModulesUrl + "sockets/socketDebugger.html"
        };
    }
]);