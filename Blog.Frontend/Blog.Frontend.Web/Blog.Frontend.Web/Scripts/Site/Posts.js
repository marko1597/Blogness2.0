window.Blog.Posts =
{
    renderPostMessage: function () {
        var parentWidth = $("div.post-container div.post-content-container div.post-content div.post-content-text").width();
        $("div.post-container div.post-content-container div.post-content div.post-content-text img").each(function () {
            var elWidth = $(this).css("width").replace("px", "");
            if (elWidth >= parentWidth) {
                $(this).css({"height":"auto", "width":"100%"}); 
            }
        });
    },

    addPost: function () {
        var textData = CKEDITOR.instances.PostMessage.getData();
        $("#PostMessage").val(textData);

        $.ajax({
            type: "POST",
            url: Blog.Util.baseDomain() + "Posts/SavePost",
            data: { PostId: null, PostTitle: $("#PostTitle").val(), PostMessage: Blog.Util.convertSpecialChars($("#PostMessage").val()) },
            success: function() {
                window.location.href = Blog.Util.baseDomain() + "PostsPage/PopularPosts";
            },
            error: function(xhr, ajaxOptions, error) {
                console.log(error);
            }
        });
    },

    modifyPost: function (id) {
        var postTitle = $("#PostTitle").val();

        if (postTitle === "" || typeof postTitle === "undefined") {
            $("#missingFieldsMessage").dialog({
                modal: true,
                buttons: {
                    Ok: function () {
                        $(this).dialog("close");
                    }
                }
            });
        }

        var textData = CKEDITOR.instances.Post_PostMessage.getData();

        $.ajax({
            type: "POST",
            url: Blog.Util.baseDomain() + "Posts/SavePost",
            data: { PostId: id, PostTitle: $("#PostTitle").val(), PostMessage: Blog.Util.convertSpecialChars(textData) },
            success: function () {
                window.location.href = Blog.Util.baseDomain() + "PostsPage/PopularPosts";
            },
            error: function (xhr, ajaxOptions, error) {
                console.log(error);
            }
        });
    },

    launchRemovePostDialog: function (id) {
        $("#deletePostModal_" + id).dialog("open");
    },

    deleteAddedPost: function (id) {
        $.ajax({
            url: Blog.Util.baseDomain() + "Posts/DeletePost",
            type: "POST",
            dataType: "text",
            data: { "postId": id },
            success: function () {
                window.location.href = Blog.Util.baseDomain() + "PostsPage/PopularPosts";
            },
            error: function (xhr, ajaxOptions, error) {
                console.log(error);
            }
        });
    },

    addDialogToRemovePost: function (postId) {
        $("#deletePostModal_" + postId).dialog({
            autoOpen: false,
            width: 350,
            modal: true,
            buttons: {
                "Do you want to delete this post?": function () {
                    $.ajax({
                        url: Blog.Util.baseDomain() + "Posts/DeletePost",
                        type: "POST",
                        dataType: "text",
                        data: { "postId": postId },
                        success: function () {
                            window.location.href = Blog.Util.baseDomain() + "PostsPage/PopularPosts";
                        },
                        error: function (xhr, ajaxOptions, error) {
                            console.log(error);
                        }
                    });
                    $(this).dialog("close");
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });
    }
}