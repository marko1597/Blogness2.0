ngPosts.directive('postRelatedItem', [function () {
    var ctrlFn = function ($scope) {
        $scope.thumbnailUrl = {
            "background-image": "url(" + $scope.post.PostContents[0].Media.ThumbnailUrl + ")"
        };
    };
    ctrlFn.$inject = ["$scope"];

    return {
        restrict: 'EA',
        scope: { post: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "posts/postrelateditem.html",
        controller: ctrlFn
    };
}]);
