ngMedia.factory('mediaService', ["$http", "$q", "configProvider",
    function ($http, $q, configProvider) {
        var baseApi = configProvider.getSettings().BlogApi == "" ?
            window.blogConfiguration.blogApi :
            configProvider.getSettings().BlogApi;

        var viewedMediaList = [];

        return {
            getMediaByAlbum: function (albumId) {
                var deferred = $q.defer();

                $http({
                    url: baseApi + "album/" + albumId + "/media",
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
                    url: baseApi + "users/" + userId + "/media",
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
                    url: baseApi + "media?username=" + username + "&album=" + albumName,
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
                    url: baseApi + "media/" + mediaId,
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
                var deferred = $q.defer();
                var self = this;
                var mediaList = _.where(viewedMediaList, { postId: postId });

                if (!mediaList[0] || mediaList[0].length === 0) {
                    $http({
                        url: baseApi + "posts/" + postId + "/contents",
                        method: "GET",
                    }).success(function (response) {
                        if (!response.Error) {
                            var responseMediaList = _.pluck(response, 'Media');
                            self.addViewedMediaListFromPost(responseMediaList, postId);

                            deferred.resolve(responseMediaList);
                        } else {
                            deferred.reject(response.Error);
                        }
                    }).error(function (error) {
                        deferred.reject(error);
                    });
                } else {
                    deferred.resolve(mediaList[0].media);
                }


                return deferred.promise;
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
                var deferred = $q.defer();

                if (albumName && username) {
                    var self = this;

                    var mediaList = _.where(viewedMediaList, { username: username, albumName: albumName.toLowerCase() });

                    if (!mediaList[0] || mediaList[0].length === 0) {
                        $http({
                            url: baseApi + "users/" + username + "/" + albumName,
                            method: "GET",
                        }).success(function (response) {
                            if (!response.Error) {
                                var responseMediaList = _.pluck(response, 'Media');
                                self.addViewedMediaListFromAlbum(responseMediaList, username, albumName);

                                deferred.resolve(responseMediaList);
                            } else {
                                deferred.reject(response.Error);
                            }
                        }).error(function (error) {
                            deferred.reject(error);
                        });
                    }
                    else {
                        deferred.resolve(mediaList[0].media);
                    }
                } else {
                    deferred.reject({ Message: "Invalid request!" });
                }

                return deferred.promise;
            },
        };
    }
]);