postsModule.directive('postsMain', function () {
    var ctrlFn = function ($scope, $rootScope) {

    };
    ctrlFn.$inject = ["$scope", "$rootScope"];

    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        template:
            '<div class="row">' +
                '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">' +
                    '<div id="posts-main">' +
                        '<div class="jumbotron">' +
                            '<h2>Jason Magpantay</h2>' +
                            '<p>Kris Arianne</p>' +
                            '<p><a class="btn btn-primary btn-lg" role="button">Learn more</a></p>' +
                        '</div>' +
                    '</div>' +
                '</div>' +
            '</div>',
        controller: ctrlFn
    };
});
