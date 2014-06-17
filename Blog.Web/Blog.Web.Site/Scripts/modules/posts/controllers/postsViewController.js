ngPosts.controller('postsViewController', ["$scope", "$location", "$routeParams", "postsService", "commentsService",
    "userService", "errorService", "blockUiService",
    function ($scope, $location, $routeParams, postsService, commentsService, userService, errorService, blockUiService) {
        $scope.postId = $routeParams.postId;
        $scope.post = {};
        $scope.user = {};
        $scope.comments = [];
        $scope.postsList = [];
        $scope.isBusy = false;

        $scope.init = function () {
            if ($scope.isBusy) {
                return;
            }
            $scope.isBusy = true;

            userService.getUserInfo().then(function (user) {
                $scope.user = user;
                $scope.getViewedPost();
                $scope.getPostComments();
            }, function (e) {
                blockUiService.unblockIt();
                errorService.displayErrorRedirect({ Message: e });
            });
        };

        $scope.getPostsList = function() {
            postsService.getPopularPosts().then(function(list) {
                $scope.postsList = list;
            }, function(e) {
                errorService.displayError({ Message: e });
            });
        };

        $scope.getViewedPost = function () {
            blockUiService.blockIt();

            postsService.getPost($scope.postId).then(function (post) {
                if (post.Error == undefined) {
                    $scope.post = post;
                    $scope.isBusy = false;
                    $scope.$broadcast("viewedPostLoaded", { PostId: $scope.post.PostId, PostLikes: $scope.post.PostLikes });
                    $scope.$broadcast("resizeIsotopeItems");
                    blockUiService.unblockIt();
                } else {
                    blockUiService.unblockIt();
                    errorService.displayError({ Message: e });
                }
            }, function (e) {
                blockUiService.unblockIt();
                errorService.displayErrorRedirect({ Message: e });
            });
        };

        $scope.getPostComments = function() {
            commentsService.getCommentsByPost($scope.postId).then(function (comments) {
                if (comments.length > 0) {
                    $scope.comments = comments;
                    blockUiService.unblockIt();
                } else {
                    blockUiService.unblockIt();
                    errorService.displayError({ Message: e });
                }
            }, function (e) {
                blockUiService.unblockIt();
                errorService.displayErrorRedirect({ Message: e });
            });
        };

        $scope.init();
    }
]);