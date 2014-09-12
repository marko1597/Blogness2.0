blog.controller('blogMainController', ["$scope", "$location", "$rootScope", "$log",
    function ($scope, $location, $rootScope, $log) {
        $rootScope.$on("$locationChangeStart", function (event, next, current) {
            $log.info("location changing from " + current + " to " + next);
        });
    }
]);