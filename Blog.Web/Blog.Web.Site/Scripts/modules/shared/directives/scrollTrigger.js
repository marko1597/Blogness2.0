ngShared.directive('scrollTrigger', ["$rootScope", function ($rootScope) {
    return {
        link: function (scope, element, attrs) {
            var elemWatch = "#" + attrs.scrollTriggerWatch;

            angular.element(element).bind("scroll", function() {
                var scroll = $(element).scrollTop();
                if (scroll + $(window).height() >= $(elemWatch).outerHeight()) {
                    $rootScope.$broadcast("scrollBottom");
                }
            });
        }
    };
}]);