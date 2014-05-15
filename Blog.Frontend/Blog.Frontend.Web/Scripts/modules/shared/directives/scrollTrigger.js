ngShared.directive('scrollTrigger', ["$rootScope", function ($rootScope) {
    return {
        link: function (scope, element, attrs) {
            var elemWatch = "#" + attrs.scrollTriggerWatch;
            var threshold = attrs.scrollTriggerThreshold;

            angular.element(element).bind("scroll", function() {
                var scroll = $(element).scrollTop();
                if (scroll >= $(elemWatch).outerHeight() - threshold) {
                    $rootScope.$broadcast("scrollBottom");
                }
            });
        }
    };
}]);