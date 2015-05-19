ngCommunities.controller('communityViewController', ["$scope", "$rootScope", "$location",
    "localStorageService", "communitiesService", "postsService", "errorService",
    function ($scope, $rootScope, $location, localStorageService, communitiesService, postsService, errorService) {
        $scope.community = {};

        $scope.posts = [];

        $scope.members = [];

        $scope.postSize = null;

        $scope.communityId = parseInt($rootScope.$stateParams.communityId);

        $scope.isBusy = false;

        $scope.getData = function () {
            if (!isNaN($rootScope.$stateParams.communityId)) {
                communitiesService.getById($scope.communityId).then(function (community) {
                    if (!community.Error) {
                        $scope.community = community;
                        $scope.isBusy = false;

                        $scope.$broadcast("viewedCommunityLoaded", community);
                    } else {
                        errorService.displayError({ Message: e });
                    }
                }, function (e) {
                    errorService.displayError({ Message: e });
                });

                postsService.getByCommunity($scope.communityId).then(function (posts) {
                    $scope.posts = posts;
                }, function (e) {
                    errorService.displayError({ Message: e });
                });
            } else {
                errorService.displayErrorRedirect({ Message: "No community found!" });
            }
        };
        
        $scope.$on("updatePostsSize", function (ev, size) {
            $scope.postSize = size;
        });

        $scope.init = function () {
            if ($scope.isBusy) {
                return;
            }
            $scope.isBusy = true;

            $scope.getData();
        };

        $scope.init();
    }
]);