ngUser.controller('userProfileCommentsController', ["$scope", "$rootScope", "$stateParams", "commentsService", "userService", "blockUiService", "errorService", "localStorageService",
    function ($scope, $rootScope, $stateParams, commentsService, userService, blockUiService, errorService, localStorageService) {
        $scope.user = null;
        $scope.comments = [];
        $scope.isBusy = false;
        $scope.username = ($rootScope.$stateParams.username == null || $rootScope.$stateParams.username === "undefined") ?
                localStorageService.get("username") : $rootScope.$stateParams.username;

        $scope.init = function () {
            $scope.getUserInfo();
            $rootScope.$broadcast("updateScrollTriggerWatch", "user-profile-comments-list");
        };

        $scope.getUserInfo = function () {
            blockUiService.blockIt();

            if ($scope.username) {
                userService.getUserInfo($scope.username).then(function (response) {
                    if (response.Error == null) {
                        $scope.user = response;
                        blockUiService.unblockIt();
                        $scope.getCommentsByUser();
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

        $scope.getCommentsByUser = function () {
            if ($scope.isBusy) {
                return;
            }
            $scope.isBusy = true;

            commentsService.getCommentsByUser($scope.user.Id).then(function (resp) {
                $scope.comments = resp;
                $scope.isBusy = false;
            }, function (e) {
                errorService.displayErrorRedirect(e);
            });
        };

        $scope.init();
    }
]);