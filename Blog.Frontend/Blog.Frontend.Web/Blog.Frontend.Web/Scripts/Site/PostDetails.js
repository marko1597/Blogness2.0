window.Blog.PostDetails =
{
    likePost: function (postId) {
        $.ajax({
            type: "POST",
            url: Blog.Util.baseDomain() + "Posts/LikePost",
            data: { "postId": postId },
            success: function (response) {
                if (response !== '') {
                    $("#postDetailsLikes_" + postId).html(response);
                }
                else {
                    window.location.href = Blog.Util.baseDomain() + "Account/Login";
                }
            },
            error: function (xhr, ajaxOptions, error) {
                console.log(error);
            }
        });
    }
}