ngPosts.directive('postLikes', function () {
    var ctrlFn = function ($scope, $rootScope, postsHubService, postsService, localStorageService) {
        $scope.username = localStorageService.get("username");

        $scope.tooltip = {
            "title": "Click to favorite this post.",
        };

        $scope.$on("postsLikeUpdate", function(e, d) {
            if (d.PostId == $scope.data.PostId) {
                $scope.data.PostLikes = d.PostLikes;
                $rootScope.$apply();
            }
        });

        $scope.likePost = function() {
            postsService.likePost($scope.data.PostId, $scope.username).then(function(resp) {
                console.log(resp);
            }, function(e) {
                console.log(e);
            });
        };
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "postsHubService", "postsService", "localStorageService"];
    
    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesUrl + "posts/postlikes.html",
        controller: ctrlFn
    };
});
