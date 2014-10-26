ngMedia.factory('mediaService', ["$http", "$q", "configProvider",
    function ($http, $q, configProvider) {
        var mediaApi = configProvider.getSettings().BlogApi == "" ?
            window.blogConfiguration.blogApi :
            configProvider.getSettings().BlogApi;

        var viewedMediaList = [];

        return {
            getMediaByAlbum: function (albumId) {
                var deferred = $q.defer();

                $http({
                    url: mediaApi + "album/" + albumId + "/media",
                    method: "GET"
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            getMediaByUser: function (userId) {
                var deferred = $q.defer();

                $http({
                    url: mediaApi + "users/" + userId + "/media",
                    method: "GET"
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            addMedia: function (media, albumName, username) {
                var deferred = $q.defer();

                $http({
                    url: mediaApi + "media?username=" + username + "&album=" + albumName,
                    method: "POST",
                    data: comment
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (e) {
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
            },

            addViewedMediaListFromPost: function (mediaList, postId) {
                var viewedMedia = {
                    postId: postId.toString(),
                    media: mediaList
                };

                var existingViewedMedia = _.where(viewedMediaList, { postId: postId });
                if (existingViewedMedia && existingViewedMedia.length > 0) {
                    var index = _.indexOf(viewedMediaList, existingViewedMedia[0]);
                    viewedMediaList.splice(index, 1);
                }
                viewedMediaList.push(viewedMedia);
            },

            getViewMediaListFromPost: function (postId) {
                var mediaList = _.where(viewedMediaList, { postId: postId });
                return mediaList[0] ? mediaList[0].media : [];
            },

            addViewedMediaListFromAlbum: function (mediaList, username, albumName) {
                var viewedMedia = {
                    username: username,
                    albumName: albumName.toLowerCase(),
                    media: mediaList
                };

                var existingViewedMedia = _.where(viewedMediaList, {
                    username: viewedMedia.username,
                    albumName: viewedMedia.albumName.toLowerCase()
                });

                if (existingViewedMedia && existingViewedMedia.length > 0) {
                    var index = _.indexOf(viewedMediaList, existingViewedMedia[0]);
                    viewedMediaList.splice(index, 1);
                }
                viewedMediaList.push(viewedMedia);
            },

            getViewMediaListFromAlbum: function (username, albumName) {
                if (albumName && username) {
                    var mediaList = _.where(viewedMediaList, { username: username, albumName: albumName.toLowerCase() });
                    return mediaList[0] ? mediaList[0].media : [];
                }
                return [];
            },
        };
    }
]);