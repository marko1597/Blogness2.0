ngLogger.factory("errorLogService", ["$log", "$window", "configProvider", "stacktraceService", function ($log, $window, configProvider, stacktraceService) {
    function log(exception, cause) {
        var logApi = configProvider.getSettings().BlogApi == "" ?
            window.blogConfiguration.blogApi + "log" :
            configProvider.getSettings().BlogApi + "log";

        $log.error.apply($log, arguments);

        try {
            var errorMessage = exception.toString();
            var stackTrace = stacktraceService.print({ e: exception });

            $.ajax({
                type: "POST",
                url: logApi,
                contentType: "application/json",
                data: JSON.stringify({
                    ErrorUrl: $window.location.href,
                    ErrorMessage: errorMessage,
                    StackTrace: stackTrace,
                    Cause: (cause || "")
                }),
                success: function (d) {
                    $log.error.apply(d);
                }
            });

        } catch (loggingError) {
            $log.warn("Error logging failed");
            $log.log(loggingError);
        }
    }
    return (log);
}]);