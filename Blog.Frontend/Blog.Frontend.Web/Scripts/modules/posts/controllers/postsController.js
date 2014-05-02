ngPosts.controller('postsController', ["$scope", "$interval", "snapRemote", "postsService",
    function ($scope, $interval, snapRemote, postsService) {
        $scope.posts = [];
        $scope.size = "";
        $scope.errorContent = { Show: false, Type: "" };

        $scope.getPopularPosts = function () {
            postsService.getPopularPosts().then(function (resp) {
                $scope.posts = resp;
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

        snapRemote.getSnapper().then(function (snapper) {
            snapper.on('open', function () {
                console.log("open");
            });

            snapper.on('close', function () {
                console.log('closed!');
            });
        });

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

        $scope.getPopularPosts();
        $scope.applyLayout();
    }
]);