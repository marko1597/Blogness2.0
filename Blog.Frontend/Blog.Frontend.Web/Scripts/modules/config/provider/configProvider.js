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
        { text: "Home", icon: "/blog/content/images/nav-home.png", href: "./" },
        { text: "Profile", icon: "/blog/content/images/nav-profile.png", href: "./#/profile" },
        { text: "Friends", icon: "/blog/content/images/nav-friends.png", href: "./#/friends" },
        { text: "Groups", icon: "/blog/content/images/nav-groups.png", href: "./#/groups" },
        { text: "Events", icon: "/blog/content/images/nav-events.png", href: "./#/events" }
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