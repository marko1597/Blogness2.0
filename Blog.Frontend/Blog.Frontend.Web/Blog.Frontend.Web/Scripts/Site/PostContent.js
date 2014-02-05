window.Blog.PostContent =
{
    postContentUploading: function () {
        $.blockUI({
            message: $("#blockMessage"),
            fadeIn: 1000,
            timeout: 999999,
            onBlock: function () {
                $(this).dialog("close");
            }
        });
    },

    removePostContentIconHoverIn: function (id) {
        $("#removeContent_" + id).find("img").attr("src", window.Blog.Util.baseDomain() + "Content/images/close_highlighted.png");
        $("#removeContent_" + id).find("span").attr("style", "color: #1289ce");
    },

    removePostContentIconHoverOut: function (id) {
        $("#removeContent_" + id).find("img").attr("src", window.Blog.Util.baseDomain() + "Content/images/close.png");
        $("#removeContent_" + id).find("span").attr("style", "color: #fff");
    },

    launchRemovePostContentDialog: function (id) {
        $("#removeContentForm_" + id).dialog("open");
    },

    addDialogToRemovePostContent: function (postContentId, postId) {
        $("#removeContentForm_" + postContentId).dialog({
            autoOpen: false,
            width: 350,
            modal: true,
            buttons: {
                "Delete this content?": function () {
                    var that = this;
                    $.blockUI({
                        message: $("#blockMessage"),
                        fadeIn: 1000,
                        timeout: 2000,
                        onBlock: function () {
                            $.ajax({
                                url: window.Blog.Util.baseDomain() + 'PostContent/DeletePostContent',
                                type: "POST",
                                dataType: "text",
                                data: { "PostId": postId, "PostContentId": postContentId },
                                success: function (response) {
                                    $("div.post-edit-container div.post-modify-container").html(response);
                                },
                                error: function (xhr, ajaxOptions, error) {
                                    console.log(error);
                                }
                            });
                            $(that).dialog("close");
                        }
                    });
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });
    }
}