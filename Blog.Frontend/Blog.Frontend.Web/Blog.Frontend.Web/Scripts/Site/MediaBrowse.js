Blog = {};

if (!window.Blog.MediaBrowse)
    window.Blog.MediaBrowse = {};

window.Blog.MediaBrowse =
{
    init: function () {
        $("#media-browse-container").on("click", "h3", function () {
            var group = $(this).siblings("div.media-group-items");
            if (group.is(":hidden")) {
                group.slideDown(500);
                group.css({
                    "-webkit-border-bottom-right-radius": "6px",
                    "-webkit-border-bottom-left-radius": "6px",
                    "-moz-border-radius-bottomright": "6px",
                    "-moz-border-radius-bottomleft": "6px",
                    "border-bottom-right-radius": "6px",
                    "border-bottom-left-radius": "6px",
                    "display": "inline-block"
                });

                $(this).css({
                    "-webkit-border-bottom-right-radius": "0px",
                    "-webkit-border-bottom-left-radius": "0px",
                    "-moz-border-radius-bottomright": "0px",
                    "-moz-border-radius-bottomleft": "0px",
                    "border-bottom-right-radius": "0px",
                    "border-bottom-left-radius": "0px"
                });
            }
            else {
                group.slideUp(500);
                $(this).css({
                    "-webkit-border-radius": "6px",
                    "-moz-border-radius-": "6px",
                    "border-radius": "6px",
                });
            }
        });
    }
}