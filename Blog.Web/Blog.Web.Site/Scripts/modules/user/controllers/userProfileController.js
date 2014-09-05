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
                type: 1,
                isAdding: false,
                content: []
            },
            {
                id: "high-school",
                title: "High School",
                type: 2,
                isAdding: false,
                content: []
            },
            {
                id: "college",
                title: "College",
                type: 3,
                isAdding: false,
                content: []
            },
            {
                id: "graduate-school",
                title: "Graduate School",
                type: 4,
                isAdding: false,
                content: []
            }];
        $scope.work = [];
        $scope.address = [];
        $scope.error = {
            details: {},
            address: {}
        };

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

        $scope.init = function() {
            $scope.getUserInfo();
        };

        $scope.editDetails = function () {
            $scope.isEditing.details = true;
        };

        $scope.saveDetails = function () {
            blockUiService.blockIt();
            userService.updateUser($scope.user).then(function (response) {
                if (response.Error == null) {
                    delete response.Education;
                    delete response.Address;
                    delete response.Hobbies;

                    $scope.user = response;
                    $scope.userFullName = $scope.user.FirstName + " " + $scope.user.LastName;

                    blockUiService.unblockIt();
                    $scope.isEditing.details = false;
                } else {
                    errorService.displayErrorRedirect(response.Error);
                    blockUiService.unblockIt();
                    $scope.isEditing.details = false;
                }
            }, function (err) {
                $scope.getModelStateErrors(err.ModelState, "address");
                blockUiService.unblockIt();
            });
        };

        $scope.editAddress = function () {
            $scope.isEditing.address = true;
        };

        $scope.saveAddress = function () {
            userService.updateUserAddress($scope.address).then(function (response) {
                if (response.Error == null) {
                    $scope.address = response;
                    blockUiService.unblockIt();
                } else {
                    errorService.displayErrorRedirect(response.Error);
                    blockUiService.unblockIt();
                }
                $scope.isEditing.address = false;
            }, function (err) {
                $scope.getModelStateErrors(err.ModelState, "details");
                blockUiService.unblockIt();
            });
        };

        $scope.addHobby = function () {
            $scope.isEditing.hobbies = true;
        };

        $scope.editHobby = function (hobbyId) {
            var hobby = _.where($scope.hobbies, { HobbyId: hobbyId })[0];
            hobby.isEditing = true;
        };

        $scope.saveHobby = function (hobbyId) {
            if (hobbyId == undefined) {
                userService.addUserHobby($scope.newHobby).then(function (response) {
                    if (response.Error == null) {
                        $scope.hobbies.push(response);
                        $scope.newHobby = "";
                        blockUiService.unblockIt();
                    } else {
                        errorService.displayErrorRedirect(response.Error);
                        blockUiService.unblockIt();
                    }
                    $scope.isEditing.hobbies = false;
                }, function (err) {
                    $scope.getModelStateErrors(err.ModelState, "hobbies");
                    blockUiService.unblockIt();
                });
            } else {
                var hobby = _.where($scope.hobbies, { HobbyId: hobbyId })[0];

                userService.updateUserHobby(hobby).then(function (response) {
                    if (response.Error == null) {
                        hobby = response;
                        blockUiService.unblockIt();
                    } else {
                        errorService.displayErrorRedirect(response.Error);
                        blockUiService.unblockIt();
                    }

                    hobby.isEditing = false;
                }, function (err) {
                    $scope.getModelStateErrors(err.ModelState, "hobbies");
                    blockUiService.unblockIt();
                });
            }
        };
        
        $scope.editEducation = function (id, educationId) {
            var educationGroup = _.where($scope.education, { id: id })[0];
            var data = _.where(educationGroup.content, { EducationId: educationId })[0];
            data.isEditing = true;
        };

        $scope.saveEducation = function (id, educationId) {
            var educationGroup = _.where($scope.education, { id: id })[0];
            var data = _.where(educationGroup.content, { EducationId: educationId })[0];
            data.isEditing = false;
        };

        $scope.addEducation = function (id) {
            var educationGroup = _.where($scope.education, { id: id })[0];

            if (!educationGroup.isAdding) {
                var newEducation = {
                    isAdding: true,
                    isEditing: true,
                    SchoolName: "",
                    Course: "",
                    YearAttended: "",
                    YearGraduated: "",
                    State: "",
                    City: "",
                    Country: ""
                };
                
                educationGroup.content.push(newEducation);
                educationGroup.isAdding = true;
            }
        };
        
        $scope.cancelAddingEducation = function (id) {
            var educationGroup = _.where($scope.education, { id: id })[0];
            educationGroup.isAdding = false;

            var data = _.where(educationGroup.content, { isAdding: true });

            if (data.length > 0) {
                _.each(data, function(e) {
                    var index = educationGroup.content.indexOf(e);
                    educationGroup.content.splice(index, 1);
                });
            } else {
                
            }
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
                        $scope.address = response.Address;
                        $scope.work = response.Work;

                        _.each(response.Hobbies, function (h) {
                            h.isEditing = false;
                            $scope.hobbies.push(h);
                        });

                        _.each(response.Education, function (e) {
                            e.isEditing = false;
                            e.YearAttendedDisplay = dateHelper.getMonthYear(e.YearAttended);
                            e.YearGraduatedDisplay = dateHelper.getMonthYear(e.YearGraduated);

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

                        delete response.Education;
                        delete response.Address;
                        delete response.Hobbies;

                        $scope.user = response;
                        $scope.userFullName = $scope.user.FirstName + " " + $scope.user.LastName;
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

        $scope.getModelStateErrors = function(error, errorProperty) {
            for (var name in error) {
                var tmp = name.toString().split('.')[1];
                $scope.error[errorProperty][tmp] = error[name][0];
            }
        };

        $scope.hasError = function(errorProperty, errorSubProperty) {
            if ($scope.error[errorProperty][errorSubProperty] == undefined) {
                return "";
            }
            return "has-error";
        };

        $rootScope.$on("userLoggedIn", function () {
            $scope.authData = localStorageService.get("authorizationData");
            $scope.getUserInfo();
        });

        $scope.init();
    }
]);