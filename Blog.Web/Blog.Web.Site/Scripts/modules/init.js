﻿window.blogInit = {};

window.blogInit =
{
    start: function () {
        var navigationItems = [
            { text: "Home", icon: window.blogConfiguration.blogRoot + "/content/images/nav-home.png", href: "/blog#/" },
            { text: "Profile", icon: window.blogConfiguration.blogRoot + "/content/images/nav-profile.png", href: "/blog#/profile" },
            { text: "Friends", icon: window.blogConfiguration.blogRoot + "/content/images/nav-friends.png", href: "/blog#/friends" },
            { text: "Groups", icon: window.blogConfiguration.blogRoot + "/content/images/nav-groups.png", href: "/blog#/groups" },
            { text: "Events", icon: window.blogConfiguration.blogRoot + "/content/images/nav-events.png", href: "/blog#/events" }
        ];

        var settings = angular.element(document.querySelector('[ng-app]')).injector().get("configProvider");
        settings.setBlogApiEndpoint(window.blogConfiguration.blogApi);
        settings.setBlogRoot(window.blogConfiguration.blogRoot);
        settings.setHubUrl(window.blogConfiguration.hubUrl);
        settings.setDimensions(window.innerWidth, window.innerHeight);
        settings.setNavigationItems(navigationItems);

        // TODO: This is a temporary hack. It should be in its respective module
        ngLogger.provider("$exceptionHandler", {
            $get: function (errorLogService) {
                return (errorLogService);
            }
        });
    }
}