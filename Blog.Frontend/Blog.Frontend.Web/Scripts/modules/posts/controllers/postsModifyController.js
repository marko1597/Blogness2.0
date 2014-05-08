ngPosts.controller('postsModifyController', ["$scope", "$interval", "$fileUploader", "snapRemote", 
    "localStorageService", "postsService", "postsStateService", "blockUiService", "dateHelper", "configProvider",
    function ($scope, $interval, $fileUploader, snapRemote, localStorageService, postsService,
        postsStateService, blockUiService, dateHelper, configProvider) {
        $scope.post = null;
        $scope.username = localStorageService.get("username");
        $scope.uploadUrl = configProvider.getSettings().BlogApi == "" ?
            window.blogConfiguration.blogApi + "upload?username=" + $scope.username :
            configProvider.getSettings().BlogApi + "upload?username=" + $scope.username;

        var uploader = $scope.uploader = $fileUploader.create({
            scope: $scope,
            url: $scope.uploadUrl
        });

        uploader.filters.push(function (item /*{File|HTMLInputElement}*/) {
            var type = uploader.isHTML5 ? item.type : '/' + item.value.slice(item.value.lastIndexOf('.') + 1);
            type = '|' + type.toLowerCase().slice(type.lastIndexOf('/') + 1) + '|';
            return '|jpg|png|jpeg|bmp|gif|'.indexOf(type) !== -1;
        });
    }
]);