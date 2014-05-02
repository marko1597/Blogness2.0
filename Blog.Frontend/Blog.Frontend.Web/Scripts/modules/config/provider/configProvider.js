ngConfig.provider('configProvider', function () {
    var settings = {
        "BlogApi": "",
        "BlogRoot": "",
        "IsLoggedIn": false,
        "SessionId": 0,
        "AlertTimer": 5000,
        "PostsThreshold": 20
    };

    var pageState = {
        POPULAR: "Popular",
        RECENT: "Recent",
        USEROWNED: "UserOwned"
    };

    var navigationItems = [
        { text: "Home", icon: "/blog/content/images/nav-home.png" },
        { text: "Profile", icon: "/blog/content/images/nav-profile.png" },
        { text: "Friends", icon: "/blog/content/images/nav-friends.png" },
        { text: "Groups", icon: "/blog/content/images/nav-groups.png" },
        { text: "Events", icon: "/blog/content/images/nav-events.png" }
    ];

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