ngNavigation.directive('navigationMenu', function () {
    var ctrlFn = function ($scope, $rootScope, configProvider) {
        $scope.navigationItems = configProvider.navigationItems;

    };
    ctrlFn.$inject = ["$scope", "$rootScope", "configProvider"];

    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        template:
            '<nav class="navigation-menu navigation-effect" id="navigation-menu">' +
				'<h2 class="icon icon-lab">Sidebar</h2>' +
				'<ul>' +
					'<li ng-repeat="navigationItem in navigationItems">' +
                        '<a class="icon icon-data" href="#">{{navigationItem}}</a>' +
                    '</li>' +
				'</ul>' +
			'</nav>',
        controller: ctrlFn
    };
});
