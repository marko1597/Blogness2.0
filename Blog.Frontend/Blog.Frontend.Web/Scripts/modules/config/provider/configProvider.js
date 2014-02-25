configModule.provider('configProvider', function () {
    var settings = {
        "BlogApi": "",
        "BlogRoot": "",
        "IsLoggedIn": false,
        "SessionId": 0,
        "PostsThreshold": 20
    };

    var pageState = {
        POPULAR: "Popular",
        RECENT: "Recent",
        USEROWNED: "UserOwned"
    };

    var navigationItems = ["Home", "Profile", "Friends", "Groups", "Events"];

    this.$get = [function () {
        return {
            /* Getters */
            getSettings: function () {
                return settings;
            },

            /* Setters */
            setBlogApiEndpoint: function (val) {
                settings.BlogApi = val;
            },

            setBlogRoot: function (val) {
                settings.BlogRoot = val;
            },

            setSessionId: function (sessionId) {
                settings.SessionId = sessionId;
                settings.IsLoggedIn = sessionId !== "" ? true : false;
            },

            /* Constants */
            pageState: pageState,
            navigationItems: navigationItems
        };
    }];
});