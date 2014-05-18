ngPosts.controller('postsModifyController', ["$scope", "$rootScope", "$location", "$routeParams", "$fileUploader", "localStorageService", "postsService", "userService", "tagsService", "blockUiService", "dateHelper", "configProvider",
    function ($scope, $rootScope, $location, $routeParams, $fileUploader, localStorageService, postsService, userService, tagsService, blockUiService, dateHelper, configProvider) {
        $scope.isAdding = true;

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

                if ($scope.isAdding) {
                    postsService.addPost($scope.post).then(function (resp) {
                        if (resp.Error == undefined) {
                            blockUiService.unblockIt();
                            $location.path("/");
                        } else {
                            blockUiService.unblockIt();
                            $rootScope.$broadcast("displayError", { Message: resp.Error.Message });
                        }
                    }, function(e) {
                        console.log(e);
                        $location.path("/404");
                    });
                } else {
                    postsService.updatePost($scope.post).then(function (resp) {
                        if (resp.Error == undefined) {
                            blockUiService.unblockIt();
                            $location.path("/");
                        } else {
                            blockUiService.unblockIt();
                            $rootScope.$broadcast("displayError", { Message: resp.Error.Message });
                        }
                    }, function (e) {
                        console.log(e);
                        $location.path("/404");
                    });
                }
            }, function(e) {
                console.log(e);
                $location.path("/404");
            });
        };

        $scope.init = function() {
            if (!isNaN($routeParams.postId)) {
                blockUiService.blockIt();
                postsService.getPost($routeParams.postId).then(function (resp) {
                    if ($scope.username === resp.User.UserName) {
                        if (resp.Error == undefined) {
                            $scope.isAdding = false;
                            $scope.post = resp;
                            _.each(resp.Tags, function (t) {
                                $scope.Tags.push({ text: t.TagName });
                            });
                        }
                    } else {
                        $rootScope.$broadcast("displayError", { Message: "Oh you sneaky bastard! This post is not yours to edit." });
                        $location.path("/404");
                    }
                    blockUiService.unblockIt();
                }, function (e) {
                    console.log(e);
                    $location.path("/404");
                });
            }
        };

        $scope.$on("windowSizeChanged", function (e, d) {
            configProvider.setDimensions(d.width, d.height);
            $scope.dimensionMode = configProvider.windowDimensions.mode;
        });

        $scope.init();

        var uploader = $scope.uploader = $fileUploader.create({
            scope: $scope,
            url: $scope.uploadUrl
        });

        uploader.filters.push(function (item /*{File|HTMLInputElement}*/) {
            var type = uploader.isHTML5 ? item.type : '/' + item.value.slice(item.value.lastIndexOf('.') + 1);
            type = '|' + type.toLowerCase().slice(type.lastIndexOf('/') + 1) + '|';
            return '|jpg|png|jpeg|bmp|gif|mp4|flv'.indexOf(type) !== -1;
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