ngMessaging.directive('messagesPanel', ["$templateCache", function ($templateCache) {
    var ctrlFn = function ($scope, $rootScope, $interval, messagingService, dateHelper, errorService, configProvider,
        localStorageService) {

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

        var stop;
        stop = $interval(function () {
            if (configProvider.getSocketClientFunctions().sendChatMessage) {
                $rootScope.$on(configProvider.getSocketClientFunctions().sendChatMessage, function (e, d) {
                    if (d && d.FromUser) {
                        var messageItem = null;
                        var messageItemIndex = -1;

                        for (var i = 0; i < $scope.messagesList.length; i++) {
                            if (d.FromUser.Id === $scope.messagesList[i].User.Id) {
                                messageItem = $scope.messagesList[i];
                                messageItemIndex = i;
                                break;
                            }
                        }

                        if (messageItem && messageItemIndex > -1) {
                            messageItem.LastChatMessage.Text = d.Text;
                            messageItem.LastChatMessage.CreatedDateDisplay = dateHelper.getDateDisplay(d.CreatedDate);
                            $scope.messagesList.splice(messageItemIndex, 1);
                            $scope.messagesList.unshift(messageItem);

                            $(".message-item[data-user-id='" + d.FromUser.Id + "']").effect("highlight", { color: "#B3C833" }, 1500);
                        }
                    }
                });

                $interval.cancel(stop);
                stop = undefined;
            }
        }, 250);

        var getUserChatMessageList = function () {
            if ($scope.authData && $rootScope.user) {
                $scope.user = $rootScope.user;

                messagingService.getUserChatMessageList($scope.user.Id).then(function (response) {
                    if (response) {
                        $scope.messagesList = response;
                    } else {
                        errorService.displayError({ Message: "No messages found! " });
                    }
                }, function () {
                    errorService.displayError({ Message: "Failed getting messages!" });
                });
            }
        };

        $scope.init();
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$interval", "messagingService", "dateHelper", "errorService", "configProvider", "localStorageService"];

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
        template: $templateCache.get("messaging/messagesPanel.html"),
        controller: ctrlFn,
        link: linkFn
    };
}]);
