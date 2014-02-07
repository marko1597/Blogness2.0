loginForm.directive('loginForm', function () {
    var ctrlFn = function ($scope, $rootScope) {

    };
    ctrlFn.$inject = ["$scope", "$rootScope"];

    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        template:
			'<div class="row">' +
                '<div class="col-xs-0 col-sm-6 col-md-8 col-lg-8"></div>' +
                '<div class="col-xs-12 col-sm-6 col-md-4 col-lg-4">' +
			        '<div id="wrapper">' +
	                    '<form name="login-form" class="login-form" action="" method="post">' +
		                    '<div class="header">' +
		                        '<h1>Login Form</h1>' +
		                        '<span>Fill out the form below to login to Bloggity.</span>' +
		                    '</div>' +
	
		                    '<div class="content">' +
		                        '<input name="username" type="text" class="input username" placeholder="Username" />' +
		                        '<div class="user-icon"></div>' +
		                        '<input name="password" type="password" class="input password" placeholder="Password" />' +
		                        '<div class="pass-icon"></div>' +
		                    '</div>' +

		                    '<div class="footer">' +
		                        '<input type="submit" name="submit" value="Login" class="button" />' +
		                        '<input type="submit" name="submit" value="Register" class="register" />' +
		                    '</div>' +
	                    '</form>' +
                    '</div>' +
                '</div>' +
			'</div>',
        controller: ctrlFn
    };
});
