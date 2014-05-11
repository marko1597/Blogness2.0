ngPosts.directive('postContents', function () {
    var ctrlFn = function ($scope) {
    };
    ctrlFn.$inject = ["$scope"];

    return {
        restrict: 'EA',
        scope: { contents: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesUrl + "posts/postContents.html",
        controller: ctrlFn
    };
});
