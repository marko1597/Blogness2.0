ngPosts.controller('postsViewController', ["$scope", "$routeParams",
    function ($scope, $routeParams) {
        $scope.postId = $routeParams.postId;
    }
]);