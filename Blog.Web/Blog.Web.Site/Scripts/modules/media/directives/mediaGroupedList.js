ngMedia.directive('mediaGroupedList', function () {
    var ctrlFn = function ($scope) {
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

        $scope.$on('successDeletingAlbum', function (ev, data) {
            var index = $scope.albums.indexOf(data);
            $scope.albums.splice(index, 1);
        });
    };
    ctrlFn.$inject = ["$scope"];

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
