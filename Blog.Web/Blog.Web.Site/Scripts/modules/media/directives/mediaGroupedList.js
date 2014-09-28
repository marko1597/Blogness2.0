ngMedia.directive('mediaGroupedList', function () {
    var ctrlFn = function ($scope, $rootScope, albumService, localStorageService) {
        $scope.isAdding = false;

        $scope.addAlbum = function () {
            var newAlbum = {
                AlbumName: '',
                IsNew: true,
                IsEditing: true
            };
            $scope.albums.push(newAlbum);
            $scope.isAdding = true;
        };

        $scope.$on('cancelledAddingOfAlbum', function (ev, data) {
            var index = $scope.albums.indexOf(data);
            $scope.albums.splice(index, 1);
            $scope.isAdding = false;
        });
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "albumService", "localStorageService"];

    return {
        restrict: 'EA',
        scope: {
            albums: '=',
            user: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "media/mediaGroupedList.html",
        controller: ctrlFn
    };
});
