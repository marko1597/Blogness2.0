ngLogin.directive('registerForm', function () {
    var ctrlFn = function ($scope, $rootScope, $timeout, $window, errorService, localStorageService, configProvider, authenticationService, blockUiService) {
        $scope.username = "";
        $scope.password = "";
        $scope.confirmPassword = "";
        $scope.firstName = "";
        $scope.lastName = "";
        $scope.email = "";
        $scope.birthDate = "";
        $scope.errors = [];
        
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
                    localStorageService.add("username", $scope.username);
                    blockUiService.unblockIt();

                    if ($scope.modal == undefined) {
                        $window.location.href = configProvider.getSettings().BlogRoot;
                    } else {
                        $rootScope.$broadcast("hideLoginForm");
                        $rootScope.$broadcast("userLoggedIn");
                    }
                } else {
                    blockUiService.unblockIt();
                    $scope.errorMessage = response.error_description;
                }
            }, function (error) {
                for (var key in error.ModelState) {
                    var errorItem = {
                        field: key.split('.')[1].toLowerCase(),
                        message: error.ModelState[key][0]
                    };
                    $scope.errors.push(errorItem);
                }

                blockUiService.unblockIt();
            });
        };

        $scope.hasError = function (name) {
            var classStr = "";

            _.each($scope.errors, function(e) {
                if (e.field == name) {
                    classStr = "has-error";
                    $(".login-form.register").find(".content input[name='" + e.field + "']").prev('p').text(e.message);
                }
            });

            return classStr;
        };

        $scope.isModal = function () {
            if ($scope.modal == undefined) {
                return false;
            } else {
                return $scope.modal ? true : false;
            }
        };
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$timeout", "$window", "errorService", "localStorageService", "configProvider", "authenticationService", "blockUiService"];

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
        templateUrl: window.blogConfiguration.templatesModulesUrl + "login/registerform.html",
        controller: ctrlFn
    };
});
