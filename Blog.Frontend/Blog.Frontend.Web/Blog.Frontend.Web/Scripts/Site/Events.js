window.Blog.Events =
{
    init: function () {
        var headerTop = $('#siteheader').offset().top;

        $(window).on("scroll", window, function () {
            if ($(window).scrollTop() > headerTop) {
                $('#siteheader').css({ 'top': '0px', 'left': '0px', 'right': '0px', 'z-index': '99999' });
            }
        });

        $(document).tooltip({
            position: {
                my: "center bottom-20",
                at: "center top",
                using: function (position, feedback) {
                    $(this).css(position);
                    $("<div>")
                      .addClass("arrow")
                      .addClass(feedback.vertical)
                      .addClass(feedback.horizontal)
                      .appendTo(this);
                }
            }
        });
    },

    initCommentItem: function (cId, pId, y, m, d, h, mm, s, ms) {
        if ($("#commentRepliesHide_" + cId).parent().parent().siblings("#commentListContainer_" + cId + " div.comment-list-container ul").children().length == 0) {
            $("#commentRepliesHide_" + cId).attr("title", "No replies available");
            $("#commentRepliesHide_" + cId).tooltip({
                show: {
                    effect: "slideDown",
                    delay: 250
                }
            });
        }

        $("div.comment-item-container div.comment-item div.comment-bottom-row").on("click", "#commentRepliesHide_" + cId, function () {
            window.Blog.Comments.toggleReplies(cId);
        });

        $("div.comment-item-container div.comment-item div.comment-bottom-row").on("click", "#commentReply_" + cId, function () {
            window.Blog.Comments.toggleReplyToComment(cId);
        });

        $("div.comment-item-container div.comment-item div.comment-bottom-row").on("click", "#commentlikes-container_" + cId, function () {
            window.Blog.Comments.likeComment(cId);
        });

        var timespan = window.Blog.DateTime.generateDateTimeDifference(y, m, d, h, mm, s, ms);
        $("#commentTimespan_" + cId).html(timespan);
    },

    initCommentNewItem: function (cId, pId) {
        if (cId != 0) {
            $("#commentAddNew_" + cId).css({ "margin-left": "50px", "border-bottom": "1px solid #ccc" });
        }

        $("div.comments-container div.comments-list div.comment-add-new-container").on("click", "img.comment-add-new-close", function () {
            window.Blog.Comments.closeAddNewCommentDisplay(cId);
        });

        $("div.comments-container div.comments-list div.comment-add-new-container").on("click", "#addCommentButton_" + cId, function () {
            window.Blog.Comments.addComment(pId, cId);
        });
    },

    initCommentSection: function () {
        $("div.comments-container div.comments-header").on("click", "img", function () {
            window.Blog.Comments.toggleCommentsSection();
        });

        $("div.comments-container div.comments-header").on("click", "span", function () {
            window.Blog.Comments.toggleCommentsSection();
        });

        $("div.comments-container div.comments-header").on("click", "a.comment-add-new-item", function () {
            window.Blog.Comments.toggleAddNewCommentDisplay(0);
        });
    },

    initErrorPage: function () {
        $("section.error div.reporterror").on("click", "a", function () {
            $.blockUI({
                message: $("div.report-error-dialog"),
                fadeIn: 700,
                fadeOut: 700,
                showOverlay: true,
                centerX: true,
                centerY: true,
                css: {
                    width: '800px',
                    height: '470px',
                    'text-align': 'left',
                    border: 'none',
                    background: 'none',
                    //left: '20%',
                    top: '20%',
                    cursor: 'default',
                    margin: "0 auto",
                }
            });
        });

        $("#body section.content-body").css({ "max-width": "100%" });
    },

    initLogin: function () {
        $(window).on("resize", window, function () {
            window.Blog.Util.resizeBody();
        });

        $("body #body section.content-body").css({ "background": "url('" + window.Blog.Util.baseDomain() + "Content/images/login_bg.png')", "backround-repeat": "none" });
    },

    initModifyPost: function (id, a) {
        if ($("#PostTitle").val() === "") {
            $("#PostTitle").val("Enter your title here..");
        }

        $("div.post-edit-container").on("focus", "#PostTitle", function (ev) {
            ev.preventDefault();
            if ($("#PostTitle").val() == "Enter your title here..") {
                $("#PostTitle").val("");
            }
        });

        $("div.post-edit-container").on("focusout", "#PostTitle", function (ev) {
            ev.preventDefault();
            if ($("#PostTitle").val() == "") {
                $("#PostTitle").val("Enter your title here..");
            }
        });

        $("div.post-modify-container div.post-modify-date-buttons").on("click", "#savePost", function () {
            window.Blog.Posts.modifyPost(id);
        });

        $("div.post-modify-container div.post-modify-date-buttons").on("click", "#cancelModifyPost", function () {
            if (a) {
                window.Blog.Posts.deleteAddedPost(id);
            }
            else {
                location.href = window.Blog.Util.baseDomain() + 'PostsPage/PopularPosts';
            }
        });

        CKEDITOR.replace('Post_PostMessage', {
            extraPlugins: 'autogrow',
            //autoGrow_maxHeight: 800,
            removePlugins: 'resize'
        });
    },

    initPost: function (id) {
        $("div.post-container div.post-content-container div.post-content div.post-content-text > *").each(function () {
            var that = this;
            var contentWidth = $("div.post-container div.post-content-container div.post-content").width();
            if ($(that).width() > contentWidth) {
                $(that).width(contentWidth);

                if ($(that).children().length > 0) {
                    $(that).children().each(function () {
                        if ($(this).width() > $(this).parent().width()) {
                            window.Blog.Util.resizeElementByParent($(this));
                        }
                    });
                }
            }
        });

        window.Blog.Posts.addDialogToRemovePost(id);
        window.Blog.Posts.renderPostMessage();
        $(function () {
            $(".postImageContainer").attr("style", "margin-left: 0px !important");
        });
    },

    initPostDetails: function (id) {
        $("div.post-details-social-container").on("click", "#postDetailsLikes_" + id, function () {
            window.Blog.PostDetails.likePost(id);
        });
    },

    initPostMedia: function (id, pId) {
        $("div.post-media-container").on("click", "#removeContent_" + id, function () {
            window.Blog.PostContent.launchRemovePostContentDialog(id);
        });

        $("div.post-media-container").on("mouseover", "#removeContent_" + id, function () {
            window.Blog.PostContent.removePostContentIconHoverIn(id);
        });

        $("div.post-media-container").on("mouseout", "#removeContent_" + id, function () {
            window.Blog.PostContent.removePostContentIconHoverOut(id);
        });

        window.Blog.PostContent.addDialogToRemovePostContent(id, pId);
    },

    initPostMediaUpload: function () {
        $('#PostContentUploadFile').customFileInput();

        $("div.content-upload-container").on("focus", "#uploadContentsButton", function () {
            window.Blog.PostContent.postContentUploading();
        });
    },

    initPostModification: function (id) {
        $("div.post-edit-buttons-container").on("click", "#btnModifyPost_" + id, function () {
            location.href = window.Blog.Util.baseDomain() + "Posts/ModifyPost/" + id;
        });

        $("div.post-edit-buttons-container").on("click", "#btnDeletePost_" + id, function () {
            window.Blog.Posts.launchRemovePostDialog(id);
        });
    },

    initReportError: function () {
        $("div.report-error-dialog").on("click", "span.report-error-close", function () {
            $(".blockUI").fadeOut(500);
        });

        $("div.report-error-dialog div.feedback div.feedback-submit").on("click", "#submit-report-btn", function () {
            var name = $("div.report-error-dialog div.feedback div.feedback-name input[type='text']").val();
            var email = $("div.report-error-dialog div.feedback div.feedback-email input[type='text']").val();
            var message = $("div.report-error-dialog div.feedback div.feedback-message textarea").val();

            alert("These are the values you submitted:\r\n" + "Name: " + name + "\r\nEmail: " + email + "\r\nMessage: " + message);
            $(".blockUI").fadeOut(500);

            $("div.report-error-dialog div.feedback div.feedback-name input[type='text']").val("");
            $("div.report-error-dialog div.feedback div.feedback-email input[type='text']").val("");
            $("div.report-error-dialog div.feedback div.feedback-message textarea").val("");
        });
    }
}