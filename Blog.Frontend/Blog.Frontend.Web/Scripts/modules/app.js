var blog = angular.module("blog", ["ngRoute", "mgcrea.ngStrap", "snap", "ngLogger",
    "ngHeader", "ngLogin", "ngPosts", "ngNavigation", "ngNavigation", "ngUser", "ngTags"]);

blog.config(["$routeProvider", function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: '/blog/scripts/templates/posts.html',
            controller: 'postsController'
        })
        .when('/friends', {
            templateUrl: '/blog/scripts/templates/friends.html',
            controller: ''
        })
        .when('/groups', {
            templateUrl: '/blog/scripts/templates/groups.html',
            controller: ''
        })
        .when('/profile', {
            templateUrl: '/blog/scripts/templates/profile.html',
            controller: ''
        })
        .when('/post/new', {
            templateUrl: '/blog/scripts/templates/modifyPost.html',
            controller: 'postsModifyController'
        })
        .when('/events', {
            templateUrl: '/blog/scripts/templates/events.html',
            controller: ''
        });
}]);