ngUser.controller('userProfileMediaController', ["$scope", "$rootScope", "$stateParams", "userService",
    "albumService", "errorService", "localStorageService", 
    function ($scope, $rootScope, $stateParams, userService, albumService, errorService, localStorageService) {
        $scope.user = null;

        $scope.albums = [];

        $scope.isBusy = false;

        $scope.username = ($rootScope.$stateParams.username == null || $rootScope.$stateParams.username === "undefined") ?
                localStorageService.get("username") : $rootScope.$stateParams.username;
        
        $scope.init = function () {
            if ($rootScope.$stateParams.username != null || $rootScope.$stateParams.username !== "undefined") {
                $scope.getUserInfo();
            }
        };

        $scope.getUserInfo = function () {
            if ($scope.username) {
                userService.getUserInfo($scope.username).then(function (response) {
                    if (response.Error == null) {
                        $scope.user = response;
                        $scope.getMediaByUser();
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

        $scope.getMediaByUser = function () {
            if ($scope.isBusy) {
                return;
            }
            $scope.isBusy = true;

            albumService.getAlbumsByUser($scope.user.Id).then(function (resp) {
                $scope.albums = resp;
                $scope.isBusy = false;
            }, function (e) {
                errorService.displayError(e);
            });
        };

        $rootScope.$watch('user', function () {
            if ($rootScope.user) {
                $scope.user = $rootScope.user;
                $scope.getMediaByUser();
            }
        });

        $scope.init();
    }
]);