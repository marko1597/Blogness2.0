postsModule.directive('postContent', function () {
    var ctrlFn = function ($scope) {

    };
    ctrlFn.$inject = ["$scope"];

    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        template:
            '<div id="postContent">' +
                '<div id="posts-main">' +
                    '<div class="jumbotron">' +
                        '<h2>Jason Magpantay</h2>' +
                        '<p>Kris Arianne</p>' +
                        '<p><a class="btn btn-primary btn-lg" role="button">Learn more</a></p>' +
                    '</div>' +
                '</div>' +
            '</div>',
        controller: ctrlFn
    };
});
