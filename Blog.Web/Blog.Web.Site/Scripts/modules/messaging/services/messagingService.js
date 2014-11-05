ngMessaging.factory('messagingService', ["$http", "$q", "configProvider", "dateHelper",
    function ($http, $q, configProvider, dateHelper) {
        var baseUrl = configProvider.getSettings().BlogApi == "" ?
            window.blogConfiguration.blogApi :
            configProvider.getSettings().BlogApi;

        return {
            getChatMessages: function (fromUserId, toUserId) {
                var deferred = $q.defer();

                var chatMessages = [];
                for (var i = 0; i < 20; i++) {
                    var messageItem = {};

                    if (i % 2 == 0) {
                        messageItem = {
                            FromUser: {
                                Id: 2,
                                UserName: 'avelness'
                            },
                            ToUser: {
                                Id: 1,
                                UserName: 'jamaness'
                            },
                            Text: 'Lorem ipsum dolor ' + i,
                            CreatedDateDisplay: dateHelper.getDateDisplay("2014-01-01T00:00:00Z")
                        };
                    } else {
                        messageItem = {
                            FromUser: {
                                Id: 1,
                                UserName: 'jamaness'
                            },
                            ToUser: {
                                Id: 2,
                                UserName: 'avelness'
                            },
                            Text: 'Lorem ipsum dolor ' + i,
                            CreatedDateDisplay: dateHelper.getDateDisplay("2014-01-01T00:00:00Z")
                        };
                    }
                    chatMessages.push(messageItem);
                }

                deferred.resolve(chatMessages);

                //$http({
                //    url: baseUrl + "chat/" + fromUserId + "/" + toUserId,
                //    method: "GET"
                //}).success(function (response) {
                //    _.each(response, function (a) {
                //        a.CreatedDateDisplay = dateHelper.getDateDisplay(a.CreatedDate);
                //    });
                //    deferred.resolve(response);
                //}).error(function (e) {
                //    deferred.reject(e);
                //});

                return deferred.promise;
            },

            addChatMessage: function (chatMessage) {
                var deferred = $q.defer();

                $http({
                    url: baseUrl + "chat",
                    method: "POST",
                    data: chatMessage
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            }
        };
    }
]);