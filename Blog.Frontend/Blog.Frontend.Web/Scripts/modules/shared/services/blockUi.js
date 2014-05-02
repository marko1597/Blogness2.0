ngShared.factory('blockUiService', [function () {
    return {
        blockIt: function (html, css, elem) {
            if (elem == undefined) {
                $.blockUI({
                    message: html,
                    css: css
                });
            } else {
                $(elem).block({
                    message: html,
                    css: css
                });
            }
        },

        unblockIt: function (elem) {
            if (elem == undefined) {
                $.unblockUI();
            } else {
                $(elem).unblock();
            }
        }
    };
}]);