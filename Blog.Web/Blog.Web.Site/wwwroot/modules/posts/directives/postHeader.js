ngPosts.directive('postHeader', [function () {
    var ctrlFn = function ($scope, $rootScope, $location, localStorageService) {
        $scope.username = localStorageService.get("username");

        $scope.isEditable = function () {
            if ($scope.user && $scope.user.UserName === $scope.username) {
                return true;
            }
            return false;
        };

        $scope.$on("loggedInUserInfo", function (ev, data) {
            if (data) {
                $scope.username = data.UserName;
            }
        });

        $rootScope.$watch('user', function () {
            if ($rootScope.user) {
                $scope.username = $rootScope.user.UserName;
            }
        });

        $scope.editPost = function () {
            $location.path("/post/edit/" + $scope.post.Id);
        };
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$location", "localStorageService"];

    return {
        restrict: 'EA',
        scope: {
            post: '=',
            user: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "posts/postHeader.html",
        controller: ctrlFn
    };
}]);
