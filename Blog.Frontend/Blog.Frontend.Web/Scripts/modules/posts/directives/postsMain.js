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
            } else if (postsMain > 550 && postsMain < 720) {
                $timeout(function () {
                    _.each($("div.post-item"), function (a) {
                        $(a).width("46%");
                    });
                    scope.$emit("updatePostsSize", "small");
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
        template:
            '<div>' +
                '<div class="row">' +
                    '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">' +
                        '<div>' +
                            '<div id="posts-main" isotope-container infinite-scroll="loadMorePosts()" infinite-scroll-distance="0">' +
                                '<div class="post-item" ng-repeat="post in posts" isotope-item post-item data="{ Post: post, Width: size }"></div>' +
                            '</div>' +
                            '<div id="error-main" ng-show="errorContent.Show">' +
                                '<a href="#">' +
                                    '<div ng-switch="getErrorType()">' +
                                        '<div ng-switch-when="404">' +
                                            '<img src="../blog/content/images/error-pages/pagenotfound_bg.png" />' +
                                        '</div>' +
                                        '<div ng-switch-when="500">' +
                                            '<img src="../blog/content/images/error-pages/servererror_bg.png" />' +
                                        '</div>' +
                                    '</div>' +
                                '</a>' +
                            '</div>' +
                        '</div>' +
                    '</div>' +
                '</div>' +
            '</div>',
        controller: ctrlFn,
        link: linkFn
    };
}]);
