﻿ngMessaging.directive('messagesPanel', function () {
    var ctrlFn = function ($scope, $rootScope, dateHelper, localStorageService) {
        $scope.user = null;

        $scope.authData = localStorageService.get("authorizationData");

        $scope.messagesList = [];

        $scope.isLoggedIn = function () {
            if ($scope.authData && $scope.user) {
                return true;
            }
            return false;
        };

        $scope.init = function () {
            // TODO: dummy message list data
            var messagesList = [];
            for (var i = 0; i < 10; i++) {
                var messageItem = {
                    User: {
                        UserName: 'test-user_' + i,
                        FirstName: 'FirstName_' + i,
                        LastName: 'LastName_' + i,
                        Picture: {
                            MediaUrl: "https://localhost:4414/api/media/defaultprofilepicture"
                        }
                    },
                    LastMessage: {
                        Text: 'Lorem ipsum dolor',
                        DateDisplay: dateHelper.getDateDisplay("2014-01-01T00:00:00Z")
                    }
                };
                messagesList.push(messageItem);
            }

            $scope.messagesList = messagesList;
        };

        $rootScope.$watch('user', function () {
            if ($rootScope.user) {
                $scope.user = $rootScope.user;
                $scope.user.FullName = $scope.user.FirstName + " " + $scope.user.LastName;
            }
        });

        $rootScope.$on("userLoggedIn", function () {
            $scope.authData = localStorageService.get("authorizationData");
        });

        $scope.init();
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "dateHelper", "localStorageService"];

    return {
        restrict: 'EA',
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "messaging/messagesPanel.html",
        controller: ctrlFn
    };
});
