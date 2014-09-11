var blog = angular.module("blog", ["ngRoute", "ngAnimate", "mgcrea.ngStrap", "snap", "ngSockets", "ngLogger",
    "ngHeader", "ngLogin", "ngPosts", "ngComments", "ngError", "ngNavigation", "ngNavigation", "ngUser",
    "ngTags", "ui.router"]);
    
blog.run([
    '$rootScope', '$state', '$stateParams',
    function ($rootScope, $state, $stateParams) {
            $rootScope.$state = $state;
            $rootScope.$stateParams = $stateParams;
        }
]);