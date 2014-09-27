ngMedia.factory('mediaService', ["$http", "$q", "configProvider",
    function ($http, $q, configProvider) {
        var mediaApi = configProvider.getSettings().BlogApi == "" ?
            window.blogConfiguration.blogApi :
            configProvider.getSettings().BlogApi;

        return {
            getMediaByAlbum: function(albumId) {
                var deferred = $q.defer();

                $http({
                    url: mediaApi + "album/" + albumId + "/media",
                    method: "GET"
                }).success(function(response) {
                    deferred.resolve(response);
                }).error(function(e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            getMediaByUser: function(userId) {
                var deferred = $q.defer();

                $http({
                    url: mediaApi + "users/" + userId + "/media",
                    method: "GET"
                }).success(function(response) {
                    deferred.resolve(response);
                }).error(function(e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            addMedia: function(media, albumName, username) {
                var deferred = $q.defer();

                $http({
                    url: mediaApi + "media?username=" + username + "&album=" + albumName,
                    method: "POST",
                    data: comment
                }).success(function(response) {
                    deferred.resolve(response);
                }).error(function(e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            deleteMedia: function (mediaId) {
            var deferred = $q.defer();

            $http({
                url: mediaApi + "media/" + mediaId,
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