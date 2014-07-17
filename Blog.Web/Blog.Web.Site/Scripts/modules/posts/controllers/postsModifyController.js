ngPosts.controller('postsModifyController', ["$scope", "$rootScope", "$location", "$routeParams", "$timeout", "$fileUploader", "localStorageService", "postsService", "userService", "tagsService", "errorService", "blockUiService", "dateHelper", "configProvider",
    function ($scope, $rootScope, $location, $routeParams, $timeout, $fileUploader, localStorageService, postsService, userService, tagsService, errorService, blockUiService, dateHelper, configProvider) {
        $scope.isAdding = true;
        $scope.existingContents = [];
        $scope.username = localStorageService.get("username");
        $scope.authData = localStorageService.get("authorizationData");

        $scope.dimensionMode = configProvider.windowDimensions.mode == "" ?
            window.getDimensionMode() : configProvider.windowDimensions.mode;

        $scope.post = {
            PostTitle: "",
            PostMessage: "",
            PostContents: [],
            Tags: []
        };

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

        $scope.getTagsSource = function (t) {
            return tagsService.getTagsByName(t);
        };

        $scope.getPost = function () {
            blockUiService.blockIt();
            postsService.getPost($routeParams.postId).then(function(resp) {
                if ($scope.username === resp.User.UserName) {
                    if (resp.Error == undefined) {
                        $scope.isAdding = false;
                        $scope.post = resp;

                        _.each(resp.Tags, function(t) {
                            $scope.Tags.push({ text: t.TagName });
                        });

                        _.each(resp.PostContents, function(t) {
                            var item = {
                                file: {
                                    name: t.Media.FileName,
                                    size: 1e6
                                },
                                mediaId: t.Media.MediaId,
                                progress: 100,
                                isUploaded: true,
                                isSuccess: true,
                                isExisting: true,
                                url: t.Media.ThumbnailUrl,
                                base: t,
                                remove: function() {
                                    var index = $scope.post.PostContents.indexOf(this.base);
                                    $scope.post.PostContents.splice(index);
                                    uploader.removeFromQueue(this);
                                    console.log($scope.post);
                                }
                            };
                            $scope.existingContents.push(item);
                        });

                        $timeout(function() {
                            _.each($scope.existingContents, function(c) {
                                uploader.queue.push(c);
                            });
                            $scope.$broadcast("resizeIsotopeItems");
                        }, 500);

                        blockUiService.unblockIt();
                    } else {
                        blockUiService.unblockIt();
                        errorService.displayError(resp.Error);
                    }
                } else {
                    blockUiService.unblockIt();
                    errorService.displayErrorRedirect({ Message: "Oh you sneaky bastard! This post is not yours to edit." });
                }
            }, function(e) {
                blockUiService.unblockIt();
                errorService.displayErrorRedirect(e);
            });
        };

        $scope.savePost = function () {
            if ($scope.authData) {
                blockUiService.blockIt();

                userService.getUserInfo($scope.username).then(function (userinfo) {
                    $scope.post.User = userinfo;

                    if ($scope.isAdding) {
                        postsService.addPost($scope.post).then(function (resp) {
                            if (resp.Error == undefined) {
                                blockUiService.unblockIt();
                                $location.path("/");
                            } else {
                                blockUiService.unblockIt();
                                errorService.displayError(resp.Error);
                            }
                        }, function (e) {
                            blockUiService.unblockIt();
                            errorService.displayErrorRedirect(e);
                        });
                    } else {
                        postsService.updatePost($scope.post).then(function (resp) {
                            if (resp.Error == undefined) {
                                blockUiService.unblockIt();
                                $location.path("/");
                            } else {
                                blockUiService.unblockIt();
                                errorService.displayError(resp.Error);
                            }
                        }, function (e) {
                            blockUiService.unblockIt();
                            errorService.displayErrorRedirect(e);
                        });
                    }
                }, function (e) {
                    blockUiService.unblockIt();
                    errorService.displayErrorRedirect(e);
                });
            } else {
                $rootScope.$broadcast("launchLoginForm");
            }
        };

        $scope.init = function () {
            if ($scope.authData) {
                if (!isNaN($routeParams.postId)) {
                    $scope.getPost();
                }
            } else {
                $rootScope.$broadcast("launchLoginForm");
            }
        };

        $rootScope.$on("userLoggedIn", function() {
            $scope.username = localStorageService.get("username");
            $scope.authData = localStorageService.get("authorizationData");
        });

        $scope.$on("windowSizeChanged", function (e, d) {
            configProvider.setDimensions(d.width, d.height);
            $scope.dimensionMode = configProvider.windowDimensions.mode;

        });

        var uploader = $scope.uploader = $fileUploader.create({
            scope: $rootScope,
            url: $scope.uploadUrl,
            headers: { Authorization: 'Bearer ' + ($scope.authData ? $scope.authData.token : "") }
        });

        uploader.filters.push(function (item /*{File|HTMLInputElement}*/) {
            var type = uploader.isHTML5 ? item.type : '/' + item.value.slice(item.value.lastIndexOf('.') + 1);
            type = '|' + type.toLowerCase().slice(type.lastIndexOf('/') + 1) + '|';
            return '|jpg|png|jpeg|bmp|gif|mp4|flv|webm|'.indexOf(type) !== -1;
        });

        uploader.bind('success', function (event, xhr, item, response) {
            item.mediaId = response.MediaId;
            var media = {
                PostId: null,
                Media: response,
                PostContentTitle: item.postContentTitle,
                PostContentText: item.postContentText
            };
            $scope.post.PostContents.push(media);
        });

        uploader.bind('afteraddingfile', function (e, a) {
            a.allowCaptions = true;
            a.postContentTitle = "";
            a.postContentText = "";
        });

        uploader.bind('afteraddingall', function () {
        });

        $scope.init();
    }
]);