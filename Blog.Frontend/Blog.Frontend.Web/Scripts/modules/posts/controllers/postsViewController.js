ngPosts.controller('postsViewController', ["$scope", "$location", "$routeParams", "postsService", "blockUiService",
    function ($scope, $location, $routeParams, postsService, blockUiService) {
        $scope.postId = $routeParams.postId;
        $scope.posts = {};
        $scope.postsList = [];
        $scope.isBusy = false;
        
        $scope.init = function () {
            blockUiService.blockIt();

            if ($scope.isBusy) {
                return;
            }
            $scope.isBusy = true;

            postsService.getPost($scope.postId).then(function (post) {
                $scope.post = post;
                blockUiService.unblockIt();

                postsService.getPopularPosts().then(function (resp) {
                    $scope.postsList = resp;
                    $scope.isBusy = false;
                }, function (e) {
                    console.log(e);
                });
            }, function (e) {
                console.log(e);
                $location.path("/404");
            });
        };

        $scope.init();
    }
]);