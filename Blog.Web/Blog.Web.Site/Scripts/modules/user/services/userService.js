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

                    if (response.FirstName == null) response.FirstName = "n/a";
                    if (response.LastName == null) response.LastName = "n/a";
                    if (response.Picture == null) response.Picture = { MediaUrl: configProvider.getDefaults().profilePictureUrl };
                    if (response.Background == null) response.Background = { MediaUrl: configProvider.getDefaults().backgroundPictureUrl };

                    deferred.resolve(response);
                }).error(function () {
                    deferred.reject("An error occurred!");
                });

                return deferred.promise;
            }
        };
    }
]);