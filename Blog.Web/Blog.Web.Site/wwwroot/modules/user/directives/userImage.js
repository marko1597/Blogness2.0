ngUser.directive('userImage', [function () {
    var ctrlFn = function ($scope, $rootScope, userService, configProvider, FileUploader, localStorageService) {
        $scope.authData = null;

        $scope.username = null;

        $scope.albumName = null;

        $scope.profileImageUrl = null;

        $scope.backgroundImageUrl = null;

        $scope.uploadUrl = null;

        $scope.showUpdateImages = function () {
            if ($scope.authData && $scope.user) {
                if ($scope.user.UserName === $scope.username) {
                    return true;
                }
            }
            return false;
        };

        $scope.updateProfileImage = function () {
            $scope.albumName = 'profile';
        };

        $scope.updateBackgroundImage = function () {
            $scope.albumName = 'background';
        };

        $scope.$watch('user', function () {
            if ($scope.user !== null && $scope.user !== undefined) {
                $scope.profileImageUrl = $scope.user.Picture.MediaUrl;
                $scope.backgroundImageUrl = $scope.user.Background.MediaUrl;
            }
        });

        $scope.$watch('username', function () {
            if ($scope.username !== null && $scope.username !== undefined) {
                $scope.uploadUrl = configProvider.getSettings().BlogApi == "" ?
                    window.blogConfiguration.blogApi + "media?username=" + $scope.username :
                    configProvider.getSettings().BlogApi + "media?username=" + $scope.username;
            }
        });

        $scope.init = function () {
            $scope.authData = localStorageService.get("authorizationData");
            $scope.username = localStorageService.get("username");
        };

        $scope.init();

        // #region image uploader object

        var uploader = $scope.uploader = new FileUploader({
            scope: $rootScope,
            url: $scope.uploadUrl,
            headers: { Authorization: 'Bearer ' + ($scope.authData ? $scope.authData.token : "") },
            autoUpload: true
        });

        uploader.filters.push({
            name: 'imageFilter',
            fn: function (item /*{File|HTMLInputElement}*/) {
                var type = uploader.isHTML5 ? item.type : '/' + item.value.slice(item.value.lastIndexOf('.') + 1);
                type = '|' + type.toLowerCase().slice(type.lastIndexOf('/') + 1) + '|';
                return '|jpg|png|jpeg|bmp|'.indexOf(type) !== -1;
            }
        });

        uploader.onSuccessItem = function (fileItem, response) {
            if ($scope.albumName === 'profile') {
                $scope.profileImageUrl = response.MediaUrl;
            } else if ($scope.albumName === 'background') {
                $scope.backgroundImageUrl = response.MediaUrl;
            }
        };

        uploader.onAfterAddingFile = function (item) {
            item.url = $scope.uploadUrl + '&album=' + $scope.albumName;
        };

        // #endregion
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "userService", "configProvider", "FileUploader", "localStorageService"];

    return {
        restrict: 'EA',
        scope: {
            user: '=',
            fullname: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "user/userImage.html",
        controller: ctrlFn
    };
}]);
