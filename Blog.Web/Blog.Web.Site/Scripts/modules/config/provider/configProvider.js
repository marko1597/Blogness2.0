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

    var defaults = {
        profilePictureUrl: "",
        backgroundPictureUrl: ""
    };

    var navigationItems = [];

    this.$get = [function () {
        return {
            /* Getters */
            getSettings: function () {
                return settings;
            },

            getNavigationItems: function () {
                return navigationItems;
            },

            getDefaults: function () {
                return defaults;
            },

            /* Setters */
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

            setDefaultProfilePicture: function (val) {
                defaults.profilePictureUrl = val;
            },

            setDefaultBackgroundPicture: function (val) {
                defaults.backgroundPictureUrl = val;
            },

            setNavigationItems: function (items) {
                navigationItems = items;
            },

            /* Constants */
            pageState: pageState,
            windowDimensions: windowDimensions
        };
    }];
});