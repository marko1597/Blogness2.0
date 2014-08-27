ngUser.factory('userService', ["$http", "$q", "configProvider", "dateHelper",
    function ($http, $q, configProvider, dateHelper) {
        var userApi = configProvider.getSettings().BlogApi == "" ? window.blogConfiguration.blogApi + "Users" : configProvider.getSettings().BlogApi + "Users";

        var applyUserModelDefaults = function(user) {
            user.BirthDateDisplay = dateHelper.getJsDate(user.BirthDate);
            user.BirthDate = dateHelper.getJsFullDate(user.BirthDate);

            if (user.FirstName == null) user.FirstName = "n/a";
            if (user.LastName == null) user.LastName = "n/a";
            if (user.Picture == null) user.Picture = { MediaUrl: configProvider.getDefaults().profilePictureUrl };
            if (user.Background == null) user.Background = { MediaUrl: configProvider.getDefaults().backgroundPictureUrl };

            return user;
        };

        return {
            getUserInfo: function (username) {
                var deferred = $q.defer();

                $http({
                    url: userApi + "/" + username,
                    method: "GET"
                }).success(function (response) {
                    deferred.resolve(applyUserModelDefaults(response));
                }).error(function () {
                    deferred.reject("An error occurred!");
                });

                return deferred.promise;
            },

            updateUser: function (user) {
                var deferred = $q.defer();

                $http({
                    url: userApi,
                    method: "PUT",
                    data: user
                }).success(function (response) {
                    deferred.resolve(applyUserModelDefaults(response));
                }).error(function (error) {
                    deferred.reject(error);
                });

                return deferred.promise;
            }
        };
    }
]);