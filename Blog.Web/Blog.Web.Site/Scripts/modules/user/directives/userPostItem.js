ngUser.directive('userPostItem', [function () {
    var ctrlFn = function ($scope, $rootScope, $location, localStorageService) {
        $scope.post = $scope.data.Post;

        $scope.user = $scope.data.User;

        $scope.username = $scope.user.UserName;

        $scope.loggedInUsername = localStorageService.get("username");

        $scope.hasTags = $scope.data.Post.Tags.length > 0 ? true : false;

        $scope.getPostSize = function () {
            return $scope.data.Width;
        };

        $scope.isEditable = function() {
            if (($scope.user != null || $scope.user != undefined) && $scope.username === $scope.loggedInUsername) {
                return true;
            }
            return false;
        };
        
        $scope.editPost = function () {
            $location.path("/post/edit/" + $scope.post.Id);
        };

        $rootScope.$watch('user', function () {
            if ($rootScope.user) {
                $scope.loggedInUsername = $rootScope.user.UserName;
            }
        });
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$location", "localStorageService"];

    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "user/userPostItem.html",
        controller: ctrlFn
    };
}]);
