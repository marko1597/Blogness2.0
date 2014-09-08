ngPosts.directive('postRelatedItems', [function () {
    var ctrlFn = function ($scope, $rootScope, postsService, errorService) {
        $scope.postsByTag = [];
        $scope.postsByUser = [];
        
        $scope.relatedCategories = [
            { name: 'By user', id: "user" },
            { name: 'By similar tags', id: "tags" }
        ];
        $scope.selectedCategory = $scope.relatedCategories[0];

        $scope.getRelatedPosts = function () {
            postsService.getRelatedPosts($scope.parentpostid).then(function (response) {
                $scope.postsByTag = response.PostsByTags;
                $scope.postsByUser = response.PostsByUser;
            }, function (e) {
                errorService.displayError(e);
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
    ctrlFn.$inject = ["$scope", "$rootScope", "postsService", "errorService"];

    return {
        restrict: 'EA',
        scope: { parentpostid: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "posts/postrelateditems.html",
        controller: ctrlFn
    };
}]);
