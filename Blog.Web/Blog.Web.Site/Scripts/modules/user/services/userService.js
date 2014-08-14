ngUser.factory('userService', ["$http", "$q", "configProvider", "dateHelper",
    function ($http, $q, configProvider, dateHelper) {
        var userApi = configProvider.getSettings().BlogApi == "" ? window.blogConfiguration.blogApi + "Users" : configProvider.getSettings().BlogApi + "Users";

        return {
            getUserInfo: function (username) {
                var deferred = $q.defer();

                $http({
                    url: userApi + "/" + username,
                    method: "GET"
                }).success(function (response) {
                    response.BirthDate = dateHelper.getJsDate(response.BirthDate);
                    deferred.resolve(response);
                }).error(function () {
                    deferred.reject("An error occurred!");
                });

                return deferred.promise;
            }
        };
    }
]);