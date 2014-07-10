window.blogInit = {};

window.blogInit =
{
    start: function() {
        var settings = angular.element(document.querySelector('[ng-app]')).injector().get("configProvider");
        settings.setBlogApiEndpoint(window.blogConfiguration.blogApi);
        settings.setBlogRoot(window.blogConfiguration.blogRoot);
        settings.setHubUrl(window.blogConfiguration.hubUrl);
        settings.setDimensions(window.innerWidth, window.innerHeight);

        // TODO: This is a temporary hack. It should be in its respective module
        ngLogger.provider("$exceptionHandler", {
            $get: function (errorLogService) {
                return (errorLogService);
            }
        });
    }
}