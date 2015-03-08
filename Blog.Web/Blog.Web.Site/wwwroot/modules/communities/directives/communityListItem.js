ngCommunities.directive('communityListItem', ["$templateCache", function ($templateCache) {
    var ctrlFn = function ($scope, $rootScope, $location, $interval, localStorageService, configProvider) {
        $scope.username = localStorageService.get("username");

        $scope.isEditable = ($scope.community && $scope.community.Leader && $scope.community.Leader.UserName === $scope.username) ? true : false;

        $scope.toggleIsEditable = function () {
            if ($scope.community && $scope.community.Leader && $scope.community.Leader.UserName === $scope.username) {
                $scope.isEditable = true;
            }
            $scope.isEditable = false;
        };
        
        $scope.$on("loggedInUserInfo", function (ev, data) {
            if (data) {
                $scope.username = data.UserName;
                $scope.toggleIsEditable();
            }
        });

        $rootScope.$watch('user', function () {
            if ($rootScope.user) {
                $scope.username = $rootScope.user.UserName;
                $scope.toggleIsEditable();
            }
        });
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$location", "$interval", "localStorageService", "configProvider"];

    return {
        restrict: 'EA',
        scope: {
            community: '='
        },
        replace: true,
        template: $templateCache.get("communities/communityListItem.html"),
        controller: ctrlFn
    };
}]);
