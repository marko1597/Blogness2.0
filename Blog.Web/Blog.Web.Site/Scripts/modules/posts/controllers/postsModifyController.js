ngPosts.controller('postsModifyController', ["$scope", "$rootScope", "$location", "$timeout", "FileUploader", "localStorageService",
    "postsService", "userService", "tagsService", "errorService", "dateHelper", "configProvider", "authenticationService", 
    function ($scope, $rootScope, $location, $timeout, FileUploader, localStorageService, postsService, userService, tagsService,
        errorService, dateHelper, configProvider, authenticationService) {

        $scope.isAdding = true;

        $scope.existingContents = [];

        $scope.username = null;

        $scope.authData = null;

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
            postsService.getPost($rootScope.$stateParams.postId).then(function (resp) {
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
                                }
                            };
                            $scope.existingContents.push(item);
                        });

                        $timeout(function () {
                            _.each($scope.existingContents, function (c) {
                                uploader.queue.push(c);
                            });
                            $scope.$broadcast("resizeIsotopeItems");
                        }, 500);
                    } else {
                        errorService.displayError(resp.Error);
                    }
                } else {
                    errorService.displayError({ Message: "Oh you sneaky bastard! This post is not yours to edit." });
                }
            }, function (e) {
                errorService.displayError(e);
            });
        };

        $scope.savePost = function () {
            if ($scope.authData) {
                userService.getUserInfo($scope.username).then(function (userinfo) {
                    $scope.post.User = userinfo;

                    if ($scope.isAdding) {
                        postsService.addPost($scope.post).then(function (resp) {
                            if (resp.Error == undefined) {
                                $location.path("/");
                            } else {
                                errorService.displayError(resp.Error);
                            }
                        }, function (e) {
                            errorService.displayError(e);
                        });
                    } else {
                        postsService.updatePost($scope.post).then(function (resp) {
                            if (resp.Error == undefined) {
                                $location.path("/");
                            } else {
                                errorService.displayError(resp.Error);
                            }
                        }, function (e) {
                            errorService.displayError(e);
                        });
                    }
                }, function (e) {
                    errorService.displayError(e);
                });
            } else {
                $rootScope.$broadcast("launchLoginForm");
            }
        };

        $scope.init = function () {
            authenticationService.getUserInfo().then(function (response) {
                if (response.Message == undefined || response.Message == null) {
                    if (!isNaN($rootScope.$stateParams.postId)) {
                        $scope.username = localStorageService.get("username");
                        $scope.authData = localStorageService.get("authorizationData");
                        $scope.getPost();
                    }
                } else {
                    errorService.displayErrorRedirect(response.Message);
                }
            });
        };

        $rootScope.$on("loggedInUserInfo", function (ev, data) {
            $scope.user = data;
        });

        $rootScope.$on("userLoggedIn", function () {
            $scope.username = localStorageService.get("username");
            $scope.authData = localStorageService.get("authorizationData");
        });

        $scope.$on("windowSizeChanged", function (e, d) {
            configProvider.setDimensions(d.width, d.height);
            $scope.dimensionMode = configProvider.windowDimensions.mode;

        });

        var uploader = $scope.uploader = new FileUploader({
            scope: $rootScope,
            url: $scope.uploadUrl,
            headers: { Authorization: 'Bearer ' + ($scope.authData ? $scope.authData.token : "") }
        });

        uploader.filters.push({
            name: 'imageFilter',
            fn: function (item /*{File|HTMLInputElement}*/) {
                var type = uploader.isHTML5 ? item.type : '/' + item.value.slice(item.value.lastIndexOf('.') + 1);
                type = '|' + type.toLowerCase().slice(type.lastIndexOf('/') + 1) + '|';
                return '|jpg|png|jpeg|bmp|gif|mp4|flv|webm|'.indexOf(type) !== -1;
            }
        });


        uploader.onSuccessItem = function (fileItem, response) {
            fileItem.mediaId = response.MediaId;
            var media = {
                PostId: 0,
                Media: response,
                PostContentTitle: fileItem.postContentTitle,
                PostContentText: fileItem.postContentText
            };
            $scope.post.PostContents.push(media);
        };

        uploader.onAfterAddingFile = function (fileItem) {
            fileItem.allowCaptions = true;
            fileItem.postContentTitle = "";
            fileItem.postContentText = "";
        };

        uploader.onAfterAddingAll = function () {
        };

        $scope.init();
    }
]);