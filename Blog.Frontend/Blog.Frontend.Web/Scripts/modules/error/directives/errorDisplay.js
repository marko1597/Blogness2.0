ngError.directive('errorDisplay', [
    function () {
        var ctrlFn = function ($scope) {
            $scope.errorMessage = "";
        };
        ctrlFn.$inject = ["$scope"];

        var linkFn = function (scope, element) {
            scope.$on("displayError", function (e, d) {
                scope.errorMessage = d.Message;
                $(element).removeClass("hidden");
            });

            $("#blog-error-global span.close-error").on("click", function (ev) {
                ev.preventDefault();
                $(element).addClass('hidden');
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
