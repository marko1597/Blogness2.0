configModule.provider('configProvider', function () {
    var settings = {
        "BlogApi": "",
        "IsLoggedIn": false,
        "SessionId": 0
    };

    this.$get = [function () {
        return {
            getSettings: function () {
                return settings;
            },

            setBlogApiEndpoint: function (val) {
                settings.BlogApi = val;
            },

            setSessionId: function (sessionId) {
                settings.SessionId = sessionId;
                settings.IsLoggedIn = sessionId !== "" ? true : false;
            }
        };
    }];

});