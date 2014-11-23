ngTags.factory('tagsService', ["$http", "$q", "configProvider", function ($http, $q, configProvider) {
    var tagsApi = configProvider.getSettings().BlogApi == "" ? window.blogConfiguration.blogApi + "tags" : configProvider.getSettings().BlogApi + "tags";

    return {
        getTagsByName: function (tag) {
            var deferred = $q.defer();

            $http({
                url: tagsApi + "/" + tag,
                method: "GET"
            }).success(function (response) {
                var tagItems = [];
                _.each(response, function (r) {
                    tagItems.push({ text: r.TagName });
                });
                deferred.resolve(tagItems);
            }).error(function () {
                deferred.reject("An error occurred!");
            });

            return deferred.promise;
        }
    };
}]);