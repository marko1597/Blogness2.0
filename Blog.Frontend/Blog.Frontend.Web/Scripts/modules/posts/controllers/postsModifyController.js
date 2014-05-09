ngPosts.controller('postsModifyController', ["$scope", "$interval", "$fileUploader", "snapRemote", 
    "localStorageService", "postsService", "postsStateService", "blockUiService", "dateHelper", "configProvider",
    function ($scope, $interval, $fileUploader, snapRemote, localStorageService, postsService,
        postsStateService, blockUiService, dateHelper, configProvider) {
        $scope.post = null;
        $scope.username = localStorageService.get("username");
        $scope.uploadUrl = configProvider.getSettings().BlogApi == "" ?
            window.blogConfiguration.blogApi + "media?username=" + $scope.username + "&album=default" :
            configProvider.getSettings().BlogApi + "media?username=" + $scope.username + "&album=default";

        var uploader = $scope.uploader = $fileUploader.create({
            scope: $scope,
            url: $scope.uploadUrl
        });

        uploader.filters.push(function (item /*{File|HTMLInputElement}*/) {
            var type = uploader.isHTML5 ? item.type : '/' + item.value.slice(item.value.lastIndexOf('.') + 1);
            type = '|' + type.toLowerCase().slice(type.lastIndexOf('/') + 1) + '|';
            return '|jpg|png|jpeg|bmp|gif|'.indexOf(type) !== -1;
        });

        uploader.bind('success', function (event, xhr, item, response) {
            item.mediaId = response.MediaId;
            console.info('Success', xhr, item, response);
        });
    }
]);