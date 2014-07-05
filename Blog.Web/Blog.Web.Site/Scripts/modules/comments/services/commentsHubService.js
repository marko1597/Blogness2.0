﻿ngComments.factory("commentsHubService", ["$rootScope", "$interval", "signalrHub", "commentsService", "configProvider", 
    function ($rootScope, $interval, signalrHub, commentsService, configProvider) {
        var hubUrl = configProvider.getSettings().HubUrl == "" ?
            window.blogConfiguration.hubUrl :
            configProvider.getSettings().HubUrl;

        var hub = new signalrHub(hubUrl, "commentsHub", {
            commentLikesUpdate: function (commentId, commentLikes) {
                $rootScope.$broadcast("commentLikesUpdate", { CommentId: commentId, CommentLikes: commentLikes });
            },
            commentAdded: function (postId, comment) {
                if (comment.PostId != null) {
                    comment = commentsService.addViewProperties(comment, false, false);
                } else {
                    comment = commentsService.addViewProperties(comment);
                }
                
                $rootScope.$broadcast("commentAdded", { PostId: postId, Comment: comment });
            }
        }, ["viewPost"]);

        var stop;

        return {
            viewPost: function (postId) {
                stop = $interval(function () {
                    if (hub.connection.state != 0) {
                        hub.viewPost(postId);
                        if (angular.isDefined(stop)) {
                            $interval.cancel(stop);
                            stop = undefined;
                        }
                    }
                }, 200);
            }
        };
    }
]);