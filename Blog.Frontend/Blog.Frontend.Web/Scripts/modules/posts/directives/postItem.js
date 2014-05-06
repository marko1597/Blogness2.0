ngPosts.directive('postItem', [function () {
    var ctrlFn = function ($scope, postsService) {
        $scope.post = $scope.data.Post;
        $scope.user = $scope.data.Post.User;
        
        $scope.getCommentPopover = function(commentId) {
            var comment = _.where($scope.comments, { CommentId: commentId });
            var user = comment.User.FirstName + " " + comment.User.LastName;
            return { "title": user, "content": comment.CommentMessage };
        };

        $scope.getPostSize = function() {
            return $scope.data.Width;
        };
    };
    ctrlFn.$inject = ["$scope", "postsService"];
    
    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        template:
            '<div id="post-item-{{post.PostId}}" ng-class="getPostSize()">' +
                '<div class="post-item-header">' +
                    '<div class="post-item-user" data-user-id="{{post.CreatedBy"}}">' +
                        '<img ng-src="{{user.Media.MediaUrl}}" />' +
                        '<p><a>{{post.User.UserName}}</a>' +
                            '<span>{{post.CreatedDate}}</span>' +
                        '</p>' +
                    '</div>' +
                    '<h4>{{post.PostTitle}}</h4>' +
                '</div>' +
                '<div class="post-item-body">' +
                    '<p>{{post.PostMessage}}</p>' +
                    '<div class="post-item-contents">' +
                        '<div ng-repeat="content in post.PostContents">' +
                            '<div class="post-item-content">' +
                                '<img ng-src="{{content.Media.MediaUrl}}" />' +
                            '</div>' +
                        '</div>' +
                    '</div>' +
                '</div>' +
                '<div class="post-item-details">' +
                    '<div post-likes data="{ PostLikes: post.PostLikes, PostId: post.PostId }"></div>' +
                    '<div post-tags data="{ Tags: post.Tags, PostId: post.PostId }"></div>' +
                '</div>' +
                '<div class="post-item-comments">' +
                    '<ul ticker data-enable-pause="true" data-pause-element="popover">' +
                        '<li ng-repeat="comment in post.Comments">' +
                            '<div post-item-comment comment="comment">' +
                        '</li>' +
                    '</ul>' +
                '</div>' +
            '</div>',
        controller: ctrlFn
    };
}]);
