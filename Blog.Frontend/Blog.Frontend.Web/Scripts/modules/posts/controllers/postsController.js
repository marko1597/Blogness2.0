ngPosts.controller('postsController', ["$scope", "$interval", "snapRemote", "localStorageService", "postsService", "blockUiService", "dateHelper",
    function ($scope, $interval, snapRemote, localStorageService, postsService, blockUiService, dateHelper) {
        $scope.posts = [];
        $scope.size = "";
        $scope.errorContent = { Show: false, Type: "" };
        
        $scope.getPopularPosts = function () {
            blockUiService.blockIt(
                '<h4>Loading...</h4>', {
                    border: 'none',
                    padding: '5px',
                    backgroundColor: '#000',
                    opacity: .5,
                    color: '#fff'
                });
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

        snapRemote.getSnapper().then(function (snapper) {
            snapper.on('open', function () {
                //blockUiService.blockIt(
                //'<h4>Loading...</h4>', {
                //    border: 'none',
                //    padding: '5px',
                //    backgroundColor: '#000',
                //    opacity: .5,
                //    color: '#fff'
                //}, $(".snap-content"));
                console.log("open");
            });

            snapper.on('close', function () {
                //blockUiService.unblockIt($(".snap-content"));
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