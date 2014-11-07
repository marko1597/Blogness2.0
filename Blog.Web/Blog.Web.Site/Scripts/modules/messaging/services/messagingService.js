ngMessaging.factory('messagingService', ["$http", "$q", "configProvider", "dateHelper",
    function ($http, $q, configProvider, dateHelper) {
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