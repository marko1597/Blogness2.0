var ngLogger = angular.module("ngLogger", ["ngConfig"]);

ngLogger.provider("$exceptionHandler", {
    $get: function (errorLogService) {
        return (errorLogService);
    }
});