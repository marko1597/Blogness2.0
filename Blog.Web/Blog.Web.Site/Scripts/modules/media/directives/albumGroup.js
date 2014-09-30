// ReSharper disable InconsistentNaming

ngMedia.directive('albumGroup', function () {
    var ctrlFn = function ($scope, $rootScope, $window, albumService, errorService, dateHelper, configProvider, $modal, FileUploader) {
        $scope.uploadUrl = configProvider.getSettings().BlogApi == "" ?
           $window.blogConfiguration.blogApi + "media?username=" + $scope.user.UserName + "&album=" + encodeURIComponent($scope.album.AlbumName) :
           configProvider.getSettings().BlogApi + "media?username=" + $scope.user.UserName + "&album=" + encodeURIComponent($scope.album.AlbumName);

        $scope.isExpanded = true;

        $scope.toggleExpandClass = function () {
            if ($scope.isExpanded) {
                return "fa-minus";
            }
            return "fa-plus";
        };

        $scope.toggleExpanded = function () {
            $scope.isExpanded = !$scope.isExpanded;
        };

        $scope.editAlbum = function () {
            $scope.album.IsEditing = true;
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
            mediaDeleteDialog.$promise.then(mediaDeleteDialog.show);
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

        var mediaDeleteDialog = $modal({
            title: 'Delete?',
            content: "Are you sure you want to delete this album? Doing so will also delete all the media in it.",
            scope: $scope,
            template: $window.blogConfiguration.templatesModulesUrl + "media/mediaDeleteDialog.html",
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
            headers: { Authorization: 'Bearer ' + ($rootScope.authData ? $rootScope.authData.token : "") }
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
        };

        // #endregion
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$window", "albumService", "errorService", "dateHelper", "configProvider", "$modal", "FileUploader"];

    return {
        restrict: 'EA',
        scope: {
            album: '=',
            user: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "media/albumGroup.html",
        controller: ctrlFn
    };
});

// ReSharper restore InconsistentNaming