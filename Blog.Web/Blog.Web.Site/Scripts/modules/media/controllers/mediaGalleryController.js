ngMedia.controller('mediaGalleryController', ["$scope", "$rootScope", "$interval", "$timeout",
    function ($scope, $rootScope, $interval, $timeout) {
        $scope.init = function () {
            var stop;
            var topic = "launchMediaGallery";

            stop = $interval(function () {
                if ($rootScope.$$listeners[topic] && $rootScope.$$listeners[topic].length > 0) {
                    $timeout(function () {
                        $rootScope.$broadcast(topic, {});
                        $interval.cancel(stop);
                        stop = undefined;
                    }, 250);
                }
            }, 250);
        };

        $scope.init();
    }
]);