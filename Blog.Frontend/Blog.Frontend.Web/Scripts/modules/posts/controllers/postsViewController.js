ngPosts.controller('postsViewController', ["$scope", "$location", "$routeParams", "postsService", "userService", "errorService", "blockUiService",
    function ($scope, $location, $routeParams, postsService, userService, errorService, blockUiService) {
        $scope.postId = $routeParams.postId;
        $scope.post = {};
        $scope.user = {};
        $scope.postsList = [];
        $scope.isBusy = false;

        $scope.init = function () {
            blockUiService.blockIt();

            if ($scope.isBusy) {
                return;
            }
            $scope.isBusy = true;

            userService.getUserInfo().then(function (user) {
                $scope.user = user;

                postsService.getPost($scope.postId).then(function (post) {
                    $scope.post = post;
                    blockUiService.unblockIt();

                    postsService.getPopularPosts().then(function (list) {
                        $scope.postsList = list;
                        $scope.isBusy = false;
                        $scope.$broadcast("viewedPostLoaded", { PostId: $scope.post.PostId, PostLikes: $scope.post.PostLikes });
                    }, function (e) {
                        errorService.displayErrorUnblock({ Message: e });
                    });
                }, function (e) {
                    errorService.displayErrorUnblock({ Message: e });
                });
            }, function (e) {
                errorService.displayErrorUnblock({ Message: e });
            });
        };

        $scope.init();
    }
]);