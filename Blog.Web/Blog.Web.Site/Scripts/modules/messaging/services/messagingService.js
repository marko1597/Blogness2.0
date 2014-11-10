ngMessaging.factory('messagingService', ["$http", "$q", "configProvider", "dateHelper", "blogSocketsService",
    function ($http, $q, configProvider, dateHelper, blogSocketsService) {
        var baseUrl = configProvider.getSettings().BlogApi == "" ?
            window.blogConfiguration.blogApi :
            configProvider.getSettings().BlogApi;

        return {
            getUserChatMessageList: function(userId) {
                var deferred = $q.defer();

                $http({
                    url: baseUrl + "user/" + userId + "/chats",
                    method: "GET"
                }).success(function (response) {
                    var userChatMessages = response.ChatMessageListItems;

                    _.each(userChatMessages, function (a) {
                        a.User.NameDisplay = a.User.FirstName + ' ' + a.User.LastName;
                        a.LastChatMessage.CreatedDateDisplay = dateHelper.getDateDisplay(a.LastChatMessage.CreatedDate);
                    });
                    deferred.resolve(userChatMessages);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            getChatMessages: function (fromUserId, toUserId) {
                var deferred = $q.defer();

                $http({
                    url: baseUrl + "chat/" + fromUserId + "/" + toUserId,
                    method: "GET"
                }).success(function (response) {
                    _.each(response, function (a) {
                        a.CreatedDateDisplay = dateHelper.getDateDisplay(a.CreatedDate);
                    });
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            getMoreChatMessages: function (fromUserId, toUserId, skip) {
                var deferred = $q.defer();

                $http({
                    url: baseUrl + "chat/" + fromUserId + "/" + toUserId + "/more/" + skip,
                    method: "GET"
                }).success(function (response) {
                    _.each(response, function (a) {
                        a.CreatedDateDisplay = dateHelper.getDateDisplay(a.CreatedDate);
                    });
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

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
            },

            userChatOnline: function (id) {
                blogSocketsService.emit(configProvider.getSocketClientFunctions().userChatOffline, { userId: id });
                blogSocketsService.emit(configProvider.getSocketClientFunctions().userChatOnline, { userId: id });
            },

            userChatOffline: function (id) {
                blogSocketsService.emit(configProvider.getSocketClientFunctions().userChatOffline, { userId: id });
            }
        };
    }
]);