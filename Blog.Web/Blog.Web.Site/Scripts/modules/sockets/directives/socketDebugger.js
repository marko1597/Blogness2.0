ngBlogSockets.directive("socketDebugger", [
    function () {
        var ctrlFn = function ($scope, $rootScope, blogSocketsService, configProvider) {
            $scope.messages = [];

            $scope.show = false;

            $scope.channelSubscription = null;

            $scope.echoMessage = null;

            $scope.showEmptyMessage = function() {
                if ($scope.messages.length > 0) {
                    return false;
                }
                return true;
            };

            $scope.doEcho = function () {
                blogSocketsService.emit("echo", { message: $scope.echoMessage });
            };

            $scope.subscribeToChannel = function () {
                var id = parseInt($scope.channelSubscription);
                blogSocketsService.emit(configProvider.getSocketClientFunctions().subscribeViewPost, { postId: id });
            };

            $rootScope.$on("toggleSocketDebugger", function () {
                $scope.show = !$scope.show;
            });

            $rootScope.$on(configProvider.getSocketClientFunctions().publishMessage, function (ev, data) {
                $scope.addToMessages(configProvider.getSocketClientFunctions().publishMessage, data);
            });

            $rootScope.$on(configProvider.getSocketClientFunctions().commentLikesUpdate, function (ev, data) {
                $scope.addToMessages(configProvider.getSocketClientFunctions().commentLikesUpdate, data);
            });

            $rootScope.$on(configProvider.getSocketClientFunctions().commentAdded, function (ev, data) {
                $scope.addToMessages(configProvider.getSocketClientFunctions().commentAdded, data);
            });

            $rootScope.$on(configProvider.getSocketClientFunctions().postLikesUpdate, function (ev, data) {
                $scope.addToMessages(configProvider.getSocketClientFunctions().postLikesUpdate, data);
            });

            $scope.addToMessages = function(fn, data) {
                var message = {
                    fn: fn,
                    data: JSON.stringify(data)
                };
                $scope.messages.push(message);
                console.log(message);
            };
        };
        ctrlFn.$inject = ["$scope", "$rootScope", "blogSocketsService", "configProvider"];

        return {
            controller: ctrlFn,
            restrict: 'EA',
            replace: true,
            templateUrl: window.blogConfiguration.templatesModulesUrl + "sockets/socketDebugger.html"
        };
    }
]);