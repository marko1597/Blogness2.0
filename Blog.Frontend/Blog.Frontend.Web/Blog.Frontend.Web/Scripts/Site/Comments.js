window.Blog.Comments =
{
    getCommentItemCss: function (isCommentReply) {
        var classString = "";
        if (isCommentReply) {
            classString = "commentItem noshow";
        }
        else {
            classString = "commentItem";
        }

        return classString;
    },

    isCommentReply: function (isCommentReply) {
        if (isCommentReply) {
            return true;
        }
        else {
            return false;
        }
    },

    addComment: function (postId, commentId) {
        if (postId != 0 && commentId == 0) {
            $.ajax({
                type: "POST",
                url: window.Blog.Util.baseDomain() + "Comments/AddComment",
                data: { CommentMessage: $("#commentAddNewText_0 textarea").val(), PostId: postId, CommentLocation: window.Blog.Location.getUserLocation(), ParentCommentId: null },
                success: function (response) {
                    if (response !== '') {
                        $("div.comment-list-container").html(response);
                        $('div.comment-list-container ul li').first().effect("highlight", {}, 3000);
                        $("#commentAddNewText_0 textarea").val("");
                        $("div.comments-container div.comments-header span.comments-count").text(window.Blog.Comments.getCommentCount());
                    }
                    else {
                        window.location.href = window.Blog.Util.baseDomain() + "Account/Login";
                    }
                },
                error: function (xhr, ajaxOptions, error) {
                    console.log(error);
                }
            });
        }
        else {
            $.ajax({
                type: "POST",
                url: window.Blog.Util.baseDomain() + "Comments/AddCommentReply",
                data: { CommentMessage: $("#commentAddNewText_" + commentId + " textarea").val(), PostId: null, CommentLocation: window.Blog.Location.getUserLocation(), ParentCommentId: commentId },
                success: function (response) {
                    if (response !== '') {
                        $('#commentListContainer_' + commentId).html(response);
                        $('#commentNewContainer_' + commentId).toggle(100);
                        $('#commentList_' + commentId + ' ul li').first().effect("highlight", {}, 3000);
                        $('#commentList_' + commentId + ' ul li').first().toggle();
                        $('#commentList_' + commentId + ' ul li').toggle();
                        $('#commentNewText_' + commentId).val("");
                        $('#commentRepliesHide_' + commentId).text("Hide Replies");
                    }
                    else {
                        window.location.href = window.Blog.Util.baseDomain() + "Account/Login";
                    }
                },
                error: function (xhr, ajaxOptions, error) {
                    console.log(error);
                }
            });
        }
    },

    likeComment: function (commentId) {
        $.ajax({
            type: "POST",
            url: window.Blog.Util.baseDomain() + "Comments/LikeComment",
            data: { "commentId": commentId },
            success: function (response) {
                if (response !== '') {
                    $("#commentlikes-container_" + commentId).html(response);
                }
                else {
                    window.location.href = window.Blog.Util.baseDomain() + "Account/Login";
                }
            },
            error: function (xhr, ajaxOptions, error) {
                console.log(error);
            }
        });
    },

    toggleReplyToComment: function (commentId) {
        $("div.comment-item-container #commentAddNew_" + commentId).toggle(100);
    },

    toggleReplies: function (commentId) {
        if ($('.commentItem[data-parentcommentId="' + commentId + '"]').first().is(':hidden')) {
            $('.commentItem[data-parentcommentId="' + commentId + '"]').slideDown(100);
            $('#commentList_' + commentId).css({ "padding": "0" });
            $('#commentRepliesHide_' + commentId).text('Hide replies');
        }
        else {
            $('.commentItem[data-parentcommentId="' + commentId + '"]').slideUp(100);
            $('#commentRepliesHide_' + commentId).text('Show replies');
        }
    },

    toggleCommentsSection: function () {
        if ($('div.comments-container div.comments-list').is(':hidden')) {
            $('div.comments-container div.comments-list').slideDown(400);
            $('div.comments-container div.comments-header img').attr('src', window.Blog.Util.baseDomain() + 'Content/images/toggle_down.png');
        }
        else {
            $('div.comments-container div.comments-list').slideUp(400);
            $('div.comments-container div.comments-header img').attr('src', window.Blog.Util.baseDomain() + 'Content/images/toggle_right.png');
        }
    },

    toggleAddNewCommentDisplay: function (id) {
        if ($('div.comments-container div.comments-list').is(':hidden')) {
            $('div.comments-container div.comments-list').slideDown(400);
            $('div.comments-container div.comments-header img').attr('src', window.Blog.Util.baseDomain() + 'Content/images/toggle_down.png');
        }
        $("div.comments-container div.comments-list #commentAddNew_" + id).slideDown(100);
    },

    closeAddNewCommentDisplay: function (id) {
        $("div.comments-container div.comments-list #commentAddNew_" + id).slideUp(100);
    },

    getCommentCount: function () {
        var currCount = $("div.comments-container div.comments-header span.comments-count").text().split(' ');
        return (parseInt(currCount[0]) + 1) + " comments";
    }
}