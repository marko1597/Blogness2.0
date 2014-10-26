ngMedia.directive('mediaItem', function () {
    var ctrlFn = function ($scope, $rootScope, $location, localStorageService, $modal, mediaService, errorService) {
        var mediaDeleteDialog = $modal({
            title: 'Delete?',
            content: "Are you sure you want to delete this item?",
            scope: $scope,
            template: window.blogConfiguration.templatesModulesUrl + "media/mediaDeleteDialog.html",
            show: false
        });

        $scope.username = localStorageService.get("username");

        $scope.deleteButtonVisible = false;

        $scope.viewMode = function () {
            if ($scope.mode && $scope.mode === 'thumbnail') {
                return "thumbnail";
            }
            return "";
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
            if ($scope.allowDelete && $scope.allowDelete === 'true' && $rootScope.authData) {
                if ($scope.user && $scope.username === $scope.user.UserName) {
                    return true;
                }
            }
            return false;
        };

        $scope.deleteMedia = function () {
            mediaDeleteDialog.$promise.then(mediaDeleteDialog.show);
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
                if ($rootScope.$stateParams.username) {
                    $location.path("/user/" + $scope.user.UserName + "/media/gallery/" + $scope.albumName.toLowerCase());
                } else {
                    $location.path("/user/media/gallery/" + $scope.albumName.toLowerCase());
                }
            }
        };

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

        $scope.getContentType = function (content) {
            if (content == undefined) return "image";

            var contentType = content.split('/');
            if (contentType[0] == "video") {
                return "video";
            } else {
                return "image";
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
        templateUrl: window.blogConfiguration.templatesModulesUrl + "media/mediaItem.html",
        controller: ctrlFn,
        link: linkFn
    };
});
