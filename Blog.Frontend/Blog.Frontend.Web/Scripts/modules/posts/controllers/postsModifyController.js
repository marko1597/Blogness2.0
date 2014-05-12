ngPosts.controller('postsModifyController', ["$scope", "$location", "$fileUploader", "localStorageService",
    "postsService", "userService", "postsStateService", "tagsService", "blockUiService",
    "dateHelper", "configProvider",
    function ($scope, $location, $fileUploader, localStorageService, postsService, userService,
        postsStateService, tagsService, blockUiService, dateHelper, configProvider) {

        $scope.dimensionMode = configProvider.windowDimensions.mode == "" ?
            window.getDimensionMode() : configProvider.windowDimensions.mode;

        $scope.post = { PostTitle: "", PostMessage: "", PostContents: [], Tags: [] };

        $scope.username = localStorageService.get("username");

        $scope.uploadUrl = configProvider.getSettings().BlogApi == "" ?
            window.blogConfiguration.blogApi + "media?username=" + $scope.username + "&album=default" :
            configProvider.getSettings().BlogApi + "media?username=" + $scope.username + "&album=default";

        $scope.onTagAdded = function (t) {
            var tag = { TagName: t.text };
            $scope.post.Tags.push(tag);
        };

        $scope.onTagRemoved = function (t) {
            var tag = { TagName: t.text };
            var index = $scope.post.Tags.indexOf(tag);
            $scope.post.Tags.splice(index);
        };

        $scope.getTagsSource = function(t) {
            return tagsService.getTagsByName(t);
        };

        $scope.savePost = function () {
            blockUiService.blockIt();
            userService.getUserInfo().then(function(userinfo) {
                $scope.post.User = userinfo;

                postsService.savePost($scope.post).then(function (resp) {
                    blockUiService.unblockIt();
                    
                    if (resp != null) {
                        $location.path("/");
                    }

                    console.log(resp);
                }, function (e) {
                    alert(e);
                });
            }, function(e) {
                alert(e);
            });
        };

        $scope.$on("windowSizeChanged", function (e, d) {
            configProvider.setDimensions(d.width, d.height);
            $scope.dimensionMode = configProvider.windowDimensions.mode;
            console.log($scope.dimensionMode);
        });

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