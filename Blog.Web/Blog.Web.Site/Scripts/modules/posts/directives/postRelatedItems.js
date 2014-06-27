ngPosts.directive('postRelatedItems', [function () {
    var ctrlFn = function ($scope, $rootScope, postsService, blockUiService, errorService) {
        $scope.posts = [];

        $scope.getRelatedPosts = function () {
            blockUiService.blockIt({ elem: ".post-related-list" });

            postsService.getRecentPosts().then(function (resp) {
                _.each(resp, function(p) {
                    p.Url = "/blog/#/post/" + p.PostId;
                });

                $scope.posts = resp;
                blockUiService.unblockIt(".post-related-list");
            }, function(e) {
                errorService.displayErrorRedirect(e);
            });
        };

        $scope.getRelatedPosts();
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "postsService", "blockUiService", "errorService"];

    return {
        restrict: 'EA',
        scope: { parentpostid: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesUrl + "posts/postrelateditems.html",
        controller: ctrlFn
    };
}]);
