ngCommunities.factory('communitiesService', ["$http", "$q", "configProvider", "dateHelper", 
    function ($http, $q, configProvider, dateHelper) {
        var baseApi = configProvider.getSettings().BlogApi == "" ?
            window.blogConfiguration.blogApi :
            configProvider.getSettings().BlogApi;

        var addCommunityViewData = function (community) {
            community.DateDisplay = dateHelper.getDateDisplay(community.CreatedDate);
            community.Url = "/#/community/" + community.Id;

            return community;
        };

        return {
            getById: function (id) {
                var deferred = $q.defer();

                $http({
                    url: baseApi + "community/" + id,
                    method: "GET"
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            getList: function () {
                var deferred = $q.defer();

                $http({
                    url: baseApi + "community/",
                    method: "GET"
                }).success(function (response) {
                    _.each(response, function (r) {
                        addCommunityViewData(r);
                    });
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            getMoreList: function (skip) {
                var deferred = $q.defer();

                $http({
                    url: baseApi + "community/more/" + skip,
                    method: "POST",
                    data: comment
                }).success(function (response) {
                    _.each(response, function (r) {
                        addCommunityViewData(r);
                    });
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            getCreatedByUser: function (userId) {
                var deferred = $q.defer();

                $http({
                    url: baseApi + "user/" + userId + "/communities/created",
                    method: "GET"
                }).success(function (response) {
                    _.each(response, function (r) {
                        addCommunityViewData(r);
                    });
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            getMoreCreatedByUser: function (userId, skip) {
                var deferred = $q.defer();

                $http({
                    url: baseApi + "user/" + userId + "/communities/created/more/" + skip,
                    method: "GET"
                }).success(function (response) {
                    _.each(response, function (r) {
                        addCommunityViewData(r);
                    });
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            getJoinedByUser: function (userId) {
                var deferred = $q.defer();

                $http({
                    url: baseApi + "user/" + userId + "/communities/joined",
                    method: "GET"
                }).success(function (response) {
                    _.each(response, function (r) {
                        addCommunityViewData(r);
                    });
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            getMoreJoinedByUser: function (userId, skip) {
                var deferred = $q.defer();

                $http({
                    url: baseApi + "user/" + userId + "/communities/joined/more/" + skip,
                    method: "GET"
                }).success(function (response) {
                    _.each(response, function (r) {
                        addCommunityViewData(r);
                    });
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            deleteCommunity: function (communityId) {
                var deferred = $q.defer();

                $http({
                    url: baseApi + "community/" + communityId,
                    method: "DELETE",
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (error) {
                    deferred.reject(error);
                });

                return deferred.promise;
            },

            addCommunity: function (community) {
                var deferred = $q.defer();

                $http({
                    url: baseApi + "community",
                    method: "POST",
                    data: community
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            updateCommunity: function (community) {
                var deferred = $q.defer();

                $http({
                    url: baseApi + "community",
                    method: "PUT",
                    data: community
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            }
        };
    }
]);