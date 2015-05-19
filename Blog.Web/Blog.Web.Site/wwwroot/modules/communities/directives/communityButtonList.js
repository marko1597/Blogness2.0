ngCommunities.directive("communityButtonList", ["$templateCache",
    function ($templateCache) {
        var ctrlFn = function ($scope, localStorageService) {
            $scope.username = localStorageService.get("username");

            $scope.hasCommunities = function () {
                return $scope.communities && $scope.communities.length > 0;
            };

            $scope.communityCreatedByLoggedUser = function (community) {
                if (community && community.Leader) {
                    return $scope.username === community.Leader.UserName;
                }
                return false;
            };

            $scope.removeFromList = function (community) {
                if (community && $scope.allowDelete) {
                    var index = $scope.communities.indexOf(community);
                    $scope.communities.splice(index, 1  );
                }
            };
        };
        ctrlFn.$inject = ["$scope", "localStorageService"];

        var linkFn = function (scope, elem, attrs) {
            scope.allowDelete = attrs.allowDelete === "true" ? true : false;
        };

        return {
            restrict: 'EA',
            scope: { communities: '=' },
            replace: true,
            template: $templateCache.get("communities/communityButtonList.html"),
            controller: ctrlFn,
            link: linkFn
        };
    }
]);