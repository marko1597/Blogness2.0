ngMedia.controller('mediaGalleryController', ["$scope", "$rootScope",
    function ($scope, $rootScope) {
        $scope.init = function () {
            $rootScope.$broadcast("launchMediaGallery", {});
        };

        $scope.init();
    }
]);