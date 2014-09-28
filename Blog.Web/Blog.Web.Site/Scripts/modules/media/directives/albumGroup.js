ngMedia.directive('albumGroup', function () {
    var ctrlFn = function ($scope, $rootScope, albumService, localStorageService) {
        $scope.isExpanded = true;

        $scope.newAlbumName = '';
        
        $scope.toggleExpandClass = function() {
            if ($scope.isExpanded) {
                return "fa-minus";
            }
            return "fa-plus";
        };

        $scope.toggleExpanded = function() {
            $scope.isExpanded = !$scope.isExpanded;
        };

        $scope.editAlbum = function() {
            $scope.album.IsEditing = true;
        };

        $scope.cancelEditAlbum = function () {
            if ($scope.album.IsNew) {
                $scope.$emit('cancelledAddingOfAlbum', $scope.album);
                $scope.album.IsEditing = false;
            }
            $scope.album.IsEditing = false;
        };
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "albumService", "localStorageService"];

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
