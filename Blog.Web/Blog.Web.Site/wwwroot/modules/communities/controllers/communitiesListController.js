ngCommunities.controller('communitiesListController', ["$scope", "$rootScope", "$location",
    "localStorageService", "communitiesService",  "errorService",
    function ($scope, $rootScope, $location, localStorageService, communitiesService, errorService) {
        $scope.communities = [];
        $scope.size = "";
        $scope.isBusy = false;

        $scope.authData = localStorageService.get("authorizationData");

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
            }, function (e) {
                errorService.displayError(e);
            });
        };

        $scope.getMoreList = function () {
            if ($scope.isBusy) {
                return;
            }
            $scope.isBusy = true;

            communitiesService.getMoreList($scope.communities.length).then(function (resp) {
                _.each(resp, function (p) {
                    $scope.communities.push(p);
                });
                $scope.isBusy = false;
            }, function (e) {
                errorService.displayError(e);
            });
        };

        $scope.isLoggedIn = function () {
            if ($scope.authData) {
                return true;
            }
            return false;
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