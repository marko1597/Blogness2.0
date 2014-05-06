var blog = angular.module("blog", ["ngRoute", "localStorageModule", "mgcrea.ngStrap", "snap",
    "ngHeader", "ngLogin", "ngPosts", "ngNavigation", "ngNavigation", "ngUser"]);

blog.config(function ($routeProvider) {
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
        .when('/events', {
            templateUrl: '/blog/scripts/templates/events.html',
            controller: ''
        });
});