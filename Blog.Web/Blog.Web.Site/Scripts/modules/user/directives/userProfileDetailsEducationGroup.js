ngUser.directive('userProfileDetailsEducationGroup', [function () {
    var ctrlFn = function ($scope, localStorageService) {
        $scope.username = localStorageService.get("username");
        $scope.isAdding = false;
        $scope.newEducation = {
            City: "",
            Country: "",
            Course: "",
            EducationType: {
                EducationTypeId: $scope.educationGroup.EducationType,
                EducationTypeName: $scope.educationGroup.Title
            },
            SchoolName: "",
            State: "",
            UserId: $scope.user.Id,
            YearAttended: "",
            YearAttendedDisplay: "",
            YearGraduated: "",
            YearGraduatedDisplay: ""
        };

        $scope.emptyRecordMessage = "This person has no " + $scope.educationGroup.Title + " education..pathetic right?";

        $scope.addEducation = function() {
            $scope.isAdding = true;
        };

        $scope.showNoRecordsMessage = function() {
            if ($scope.educationGroup.Content.length > 0 || $scope.isAdding) {
                return false;
            }
            return true;
        };

        $scope.showButtons = function () {
            if ($scope.username == undefined || $scope.username == null || $scope.username == "") {
                return "hidden";
            } else {
                if ($scope.user == undefined) {
                    return "hidden";
                } else {
                    if ($scope.username !== $scope.user.UserName) {
                        return "hidden";
                    }
                    return "";
                }
            }
        };

        $scope.$on("cancelAddingUserEducation", function () {
            $scope.newEducation = {
                City: "",
                Country: "",
                Course: "",
                EducationType: {
                    EducationTypeId: $scope.educationGroup.type,
                    EducationTypeName: $scope.educationGroup.title
                },
                SchoolName: "",
                State: "",
                UserId: $scope.user.Id,
                YearAttended: "",
                YearAttendedDisplay: "",
                YearGraduated: "",
                YearGraduatedDisplay: ""
            };
            $scope.isAdding = false;
        });

        $scope.$on("successAddingUserEducation", function (ev, data) {
            $scope.educationGroup.Content.push(data);
        });

        $scope.$on("successDeletingUserEducation", function(ev, data) {
            var educationIndex = $scope.educationGroup.Content.indexOf(data);
            $scope.educationGroup.Content.splice(educationIndex, 1);
        });
    };
    ctrlFn.$inject = ["$scope", "localStorageService"];

    return {
        restrict: 'EA',
        scope: {
            educationGroup: '=',
            user: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "user/userProfileDetailsEducationGroup.html",
        controller: ctrlFn
    };
}]);
