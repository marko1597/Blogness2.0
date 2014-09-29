window.blogInit = {};

window.blogInit =
{
    start: function () {
        var navigationItems = [
            { text: "Home", icon: "fa-home", href: "/#/" },
            { text: "People", icon: "fa-user", href: "/#/user" },
            { text: "Friends", icon: "fa-comments", href: "/#/friends" },
            { text: "Groups", icon: "fa-users", href: "/#/groups" },
            { text: "Events", icon: "fa-calendar", href: "/#/events" }
        ];

        var settings = angular.element(document.querySelector('[ng-app]')).injector().get("configProvider");
        settings.setBlogSockets(window.blogConfiguration.blogSockets);
        settings.setBlogApiEndpoint(window.blogConfiguration.blogApi);
        settings.setBlogRoot(window.blogConfiguration.blogRoot);
        settings.setBlogSocketsAvailability(window.blogConfiguration.blogSocketsAvailable);
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