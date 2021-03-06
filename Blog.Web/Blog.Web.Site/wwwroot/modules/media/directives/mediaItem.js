﻿ngMedia.directive('mediaItem', ["$templateCache", function ($templateCache) {
    var ctrlFn = function ($scope, $rootScope, $location, localStorageService, $modal, mediaService, errorService) {
        var mediaDeleteDialog = $modal({
            title: 'Delete?',
            content: "Are you sure you want to delete this item?",
            scope: $scope,
            template: "media/mediaDeleteDialog.html",
            show: false
        });

        $scope.username = localStorageService.get("username");

        $scope.deleteButtonVisible = false;

        $scope.isThumbnail = function() {
            if ($scope.mode && $scope.mode === 'thumbnail') {
                return true;
            }
            return false;
        };

        $scope.getThumbnailUrl = function () {
            if ($scope.crop && $scope.crop === 'true') {
                if ($scope.media) {
                    return {
                        "background-image": "url(" + $scope.media.ThumbnailUrl + ")"
                    };
                }
                return {
                    "background-image": "url(/content/images/warning.png)"
                };
            }
            return {};
        };

        $scope.toggleDelete = function () {
            if ($scope.allowDelete && $rootScope.authData) {
                if ($scope.user && $scope.username === $scope.user.UserName) {
                    return true;
                }
            }
            return false;
        };

        $scope.deleteMedia = function () {
            mediaDeleteDialog.show();
        };

        $scope.toggleGallery = function () {
            if ($scope.galleryMode && $scope.galleryMode === 'true') {
                return true;
            }
            return false;
        };

        $scope.viewAsGallery = function () {
            if ($rootScope.$stateParams.postId) {
                $location.path("/post/" + $rootScope.$stateParams.postId + '/gallery');
            } else {
                if ($scope.albumName) {
                    if ($rootScope.$stateParams.username) {
                        $location.path("/user/" + $scope.user.UserName + "/media/gallery/" + $scope.albumName.toLowerCase());
                    } else {
                        $location.path("/user/media/gallery/" + $scope.albumName.toLowerCase());
                    }
                } else {
                    errorService.displayError("Oops! This album's name seems to be invalid.");
                }
            }
        };

        $scope.$on("albumNameDoneLoading", function (ev, data) {
            $scope.albumName = data.albumName;
        });

        $scope.$on("albumUserDoneLoading", function (ev, data) {
            $scope.user = data.user;
        });

        $scope.confirmDelete = function () {
            mediaService.deleteMedia($scope.media.Id).then(function (response) {
                mediaDeleteDialog.hide();

                if (response.Error != null) {
                    errorService.displayError(response.Error);
                    return;
                }

                if (response) {
                    $scope.$emit("successDeletingMedia", $scope.media);
                } else {
                    errorService.displayError(response);
                }
            }, function (err) {
                $scope.setModelStateErrors(err.ModelState);
                mediaDeleteDialog.hide();
            });
        };

        $scope.isVideo = function () {
            var supportedVideos = [
               "video/avi",
               "video/quicktime",
               "video/mpeg",
               "video/mp4",
               "video/x-flv"
            ];

            if (!$scope.media || !$scope.media.MediaType) {
                return false;
            } else {
                var isVideo = _.contains(supportedVideos, $scope.media.MediaType);
                return isVideo;
            }
        };
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$location", "localStorageService", "$modal", "mediaService", "errorService"];

    var linkFn = function (scope, elem, attrs) {
        scope.mode = attrs.mode;

        scope.crop = attrs.crop;

        scope.galleryMode = attrs.galleryMode;

        scope.allowDelete = attrs.allowDelete;

        scope.isCropped = function () {
            if (attrs.crop && attrs.crop === 'true') {
                return "center-cropped";
            }
            return "";
        };
    };

    return {
        restrict: 'EA',
        scope: {
            media: '=',
            user: '=',
            albumName: '='
        },
        replace: true,
        template: $templateCache.get("media/mediaItem.html"),
        controller: ctrlFn,
        link: linkFn
    };
}]);
