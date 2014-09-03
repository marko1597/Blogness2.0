ngPosts.controller('postsController', ["$scope", "$rootScope", "$location", "$timeout", "$interval", "localStorageService", "postsService", "blockUiService", "errorService",
    function ($scope, $rootScope, $location, $timeout, $interval, localStorageService, postsService, blockUiService, errorService) {
        $scope.posts = [];
        $scope.size = "";
        $scope.isBusy = false;

        $scope.init = function() {
            $scope.getRecentPosts();
            $rootScope.$broadcast("updateScrollTriggerWatch", "posts-main");
        };
        
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
                $scope.$broadcast("resizeIsotopeItems");
            }, function (e) {
                errorService.displayErrorRedirect(e);
            });
        };

        $scope.getMoreRecentPosts = function () {
            if ($scope.isBusy) {
                return;
            }
            $scope.isBusy = true;
            
            postsService.getMoreRecentPosts($scope.posts.length).then(function (resp) {
                _.each(resp, function (p) {
                    $scope.posts.push(p);
                });
                $scope.isBusy = false;
                $scope.$broadcast("resizeIsotopeItems");
            }, function (e) {
                errorService.displayErrorRedirect(e);
            });
        };

        $scope.$on("updatePostsSize", function (ev, size) {
            $scope.size = size;
        });

        $scope.$on("scrollBottom", function () {
            $scope.getMoreRecentPosts();
        });

        $scope.init();
    }
]);