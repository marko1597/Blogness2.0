ngLogin.factory('authenticationInterceptorService', ['$q', '$rootScope', '$location', 'localStorageService',
    function ($q, $rootScope, $location, localStorageService) {
        return {
            request: function(config) {
                config.headers = config.headers || {};
                var authData = localStorageService.get('authorizationData');
                if (authData) {
                    config.headers.Authorization = 'Bearer ' + authData.token;
                }

                return config;
            },

            responseError: function(rejection) {
                if (rejection.status === 401) {
                    $location.path("/");
                    $rootScope.$broadcast("launchLoginForm", { canClose: true });
                    localStorageService.remove('username');
                    localStorageService.remove('authorizationData');
                }
                return $q.reject(rejection);
            }
        };
    }
]);