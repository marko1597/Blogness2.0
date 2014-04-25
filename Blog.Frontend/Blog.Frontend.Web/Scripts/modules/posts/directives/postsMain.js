postsModule.directive('postsMain', function () {
    var ctrlFn = function ($scope, $timeout, postsService) {
        $scope.posts = [];
        $scope.errorContent = { Show: false, Type: "" };

        $scope.getPopularPosts = function () {
            postsService.getPopularPosts().then(function (resp) {
                $scope.posts = resp;
            }, function (errorMsg) {
                alert(errorMsg);
            });
        };

        $scope.getErrorType = function() {
            return $scope.errorContent.Type;
        };

        $timeout(function () {
            $scope.$emit('iso-option', { layoutMode: 'masonry' });
        }, 2000);

        $scope.getPopularPosts();
    };
    ctrlFn.$inject = ["$scope", "$timeout", "postsService"];

    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        template:
            '<div>' +
                '<div class="row">' +
                    '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">' +
                        '<div>' +
                            '<div id="posts-main" isotope-container infinite-scroll="loadMorePosts()" infinite-scroll-distance="0">' +
                                '<div class="post-item" ng-repeat="post in posts" isotope-item post-item data="post"></div>' +
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
        controller: ctrlFn
    };
});
