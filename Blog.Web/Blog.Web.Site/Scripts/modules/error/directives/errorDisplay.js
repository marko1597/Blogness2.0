ngError.directive('errorDisplay', [
    function () {
        var ctrlFn = function ($scope, errorService) {
            $scope.errorMessage = "";

            $scope.$on("displayError", function (e, d) {
                errorService.setError(d);
            });
        };
        ctrlFn.$inject = ["$scope", "errorService"];

        var linkFn = function (scope, element) {
            scope.$on("displayError", function (e, d) {
                scope.errorMessage = d.Message != undefined ? d.Message : d;
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
