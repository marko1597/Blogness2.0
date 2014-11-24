ngPosts.directive('postViewCount', ["$templateCache", function ($templateCache) {
    var ctrlFn = function ($scope, $rootScope, $interval, configProvider) {
        $scope.viewCount = $scope.list;

        var stop;
        stop = $interval(function () {
            if (configProvider.getSocketClientFunctions().viewCountUpdate) {
                $scope.$on(configProvider.getSocketClientFunctions().viewCountUpdate, function (e, d) {
                    if (d.postId == $scope.postId) {
                        $scope.viewCount = d.viewCount;
                    }
                });
                $interval.cancel(stop);
                stop = undefined;
            }
        }, 250);
        
        $scope.$watch('list', function () {
            $scope.viewCount = $scope.list;
        });
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$interval", "configProvider"];

    return {
        restrict: 'EA',
        scope: {
            list: '=',
            postId: '='
        },
        replace: true,
        template: $templateCache.get("posts/postViewCount.html"),
        controller: ctrlFn
    };
}]);
