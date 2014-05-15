ngPosts.controller('postsController', ["$scope", "$interval", "localStorageService", "postsService", "postsStateService", "blockUiService", "dateHelper",
    function ($scope, $interval, localStorageService, postsService, postsStateService, blockUiService, dateHelper) {
        $scope.posts = [];
        $scope.size = "";
        $scope.isBusy = false;
        $scope.errorContent = { Show: false, Type: "" };

        $scope.getPopularPosts = function () {
            blockUiService.blockIt();

            if ($scope.isBusy) {
                return;
            }
            $scope.isBusy = true;

            postsService.getPopularPosts().then(function (resp) {
                $scope.posts = [];

                _.each(resp, function(p) {
                    p.CreatedDate = dateHelper.getDateDisplay(p.CreatedDate);
                    $scope.posts.push(p);
                });

                $scope.isBusy = false;
                blockUiService.unblockIt();
            }, function (errorMsg) {
                alert(errorMsg);
            });
        };

        $scope.getMorePosts = function () {
            blockUiService.blockIt();

            if ($scope.isBusy) {
                return;
            }
            $scope.isBusy = true;
            
            postsService.getMorePosts($scope.posts.length).then(function (resp) {
                _.each(resp, function (p) {
                    p.CreatedDate = dateHelper.getDateDisplay(p.CreatedDate);
                    $scope.posts.push(p);
                });

                $scope.isBusy = false;
                blockUiService.unblockIt();
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

        $scope.$on("scrollBottom", function () {
            $scope.getMorePosts();
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