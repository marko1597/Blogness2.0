ngShared.directive('ticker', function () {
    var filterFn;
    filterFn = function (scope, element, attrs) {
        var ticker = $(element).newsTicker({
            row_height: 80,
            max_rows: 1,
            duration: 5000
        });

        if (attrs.enablePause) {
            $(element).on("click", $(element).find("[data-pause-trigger]"), function (ev) {
                ticker.newsTicker('toggle');
            });
        }
    };

    return {
        restrict: 'EA',
        link: filterFn
    };
});