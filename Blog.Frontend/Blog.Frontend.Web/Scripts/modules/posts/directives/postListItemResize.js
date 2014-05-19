ngPosts.directive('postItemResize', [
    function () {
        var linkFn = function (scope) {
            scope.$on("windowSizeChanged", function (e, d) {
                resizePosts();
            });

            var resizePosts = function () {
                var postsMain = $("#posts-main").outerWidth();
                if (postsMain == 940) {
                    _.each($("div.post-item"), function (a) {
                        $(a).width("448px");
                    });
                    scope.$emit("updatePostsSize", "large");
                } else if (postsMain == 1140) {
                    _.each($("div.post-item"), function (a) {
                        $(a).width("368px");
                    });
                    scope.$emit("updatePostsSize", "xlarge");
                } else if (postsMain == 720) {
                    _.each($("div.post-item"), function (a) {
                        $(a).width("338px");
                    });
                    scope.$emit("updatePostsSize", "medium");
                } else {
                    _.each($("div.post-item"), function (a) {
                        $(a).width("98%");
                    });
                    scope.$emit("updatePostsSize", "xsmall");
                }
                scope.$emit('iso-option', { layoutMode: 'masonry' });
            };
            resizePosts();
        };

        return {
            restrict: 'EA',
            link: linkFn
        };
    }
]);
