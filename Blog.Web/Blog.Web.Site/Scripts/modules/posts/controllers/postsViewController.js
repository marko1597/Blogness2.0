ngPosts.controller('postsViewController', ["$scope", "$rootScope", "$location", "postsService",
    "userService", "configProvider", "errorService", "localStorageService",
    function ($scope, $rootScope, $location, postsService, userService, configProvider,
        errorService, localStorageService) {

        $scope.postId = parseInt($rootScope.$stateParams.postId);

        $scope.post = {};

        $scope.user = {};

        $scope.viewCount = [];

        $scope.postsList = [];

        $scope.postLikes = [];

        $scope.isBusy = false;

        $scope.authData = localStorageService.get("authorizationData");

        $scope.init = function () {
            if ($scope.isBusy) {
                return;
            }
            $scope.isBusy = true;

            if ($scope.authData) {
                var username = localStorageService.get("username");
                userService.getUserInfo(username).then(function (user) {
                    $scope.user = user;
                }, function (e) {
                    errorService.displayError({ Message: e });
                });
            }

            $scope.getViewedPost();
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
                        $scope.$broadcast("viewedPostLoaded", { PostId: $scope.post.Id, PostLikes: $scope.post.PostLikes });
                        $scope.$broadcast("resizeIsotopeItems");
                        postsService.subscribeToPost($scope.post.Id);
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

        $scope.hasContents = function() {
            if ($scope.post && $scope.post.PostContents && $scope.post.PostContents.length > 0) {
                return true;
            }
            return false;
        };

        $rootScope.$on(configProvider.getSocketClientFunctions().getPostLikes, function (e, d) {
            if (d.postId == $scope.post.Id) {
                $scope.postLikes = d.postLikes;
            }
        });

        $rootScope.$on(configProvider.getSocketClientFunctions().viewCountUpdate, function (e, d) {
            if (d.postId == $scope.post.Id) {
                $scope.viewCount = d.viewCount;
            }
        });

        $scope.init();
    }
]);