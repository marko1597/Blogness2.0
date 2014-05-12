ngPosts.controller('postsController', ["$scope", "$interval", "localStorageService", "postsService", "postsStateService", "blockUiService", "dateHelper",
    function ($scope, $interval, localStorageService, postsService, postsStateService, blockUiService, dateHelper) {
        $scope.posts = [];
        $scope.size = "";
        $scope.errorContent = { Show: false, Type: "" };

        $scope.getPopularPosts = function () {
            blockUiService.blockIt();
            postsService.getPopularPosts().then(function (resp) {
                $scope.posts = resp;

                _.each(resp, function(p) {
                    p.CreatedDate = dateHelper.getDateDisplay(p.CreatedDate);
                });

                blockUiService.unblockIt();
                console.log(resp);
            }, function (errorMsg) {
                alert(errorMsg);
            });
        };

        $scope.getErrorType = function () {
            return $scope.errorContent.Type;
        };

        $scope.$on("updatePostsSize", function (ev, size) {
            $scope.size = size;
        });

        $scope.addPost = function() {
            postsStateService.setPostItem(null);
        };

        $scope.editPost = function(postId) {
            var post = _.findWhere($scope.posts, { PostId: postId });
            postsStateService.setPostItem(post);
        };

        /*
         * Layout Fix for Isotope
         * ----------------------
         */
        var stopApplyLayoutFlag;
        $scope.applyLayout = function () {
            if (angular.isDefined(stopApplyLayoutFlag)) return;

            stopApplyLayoutFlag = $interval(function () {
                $scope.$broadcast('iso-method', { name: null, params: null });
            }, 2000, 5);
        };
        $scope.stopApplyLayout = function () {
            if (angular.isDefined(stopApplyLayoutFlag)) {
                $interval.cancel(stopApplyLayoutFlag);
                stopApplyLayoutFlag = undefined;
            }
        };

        /*
         * Initial calls
         * -----------------------
         */
        $scope.getPopularPosts();
        $scope.applyLayout();
    }
]);