ngUser.directive('userPostItem', [function () {
    var ctrlFn = function ($scope) {
        $scope.post = $scope.data.Post;
        $scope.user = $scope.data.Post.User;
        $scope.username = $scope.user.Username;
        $scope.hasTags = $scope.data.Post.Tags.length > 0 ? true : false;

        $scope.getPostSize = function () {
            return $scope.data.Width;
        };
        
        $scope.editPost = function () {
            $location.path("/post/edit/" + $scope.post.Id);
        };
    };
    ctrlFn.$inject = ["$scope"];

    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "user/userPostItem.html",
        controller: ctrlFn
    };
}]);
