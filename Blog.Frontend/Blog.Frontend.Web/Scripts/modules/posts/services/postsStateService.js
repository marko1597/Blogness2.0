ngPosts.factory('postsStateService', [function () {
    var isAdding = true;
    var postItem = {};

    return {
        setPostItem: function (p) {
            this.isAdding = p == null;
            this.postItem = p;
        },

        getPostItem: function () {
            return this.postItem;
        }
    };
}]);