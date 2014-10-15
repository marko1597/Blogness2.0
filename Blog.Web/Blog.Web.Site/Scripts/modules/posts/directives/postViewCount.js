ngPosts.directive('postViewCount', [function () {
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
        templateUrl: window.blogConfiguration.templatesModulesUrl + "posts/postViewCount.html",
        controller: ctrlFn
    };
}]);
