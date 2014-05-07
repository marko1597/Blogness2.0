ngHeader.directive('headerMenu', function () {
    var ctrlFn = function ($scope) {
        $scope.userLoggedIn = false;
    };
    ctrlFn.$inject = ["$scope"];

    
    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesUrl + "header/headerMenu.html",
        controller: ctrlFn
    };
});
