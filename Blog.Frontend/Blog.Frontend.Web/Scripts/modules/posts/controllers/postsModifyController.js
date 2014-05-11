ngPosts.controller('postsModifyController', ["$scope", "$fileUploader", "localStorageService",
    "postsService", "userService", "postsStateService", "blockUiService", "dateHelper", "configProvider",
    function ($scope, $fileUploader, localStorageService, postsService, userService,
        postsStateService, blockUiService, dateHelper, configProvider) {

        $scope.post = { PostTitle : "", PostMessage : "", PostContents: [], Tags: []};
        $scope.username = localStorageService.get("username");

        $scope.uploadUrl = configProvider.getSettings().BlogApi == "" ?
            window.blogConfiguration.blogApi + "media?username=" + $scope.username + "&album=default" :
            configProvider.getSettings().BlogApi + "media?username=" + $scope.username + "&album=default";

        $scope.onTagAdded = function (t) {
            var tag = { TagName: t.text };
            $scope.post.Tags.push(tag);
            console.log($scope.post);
        };

        $scope.savePost = function () {
            userService.getUserInfo().then(function(userinfo) {
                $scope.post.User = userinfo;

                postsService.savePost($scope.post).then(function (resp) {
                    console.log(resp);
                }, function (e) {
                    alert(e);
                });
            }, function(e) {
                alert(e);
            });
        };

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
            var media = {
                PostId: null,
                Media: response,
            };
            $scope.post.PostContents.push(media);

            console.info('Success', xhr, item, response);
        });
    }
]);