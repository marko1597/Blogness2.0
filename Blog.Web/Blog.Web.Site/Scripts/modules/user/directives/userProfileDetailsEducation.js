ngUser.directive('userProfileDetailsEducation', [function () {
    var ctrlFn = function ($scope, $rootScope, dateHelper) {
        $scope.educationGroups = [];

        $scope.initializeModel = function (education) {
            $scope.educationGroups = education;

            _.each($scope.educationGroups, function (g) {
                g.isAdding = false;

                _.each(g.Content, function (e) {
                    e.YearAttendedDisplay = dateHelper.getMonthYear(e.YearAttended);
                    e.YearGraduatedDisplay = dateHelper.getMonthYear(e.YearGraduated);
                });
            });
        };

        $rootScope.$on("viewedUserLoaded", function (ev, data) {
            $scope.initializeModel(data.EducationGroups);
        });
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "dateHelper"];

    return {
        restrict: 'EA',
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "user/userProfileDetailsEducation.html",
        controller: ctrlFn
    };
}]);
