ngLogin.directive('registerForm', ["$templateCache", function ($templateCache) {
    var ctrlFn = function ($scope, $rootScope, $timeout, $window, errorService, configProvider, authenticationService, blockUiService) {
        $scope.username = "";
        $scope.password = "";
        $scope.confirmPassword = "";
        $scope.firstName = "";
        $scope.lastName = "";
        $scope.email = "";
        $scope.birthDate = "";
        $scope.errors = [];
        $scope.messageDisplay = {
            show: false,
            type: "alert-success",
            message: ""
        };

        $scope.register = function () {
            blockUiService.blockIt({
                html: '<h4><img src="content/images/loader-girl.gif" height="128" /></h4>',
                css: {
                    border: 'none',
                    padding: '5px',
                    backgroundColor: '#000',
                    opacity: .5,
                    color: '#fff'
                }
            });

            var registrationInfo = {
                Username: $scope.username,
                Password: $scope.password,
                ConfirmPassword: $scope.confirmPassword,
                FirstName: $scope.firstName,
                LastName: $scope.lastName,
                Email: $scope.email,
                BirthDate: $scope.birthDate
            };

            authenticationService.saveRegistration(registrationInfo).then(function (response) {
                if (response.error == undefined || response.error == null) {
                    blockUiService.unblockIt();
                    blockUiService.blockIt({
                        html: '<div class="alert alert-success"><p>Hooray! You have successfully registered an account to Bloggity! Let\'s sign you in now.</p></div>',
                        css: {
                            border: 'none',
                            padding: '0',
                            backgroundColor: '#000',
                            color: '#fff'
                        }
                    });

                    authenticationService.login($scope.username, $scope.password).then(function (loginResponse) {
                        if (loginResponse.error == undefined || loginResponse.error == null) {
                            if (!$scope.isModal()) {
                                $window.location.href = configProvider.getSettings().BlogRoot;
                            } else {
                                $rootScope.$broadcast("hideLoginForm");
                                $rootScope.$broadcast("userLoggedIn");
                            }
                        } else {
                            $scope.showMessageDisplay("alert-warning",
                                "Oops! We've managed to create your account but there was a problem logging you in. (" + response.error_description + ")");
                        }
                        blockUiService.unblockIt();
                    }, function (error) {
                        $scope.showMessageDisplay("alert-warning",
                                "Oops! We've managed to create your account but there was a problem logging you in. (" + error.Message + ")");
                        blockUiService.unblockIt();
                    });
                } else {
                    blockUiService.unblockIt();
                    $scope.showMessageDisplay("alert-danger", error.Message);
                }
            }, function (error) {
                try {
                    for (var key in error.ModelState) {
                        var errorItem = {
                            field: key == "" ? "username" : key.split('.')[1].toLowerCase(),
                            message: error.ModelState[key][0]
                        };
                        $scope.errors.push(errorItem);
                    }
                    blockUiService.unblockIt();
                } catch (ex) {
                    blockUiService.unblockIt();
                }
            });
        };

        $scope.hasError = function (name) {
            var classStr = "";

            _.each($scope.errors, function (e) {
                if (e.field == name) {
                    classStr = "has-error";
                    $(".login-form.register").find(".content input[name='" + e.field + "']").prev('p').text(e.message);
                }
            });

            return classStr;
        };

        $scope.showMessageDisplay = function(type, message) {
            $scope.messageDisplay.type = type;
            $scope.messageDisplay.message = message;
            $scope.messageDisplay.show = true;
        };

        $scope.isModal = function () {
            if ($scope.modal == undefined) {
                return false;
            } else {
                return $scope.modal ? true : false;
            }
        };
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$timeout", "$window", "errorService", "configProvider", "authenticationService", "blockUiService"];

    var linkFn = function (scope, elem) {
        scope.showLoginForm = function () {
            $(elem).closest(".modal-body").removeClass("hover");
        };
    };

    return {
        restrict: 'EA',
        scope: { modal: '=' },
        replace: true,
        link: linkFn,
        template: $templateCache.get("login/registerForm.html"),
        controller: ctrlFn
    };
}]);
