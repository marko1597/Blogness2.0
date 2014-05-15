ngShared.directive('ellipsis', function () {
    var filterFn;
    filterFn = function (scope, element) {
        $(element).dotdotdot({
            ellipsis: "...",
            height: 180
        });
    };

    return {
        restrict: 'EA',
        link: filterFn
    };
});