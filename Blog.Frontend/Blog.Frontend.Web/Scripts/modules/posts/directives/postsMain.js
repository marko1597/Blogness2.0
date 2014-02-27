postsModule.directive('postsMain', function () {
    var ctrlFn = function ($scope, postsService) {
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

        $scope.getPopularPosts();
    };
    ctrlFn.$inject = ["$scope", "postsService"];

    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        template:
            '<div>' +
                '<div class="row">' +
                    '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">' +
                        '<div id="posts-main">' +
                            '<div class="jumbotron">' +
                                '<h2>Jason Magpantay</h2>' +
                                '<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>' +
                                '<p><a class="btn btn-primary btn-lg" role="button">Learn more</a></p>' +
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
