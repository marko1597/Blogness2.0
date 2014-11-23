ngPosts.directive('postRelatedItems', [function () {
    var ctrlFn = function ($scope, $rootScope, postsService, errorService) {
        $scope.hasError = false;

        $scope.postsByTag = [];

        $scope.postsByUser = [];
        
        $scope.relatedCategories = [
            { name: 'By user', id: "user" },
            { name: 'By similar tags', id: "tags" }
        ];

        $scope.selectedCategory = $scope.relatedCategories[0];

        $scope.emptyPostsMessage = "There are no related posts.";

        $scope.getRelatedPosts = function () {
            if (!isNaN($scope.parentpostid)) {
                postsService.getRelatedPosts($scope.parentpostid).then(function (response) {
                    $scope.hasError = false;
                    $scope.postsByTag = response.PostsByTags;
                    $scope.postsByUser = response.PostsByUser;
                }, function(e) {
                    errorService.displayError(e);
                    $scope.hasError = true;
                });
            } else {
                $scope.hasError = true;
            }
        };

        $scope.displayEmptyPostsMessage = function() {
            if ($scope.selectedCategory.id == "user") {
                if ($scope.postsByUser.length > 0) {
                    return false;
                }
                return true;
            } else {
                if ($scope.postsByTag.length > 0) {
                    return false;
                }
                return true;
            }
        };

        $scope.emptyPostsStyle = function() {
            return $scope.hasError ? "alert-danger" : "alert-warning";
        };

        $scope.getEmptyPostsMessage = function () {
            return $scope.hasError ?
                "Something went wrong with loading the related posts! :(" :
                "There are no related posts yet.";
        };

        $scope.displayUser = function() {
            if ($scope.selectedCategory.id == "user") {
                if ($scope.postsByUser.length > 0) {
                    return true;
                }
                return false;
            }
            return false;
        };

        $scope.displayTag = function () {
            if ($scope.selectedCategory.id == "tags") {
                if ($scope.postsByTag.length > 0) {
                    return true;
                }
                return false;
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
