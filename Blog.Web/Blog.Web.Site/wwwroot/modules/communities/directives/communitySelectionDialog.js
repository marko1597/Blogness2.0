ngCommunities.directive('communitySelectionDialog', ["$templateCache", function ($templateCache) {
    var ctrlFn = function ($scope, $rootScope, $modal, localStorageService,
        communitiesService, errorService) {

        $scope.isInitialized = false;

        $scope.username = localStorageService.get("username");

        $scope.authData = localStorageService.get("authorizationData");

        $scope.user = $rootScope.user;

        $scope.communities = [];

        $scope.listDialog = $modal({
            scope: $scope,
            template: "communities/communitySelectionDialog.html",
            show: false,
            keyboard: false,
            backdrop: 'static'
        });

        $scope.toggleCommunitySelected = function(community) {
            community.isSelected = !community.isSelected;
        };

        $scope.communitySelectedIcon = function(community) {
            return community.isSelected ? "fa-check-square-o" : "fa-square-o";
        };

        $scope.isCreatedByLoggedUser = function(community) {
            if (community && community.Leader) {
                return $scope.username === community.Leader.UserName ? "fa-user" : "";
            }
            return "";
        };

        $scope.loadMoreCommunities = function() {
            if ($scope.isBusy) {
                return;
            }
            $scope.isBusy = true;

            if ($scope.user) {
                communitiesService.getMoreJoinedByUser($scope.user.Id, $scope.communities.length).then(function (resp) {
                    _.each(resp, function (a) {
                        a.isSelected = false;
                        $scope.communities.push(a);
                    });
                    $scope.isBusy = false;
                }, function (e) {
                    errorService.displayError(e);
                    $scope.isBusy = false;
                });
            } else {
                $rootScope.$broadcast("launchLoginForm", { canClose: true });
            }
        };

        $rootScope.$on("launchCommunitySelectionDialog", function (ev, data) {
            try {
                if ($scope.listDialog.$options.show) return;

                if (data.canClose) {
                    $scope.listDialog.$options.keyboard = true;
                    $scope.listDialog.$options.backdrop = true;
                } else {
                    $scope.listDialog.$options.keyboard = false;
                    $scope.listDialog.$options.backdrop = 'static';
                }
                $scope.listDialog.$promise.then($scope.listDialog.show);
                getCommunities();
            } catch (ex) {
                $scope.listDialog.$options.keyboard = false;
                $scope.listDialog.$options.backdrop = 'static';
                $scope.listDialog.show();
                getCommunities();
            }
        });

        $rootScope.$on("hideCommunitySelectionDialog", function () {
            $scope.listDialog.hide();
        });

        $rootScope.$on("loggedInUserInfo", function (ev, data) {
            if (data) {
                $scope.username = data.UserName;
                $scope.user = data;
            }
        });

        $scope.closeCommunitySelectionDialog = function() {
            var selectedCommunities = _.where($scope.communities, { isSelected: true });
            $scope.$emit("doneSelectingCommunities", { items: selectedCommunities });
            $scope.listDialog.hide();
        };

        $scope.getEmblemUrl = function(community) {
            if (community && community.Emblem) {
                return community.Emblem.MediaUrl;
            }
            return "";
        };

        var getCommunities = function () {
            if ($scope.user) {
                communitiesService.getJoinedByUser($scope.user.Id).then(function (resp) {
                    _.each(resp, function (a) {
                        a.isSelected = false;
                    });

                    $scope.communities = resp;
                    $scope.isInitialized = true;
                }, function (e) {
                    errorService.displayError(e);
                    $scope.isInitialized = true;
                });
            } else {
                $rootScope.$broadcast("launchLoginForm", { canClose: true });
            }
        };
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$modal", "localStorageService", "communitiesService", "errorService"];

    return {
        restrict: 'EA',
        replace: true,
        controller: ctrlFn
    };
}]);
