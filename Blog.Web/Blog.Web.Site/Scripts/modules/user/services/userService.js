ngUser.factory('userService', ["$http", "$q", "configProvider", "dateHelper",
    function ($http, $q, configProvider, dateHelper) {
        var userApi = configProvider.getSettings().BlogApi == "" ? window.blogConfiguration.blogApi + "Users" : configProvider.getSettings().BlogApi + "Users";
        var addressApi = configProvider.getSettings().BlogApi == "" ? window.blogConfiguration.blogApi + "Address" : configProvider.getSettings().BlogApi + "Address";
        var hobbyApi = configProvider.getSettings().BlogApi == "" ? window.blogConfiguration.blogApi + "Hobbies" : configProvider.getSettings().BlogApi + "Hobbies";
        var educationApi = configProvider.getSettings().BlogApi == "" ? window.blogConfiguration.blogApi + "Education" : configProvider.getSettings().BlogApi + "Education";

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
            },

            addUserAddress: function (address) {
                var deferred = $q.defer();

                $http({
                    url: addressApi,
                    method: "POST",
                    data: address
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (error) {
                    deferred.reject(error);
                });

                return deferred.promise;
            },

            updateUserAddress: function(address) {
                var deferred = $q.defer();

                $http({
                    url: addressApi,
                    method: "PUT",
                    data: address
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (error) {
                    deferred.reject(error);
                });

                return deferred.promise;
            },

            addUserHobby: function(hobby) {
                var deferred = $q.defer();

                $http({
                    url: hobbyApi,
                    method: "POST",
                    data: hobby
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (error) {
                    deferred.reject(error);
                });

                return deferred.promise;
            },

            updateUserHobby: function(hobby) {
                var deferred = $q.defer();

                $http({
                    url: hobbyApi,
                    method: "PUT",
                    data: hobby
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (error) {
                    deferred.reject(error);
                });

                return deferred.promise;
            },

            deleteUserHobby: function (hobbyId) {
                var deferred = $q.defer();

                $http({
                    url: hobbyApi + "/" + hobbyId,
                    method: "DELETE"
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (error) {
                    deferred.reject(error);
                });

                return deferred.promise;
            },

            addUserEducation: function (education) {
                var deferred = $q.defer();

                $http({
                    url: educationApi,
                    method: "POST",
                    data: education
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (error) {
                    deferred.reject(error);
                });

                return deferred.promise;
            },

            updateUserEducation: function (education) {
                var deferred = $q.defer();

                $http({
                    url: educationApi,
                    method: "PUT",
                    data: education
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (error) {
                    deferred.reject(error);
                });

                return deferred.promise;
            },

            deleteUserEducation: function (educationId) {
                var deferred = $q.defer();

                $http({
                    url: educationApi + "/" + educationId,
                    method: "DELETE",
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (error) {
                    deferred.reject(error);
                });

                return deferred.promise;
            }
        };
    }
]);