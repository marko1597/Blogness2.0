ngPosts.directive('postRelatedItem', [function () {
    return {
        restrict: 'EA',
        scope: { post: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "posts/postrelateditem.html"
    };
}]);
