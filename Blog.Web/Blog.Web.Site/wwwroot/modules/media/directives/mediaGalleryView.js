ngMedia.directive('mediaGalleryView', function () {
    var ctrlFn = function ($scope, $rootScope, $window, $location, $modal, mediaService, localStorageService) {
        $scope.mediaList = [];

        $scope.username = localStorageService.get("username");

        var mediaSelectionDialog = $modal({
            title: 'Gallery view',
            scope: $scope,
            template: window.blogConfiguration.templatesModulesUrl + "media/mediaGallery.html",
            show: false
        });

        $scope.closeGallery = function () {
            mediaSelectionDialog.hide();
            $scope.mediaList = [];

            if ($rootScope.$stateParams.postId) {
                $location.path("/post/" + $rootScope.$stateParams.postId);
            } else {
                if ($rootScope.$stateParams.username) {
                    $location.path("/user/" + $scope.user.UserName + '/media');
                } else {
                    $location.path("/user/media");
                }
            }
        };

        $rootScope.$on("launchMediaGallery", function () {
            if ($rootScope.$stateParams.postId) {
                mediaService.getViewMediaListFromPost($rootScope.$stateParams.postId)
                    .then(function (response) {
                        $scope.mediaList = response;
                    }, function (error) {
                        console.log(error);
                    });
            } else {
                if ($rootScope.$stateParams.username) {
                    mediaService.getViewMediaListFromAlbum($rootScope.$stateParams.username, $rootScope.$stateParams.albumName)
                        .then(function (response) {
                            $scope.mediaList = response;
                        }, function (error) {
                            console.log(error);
                        });
                } else {
                    mediaService.getViewMediaListFromAlbum($scope.username, $rootScope.$stateParams.albumName)
                        .then(function (response) {
                            $scope.mediaList = response;
                        }, function (error) {
                            console.log(error);
                        });
                }
            }

            mediaSelectionDialog.$promise.then(mediaSelectionDialog.show);
        });

        $scope.getWindowHeight = function () {
            return { height: $window.innerHeight - 30 + 'px' };
        };

        $scope.showMediaGallery = function () {
            if ($scope.mediaList && $scope.mediaList.length > 0) {
                return true;
            }
            return false;
        };
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$window", "$location", "$modal", "mediaService", "localStorageService"];

    return {
        restrict: 'EA',
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "media/mediaGallery.html",
        controller: ctrlFn
    };
});
