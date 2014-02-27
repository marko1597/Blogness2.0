postsModule.directive('postContent', function () {
    var ctrlFn = function ($scope, $postsService) {
        $scope.posts = [];
        $scope.errorContent = { Show: false, Type: "" };

        $scope.getPopularPosts = function () {
            $postsService.getPopularPosts().then(function (resp) {
                $scope.posts = resp;
            }, function (errorMsg) {
                alert(errorMsg);
            });
        };

        $scope.getPopularPosts();
    };
    ctrlFn.$inject = ["$scope", "postsService"];

    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        template:
            '<div id="postContent">' +
                '<div id="posts-main">' +
                    '<div class="jumbotron">' +
                        '<h2>Jason Magpantay</h2>' +
                        '<p>Kris Arianne</p>' +
                        '<p><a class="btn btn-primary btn-lg" role="button">Learn more</a></p>' +
                    '</div>' +
                '</div>' +
            '</div>',
        controller: ctrlFn
    };
});
