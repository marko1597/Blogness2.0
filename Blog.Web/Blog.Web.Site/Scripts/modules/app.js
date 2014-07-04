﻿var blog = angular.module("blog", ["ngRoute", "ngAnimate", "mgcrea.ngStrap", "snap", "ngLogger",
    "ngHeader", "ngLogin", "ngPosts", "ngComments", "ngError", "ngNavigation", "ngNavigation", "ngUser",
    "ngTags"]);

blog.directive("windowResize", ["$window", "$rootScope", "$timeout", function ($window, $rootScope, $timeout) {
    return {
        restrict: 'EA',
        link: function postLink(scope) {
            scope.onResizeFunction = function () {
                scope.windowHeight = $window.innerHeight;
                scope.windowWidth = $window.innerWidth;
                $rootScope.$broadcast("windowSizeChanged", {
                    height: scope.windowHeight,
                    width: scope.windowWidth
                });
            };

            scope.onResizeFunction();

            angular.element($window).bind('resize', function () {
                $timeout(function () {
                    scope.onResizeFunction();
                    scope.$apply();
                }, 500);
            });
        }
    };
}]);

blog.config(["$routeProvider", "$httpProvider", "$provide",
    function ($routeProvider, $httpProvider, $provide) {
        $provide.factory('httpInterceptor', function ($q, $location) {
            return {
                request: function (config) {
                    return config || $q.when(config);
                },

                requestError: function (rejection) {
                    return $q.reject(rejection);
                },

                response: function (response) {
                    return response || $q.when(response);
                },

                responseError: function (rejection) {
                    return $q.reject(rejection);
                }
            };
        });

        $httpProvider.interceptors.push('httpInterceptor');

        $routeProvider
            .when('/', {
                templateUrl: 'scripts/templates/posts.html',
                controller: 'postsController'
            })
            .when('/friends', {
                templateUrl: 'scripts/templates/friends.html',
                controller: ''
            })
            .when('/groups', {
                templateUrl: 'scripts/templates/groups.html',
                controller: ''
            })
            .when('/profile', {
                templateUrl: 'scripts/templates/profile.html',
                controller: ''
            })
            .when('/post/new', {
                templateUrl: 'scripts/templates/modifypost.html',
                controller: 'postsModifyController'
            })
            .when('/post/new/:postId', {
                templateUrl: 'scripts/templates/modifypost.html',
                controller: 'postsModifyController'
            })
            .when('/events', {
                templateUrl: 'scripts/templates/events.html',
                controller: ''
            })
            .when('/error', {
                templateUrl: 'scripts/templates/errorpage.html',
                controller: 'errorPageController'
            })
            .when('/post/:postId', {
                templateUrl: 'scripts/templates/viewpost.html',
                controller: 'postsViewController'
            })
            .otherwise({
                redirectTo: '/error'
            });
    }]);