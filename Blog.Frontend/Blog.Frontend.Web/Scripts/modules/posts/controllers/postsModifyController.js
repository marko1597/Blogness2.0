﻿ngPosts.controller('postsModifyController', ["$scope", "$rootScope", "$location", "$routeParams", "$timeout", "$fileUploader", "localStorageService", "postsService", "userService", "tagsService", "errorService", "blockUiService", "dateHelper", "configProvider",
    function ($scope, $rootScope, $location, $routeParams, $timeout, $fileUploader, localStorageService, postsService, userService, tagsService, errorService, blockUiService, dateHelper, configProvider) {
        $scope.isAdding = true;

        $scope.dimensionMode = configProvider.windowDimensions.mode == "" ?
            window.getDimensionMode() : configProvider.windowDimensions.mode;

        $scope.post = {
            PostTitle: "",
            PostMessage: "",
            PostContents: [],
            Tags: []
        };

        $scope.existingContents = [];

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

        $scope.getTagsSource = function (t) {
            return tagsService.getTagsByName(t);
        };

        $scope.savePost = function () {
            blockUiService.blockIt();
            userService.getUserInfo().then(function (userinfo) {
                $scope.post.User = userinfo;

                if ($scope.isAdding) {
                    postsService.addPost($scope.post).then(function (resp) {
                        if (resp.Error == undefined) {
                            blockUiService.unblockIt();
                            $location.path("/");
                        } else {
                            errorService.displayErrorUnblock(resp.Error);
                        }
                    }, function (e) {
                        errorService.displayErrorRedirect({ Message: e });
                    });
                } else {
                    postsService.updatePost($scope.post).then(function (resp) {
                        if (resp.Error == undefined) {
                            blockUiService.unblockIt();
                            $location.path("/");
                        } else {
                            errorService.displayErrorUnblock(resp.Error);
                        }
                    }, function (e) {
                        errorService.displayErrorRedirect({ Message: e });
                    });
                }
            }, function (e) {
                errorService.displayErrorRedirect({ Message: e });
            });
        };

        $scope.init = function () {
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

                            _.each(resp.PostContents, function (t) {
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
                                    remove: function () {
                                        var index = $scope.post.PostContents.indexOf(this.base);
                                        $scope.post.PostContents.splice(index);
                                        uploader.removeFromQueue(this);
                                        console.log($scope.post);
                                    }
                                };
                                $scope.existingContents.push(item);
                            });

                            $timeout(function () {
                                _.each($scope.existingContents, function (c) {
                                    uploader.queue.push(c);
                                });
                                $timeout(function() {
                                    $rootScope.$broadcast("resizeUploadThumbnails", {});
                                }, 500);
                            }, 500);

                            blockUiService.unblockIt();
                        } else {
                            errorService.displayErrorUnblock(resp.Error);
                        }
                    } else {
                        errorService.displayErrorUnblock({ Message: "Oh you sneaky bastard! This post is not yours to edit." });
                    }
                }, function (e) {
                    errorService.displayErrorRedirect({ Message: e });
                });
            }
        };

        $scope.$on("windowSizeChanged", function (e, d) {
            configProvider.setDimensions(d.width, d.height);
            $scope.dimensionMode = configProvider.windowDimensions.mode;

        });

        $scope.$on("resizeUploadThumbnails", function () {
            $timeout(function () {
                $scope.$broadcast('iso-method', { name: null, params: null });
                blockUiService.unblockIt();
            }, 500);
        });

        var uploader = $scope.uploader = $fileUploader.create({
            scope: $rootScope,
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
        });

        uploader.bind('afteraddingfile', function () {
            blockUiService.blockIt();
        });

        uploader.bind('afteraddingall', function () {
            blockUiService.blockIt();
        });

        $scope.init();
    }
]);