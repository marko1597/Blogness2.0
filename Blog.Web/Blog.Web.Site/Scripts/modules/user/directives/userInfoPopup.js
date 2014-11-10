ngUser.directive('userInfoPopup', ["$popover", function ($popover) {
    var ctrlFn = function ($scope, $rootScope, $location, messagingService, dateHelper) {
        $scope.username = null;
                
        $scope.fullName = function () {
            if ($scope.user) {
                return $scope.user.FirstName + ' ' + $scope.user.LastName;
            }
            return "Dont dead open inside";
        };

        $scope.birthdate = function () {
            if ($scope.user) {
                var years = dateHelper.getYearsDifference($scope.user.BirthDate);
                return years + " years old";
            }
            return "I can't tell you that mate!";
        };

        $scope.viewProfile = function () {
            $location.path("/user/" + $scope.user.UserName);
        };

        $scope.goToChat = function () {
            $rootScope.$broadcast("launchChatWindow", $scope.user);
        };
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$location", "messagingService", "dateHelper"];

    var linkFn = function (scope, el, attr) {
        var popover = $popover(el, {
            title: scope.fullName(),
            animation: 'am-flip-x',
            scope: scope,
            template: window.blogConfiguration.templatesModulesUrl + "user/userInfoPopup.html",
            placement: 'bottom'
        });

        scope.hide = function () {
            popover.hide();
        };
    };

    return {
        restrict: 'A',
        scope: {
            user: '='
        },
        controller: ctrlFn,
        link: linkFn
    };
}]);
