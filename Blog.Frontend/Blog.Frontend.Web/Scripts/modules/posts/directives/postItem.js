postsModule.directive('postItem', [function () {
    var ctrlFn = function ($scope, $postsService) {
        $scope.comments = $scope.data.Comments;
        
        $scope.getCommentPopover = function(commentId) {
            var comment = _.where($scope.comments, { CommentId: commentId });
            var user = comment.User.FirstName + " " + comment.User.LastName;
            return { "title": user, "content": comment.CommentMessage };
        };
    };
    ctrlFn.$inject = ["$scope", "postsService"];
    
    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        template:
            '<div id="post-item-{{data.PostId}}">' +
                '<div class="post-item-header">' +
                    '<h4>{{data.PostTitle}}</h4>' +
                '</div>' +
                '<div class="post-item-body">' +
                    '<p>{{data.PostMessage}}</p>' +
                    '<div class="post-item-contents">' +
                        '<div ng-repeat="content in data.PostContents">' +
                            '<div class="post-item-content">' +
                                '<img ng-src="{{content.Media.MediaUrl}}" />' +
                            '</div>' +
                        '</div>' +
                    '</div>' +
                '</div>' +
                '<div class="post-item-details">' +
                    '<div post-likes data="{ PostLikes: data.PostLikes, PostId: data.PostId }"></div>' +
                    '<div post-tags data="{ Tags: data.Tags, PostId: data.PostId }"></div>' +
                '</div>' +
                '<div class="post-item-comments">' +
                    '<ul ticker data-enable-pause="true" data-pause-element="popover">' +
                        '<li ng-repeat="comment in data.Comments">' +
                            '<div post-item-comment comment="comment">' +
                        '</li>' +
                    '</ul>' +
                '</div>' +
            '</div>',
        controller: ctrlFn
    };
}]);
