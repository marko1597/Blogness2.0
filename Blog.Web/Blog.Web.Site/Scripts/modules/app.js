var blog = angular.module("blog", ["ngRoute", "ngAnimate", "mgcrea.ngStrap", "snap", "ngLogger",
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
        $provide.factory('httpInterceptor', ["$q", function ($q) {
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
        }]);

        $httpProvider.interceptors.push('httpInterceptor');

        $routeProvider
            .when('/', {
                templateUrl: window.blogConfiguration.templatesUrl + 'posts.html',
                controller: 'postsController'
            })
            .when('/friends', {
                templateUrl: window.blogConfiguration.templatesUrl + 'friends.html',
                controller: ''
            })
            .when('/groups', {
                templateUrl: window.blogConfiguration.templatesUrl + 'groups.html',
                controller: ''
            })
            .when('/profile', {
                templateUrl: window.blogConfiguration.templatesUrl + 'profile.html',
                controller: ''
            })
            .when('/post/new', {
                templateUrl: window.blogConfiguration.templatesUrl + 'modifypost.html',
                controller: 'postsModifyController'
            })
            .when('/post/new/:postId', {
                templateUrl: window.blogConfiguration.templatesUrl + 'modifypost.html',
                controller: 'postsModifyController'
            })
            .when('/events', {
                templateUrl: window.blogConfiguration.templatesUrl + 'events.html',
                controller: ''
            })
            .when('/error', {
                templateUrl: window.blogConfiguration.templatesUrl + 'errorpage.html',
                controller: 'errorPageController'
            })
            .when('/post/:postId', {
                templateUrl: window.blogConfiguration.templatesUrl + 'viewpost.html',
                controller: 'postsViewController'
            })
            .otherwise({
                redirectTo: '/error'
            });
    }]);