ngPosts.directive('postRelatedItems', [function () {
    var ctrlFn = function ($scope, $rootScope, postsService, blockUiService, errorService) {
        $scope.postsByTag = [];
        $scope.postsByUser = [];
        
        $scope.relatedCategories = [
            { name: 'By user', id: "user" },
            { name: 'By similar tags', id: "tags" }
        ];
        $scope.selectedCategory = $scope.relatedCategories[0];

        $scope.getRelatedPosts = function () {
            blockUiService.blockIt({ elem: ".post-related-list" });

            postsService.getRelatedPosts($scope.parentpostid).then(function (response) {
                $scope.postsByTag = response.PostsByTags;
                $scope.postsByUser = response.PostsByUser;
                blockUiService.unblockIt(".post-related-list");
            }, function (e) {
                errorService.displayErrorRedirect(e);
            });
        };

        $scope.displayUser = function() {
            if ($scope.selectedCategory.id == "user") {
                return true;
            }
            return false;
        };

        $scope.displayTag = function () {
            if ($scope.selectedCategory.id == "tags") {
                return true;
            }
            return false;
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
