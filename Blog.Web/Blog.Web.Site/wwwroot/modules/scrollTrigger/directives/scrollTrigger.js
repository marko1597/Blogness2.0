blogScrollTrigger.directive('scrollTrigger', ["$rootScope", function ($rootScope) {
    return {
        link: function (scope, element, attrs) {
            scope.scrollTriggerWatch = null;

            $rootScope.$on("updateScrollTriggerWatch", function (event, data) {
                scope.scrollTriggerWatch = "#" + data;
            });

            angular.element(element).bind("scroll", function () {
                if (scope.scrollTriggerWatch != null) {
                    var scroll = $(element).scrollTop();
                    if (scroll + $(window).height() >= $(scope.scrollTriggerWatch).outerHeight()) {
                        $rootScope.$broadcast("scrollBottom");
                    }
                }
            });
        }
    };
}]);