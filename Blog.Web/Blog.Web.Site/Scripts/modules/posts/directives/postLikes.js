ngPosts.directive('postLikes', [function () {
    var linkFn = function (scope, elem) {
        scope.highlight = function() {
            $(elem).effect("highlight", { color: "#B3C833" }, 1500);
        };
    };

    var ctrlFn = function ($scope, $rootScope, postsService, userService, errorService, localStorageService, configProvider) {
        $scope.postLikes = $scope.list;

        $scope.user = null;

        $scope.username = localStorageService.get("username");

        $scope.authData = localStorageService.get("authorizationData");

        $scope.tooltip = { "title": "Click to favorite this post." };

        $scope.$on(configProvider.getSocketClientFunctions().postLikesUpdate, function (e, d) {
            if (d.postId == $scope.postId) {
                $scope.postLikes = d.postLikes;
                $scope.highlight();
                $scope.isUserLiked();
            }
        });
        
        $scope.$on("loggedInUserInfo", function (ev, data) {
            $scope.user = data;
            $scope.isUserLiked();
        });

        $rootScope.$watch('user', function () {
            $scope.user = $rootScope.user;
            $scope.isUserLiked();
        });

        $scope.$on("viewedPostLoaded", function (e, d) {
            $scope.postId = d.PostId;
            $scope.postLikes = d.PostLikes;
            $scope.isUserLiked();
        });

        $scope.$watch('list', function() {
            $scope.postLikes = $scope.list;
        });

        $scope.likePost = function () {
            postsService.likePost($scope.postId, $scope.username).then(function () { },
            function (err) {
                errorService.displayError(err);
            });
        };

        $scope.isUserLiked = function () {
            var isLiked = false;
            if ($scope.authData && $scope.user) {
                _.each($scope.postLikes, function (p) {
                    if (p.UserId == $scope.user.Id) {
                        isLiked = true;
                    }
                });
            }

            return isLiked ? "fa-star" : "fa-star-o";
        };
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "postsService", "userService", "errorService", "localStorageService", "configProvider"];

    return {
        restrict: 'EA',
        scope: {
            list: '=',
            postId: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "posts/postlikes.html",
        controller: ctrlFn,
        link: linkFn
    };
}]);
