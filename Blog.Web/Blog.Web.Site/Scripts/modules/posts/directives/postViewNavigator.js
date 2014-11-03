ngPosts.directive('postViewNavigator', [function () {
    var ctrlFn = function ($scope, $location, $rootScope, postsService) {
        $scope.nextPost = function () {
            if (!$rootScope.$stateParams.postId) return;

            var nextPost = postsService.getNextPostIdFromCache(parseInt($rootScope.$stateParams.postId));

            if (nextPost) {
                $location.path("/post/" + nextPost);
            } else {
                postsService.getMoreRecentPosts().then(function () {
                    nextPost = postsService.getNextPostIdFromCache(parseInt($rootScope.$stateParams.postId));

                    if (nextPost) {
                        $location.path("/post/" + nextPost);
                    } else {
                        $location.path("/");
                    }
                }, function () {
                    $location.path("/");
                });
            }
        };

        $scope.previousPost = function () {
            if (!$rootScope.$stateParams.postId) return;

            var previousPost = postsService.getPreviousPostIdFromCache(parseInt($rootScope.$stateParams.postId));

            if (previousPost) {
                $location.path("/post/" + previousPost);
            } else {
                $location.path("/");
            }
        };

        $scope.isVisible = function() {
            if ($rootScope.$stateParams.postId) {
                return true;
            };
            return false;
        };
    };
    ctrlFn.$inject = ["$scope", "$location", "$rootScope", "postsService"];

    return {
        restrict: 'EA',
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "posts/postViewNavigator.html",
        controller: ctrlFn
    };
}]);
