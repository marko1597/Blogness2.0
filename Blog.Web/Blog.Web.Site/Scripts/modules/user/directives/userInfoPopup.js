ngUser.directive('userInfoPopup', ["$popover", "$window", function ($popover, $window) {
    var ctrlFn = function ($scope, $rootScope, $location, messagingService, dateHelper, snapRemote, localStorageService) {
        $scope.authData = localStorageService.get("authorizationData");

        $scope.showSendMessage = function () {
            if (($scope.authData && $rootScope.user) && ($rootScope.user.UserName !== $scope.user.UserName)) {
                return true;
            }
            return false;
        };
                
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
            $scope.hide();
            snapRemote.open("right");
            $rootScope.$broadcast("launchChatWindow", $scope.user);
        };
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$location", "messagingService", "dateHelper", "snapRemote", "localStorageService"];

    var linkFn = function (scope, el) {
        var popover = $popover(el, {
            title: scope.fullName(),
            animation: 'am-flip-x',
            scope: scope,
            template: $window.blogConfiguration.templatesModulesUrl + "user/userInfoPopup.html",
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
