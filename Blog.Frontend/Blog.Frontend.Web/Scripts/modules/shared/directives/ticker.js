blog.directive('ticker', function () {
    var filterFn;
    filterFn = function (scope, element, attrs) {
        var ticker = $(element).newsTicker({
            row_height: 80,
            max_rows: 1,
            duration: 5000
        });

        if (attrs.enablePause) {
            $(element).on("click", $(element).find("[data-pause-trigger]"), function (ev) {
                var pauseElement = $(ev.target).next("div." + attrs.pauseElement);
                if (pauseElement.length > 0) {
                    ticker.newsTicker('stop');
                } else {
                    ticker.newsTicker('start');
                }
            });
        }
    };

    return {
        restrict: 'EA',
        link: filterFn
    };
});