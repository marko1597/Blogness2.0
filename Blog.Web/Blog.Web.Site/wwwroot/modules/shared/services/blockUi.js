ngShared.factory('blockUiService', [function () {
    return {
        blockIt: function (properties) {
            if (properties == undefined) properties = {};

            if (properties.html == undefined) {
                properties.html = '<h4><img src="wwwroot/css/images/loader-girl.gif" height="128" /></h4>';
            }

            if (properties.css == undefined) {
                properties.css = {
                    border: 'none',
                    padding: '5px',
                    backgroundColor: '#000',
                    opacity: .5,
                    color: '#fff'
                };
            }

            if (properties.elem == undefined) {
                $.blockUI({
                    message: properties.html,
                    css: properties.css
                });
            } else {
                $(properties.elem).block({
                    message: properties.html,
                    css: properties.css
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