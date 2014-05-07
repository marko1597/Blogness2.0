ngPosts.directive('postsMain', ["$window", "$timeout", function ($window, $timeout) {
    var ctrlFn = function () {
        
    };
    ctrlFn.$inject = ["$scope", "postsService"];

    var linkFn = function (scope) {
        var window = angular.element($window);
        window.bind("resize", function () {
            resizePosts();
        });

        var resizePosts = function () {
            var postsMain = $("#posts-main").outerWidth();
            if (postsMain == 940) {
                $timeout(function () {
                    _.each($("div.post-item"), function (a) {
                        $(a).width("448px");
                    });
                    scope.$emit("updatePostsSize", "large");
                    scope.$emit('iso-option', { layoutMode: 'masonry' });
                }, 500);
            } else if (postsMain == 1140) {
                $timeout(function () {
                    _.each($("div.post-item"), function (a) {
                        $(a).width("368px");
                    });
                    scope.$emit("updatePostsSize", "xlarge");
                    scope.$emit('iso-option', { layoutMode: 'masonry' });
                }, 500);
            } else if (postsMain == 720) {
                $timeout(function () {
                    _.each($("div.post-item"), function (a) {
                        $(a).width("338px");
                    });
                    scope.$emit("updatePostsSize", "medium");
                    scope.$emit('iso-option', { layoutMode: 'masonry' });
                }, 500);
            } else {
                $timeout(function () {
                    _.each($("div.post-item"), function (a) {
                        $(a).width("98%");
                    });
                    scope.$emit("updatePostsSize", "xsmall");
                    scope.$emit('iso-option', { layoutMode: 'masonry' });
                }, 500);
            }
        };

        resizePosts();
    };

    return {
        restrict: 'EA',
        scope: {
            posts: '=',
            size: '=',
            getErrorType: '&',
            loadMorePosts: '&'
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesUrl + "posts/postsMain.html",
        controller: ctrlFn,
        link: linkFn
    };
}]);
