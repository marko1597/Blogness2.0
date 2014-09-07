ngUser.directive('userProfileDetailsEducation', [function () {
    var ctrlFn = function ($scope, $rootScope, dateHelper) {
        $scope.$watch("educationGroups", function() {
            _.each($scope.educationGroups, function (g) {
                g.isAdding = false;

                _.each(g.Content, function (e) {
                    e.YearAttendedDisplay = dateHelper.getMonthYear(e.YearAttended);
                    e.YearGraduatedDisplay = dateHelper.getMonthYear(e.YearGraduated);
                });
            });
        }, true);
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "dateHelper"];

    return {
        restrict: 'EA',
        scope: {
            educationGroups: '=',
            user: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "user/userProfileDetailsEducation.html",
        controller: ctrlFn
    };
}]);
