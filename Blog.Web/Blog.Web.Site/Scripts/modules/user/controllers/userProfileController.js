ngUser.controller('userProfileController', ["$scope", "$location", "$rootScope", "localStorageService", "userService", "blockUiService", "errorService",
    function ($scope, $location, $rootScope, localStorageService, userService, blockUiService, errorService) {
        $scope.authData = localStorageService.get("authorizationData");
        $scope.user = null;
        $scope.userFullName = null;
        $scope.newHobby = "";
        $scope.hobbies = [];
        $scope.education = [];
        $scope.work = [];
        $scope.address = [];

        $scope.emptyRecordMessage = {
            hobbies: "Uhhh..you got no hobbies..do you even life?"
        };

        $scope.username = ($rootScope.$stateParams.username == null || $rootScope.$stateParams.username === "undefined") ?
                localStorageService.get("username") : $rootScope.$stateParams.username;

        $scope.isEditing = {
            details: false,
            hobbies: false,
            address: false,
            education: false,
            work: false
        };

        $scope.editDetails = function() {
            $scope.isEditing.details = true;
        };

        $scope.editAddress = function() {
            $scope.isEditing.address = true;
        };

        $scope.editHobbies = function () {
            $scope.isEditing.hobbies = true;
        };

        $scope.saveDetails = function () {
            $scope.isEditing.details = false;
        };

        $scope.saveAddress = function () {
            $scope.isEditing.address = false;
        };

        $scope.saveHobbies = function () {
            $scope.isEditing.hobbies = false;
        };

        $scope.showNoHobbiesMessage = function() {
            if ($scope.hobbies.length > 0) {
                return false;
            }
            return true;
        };

        $scope.getUserInfo = function () {
            blockUiService.blockIt();

            if ($scope.username) {
                userService.getUserInfo($scope.username).then(function (response) {
                    if (response.Error == null) {
                        $scope.user = response;
                        $scope.address = response.Address;
                        $scope.hobbies = response.Hobbies;
                        $scope.education = response.Education;
                        $scope.work = response.Work;
                        $scope.userFullName = $scope.user.FirstName + " " + $scope.user.LastName;
                        $scope.$broadcast("resizeIsotopeItems");
                        blockUiService.unblockIt();
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

        $rootScope.$on("userLoggedIn", function () {
            $scope.authData = localStorageService.get("authorizationData");
            $scope.getUserInfo();
        });

        $scope.getUserInfo();
    }
]);