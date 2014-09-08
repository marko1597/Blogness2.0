ngUser.controller('userProfileController', ["$scope", "$location", "$rootScope", "localStorageService", "userService", "errorService",
    function ($scope, $location, $rootScope, localStorageService, userService, errorService) {
        $scope.authData = localStorageService.get("authorizationData");
        $scope.user = null;
        $scope.userFullName = null;
        $scope.hobbies = [];
        $scope.education = [];
        $scope.work = [];

        $scope.username = ($rootScope.$stateParams.username == null || $rootScope.$stateParams.username === "undefined") ?
                localStorageService.get("username") : $rootScope.$stateParams.username;

        $scope.init = function() {
            $scope.getUserInfo();
        };
        
        $scope.getUserInfo = function () {
            if ($scope.username) {
                userService.getUserInfo($scope.username).then(function (response) {
                    if (response.Error == null) {
                        $scope.address = response.Address;
                        $scope.work = response.Work;
                        $scope.hobbies = response.Hobbies;
                        $scope.education = response.EducationGroups;

                        delete response.Education;
                        delete response.Address;
                        delete response.Hobbies;

                        $scope.user = response;
                        $scope.userFullName = $scope.user.FirstName + " " + $scope.user.LastName;
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
        
        $rootScope.$on("userLoggedIn", function () {
            $scope.authData = localStorageService.get("authorizationData");
            $scope.getUserInfo();
        });

        $scope.init();
    }
]);