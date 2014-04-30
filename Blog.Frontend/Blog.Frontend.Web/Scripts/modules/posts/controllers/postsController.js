postsModule.controller('postsController', ["$scope", "$timeout", "postsService", function ($scope, $timeout, postsService) {
    $scope.posts = [];
    $scope.size = "";
    $scope.errorContent = { Show: false, Type: "" };

    $scope.getPopularPosts = function () {
        postsService.getPopularPosts().then(function (resp) {
            $scope.posts = resp;
            console.log(resp);
        }, function (errorMsg) {
            alert(errorMsg);
        });
    };

    $scope.getErrorType = function () {
        return $scope.errorContent.Type;
    };

    $scope.$on("updatePostsSize", function(ev, size) {
        $scope.size = size;
    });

    $timeout(function () {
        $scope.$emit('iso-option', { layoutMode: 'masonry' });
    }, 1500);

    $scope.getPopularPosts();
}]);