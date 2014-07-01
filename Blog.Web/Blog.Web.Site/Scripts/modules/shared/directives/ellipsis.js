ngShared.directive('ellipsis', ["$rootScope", function ($rootScope) {
    var filterFn;
    filterFn = function (scope, element, attrs) {
        scope.$on("reapplyEllipsis", function () {
            scope.applyEllipsis();
        });

        scope.applyEllipsis = function() {
            var height = parseInt(attrs.wrapHeight == undefined ? 180 : attrs.wrapHeight);
            $(element).dotdotdot({
                ellipsis: "...",
                height: height
            });
        };

        scope.applyEllipsis();
    };

    return {
        restrict: 'EA',
        link: filterFn
    };
}]);