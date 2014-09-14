window.blogInit = {};

window.blogInit =
{
    start: function () {
        var navigationItems = [
            { text: "Home", icon: window.blogConfiguration.blogRoot + "/content/images/nav-home.png", href: "/#/" },
            { text: "People", icon: window.blogConfiguration.blogRoot + "/content/images/nav-profile.png", href: "/#/user" },
            { text: "Friends", icon: window.blogConfiguration.blogRoot + "/content/images/nav-friends.png", href: "/#/friends" },
            { text: "Groups", icon: window.blogConfiguration.blogRoot + "/content/images/nav-groups.png", href: "/#/groups" },
            { text: "Events", icon: window.blogConfiguration.blogRoot + "/content/images/nav-events.png", href: "/#/events" }
        ];

        var settings = angular.element(document.querySelector('[ng-app]')).injector().get("configProvider");
        settings.setBlogSockets(window.blogConfiguration.blogSockets);
        settings.setBlogApiEndpoint(window.blogConfiguration.blogApi);
        settings.setBlogRoot(window.blogConfiguration.blogRoot);
        settings.setHubUrl(window.blogConfiguration.hubUrl);
        settings.setDimensions(window.innerWidth, window.innerHeight);
        settings.setNavigationItems(navigationItems);
        settings.setDefaultProfilePicture(window.blogConfiguration.blogApi + "media/defaultprofilepicture");
        settings.setDefaultBackgroundPicture(window.blogConfiguration.blogApi + "media/defaultbackgroundpicture");
        settings.setSocketClientFunctions(window.socketClientFunctions);

        // TODO: This is a temporary hack. It should be in its respective module
        ngLogger.provider("$exceptionHandler", {
            $get: function (errorLogService) {
                return (errorLogService);
            }
        });
    }
}