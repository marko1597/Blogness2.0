ngMedia.directive('albumGroup', function () {
    var ctrlFn = function ($scope, $rootScope, albumService, errorService, $modal) {
        var mediaDeleteDialog = $modal({
            title: 'Delete?',
            content: "Are you sure you want to delete this album? Doing so will also delete all the media in it.",
            scope: $scope,
            template: window.blogConfiguration.templatesModulesUrl + "media/mediaDeleteDialog.html",
            show: false
        });

        $scope.isExpanded = true;

        $scope.newAlbumName = '';

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
                AlbumName: $scope.newAlbumName
            };

            if ($rootScope.authData) {
                album.User = $rootScope.user;
            }

            if ($scope.album.IsNew) {
                addAlbum();
            } else {
                updateAlbum();
            }
        };

        $scope.deleteAlbum = function () {
            mediaDeleteDialog.$promise.then(mediaDeleteDialog.show);
        };

        $scope.confirmDelete = function () {
            albumService.deleteAlbum($scope.album.Id).then(function (response) {
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

        var addAlbum = function () {
            albumService.addAlbum($scope.album).then(function (response) {
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

        var updateAlbum = function () {
            albumService.updateAlbum($scope.album).then(function (response) {
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
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "albumService", "errorService", "$modal"];

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
