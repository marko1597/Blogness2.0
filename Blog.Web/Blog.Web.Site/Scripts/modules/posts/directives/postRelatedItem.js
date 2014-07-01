ngPosts.directive('postRelatedItem', [function () {
    var ctrlFn = function ($scope, $rootScope) {
       
    };
    ctrlFn.$inject = ["$scope", "$rootScope"];

    return {
        restrict: 'EA',
        scope: { post: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesUrl + "posts/postrelateditem.html",
        controller: ctrlFn
    };
}]);
