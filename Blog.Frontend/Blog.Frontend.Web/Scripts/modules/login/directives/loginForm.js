loginModule.directive('loginForm', function () {
	var ctrlFn = function ($scope, $rootScope, loginService) {
		$scope.username = "";
		$scope.password = "";
	    $scope.rememberMe = false;

		$scope.login = function() {
			loginService.loginUser($scope.username, $scope.password, $scope.rememberMe);
		};
	};
	ctrlFn.$inject = ["$scope", "$rootScope", "loginService"];

	return {
		restrict: 'EA',
		scope: { data: '=' },
		replace: true,
		template:
			'<div class="row">' +
				'<div class="col-xs-12 col-sm-6 col-sm-push-3 col-md-4 col-md-push-4 col-lg-4 col-lg-push-4">' +
					'<div id="wrapper">' +
						'<form name="login-form" class="login-form" action="" method="post">' +
							'<div class="row">' +
								'<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">' +
									'<div class="header">' +
										'<h1>Login Form</h1>' +
										'<span>Fill out the form below to login to Bloggity.</span>' +
									'</div>' +
								'</div>' +
							'</div>' +

							'<div class="content">' +
								'<div class="row">' +
									'<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">' +
										 '<input name="username" type="text" class="input username" placeholder="Username" ng-model="username" ng-enter="login()"/>' +
										 '<div class="user-icon"></div>' +
									'</div>' +
									'<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">' +
										  '<input name="password" type="password" class="input password" placeholder="Password" ng-model="password" ng-enter="login()" />' +
										  '<div class="pass-icon"></div>' +
									'</div>' +
                                    '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">' +
										 '<input name="username" type="checkbox" ng-model="rememberMe" name="rememberMe">Remember me</input>' +
									'</div>' +
								'</div>' +
							'</div>' +

							'<div class="row">' +
								'<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">' +
									'<div class="footer">' +
										'<input type="button" name="login" value="Login" class="button" ng-click="login()" />' +
										'<input type="button" name="register" value="Register" class="register" />' +
									'</div>' +
								'</div>' +
							'</div>' +
						'</form>' +
					'</div>' +
				'</div>' +
			'</div>',
		controller: ctrlFn
	};
});
