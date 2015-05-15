ngCommunities.directive('communityHeader', ["$templateCache", function ($templateCache) {
    var ctrlFn = function ($scope, $rootScope, $location, localStorageService, communitiesService, errorService) {
        $scope.username = localStorageService.get("username");

        $scope.isUserJoined = false;

        $scope.isEditable = function () {
            if ($scope.community && $scope.community.Leader && $scope.community.Leader.UserName === $scope.username) {
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

        $scope.join = function () {
            if (!$rootScope.user || !$scope.community) {
                errorService.displayError({ Message: "Cannot join at the moment." });
            }

            communitiesService.joinCommunity($rootScope.user, $scope.community.Id).then(function (resp) {
                $scope.getCommunity();
            }, function (e) {
                errorService.displayError(e);
            });
        };

        $scope.leave = function () {
            if (!$rootScope.user || !$scope.community) {
                errorService.displayError({ Message: "Cannot leave at the moment." });
            }

            communitiesService.leaveCommunity($rootScope.user, $scope.community.Id).then(function (resp) {
                $scope.getCommunity();
            }, function (e) {
                errorService.displayError(e);
            });
        };

        $scope.getCommunity = function () {
            communitiesService.getById($scope.community.Id).then(function (resp) {
                $scope.community = resp;
                $scope.init();
            }, function (e) {
                errorService.displayError(e);
            });
        };
        
        $scope.init = function () {
            if (!$rootScope.user) $scope.isUserJoined = false;

            if ($scope.community && $scope.community.Members) {
                for (var i = 0; i < $scope.community.Members.length; i++) {
                    if ($rootScope.user && $scope.community.Members[i].UserName === $rootScope.user.UserName) {
                        $scope.isUserJoined = true;
                        return;
                    }
                }
            }

            $scope.isUserJoined = false;
        };

        $scope.init();

        $rootScope.$watch('user', function () {
            if ($rootScope.user) {
                $scope.init();
            }
        });

        $scope.$on("userLoggedIn", function () {
            $scope.init();
        });
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$location", "localStorageService", "communitiesService", "errorService"];

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
