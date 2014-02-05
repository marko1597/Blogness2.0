window.Blog.Util = 
{
    baseDomain: function () {
        return "http://localhost/blog/";
    },

    resizeBody: function () {
        $("body #body").height($(window).height());
    },

    resizeElementByParent: function (a) {
        a.width(a.parent().width());
        a.css({ 'box-sizing': 'border-box', '-moz-box-sizing': 'border-box', '-webkit-box-sizing': 'border-box', 'height': 'auto' });
    },

    convertSpecialChars: function (s) {
        return s.replace(/&/g, "&amp;").replace(/>/g, "&gt;").replace(/</g, "&lt;").replace(/"/g, "&quot;");
    }
}