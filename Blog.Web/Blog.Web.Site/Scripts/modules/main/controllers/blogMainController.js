﻿blog.controller('blogMainController', ["$scope", "$location", "$rootScope", "$log", "$timeout", "configProvider",
    "localStorageService", "postsService", "userService", "authenticationService", 
    function ($scope, $location, $rootScope, $log, $timeout, configProvider, localStorageService, postsService,
        userService, authenticationService) {

        $scope.authData = localStorageService.get('authorizationData');

        $scope.username = null;

        $rootScope.$on("$locationChangeStart", function (event, next, current) {
            $log.info("location changing from " + current + " to " + next);

            if (current !== configProvider.getSettings().BlogRoot + "/#/") {
                postsService.getRecentPosts()
                    .then(function (response) {
                        $log.info(response);
                    }, function (error) {
                        console.log(error);
                    });
            }

            if ($rootScope.user) {
                $rootScope.$broadcast("loggedInUserInfo", $rootScope.user);
            }
        });

        $scope.init = function() {
            if ($scope.authData != null) {
                $scope.username = localStorageService.get('username');

                authenticationService.getUserInfo().then(function(response) {
                    if (response.Message == undefined || response.Message == null) {
                        $scope.getUserInfo($scope.username);
                    }
                }, function() {
                    authenticationService.logout();
                });
            } else {
                authenticationService.logout();
            }
        };

        $scope.getUserInfo = function (username) {
            userService.getUserInfo(username).then(function (user) {
                if (user.Error == null) {
                    $rootScope.user = user;
                    $rootScope.authData = $scope.authData;
                    $timeout(function () {
                        $rootScope.$broadcast("loggedInUserInfo", user);
                    }, 1500);
                }
            });
        };

        $scope.snapOptions = {
            maxPosition: 321,
            minPosition: -321
        };

        $rootScope.$on("userLoggedIn", function (ev, data) {
            $scope.getUserInfo(data.username);
        });

        $scope.init();
    }
]);