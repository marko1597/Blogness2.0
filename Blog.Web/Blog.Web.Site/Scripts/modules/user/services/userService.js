ngUser.factory('userService', ["$http", "$q", "configProvider", "localStorageService", function ($http, $q, configProvider, localStorageService) {
    var userApi = configProvider.getSettings().BlogApi == "" ? window.blogConfiguration.blogApi + "Users" : configProvider.getSettings().BlogApi + "Users";
    var user = null;

    return {
        getUserInfo: function (username) {
            var deferred = $q.defer();

            if (user == null) {
                $http({
                    url: userApi + "/" + username,
                    method: "GET"
                }).success(function (response) {
                    user = response;
                    deferred.resolve(response);
                }).error(function() {
                    deferred.reject("An error occurred!");
                });
            } else {
                deferred.resolve(user);
            }

            return deferred.promise;
        }
    };
}]);