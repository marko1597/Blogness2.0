ngUser.directive('userProfileNavigation', [function () {
    var ctrlFn = function ($scope) {
        $scope.aboutUrl = ($scope.username == null || $scope.username === "undefined") ?
            "/#/user" : "#/user/" + $scope.username;

        $scope.postsUrl = ($scope.username == null || $scope.username === "undefined") ?
            "/#/user/posts" : "#/user/" + $scope.username + "/posts";

        $scope.commentsUrl = ($scope.username == null || $scope.username === "undefined") ?
            "/#/user/comments" : "#/user/" + $scope.username + "/comments";

        $scope.mediaUrl = ($scope.username == null || $scope.username === "undefined") ?
            "/#/user/media" : "#/user/" + $scope.username + "/media";

        $scope.favoritesUrl = ($scope.username == null || $scope.username === "undefined") ?
            "/#/user/favorites" : "#/user/" + $scope.username + "/favorites";
    };
    ctrlFn.$inject = ["$scope"];

    return {
        restrict: 'EA',
        scope: { username: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "user/userProfileNavigation.html",
        controller: ctrlFn
    };
}]);
