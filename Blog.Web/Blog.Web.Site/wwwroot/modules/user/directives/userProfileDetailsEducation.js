﻿ngUser.directive('userProfileDetailsEducation', ["$templateCache", function ($templateCache) {
    var ctrlFn = function ($scope, $rootScope, dateHelper) {
        $scope.$on("viewedUserLoaded", function (ev, data) {
            $scope.educationGroups = data.EducationGroups;
            $scope.user = data;

            _.each($scope.educationGroups, function (g) {
                g.isAdding = false;

                _.each(g.Content, function (e) {
                    e.YearAttendedDisplay = dateHelper.getMonthYear(e.YearAttended);
                    e.YearGraduatedDisplay = dateHelper.getMonthYear(e.YearGraduated);
                });
            });
        });
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "dateHelper"];

    return {
        restrict: 'EA',
        replace: true,
        template: $templateCache.get("user/userProfileDetailsEducation.html"),
        controller: ctrlFn
    };
}]);
