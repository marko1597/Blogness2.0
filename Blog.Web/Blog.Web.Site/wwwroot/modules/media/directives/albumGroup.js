﻿// ReSharper disable InconsistentNaming

ngMedia.directive('albumGroup', ["$templateCache", function ($templateCache) {
    var ctrlFn = function ($scope, $rootScope, $window, albumService, mediaService, errorService, dateHelper, configProvider,
        $modal, FileUploader, localStorageService) {

        $scope.uploadUrl = configProvider.getSettings().BlogApi == "" ?
           $window.blogConfiguration.blogApi + "media?username=" + $scope.user.UserName + "&album=" + encodeURIComponent($scope.album.AlbumName) :
           configProvider.getSettings().BlogApi + "media?username=" + $scope.user.UserName + "&album=" + encodeURIComponent($scope.album.AlbumName);

        $scope.isExpanded = !$scope.album.IsNew ? true : false;

        $scope.authData = localStorageService.get("authorizationData");

        $scope.media = $scope.album ? $scope.album.Media : null;

        $scope.albumName = $scope.album ? $scope.album.AlbumName : '';

        $scope.toggleExpandClass = function () {
            if ($scope.isExpanded) {
                return "fa-minus";
            }
            return "fa-plus";
        };

        $scope.toggleExpanded = function () {
            if (!$scope.album.IsNew) $scope.isExpanded = !$scope.isExpanded;
        };

        $scope.editAlbum = function () {
            $scope.album.IsEditing = true;
        };

        $scope.isOwnedByLoggedUser = function () {
            if ($scope.album && $scope.album.User && $rootScope.user) {
                if ($scope.album.User.UserName === $rootScope.user.UserName) {
                    return true;
                }
                return false;
            }
            return false;
        };

        $scope.isEditControlsVisible = function () {
            if ($scope.album && $scope.album.User && $rootScope.user) {
                if ($scope.album.User.UserName !== $rootScope.user.UserName) {
                    return false;
                }

                return !$scope.album.IsEditing;
            }
            return false;
        };

        $scope.isSaveUpdatesControlsVisible = function () {
            if ($scope.album && $scope.album.User && $rootScope.user) {
                if ($scope.album.User.UserName !== $rootScope.user.UserName) {
                    return false;
                }

                return $scope.album.IsEditing;
            }
            return false;
        };

        $scope.cancelEditAlbum = function () {
            if ($scope.album.IsNew) {
                $scope.$emit('cancelledAddingOfAlbum', $scope.album);
                $scope.album.IsEditing = false;
            }
            $scope.album.IsEditing = false;
        };

        $scope.saveAlbum = function () {
            var album = {
                IsUserDefault: false,
                AlbumName: $scope.album.AlbumName,
                AlbumId: !$scope.album.AlbumId ? 0 : $scope.album.AlbumId
            };

            if ($rootScope.authData) {
                album.User = $rootScope.user;
            }

            if ($scope.album.IsNew) {
                addAlbum(album);
            } else {
                updateAlbum(album);
            }
        };

        $scope.deleteAlbum = function () {
            mediaDeleteDialog.show();
        };

        $scope.confirmDelete = function () {
            albumService.deleteAlbum($scope.album.AlbumId).then(function (response) {
                mediaDeleteDialog.hide();

                if (response.Error != null) {
                    errorService.displayError(response.Error);
                    return;
                }

                $scope.$emit("successDeletingAlbum", $scope.album);
            }, function (err) {
                $scope.setModelStateErrors(err.ModelState);
                mediaDeleteDialog.hide();
            });
        };

        $scope.$on("successDeletingMedia", function(ev, data) {
            var index = $scope.album.Media.indexOf(data);
            $scope.album.Media.splice(index, 1);
        });

        $scope.$watch('album', function(oldValue, newValue) {
            if (newValue) {
                $scope.albumName = newValue.AlbumName;
                $scope.$broadcast("albumNameDoneLoading", { albumName: $scope.albumName });
            }
        });

        $scope.$watch('user', function (oldValue, newValue) {
            if (newValue) {
                $scope.user = newValue;
                $scope.$broadcast("albumUserDoneLoading", { user: $scope.user });
            }
        });

        $scope.init = function() {
            mediaService.addViewedMediaListFromAlbum($scope.album.Media, $scope.user.UserName, $scope.album.AlbumName);
        };

        $scope.init();

        var mediaDeleteDialog = $modal({
            title: 'Delete?',
            content: "Are you sure you want to delete this album? Doing so will also delete all the media in it.",
            scope: $scope,
            template: "media/mediaDeleteDialog.html",
            show: false
        });

        var addAlbum = function (album) {
            albumService.addAlbum(album).then(function (response) {
                if (response.Error != null) {
                    errorService.displayError(response.Error);
                    return;
                }
                response.IsEdit = false;
                response.IsNew = false;
                $scope.album = response;
            }, function (err) {
                errorService.displayError(err);
            });
        };

        var updateAlbum = function (album) {
            albumService.updateAlbum(album).then(function (response) {
                if (response.Error != null) {
                    errorService.displayError(response.Error);
                    return;
                }
                response.IsEdit = false;
                response.IsNew = false;
                $scope.album = response;
            }, function (err) {
                errorService.displayError(err);
            });
        };

        // #region angular-file-upload
        
        var uploader = $scope.uploader = new FileUploader({
            scope: $rootScope,
            url: $scope.uploadUrl,
            autoUpload: true,
            headers: { Authorization: 'Bearer ' + ($scope.authData ? $scope.authData.token : "") }
        });

        uploader.filters.push({
            name: 'imageFilter',
            fn: function (item) {
                var type = uploader.isHTML5 ? item.type : '/' + item.value.slice(item.value.lastIndexOf('.') + 1);
                type = '|' + type.toLowerCase().slice(type.lastIndexOf('/') + 1) + '|';
                return '|jpg|png|jpeg|bmp|gif|mp4|flv|webm|'.indexOf(type) !== -1;
            }
        });

        uploader.onSuccessItem = function (fileItem, response) {
            response.CreatedDateDisplay = dateHelper.getDateDisplay(response.CreatedDate);
            $scope.album.Media.push(response);
            $rootScope.$broadcast("resizeIsotopeItems", {});
        };

        // #endregion
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$window", "albumService", "mediaService", "errorService", 
        "dateHelper", "configProvider", "$modal", "FileUploader", "localStorageService"];

    return {
        restrict: 'EA',
        scope: {
            album: '=',
            user: '='
        },
        replace: true,
        template: $templateCache.get("media/albumGroup.html"),
        controller: ctrlFn
    };
}]);

// ReSharper restore InconsistentNaming