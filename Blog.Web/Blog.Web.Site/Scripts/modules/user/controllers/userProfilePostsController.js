﻿ngUser.controller('userProfilePostsController', ["$scope", "$rootScope", "$stateParams", "userService", "postsService", "blockUiService", "errorService", "localStorageService",
    function ($scope, $rootScope, $stateParams, userService, postsService, blockUiService, errorService, localStorageService) {
        $scope.user = null;
        $scope.posts = [];
        $scope.isBusy = false;
        $scope.username = ($rootScope.$stateParams.username == null || $rootScope.$stateParams.username === "undefined") ?
                localStorageService.get("username") : $rootScope.$stateParams.username;
        $scope.size = "";

        $scope.init = function () {
            $scope.getUserInfo();
            $rootScope.$broadcast("updateScrollTriggerWatch", "user-profile-posts-list");
        };

        $scope.getUserInfo = function () {
            if ($scope.username) {
                userService.getUserInfo($scope.username).then(function (response) {
                    if (response.Error == null) {
                        $scope.user = response;
                        blockUiService.unblockIt();
                        $scope.getPostsByUser(response.Id);
                    } else {
                        errorService.displayErrorRedirect(response.Error);
                        blockUiService.unblockIt();
                    }
                }, function (err) {
                    errorService.displayErrorRedirect(err);
                    blockUiService.unblockIt();
                });
            } else {
                errorService.displayErrorRedirect({ Message: "User lookup failed. Sorry. :(" });
                blockUiService.unblockIt();
            }
        };

        $scope.getPostsByUser = function (userId) {
            if ($scope.isBusy) {
                return;
            }
            $scope.isBusy = true;

            postsService.getPostsByUser(userId).then(function (resp) {
                $scope.posts = resp;
                $scope.isBusy = false;
                $scope.$broadcast("resizeIsotopeItems");
            }, function(e) {
                errorService.displayErrorRedirect(e);
            });
        };

        $scope.getMorePostsByUser = function () {
            if ($scope.isBusy) {
                return;
            }
            $scope.isBusy = true;

            postsService.getMorePostsByUser($scope.user.Id, $scope.posts.length).then(function (resp) {
                _.each(resp, function (p) {
                    $scope.posts.push(p);
                });
                $scope.isBusy = false;
                $scope.$broadcast("resizeIsotopeItems");
            }, function (e) {
                errorService.displayErrorRedirect(e);
            });
        };

        $scope.$on("scrollBottom", function () {
            $scope.getMorePostsByUser();
        });
        
        $scope.$on("updateUserPostsSize", function (ev, size) {
            $scope.size = size;
        });
        
        $scope.init();
    }
]);