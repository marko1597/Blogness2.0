ngUser.directive('userProfileDetailsEducationGroup', [function () {
    var ctrlFn = function ($scope) {
        $scope.isAdding = false;
        $scope.newEducation = {
            City: "",
            Country: "",
            Course: "",
            EducationType: {
                EducationTypeId: $scope.educationGroup.EducationType,
                EducationTypeName: $scope.educationGroup.EducationTitle
            },
            SchoolName: "",
            State: "",
            UserId: $scope.user.Id,
            YearAttended: "",
            YearAttendedDisplay: "",
            YearGraduated: "",
            YearGraduatedDisplay: ""
        };

        $scope.emptyRecordMessage = "It's alright, we know school is expensive..right?";

        $scope.addEducation = function() {
            $scope.isAdding = true;
        };

        $scope.showNoRecordsMessage = function() {
            if ($scope.educationGroup.Content.length > 0 || $scope.isAdding) {
                return false;
            }
            return true;
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
    ctrlFn.$inject = ["$scope"];

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
