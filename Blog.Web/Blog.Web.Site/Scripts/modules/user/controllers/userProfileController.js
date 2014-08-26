ngUser.controller('userProfileController', ["$scope", "$location", "$rootScope", "localStorageService", "userService", "blockUiService", "errorService", "dateHelper",
    function ($scope, $location, $rootScope, localStorageService, userService, blockUiService, errorService, dateHelper) {
        $scope.authData = localStorageService.get("authorizationData");
        $scope.user = null;
        $scope.userFullName = null;
        $scope.newHobby = "";
        $scope.hobbies = [];
        $scope.education = [
            {
                id: "grade-school",
                title: "Grade School",
                content: []
            },
            {
                id: "high-school",
                title: "High School",
                content: []
            },
            {
                id: "college",
                title: "College",
                content: []
            },
            {
                id: "graduate-school",
                title: "Graduate School",
                content: []
            }];
        $scope.work = [];
        $scope.address = [];

        $scope.emptyRecordMessage = {
            hobbies: "Uhhh..you got no hobbies..do you even life?",
            education: "It's alright, we know school is expensive..and lame..right?"
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

        $scope.editDetails = function () {
            $scope.isEditing.details = true;
        };

        $scope.editAddress = function () {
            $scope.isEditing.address = true;
        };

        $scope.editHobbies = function () {
            $scope.isEditing.hobbies = true;
        };

        $scope.editEducation = function (id, educationId) {
            var educationGroup = _.where($scope.education, { id: id })[0];
            var data = _.where(educationGroup.content, { EducationId: educationId })[0];
            data.isEditing = true;
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

        $scope.saveEducation = function (id, educationId) {
            var educationGroup = _.where($scope.education, { id: id })[0];
            var data = _.where(educationGroup.content, { EducationId: educationId })[0];
            data.isEditing = false;
        };

        $scope.showNoRecordsMessage = function (field, subfield) {
            if (subfield !== undefined) {
                var data = _.where($scope[field], { id: subfield })[0];
                if (data.content.length > 0) {
                    return false;
                }
                return true;
            }

            if ($scope[field].length > 0) {
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
                        $scope.work = response.Work;

                        _.each(response.Education, function (e) {
                            e.isEditing = false;
                            e.YearAttended = dateHelper.getMonthYear(e.YearAttended);
                            e.YearGraduated = dateHelper.getMonthYear(e.YearGraduated);

                            if (e.EducationType.EducationTypeId == 1) {
                                $scope.education[0].content.push(e);
                            } else if (e.EducationType.EducationTypeId == 2) {
                                $scope.education[1].content.push(e);
                            } else if (e.EducationType.EducationTypeId == 3) {
                                $scope.education[2].content.push(e);
                            } else {
                                $scope.education[3].content.push(e);

                            }
                        });

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