ngPosts.controller('postsController', ["$scope", "$location", "$timeout", "$interval", "localStorageService", "postsService", "blockUiService", "errorService",
    function ($scope, $location, $timeout, $interval, localStorageService, postsService, blockUiService, errorService) {
        $scope.posts = [];
        $scope.size = "";
        $scope.isBusy = false;
        $scope.errorContent = { Show: false, Type: "" };
        
        $scope.getRecentPosts = function () {
            blockUiService.blockIt();

            if ($scope.isBusy) {
                return;
            }
            $scope.isBusy = true;

            postsService.getRecentPosts().then(function (resp) {
                $scope.posts = resp;
                $scope.isBusy = false;
                blockUiService.unblockIt();
                $scope.applyLayout();
            }, function (e) {
                errorService.displayErrorRedirect(e);
            });
        };

        $scope.getMorePosts = function () {
            if ($scope.isBusy) {
                return;
            }
            $scope.isBusy = true;
            
            postsService.getMorePosts($scope.posts.length).then(function (resp) {
                _.each(resp, function (p) {
                    $scope.posts.push(p);
                });
                $scope.isBusy = false;
                $scope.applyLayout();
            }, function (e) {
                errorService.displayErrorRedirect(e);
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
        $scope.getRecentPosts();
    }
]);