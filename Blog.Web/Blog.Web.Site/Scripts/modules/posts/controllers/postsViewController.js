ngPosts.controller('postsViewController', ["$scope", "$location", "$routeParams", "postsService",
    "postsHubService", "userService", "errorService", "blockUiService",
    function ($scope, $location, $routeParams, postsService, postsHubService, userService, errorService, blockUiService) {
        $scope.postId = parseInt($routeParams.postId);
        $scope.post = {};
        $scope.user = {};
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
            }, function (e) {
                blockUiService.unblockIt();
                errorService.displayErrorRedirect({ Message: e });
            });
        };

        $scope.getContentType = function (content) {
            if (content == undefined) return "image";

            var contentType = content.split('/');
            if (contentType[0] == "video") {
                return "video";
            } else {
                return "image";
            }
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
                    $scope.$broadcast("viewedPostLoaded", { PostId: $scope.post.Id, PostLikes: $scope.post.PostLikes });
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

        $scope.init();
    }
]);