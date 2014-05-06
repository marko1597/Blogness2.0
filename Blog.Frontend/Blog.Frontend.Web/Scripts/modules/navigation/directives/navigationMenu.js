ngNavigation.directive('navigationMenu', function () {
    var ctrlFn = function ($scope, $rootScope, userService, configProvider) {
        $scope.navigationItems = configProvider.navigationItems;
        $scope.user = {};
        $scope.userFullName = "";

        $scope.getUserInfo = function () {
            userService.getUserInfo().then(function(resp) {
                $scope.user = resp;
                $scope.userFullName = $scope.user.FirstName + " " + $scope.user.LastName;
            });
        };

        $scope.getUserInfo();
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "userService", "configProvider"];

    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        template:
            '<nav class="navigation-menu navigation-effect" id="navigation-menu">' +
				'<h4 class="icon icon-lab">{{userFullName}}</h4>' +
				'<ul>' +
					'<li ng-repeat="navigationItem in navigationItems">' +
                        '<a class="icon icon-data" href="{{navigationItem.href}}">' +
                            '<img ng-src="{{navigationItem.icon}}" />' +
                            '{{navigationItem.text}}' +
                        '</a>' +
                    '</li>' +
				'</ul>' +
			'</nav>',
        controller: ctrlFn
    };
});
