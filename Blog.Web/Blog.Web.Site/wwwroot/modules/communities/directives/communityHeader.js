ngCommunities.directive('communityHeader', ["$templateCache", function ($templateCache) {
    var ctrlFn = function ($scope, $rootScope, $location, localStorageService) {
        $scope.username = localStorageService.get("username");

        $scope.isEditable = function () {
            if ($scope.community.Leader && $scope.community.Leader.UserName === $scope.username) {
                return true;
            }
            return false;
        };

        $scope.$on("loggedInUserInfo", function (ev, data) {
            if (data) {
                $scope.username = data.UserName;
            }
        });

        $rootScope.$watch('user', function () {
            if ($rootScope.user) {
                $scope.username = $rootScope.user.UserName;
            }
        });

        $scope.edit = function () {
            $location.path("/community/edit/" + $scope.community.Id);
        };
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$location", "localStorageService"];

    return {
        restrict: 'EA',
        scope: {
            community: '='
        },
        replace: true,
        template: $templateCache.get("communities/communityHeader.html"),
        controller: ctrlFn
    };
}]);
