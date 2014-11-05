ngMessaging.directive('messagesPanel', function () {
    var ctrlFn = function ($scope, $rootScope, messagingService, dateHelper, errorService, localStorageService) {
        $scope.user = null;

        $scope.authData = localStorageService.get("authorizationData");

        $scope.messagesList = [];

        $scope.isLoggedIn = function () {
            if ($scope.authData && $scope.user) {
                return true;
            }
            return false;
        };

        $scope.launchChatWindow = function (messageItem) {
            $rootScope.$broadcast("launchChatWindow", messageItem.User);
        };

        $scope.init = function () {
            getUserChatMessageList();
        };

        $rootScope.$watch('user', function () {
            if ($rootScope.user) {
                $scope.user = $rootScope.user;
                getUserChatMessageList();
            }
        });

        $rootScope.$on("userLoggedIn", function () {
            $scope.authData = localStorageService.get("authorizationData");
        });

        $scope.init();

        var getUserChatMessageList = function() {
            if ($scope.authData && $rootScope.user) {
                $scope.user = $rootScope.user;

                messagingService.getUserChatMessageList($scope.user.Id).then(function(response) {
                    if (response) {
                        $scope.messagesList = messagesList;
                    } else {
                        errorService.displayError({ Message: "No messages found! " });
                    }
                }, function() {
                    errorService.displayError({ Message: "Failed getting messages!" });
                });
            }
        };
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "messagingService", "dateHelper", "errorService", "localStorageService"];

    var linkFn = function (scope, elem) {
        scope.elemHeight = ($(document).height()) + 'px';

        scope.bodyHeight = function () {
            var headerHeight = $(elem).find('.header').height();
            return ($(document).height() - 50 - headerHeight) + 'px';
        };
    };

    return {
        restrict: 'EA',
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "messaging/messagesPanel.html",
        controller: ctrlFn,
        link: linkFn
    };
});
