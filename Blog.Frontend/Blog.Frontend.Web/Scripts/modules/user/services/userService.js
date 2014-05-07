ngUser.factory('userService', ["$http", "$q", "configProvider", "localStorageService", function ($http, $q, configProvider, localStorageService) {
    var userApi = configProvider.getSettings().BlogApi == "" ? window.blogConfiguration.blogApi + "Users" : configProvider.getSettings().BlogApi + "Users";

    return {
        getUserInfo: function () {
            var deferred = $q.defer();
            var username = localStorageService.get("username");

            $http({
                url: userApi + "/" + username,
                method: "GET"
            }).success(function (response) {
                deferred.resolve(response);
            }).error(function () {
                deferred.reject("An error occurred!");
            });

            return deferred.promise;
        }
    };
}]);