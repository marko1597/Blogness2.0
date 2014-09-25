ngUser.controller('userProfileCommentsController', ["$scope", "$rootScope", "$stateParams", "commentsService", "userService", "errorService", "localStorageService",
    function ($scope, $rootScope, $stateParams, commentsService, userService, errorService, localStorageService) {
        $scope.user = null;

        $scope.comments = [];

        $scope.isBusy = false;

        $scope.username = ($rootScope.$stateParams.username == null || $rootScope.$stateParams.username === "undefined") ?
                localStorageService.get("username") : $rootScope.$stateParams.username;

        $scope.init = function () {
            if ($rootScope.$stateParams.username != null || $rootScope.$stateParams.username !== "undefined") {
                $scope.getUserInfo();
            }
            $rootScope.$broadcast("updateScrollTriggerWatch", "user-profile-comments-list");
        };

        $scope.getUserInfo = function () {
            if ($scope.username) {
                userService.getUserInfo($scope.username).then(function (response) {
                    if (response.Error == null) {
                        $scope.user = response;
                        $scope.getCommentsByUser();
                    } else {
                        errorService.displayError(response.Error);
                    }
                }, function (err) {
                    errorService.displayError(err);
                });
            } else {
                errorService.displayError({ Message: "User lookup failed. Sorry. :(" });
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
                errorService.displayError(e);
            });
        };

        $scope.$on("loggedInUserInfo", function (ev, data) {
            $scope.user = data;
            $scope.getCommentsByUser();
        });

        $scope.init();
    }
]);