ngPosts.controller('postsViewController', ["$scope", "$rootScope", "$location", "postsService",
    "userService", "mediaService", "configProvider", "errorService", "localStorageService",
    function ($scope, $rootScope, $location, postsService, userService, mediaService, configProvider,
        errorService, localStorageService) {

        $scope.postId = parseInt($rootScope.$stateParams.postId);

        $scope.post = null;

        $scope.user = null;

        $scope.viewCount = [];

        $scope.postsList = [];

        $scope.postLikes = [];

        $scope.isBusy = false;

        $scope.isEditable = false;

        $scope.authData = localStorageService.get("authorizationData");

        $scope.username = localStorageService.get("username");

        $scope.toggleIsEditable = function () {
            if ($scope.user && $scope.post && $scope.post.User.UserName === $scope.username) {
                $scope.isEditable = true;
                return;
            }
            $scope.isEditable = false;
        };

        $scope.$on("loggedInUserInfo", function (ev, data) {
            if (data) {
                $scope.username = data.UserName;
                $scope.toggleIsEditable();
            }
        });

        $rootScope.$watch('user', function () {
            if ($rootScope.user) {
                $scope.user = $rootScope.user;
                $scope.username = $rootScope.user.UserName;
                $scope.toggleIsEditable();
            }
        });

        $scope.editPost = function () {
            $location.path("/post/edit/" + $scope.post.Id);
        };

        $scope.getContentType = function (content) {
            if (content == undefined) return "image";

            var contentType = content.split('/');
            if (contentType[0] == "video") {
                return "video";
            } else {
                return "image";
            }
        };

        $scope.getPostsList = function () {
            postsService.getPopularPosts().then(function (list) {
                $scope.postsList = list;
            }, function (e) {
                errorService.displayError({ Message: e });
            });
        };

        $scope.getViewedPost = function () {
            if (!isNaN($rootScope.$stateParams.postId)) {
                postsService.getPost($scope.postId).then(function (post) {
                    if (post.Error == undefined) {
                        $scope.post = post;
                        $scope.isBusy = false;
                        $scope.toggleIsEditable();

                        $scope.$broadcast("resizeIsotopeItems");

                        // update post likes and view counts directive
                        $scope.postLikes = $scope.post.PostLikes;
                        $scope.viewCount = $scope.post.ViewCounts;

                        // subscribe to post socket.io events
                        postsService.subscribeToPost($scope.post.Id);

                        // update mediaservice viewed gallery
                        var mediaList = _.pluck(post.PostContents, 'Media');
                        mediaService.addViewedMediaListFromPost(mediaList, post.Id);
                        $rootScope.$broadcast("updateMediaGalleryFromPost", {});
                    } else {
                        errorService.displayError({ Message: e });
                    }
                }, function (e) {
                    errorService.displayError({ Message: e });
                });
            } else {
                errorService.displayErrorRedirect({ Message: "You're missing the post to edit bruh! Don't be stupid!" });
            }
        };

        $scope.hasContents = function () {
            if ($scope.post && $scope.post.PostContents && $scope.post.PostContents.length > 0) {
                return true;
            }
            return false;
        };

        $scope.init = function () {
            if ($scope.isBusy) {
                return;
            }
            $scope.isBusy = true;

            if ($scope.authData) {
                if ($rootScope.user) {
                    $scope.user = $rootScope.user;
                }
            }

            $scope.getViewedPost();
        };

        $scope.init();
    }
]);