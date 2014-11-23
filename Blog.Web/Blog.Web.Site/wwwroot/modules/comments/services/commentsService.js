ngComments.factory('commentsService', ["$http", "$q", "configProvider", "dateHelper",
    function ($http, $q, configProvider, dateHelper) {
        var commentsApi = configProvider.getSettings().BlogApi == "" ?
            window.blogConfiguration.blogApi :
            configProvider.getSettings().BlogApi;

        return {
            getCommentsByPost: function (id) {
                var deferred = $q.defer();
                var that = this;

                $http({
                    url: commentsApi + "Posts/" + id + "/Comments",
                    method: "GET"
                }).success(function (response) {
                    _.each(response, function (c) {
                        c = that.addViewProperties(c, false, false);

                        _.each(c.Comments, function (r) {
                            that.addViewProperties(r);
                        });
                    });
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            getCommentsByUser: function (userId) {
                var userCommentsUrl = configProvider.getSettings().BlogApi == "" ?
                    window.blogConfiguration.blogApi + "user/" :
                    configProvider.getSettings().BlogApi + "user/";

                var deferred = $q.defer();
                var that = this;

                $http({
                    url: userCommentsUrl + userId + "/comments",
                    method: "GET"
                }).success(function (response) {
                    _.each(response, function (c) {
                        c = that.addViewProperties(c, false, false);

                        _.each(c.Comments, function (r) {
                            that.addViewProperties(r);
                        });
                    });
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            likeComment: function (commentId, username) {
                var deferred = $q.defer();

                $http({
                    url: commentsApi + "comments/likes?username=" + username + "&commentId=" + commentId,
                    method: "POST"
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            addComment: function (comment) {
                var deferred = $q.defer();

                $http({
                    url: commentsApi + "comments",
                    method: "POST",
                    data: comment
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            addViewProperties: function(comment, showReplies, showAddReply) {
                comment.DateDisplay = dateHelper.getDateDisplay(comment.CreatedDate);
                comment.NameDisplay = comment.User != null ? comment.User.FirstName + " " + comment.User.LastName : "";
                comment.Url = "/#/user/" + (comment.User != null ? comment.User.UserName : "");

                if (showReplies != undefined) {
                    comment.ShowReplies = false;
                }

                if (showAddReply != undefined) {
                    comment.ShowAddReply = false;
                }

                return comment;
            }
        };
    }
]);