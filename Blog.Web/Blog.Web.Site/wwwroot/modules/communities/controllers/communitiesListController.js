ngCommunities.controller('communitiesListController', ["$scope", "$rootScope", "$location",
    "localStorageService", "communitiesService",  "errorService",
    function ($scope, $rootScope, $location, localStorageService, communitiesService, errorService) {
        $scope.communities = [];
        $scope.size = "";
        $scope.isBusy = false;

        $scope.init = function () {
            $scope.getList();
            $rootScope.$broadcast("updateScrollTriggerWatch", "communities-list");
        };

        $scope.getList = function () {
            if ($scope.isBusy) {
                return;
            }
            $scope.isBusy = true;

            communitiesService.getList().then(function (resp) {
                $scope.communities = resp;
                $scope.isBusy = false;
                $scope.$broadcast("resizeIsotopeItems");
            }, function (e) {
                errorService.displayError(e);
            });
        };

        $scope.getMoreList = function () {
            if ($scope.isBusy) {
                return;
            }
            $scope.isBusy = true;

            communitiesService.getMoreList($scope.posts.length).then(function (resp) {
                _.each(resp, function (p) {
                    $scope.posts.push(p);
                });
                $scope.isBusy = false;
                $scope.$broadcast("resizeIsotopeItems");
            }, function (e) {
                errorService.displayError(e);
            });
        };
        
        $scope.$on("updateCommunityItemSize", function (ev, size) {
            $scope.size = size;
        });

        $scope.$on("scrollBottom", function () {
            $scope.getMoreList();
        });

        $scope.init();
    }
]);