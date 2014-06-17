ngComments.factory('commentsService', ["$http", "$q", "configProvider", "dateHelper",
    function ($http, $q, configProvider, dateHelper) {
        var commentsApi = configProvider.getSettings().BlogApi == "" ?
            window.blogConfiguration.blogApi + "Posts/" :
            configProvider.getSettings().BlogApi + "Posts/";

        return {
            getCommentsByPost: function (id) {
                var deferred = $q.defer();

                $http({
                    url: commentsApi + id + "/Comments",
                    method: "GET"
                }).success(function (response) {
                    _.each(response, function (c) {
                        c.DateDisplay = dateHelper.getDateDisplay(c.CreatedDate);
                        c.NameDisplay = c.User.FirstName + " " + c.User.LastName;
                        c.Url = "#";

                        _.each(c.Comments, function(r) {
                            r.NameDisplay = r.User.FirstName + " " + r.User.LastName;
                            r.Url = "#";
                        });
                    });
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            }
        };
    }
]);