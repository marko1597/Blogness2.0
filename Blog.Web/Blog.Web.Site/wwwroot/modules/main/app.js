var blog = angular.module("blog",
    [
        "ngRoute",
        "ngAnimate",
        "ngCookies",
        "mgcrea.ngStrap",
        "snap",
        "ngConfig",
        "ngLogger",
        "ngHeader",
        "ngLogin",
        "ngPosts",
        "ngCommunities",
        "ngComments",
        "ngError",
        "ngNavigation",
        "ngMessaging",
        "ngUser",
        "ngTags",
        "ui.router"
    ]);

blog.run([
    '$rootScope', '$state', '$stateParams',
    function ($rootScope, $state, $stateParams) {
        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;
    }
]);