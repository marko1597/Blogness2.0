ngShared.factory('blockUiService', [function () {
    return {
        blockIt: function (html, css, elem) {
            if (html == undefined) {
                html = '<h4>Loading...</h4>';
            }

            if (css == undefined) {
                css = {
                    border: 'none',
                    padding: '5px',
                    backgroundColor: '#000',
                    opacity: .5,
                    color: '#fff'
                };
            }

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