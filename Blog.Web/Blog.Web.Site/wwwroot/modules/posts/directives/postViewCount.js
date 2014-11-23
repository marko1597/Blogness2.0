ngPosts.directive('postViewCount', ["$templateCache", function ($templateCache) {
    var ctrlFn = function ($scope, $rootScope, configProvider) {
        $scope.viewCount = $scope.list;

        $scope.$on(configProvider.getSocketClientFunctions().viewCountUpdate, function (e, d) {
            if (d.postId == $scope.postId) {
                $scope.viewCount = d.viewCount;
            }
        });
        
        $scope.$watch('list', function () {
            $scope.viewCount = $scope.list;
        });
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "configProvider"];

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
