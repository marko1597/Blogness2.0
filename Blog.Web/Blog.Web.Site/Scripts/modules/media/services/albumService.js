ngMedia.factory('albumService', ["$http", "$q", "configProvider", "dateHelper",
    function ($http, $q, configProvider, dateHelper) {
        var albumApi = configProvider.getSettings().BlogApi == "" ?
            window.blogConfiguration.blogApi :
            configProvider.getSettings().BlogApi;

        return {
            getAlbumsByUser: function (userId) {
                var deferred = $q.defer();

                $http({
                    url: albumApi + "users/" + userId + "/albums",
                    method: "GET"
                }).success(function (response) {
                    _.each(response, function (a) {
                        a.CreatedDateDisplay = dateHelper.getDateDisplay(a.CreatedDate);
                        a.IsNew = false;
                        a.IsEditing = false;

                        _.each(a.Media, function (m) {
                            m.CreatedDateDisplay = dateHelper.getDateDisplay(m.CreatedDate);
                        });
                    });
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            getUserDefaultAlbum: function (userId) {
                var deferred = $q.defer();

                $http({
                    url: albumApi + "users/" + userId + "/albums/default",
                    method: "GET"
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            addAlbum: function (album) {
                var deferred = $q.defer();

                $http({
                    url: albumApi + "album",
                    method: "POST",
                    data: album
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            updateAlbum: function (album) {
                var deferred = $q.defer();

                $http({
                    url: albumApi + "album",
                    method: "PUT",
                    data: album
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            deleteAlbum: function (albumId) {
                var deferred = $q.defer();

                $http({
                    url: albumApi + "album/" + albumId,
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