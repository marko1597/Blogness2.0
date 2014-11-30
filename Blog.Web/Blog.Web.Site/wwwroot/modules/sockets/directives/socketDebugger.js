ngBlogSockets.directive("socketDebugger", ["$templateCache", "$interval",
    function ($templateCache, $interval) {
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

            var stopPublishMessage;
            stopPublishMessage = $interval(function () {
                if (configProvider.getSocketClientFunctions().publishMessage) {
                    $rootScope.$on(configProvider.getSocketClientFunctions().publishMessage, function (ev, data) {
                        $scope.addToMessages(configProvider.getSocketClientFunctions().publishMessage, data);
                    });

                    $interval.cancel(stopPublishMessage);
                    stopPublishMessage = undefined;
                }
            }, 250);

            var stopCommentLikesUpdate;
            stopCommentLikesUpdate = $interval(function () {
                if (configProvider.getSocketClientFunctions().commentLikesUpdate) {
                    $rootScope.$on(configProvider.getSocketClientFunctions().commentLikesUpdate, function (ev, data) {
                        $scope.addToMessages(configProvider.getSocketClientFunctions().commentLikesUpdate, data);
                    });
                    $interval.cancel(stopCommentLikesUpdate);
                    stopCommentLikesUpdate = undefined;
                }
            }, 250);

            var stopPostTopComments;
            stopPostTopComments = $interval(function () {
                if (configProvider.getSocketClientFunctions().postTopComments) {
                    $rootScope.$on(configProvider.getSocketClientFunctions().postTopComments, function (ev, data) {
                        $scope.addToMessages(configProvider.getSocketClientFunctions().postTopComments, data);
                    });
                    $interval.cancel(stopPostTopComments);
                    stopPostTopComments = undefined;
                }
            }, 250);

            var stopCommentAdded;
            stopCommentAdded = $interval(function () {
                if (configProvider.getSocketClientFunctions().commentAdded) {
                    $rootScope.$on(configProvider.getSocketClientFunctions().commentAdded, function (ev, data) {
                        $scope.addToMessages(configProvider.getSocketClientFunctions().commentAdded, data);
                    });
                    $interval.cancel(stopCommentAdded);
                    stopCommentAdded = undefined;
                }
            }, 250);

            var stopPostLikesUpdate;
            stopPostLikesUpdate = $interval(function () {
                if (configProvider.getSocketClientFunctions().postLikesUpdate) {
                    $rootScope.$on(configProvider.getSocketClientFunctions().postLikesUpdate, function (ev, data) {
                        $scope.addToMessages(configProvider.getSocketClientFunctions().postLikesUpdate, data);
                    });
                    $interval.cancel(stopPostLikesUpdate);
                    stopPostLikesUpdate = undefined;
                }
            }, 250);

            $scope.addToMessages = function(fn, data) {
                var message = {
                    fn: fn,
                    data: JSON.stringify(data)
                };
                $scope.messages.push(message);
            };
        };
        ctrlFn.$inject = ["$scope", "$rootScope", "blogSocketsService", "configProvider"];

        return {
            controller: ctrlFn,
            restrict: 'EA',
            replace: true,
            template: $templateCache.get("sockets/socketDebugger.html")
        };
    }
]);