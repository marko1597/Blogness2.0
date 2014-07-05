ngConfig.provider('configProvider', function () {
    var windowDimensions = {
        width: 0,
        height: 0,
        mode: ""
    };

    var settings = {
        "BlogApi": "",
        "BlogRoot": "",
        "HubUrl": "",
        "IsLoggedIn": false,
        "SessionId": 0,
        "AlertTimer": 5000
    };

    var pageState = {
        POPULAR: "Popular",
        RECENT: "Recent",
        USEROWNED: "UserOwned"
    };

    var navigationItems = [
        { text: "Home", icon: "content/images/nav-home.png", href: "/#/" },
        { text: "Profile", icon: "content/images/nav-profile.png", href: "/#/profile" },
        { text: "Friends", icon: "content/images/nav-friends.png", href: "/#/friends" },
        { text: "Groups", icon: "content/images/nav-groups.png", href: "/#/groups" },
        { text: "Events", icon: "content/images/nav-events.png", href: "/#/events" }
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

            setHubUrl: function (val) {
                settings.HubUrl = val;
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