ngPosts.controller('postsController', ["$scope", "$rootScope", "$location", "$timeout", "$interval", "localStorageService", "postsService", "errorService",
    function ($scope, $rootScope, $location, $timeout, $interval, localStorageService, postsService, errorService) {
        $scope.posts = [];

        $scope.isBusy = false;

        $scope.init = function() {
            $scope.getRecentPosts();
            $rootScope.$broadcast("updateScrollTriggerWatch", "posts-main");
        };
        
        $scope.getRecentPosts = function () {
            if ($scope.isBusy) {
                return;
            }
            $scope.isBusy = true;

            postsService.getRecentPosts().then(function (resp) {
                $scope.posts = resp;
                $scope.isBusy = false;
                $scope.$broadcast("resizeIsotopeItems");
            }, function (e) {
                errorService.displayError(e);
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
                errorService.displayError(e);
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