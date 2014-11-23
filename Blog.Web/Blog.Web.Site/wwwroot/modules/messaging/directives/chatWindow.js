ngMessaging.directive('chatWindow', ["$timeout", function ($timeout) {
    var ctrlFn = function ($scope, $rootScope, dateHelper, messagingService, errorService, configProvider, localStorageService) {
        $scope.user = null;

        $scope.recipient = null;

        $scope.authData = localStorageService.get("authorizationData");

        $scope.isBusy = false;

        $scope.chatMessages = [];

        $scope.isActive = false;

        $scope.newMessage = "";

        $scope.hasMoreMessages = true;

        $scope.showViewMoreMessagesButton = function () {
            return $scope.hasMoreMessages;
        };

        $scope.recipientName = function () {
            return $scope.recipient ? $scope.recipient.FirstName + ' ' + $scope.recipient.LastName : '';
        };

        $scope.hideChatWindow = function () {
            $scope.isActive = false;
        };

        $scope.chatWindowVisibility = function () {
            return $scope.isActive;
        };

        $scope.isLoggedIn = function () {
            if ($scope.authData && $scope.user) {
                return true;
            }
            return false;
        };

        $scope.isFromRecipient = function (chatMessage) {
            if (chatMessage.FromUser.UserName == $scope.user.UserName) {
                return "";
            } else {
                return "recipient-message";
            }
        };

        $scope.$on("launchChatWindow", function (ev, userData) {
            if (!$scope.user || !$rootScope.user || !$scope.authData) return;

            $scope.chatMessages = [];

            $scope.isActive = true;

            $scope.recipient = userData;

            setUserInSession();

            messagingService.getChatMessages($scope.user.Id, userData.Id).then(function (response) {
                if (response) {
                    _.each(response, function (r) {
                        $scope.chatMessages.unshift(r);
                    });

                    if ($scope.chatMessages.length === 25) {
                        $scope.hasMoreMessages = true;
                    }
                } else {
                    errorService.displayError({ Message: "No messages found! " });
                }
            }, function () {
                errorService.displayError({ Message: "Failed getting messages!" });
            });
        });

        $scope.getMoreChatMessages = function () {
            if ($scope.isBusy) {
                return;
            }
            $scope.isBusy = true;

            messagingService.getMoreChatMessages($scope.user.Id, $scope.recipient.Id, $scope.chatMessages.length).then(function (response) {
                $scope.isBusy = false;

                if (response) {
                    $scope.hasMoreMessages = response.length === 10;

                    _.each(response, function (r) {
                        $scope.chatMessages.unshift(r);
                    });
                } else {
                    errorService.displayError({ Message: "No messages found! " });
                }
            }, function () {
                $scope.isBusy = false;
                errorService.displayError({ Message: "Failed getting messages!" });
            });
        };

        $rootScope.$on(configProvider.getSocketClientFunctions().sendChatMessage, function (e, d) {
            if (d && d.FromUser && $scope.recipient && d.FromUser.Id === $scope.recipient.Id) {
                d.CreatedDateDisplay = dateHelper.getDateDisplay(d.CreatedDate);
                $scope.chatMessages.push(d);
            }
        });

        $scope.sendChatMessage = function () {
            var chatMessage = {
                FromUser: $scope.user,
                ToUser: $scope.recipient,
                Text: $scope.newMessage
            };

            messagingService.addChatMessage(chatMessage).then(function (response) {
                if (response) {
                    response.CreatedDateDisplay = dateHelper.getDateDisplay(response.CreatedDate);
                    $scope.chatMessages.push(response);
                    $scope.newMessage = "";
                } else {
                    errorService.displayError({ Message: "Failed to send message!" });
                }
            }, function () {
                errorService.displayError({ Message: "Failed to send message!" });
            });
        };

        $rootScope.$watch('user', function () {
            setUserInSession();
        });

        $rootScope.$on("userLoggedIn", function () {
            $scope.authData = localStorageService.get("authorizationData");
        });

        $scope.init();

        var setUserInSession = function () {
            if ($rootScope.user) {
                $scope.user = $rootScope.user;
                $scope.user.FullName = $scope.user.FirstName + " " + $scope.user.LastName;
            }
        };
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "dateHelper", "messagingService", "errorService", "configProvider", "localStorageService"];

    var linkFn = function (scope, elem) {
        $timeout(function () {
            scope.elemHeight = ($(document).height()) + 'px';

            scope.bodyHeight = function () {
                var headerHeight = $(elem).find('.header').height();
                return ($(document).height() - (50 * 2) - headerHeight) + 'px';
            };
        }, 1000);
    };

    return {
        restrict: 'EA',
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "messaging/chatwindow.html",
        controller: ctrlFn,
        link: linkFn
    };
}]);
