ngConfig.provider('configProvider', function () {
    var windowDimensions = {
        width: 0,
        height: 0,
        mode: ""
    };

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
        { text: "Home", icon: "/blog/content/images/nav-home.png", href: "/blog/#/" },
        { text: "Profile", icon: "/blog/content/images/nav-profile.png", href: "/blog/#/profile" },
        { text: "Friends", icon: "/blog/content/images/nav-friends.png", href: "/blog/#/friends" },
        { text: "Groups", icon: "/blog/content/images/nav-groups.png", href: "/blog/#/groups" },
        { text: "Events", icon: "/blog/content/images/nav-events.png", href: "/blog/#/events" }
    ];

    this.$get = [function () {
        return {
            /* Getters */
            getSettings: function () {
                return settings;
            },

            setDimensions: function (w, h) {
                windowDimensions.width = w;
                windowDimensions.height = h;

                if (w >= 320 && w <= 767) {
                    windowDimensions.mode = "mobile";
                } else if (w >= 768 && w <= 1024) {
                    windowDimensions.mode = "tablet";
                } else if (w > 1024) {
                    windowDimensions.mode = "desktop";
                }
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
            navigationItems: navigationItems,
            windowDimensions: windowDimensions
        };
    }];
});