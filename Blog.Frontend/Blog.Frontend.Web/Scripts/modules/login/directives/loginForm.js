loginModule.directive('loginForm', function () {
    var ctrlFn = function ($scope, $rootScope, $timeout, configProvider, loginService) {
        $scope.username = "";
        $scope.password = "";
        $scope.rememberMe = false;
        $scope.alert = { Title: "Oops!", Message: "", Show: false };

        $scope.login = function () {
            $scope.response = loginService.loginUser($scope.username, $scope.password, $scope.rememberMe).then(function (resp) {
                console.log(resp);
                $.unblockUI();
            }, function (errorMsg) {
                $scope.alert.Message = errorMsg;
                $scope.alert.Show = true;
                $.unblockUI();

                $timeout(function() {
                    $scope.alert.Show = false;
                }, configProvider.getSettings().AlertTimer);
            });
        };

        $scope.closeAlert = function() {
            $scope.alert.Show = false;
        };
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$timeout", "configProvider", "loginService"];

    var filterFn;
    filterFn = function (scope, element) {
        $("input[name=login]").on("click", function () {
            doBlock();
        });

        $("form.login-form div.content :input:not(:checkbox)").on("keydown", function (e) {
            var code = e.which; 
            if (code == 13) e.preventDefault();
            if (code == 32 || code == 13 || code == 188 || code == 186) {
                doBlock();
            }
        });

        var doBlock = function() {
            $.blockUI({
                message: '<h4><img src="../content/images/loader-girl.gif" height="128" /></h4>',
                css: {
                    border: 'none',
                    padding: '5px',
                    backgroundColor: '#000',
                    opacity: .5,
                    color: '#fff'
                }
            });
        };
    };

    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        link: filterFn,
        template:
            '<div class="main-wrapper">' +
                '<div class="background" />' +
                '<div class="overlay" />' +
			    '<div class="row">' +
                    '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" ng-show="alert.Show">' +
                        '<div class="login-alert alert alert-danger alert-dismissable">' +
			                '<strong>{{alert.Title}}</strong>' +
                            '<span>{{alert.Message}}</span>' +
			            '</div>' +
			        '</div>' +

				    '<div class="col-xs-12 col-sm-6 col-sm-push-3 col-md-4 col-md-push-4 col-lg-4 col-lg-push-4">' +
					    '<div id="wrapper">' +
						    '<form name="login-form" class="login-form" action="" method="post">' +
							    '<div class="row">' +
								    '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">' +
									    '<div class="header">' +
										    '<h1>Bloggity Blog</h1>' +
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
										     '<input name="username" type="checkbox" ng-model="rememberMe" name="rememberMe">' +
                                             '<span>Remember me</span>' +
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
			    '</div>' +
            '</div>',
        controller: ctrlFn
    };
});
