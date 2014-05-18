ngError.directive('errorDisplay', ["$timeout",
    function ($timeout) {
        var ctrlFn = function ($scope) {
            $scope.errorMessage = "";
        };
        ctrlFn.$inject = ["$scope"];

        var linkFn = function (scope, element, attr) {
            scope.$on("displayError", function (e, d) {
                scope.errorMessage = d.Message;

                $(element).removeClass("hidden");
                $timeout(function() {
                    $(element).addClass("hidden");
                }, 4000);
            });
        };

        return {
            restrict: 'EA',
            scope: { data: '=' },
            replace: true,
            templateUrl: window.blogConfiguration.templatesUrl + "error/errorDisplay.html",
            controller: ctrlFn,
            link: linkFn
        };
    }]);
