ngMedia.directive('mediaGroupedList', ["$templateCache", function ($templateCache) {
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
        transclude: true,
        replace: true,
        template: $templateCache.get("media/mediaGroupedList.html"),
        controller: ctrlFn
    };
}]);
