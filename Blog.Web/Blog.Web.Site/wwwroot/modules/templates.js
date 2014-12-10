angular.module('blog').run(['$templateCache', function($templateCache) {
  'use strict';

  $templateCache.put('communities.html',
    "<div class=\"row\">\r" +
    "\n" +
    "    <div class=\"col-xs-12\">\r" +
    "\n" +
    "        <div id=\"communities-list\" isotope-container isotope-item-resize resize-layout-only=\"false\" resize-container=\"communities-list\"\r" +
    "\n" +
    "             resize-broadcast=\"updateCommunityItemSize\">\r" +
    "\n" +
    "            <div ng-repeat=\"community in communities track by $index\" isotope-item community-list-item community=\"community\" ></div>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('errorpage.html',
    "<div id=\"error-page\">\r" +
    "\n" +
    "    <img ng-src=\"{{errorImage}}\" class=\"hidden-xs\" />\r" +
    "\n" +
    "    <div class=\"row\">\r" +
    "\n" +
    "        <div class=\"col-xs-12 col-sm-6 col-md-6 col-lg-6\">\r" +
    "\n" +
    "            <h1>\r" +
    "\n" +
    "                Something went horribly wrong here.\r" +
    "\n" +
    "                Send us a message on what happened.\r" +
    "\n" +
    "            </h1>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "        <div class=\"col-xs-12 col-sm-6 col-md-6 col-lg-6\">\r" +
    "\n" +
    "            <div class=\"send-mail card\">\r" +
    "\n" +
    "                {{error.Message}}\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "    <div class=\"row\">\r" +
    "\n" +
    "        <a href=\"/#/\">Click here to go back to main page</a>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('events.html',
    "<div class=\"jumbotron card\">\r" +
    "\n" +
    "    <h1>Events</h1>\r" +
    "\n" +
    "    <p>\r" +
    "\n" +
    "        Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.\r" +
    "\n" +
    "        Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor\r" +
    "\n" +
    "        in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident,\r" +
    "\n" +
    "        sunt in culpa qui officia deserunt mollit anim id est laborum.\r" +
    "\n" +
    "    </p>\r" +
    "\n" +
    "    <p><a class=\"btn btn-primary btn-lg\" role=\"button\">Clicking me does nothing</a></p>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('friends.html',
    "<div class=\"jumbotron card\">\r" +
    "\n" +
    "    <h1>Friends</h1>\r" +
    "\n" +
    "    <p>\r" +
    "\n" +
    "        Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.\r" +
    "\n" +
    "        Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor\r" +
    "\n" +
    "        in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident,\r" +
    "\n" +
    "        sunt in culpa qui officia deserunt mollit anim id est laborum.\r" +
    "\n" +
    "    </p>\r" +
    "\n" +
    "    <p><a class=\"btn btn-primary btn-lg\" role=\"button\">Clicking me does nothing</a></p>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('modifypost.html',
    "<div id=\"modify-post-main\">\r" +
    "\n" +
    "    <div class=\"modify-post-title\">\r" +
    "\n" +
    "        <h5>Title</h5>\r" +
    "\n" +
    "        <input ng-model=\"post.PostTitle\" />\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "    <div class=\"modify-post-message\">\r" +
    "\n" +
    "        <h5>Message</h5>\r" +
    "\n" +
    "        <div ng-switch on=\"dimensionMode\">\r" +
    "\n" +
    "            <textarea ng-switch-when=\"mobile\" ng-model=\"post.PostMessage\" id=\"post-message-editor\"></textarea>\r" +
    "\n" +
    "            <textarea ng-switch-default id=\"post-message-editor\" ckeditor=\"editorOptions\" ck-editor-helper ng-model=\"post.PostMessage\"\r" +
    "\n" +
    "                      data-browse-url=\"http://localhost/oldblog/Media/Browse?CKEditorFuncNum=1\" data-upload-url=\"{{uploadUrl}}\"\r" +
    "\n" +
    "                      data-image-window-width=\"640\" data-image-window-height=\"480\"></textarea>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "    <div class=\"modify-post-contents\">\r" +
    "\n" +
    "        <div class=\"btn btn-primary\" ng-click=\"launchMediaSelectionDialog()\">Add existing media</div>\r" +
    "\n" +
    "        <div file-upload uploader=\"uploader\"></div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "    <div class=\"modify-post-tags\">\r" +
    "\n" +
    "        <tags-input ng-model=\"Tags\" add-on-enter=\"true\" add-on-comma=\"true\" add-on-space=\"true\"\r" +
    "\n" +
    "                    on-tag-added=\"onTagAdded($tag)\" on-tag-removed=\"onTagRemoved($tag)\">\r" +
    "\n" +
    "            <auto-complete source=\"getTagsSource($query)\" highlight-matched-text=\"true\" debounce-delay=\"1500\"></auto-complete>\r" +
    "\n" +
    "        </tags-input>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "    <div class=\"modify-post-buttons\">\r" +
    "\n" +
    "        <button class=\"btn btn-primary\" ng-click=\"savePost()\">Save</button>\r" +
    "\n" +
    "        <button class=\"btn btn-danger\" ng-click=\"cancelPost()\">Cancel</button>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('posts.html',
    "<div class=\"row\">\r" +
    "\n" +
    "    <div class=\"col-xs-12 col-sm-12 col-md-12 col-lg-12\">\r" +
    "\n" +
    "        <div id=\"posts-main\" isotope-container isotope-item-resize resize-layout-only=\"false\" resize-container=\"posts-main\"\r" +
    "\n" +
    "             resize-broadcast=\"updatePostsSize\">\r" +
    "\n" +
    "            <div ng-repeat=\"post in posts track by $index\" isotope-item post-list-item data=\"{ Post: post, Width: size }\"></div>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('users.html',
    "<div id=\"user-profile-page\">\r" +
    "\n" +
    "    <div user-image user=\"user\" fullname=\"userFullName\"></div>\r" +
    "\n" +
    "    <div user-profile-navigation username=\"username\"></div>\r" +
    "\n" +
    "    \r" +
    "\n" +
    "    <div ui-view></div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('viewpost.html',
    "<div class=\"row\">\r" +
    "\n" +
    "    <div class=\"col-xs-12 col-sm-8 col-md-9 col-lg-9\">\r" +
    "\n" +
    "        <div id=\"post-{{post.Id}}\" class=\"card default view-post ng-cloak\">\r" +
    "\n" +
    "            <div post-header post=\"post\" user=\"post.User\" />\r" +
    "\n" +
    "            <div class=\"content\">\r" +
    "\n" +
    "                <p ng-bind-html=\"post.PostMessage\" ellipsis></p>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "            <div class=\"content\">\r" +
    "\n" +
    "                <div post-likes list=\"postLikes\" post-id=\"postId\"></div>\r" +
    "\n" +
    "                <div post-view-count list=\"viewCount\" post-id=\"postId\"></div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "            <div class=\"content\">\r" +
    "\n" +
    "                <div tag-item tag=\"tag\" ng-repeat=\"tag in post.Tags\"></div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "\r" +
    "\n" +
    "        <div id=\"post-contents-{{post.Id}}\" class=\"card masonry ng-cloak\" ng-show=\"hasContents()\">\r" +
    "\n" +
    "            <ul isotope-container isotope-item-resize reapply-layout-only=\"false\"\r" +
    "\n" +
    "                resize-large=\"48%\" resize-medium=\"96%\" resize-small=\"96%\">\r" +
    "\n" +
    "                <li ng-repeat=\"content in post.PostContents\" isotope-item class=\"post-item-content card\">\r" +
    "\n" +
    "                    <div media-item media=\"content.Media\" data-gallery-mode=\"true\"></div>\r" +
    "\n" +
    "                    <div class=\"captions\">\r" +
    "\n" +
    "                        <p>{{content.PostContentTitle}}</p>\r" +
    "\n" +
    "                        <p>{{content.PostContentText}}</p>\r" +
    "\n" +
    "                    </div>\r" +
    "\n" +
    "                </li>\r" +
    "\n" +
    "            </ul>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "\r" +
    "\n" +
    "        <div id=\"post-comments-{{post.Id}}\" class=\"comments\">\r" +
    "\n" +
    "            <div comments-container user=\"user\" postid=\"postId\" poster=\"post.User.UserName\"></div>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "\r" +
    "\n" +
    "    <div class=\"col-xs-12 col-sm-4 col-md-3 col-lg-3\">\r" +
    "\n" +
    "        <div class=\"card default hidden-xs\" post-view-navigator></div>\r" +
    "\n" +
    "        <div post-related-items parentpostid=\"postId\"></div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "\r" +
    "\n" +
    "    <div ui-view class=\"sticky top\"></div>\r" +
    "\n" +
    "</div>\r" +
    "\n"
  );


  $templateCache.put('comments/commentItem.html',
    "<div class=\"comment-item card animate fade-up\" data-comment-id=\"{{comment.Id}}\">\r" +
    "\n" +
    "    <div class=\"header\">\r" +
    "\n" +
    "        <div class=\"expand btn-header left\" ng-click=\"toggleReplies()\">\r" +
    "\n" +
    "            <i class=\"fa\" ng-show=\"canExpandComment()\" ng-class=\"isExpanded()\"></i>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "        <div class=\"details\">\r" +
    "\n" +
    "            <i class=\"fa fa-user\" ng-class=\"isFromPostOwner()\"></i>\r" +
    "\n" +
    "            <a user-info-popup user=\"comment.User\" data-placement=\"right\">{{comment.NameDisplay}}</a>\r" +
    "\n" +
    "            <span class=\"hidden-xs\">{{comment.DateDisplay}}</span>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "        <div class=\"likes btn-header right\" data-comment-id=\"{{comment.Id}}\" ng-click=\"likeComment()\">\r" +
    "\n" +
    "            <i class=\"fa\" ng-class=\"isUserLiked()\"></i>\r" +
    "\n" +
    "            <span>{{comment.CommentLikes == null ? 0 : comment.CommentLikes.length}}</span>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "        <div class=\"reply btn-header right\" ng-class=\"canReplyToComment()\" ng-click=\"showAddReply()\">\r" +
    "\n" +
    "            <span>Reply</span>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "    <p>{{comment.CommentMessage}}</p>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('comments/commentsAddNew.html',
    "<div class=\"comment-item-new animate fade-up\">\r" +
    "\n" +
    "    <textarea ng-class=\"commentMessageStyle()\" maxlength=\"140\" placeholder=\"Enter your comment here...\"\r" +
    "\n" +
    "              ng-model=\"comment.CommentMessage\" ng-focus=\"removeCommentMessageError()\"></textarea>\r" +
    "\n" +
    "    <div>\r" +
    "\n" +
    "        <button class=\"btn btn-primary hidden-xs\" ng-click=\"saveComment()\">Submit</button>\r" +
    "\n" +
    "        <button class=\"btn btn-primary fa fa-paper-plane visible-xs\" ng-click=\"saveComment()\"></button>\r" +
    "\n" +
    "        <button class=\"btn btn-danger hidden-xs\" ng-click=\"hideAddComment()\">Cancel</button>\r" +
    "\n" +
    "        <button class=\"btn btn-danger fa fa-times-circle visible-xs\" ng-click=\"hideAddComment()\"></button>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('comments/commentsContainer.html',
    "<div class=\"comments-container card default darken\">\r" +
    "\n" +
    "    <div class=\"header\">\r" +
    "\n" +
    "        <h5>Comments</h5>\r" +
    "\n" +
    "        <div class=\"btn-header right pull-right\" ng-click=\"toggleShowAddComment()\">Add comment</div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "\r" +
    "\n" +
    "    <div ng-show=\"showAddComment\" comments-add-new commentid=\"null\" postid=\"postid\" user=\"user\" parentpostid=\"postid\"></div>\r" +
    "\n" +
    "    <div comments-list postid=\"postid\" user=\"user\" poster=\"poster\"></div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('comments/commentsList.html',
    "<div class=\"comment-items-list\">\r" +
    "\n" +
    "    <div class=\"comments-empty-message alert\" ng-class=\"emptyMessageStyle()\" ng-show=\"showEmptyCommentsMessage()\">\r" +
    "\n" +
    "        <p>\r" +
    "\n" +
    "            {{getEmptyCommentsMessage()}}\r" +
    "\n" +
    "            <div ng-show=\"hasError\">\r" +
    "\n" +
    "                <a ng-click=\"getComments()\">Click here</a> to reload\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "        </p>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "    <div ng-repeat=\"comment in comments\">\r" +
    "\n" +
    "        <div comment-item comment=\"comment\" user=\"user\" poster=\"poster\" data-allow-reply=\"true\" data-allow-expand=\"true\"></div>\r" +
    "\n" +
    "        <div ng-show=\"comment.ShowAddReply\" class=\"comment-reply-new\" comments-add-new commentid=\"comment.Id\" postid=\"null\" user=\"user\" parentpostid=\"postid\"></div>\r" +
    "\n" +
    "        <div ng-repeat=\"reply in comment.Comments\" class=\"comment-replies\" ng-show=\"comment.ShowReplies\">\r" +
    "\n" +
    "            <div comment-item comment=\"reply\" user=\"user\" poster=\"poster\"></div>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>\r" +
    "\n"
  );


  $templateCache.put('communities/communityHeader.html',
    "<div class=\"header big row community-header\">\r" +
    "\n" +
    "    <div>\r" +
    "\n" +
    "        <img ng-src=\"{{community.Emblem.MediaUrl}}\" />\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "    <div>\r" +
    "\n" +
    "        <h4>\r" +
    "\n" +
    "            <i class=\"fa fa-edit edit\" ng-show=\"isEditable()\" ng-click=\"edit()\"></i>\r" +
    "\n" +
    "            <a href=\"{{community.Url}}\">{{community.Name}}</a>\r" +
    "\n" +
    "        </h4>\r" +
    "\n" +
    "        <p>{{community.Description}}</p>\r" +
    "\n" +
    "        <p>\r" +
    "\n" +
    "            Created by\r" +
    "\n" +
    "            <a user-info-popup user=\"community.Leader\" data-placement=\"bottom-left\">\r" +
    "\n" +
    "                @{{community.Leader.UserName}}\r" +
    "\n" +
    "            </a>\r" +
    "\n" +
    "            at {{community.DateDisplay}}\r" +
    "\n" +
    "        </p>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('communities/communityListItem.html',
    "<div id=\"community-item-{{community.Id}}\" ng-class=\"getItemSize()\" class=\"community-list-item card default darken\">\r" +
    "\n" +
    "    <div community-header community=\"community\"></div>\r" +
    "\n" +
    "    <div class=\"community-members\">\r" +
    "\n" +
    "        <div class=\"member-item\" ng-repeat=\"member in community.Members\">\r" +
    "\n" +
    "            <img ng-src=\"{{member.Picture.MediaUrl}}\" user-info-popup user=\"member\" data-placement=\"bottom-left\" />\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('error/errorDisplay.html',
    "<div id=\"blog-error-global\" class=\"hidden\">\r" +
    "\n" +
    "    <i class=\"close-error glyphicon glyphicon-remove\"></i>\r" +
    "\n" +
    "    <div class=\"message\">\r" +
    "\n" +
    "        <i class=\"fa fa-exclamation-circle\"></i>\r" +
    "\n" +
    "        <strong>Error!</strong><span>{{errorMessage}}</span>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>s"
  );


  $templateCache.put('header/headerMenu.html',
    "<div id=\"blog-header\">\r" +
    "\n" +
    "    <div class=\"navbar navbar-fixed-top\" role=\"navigation\">\r" +
    "\n" +
    "        <div class=\"navbar-header\">\r" +
    "\n" +
    "            <div class=\"navmenu-toggle\" ng-class=\"toggleClass\">\r" +
    "\n" +
    "                <span class=\"icon-bar\"></span>\r" +
    "\n" +
    "                <span class=\"icon-bar\"></span>\r" +
    "\n" +
    "                <span class=\"icon-bar\"></span>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "            <a class=\"navbar-brand\" snap-toggle>\r" +
    "\n" +
    "                <span class=\"blog-brand-icon\"></span>\r" +
    "\n" +
    "                Bloggity Blog\r" +
    "\n" +
    "            </a>\r" +
    "\n" +
    "            <div class=\"messages-toggle hidden-sm hidden-md hidden-lg pull-right\" snap-toggle=\"right\">\r" +
    "\n" +
    "                <i class=\"fa fa-comments\"></i>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "        <div class=\"collapse navbar-collapse\" id=\"blog-header-collapsible\" data-toggle=\"false\">\r" +
    "\n" +
    "            <ul class=\"nav navbar-nav\">\r" +
    "\n" +
    "                <li>\r" +
    "\n" +
    "                    <a ng-click=\"goAddNewPost()\">\r" +
    "\n" +
    "                        <i class=\"fa fa-plus-circle\"></i>Add new post\r" +
    "\n" +
    "                    </a>\r" +
    "\n" +
    "                </li>\r" +
    "\n" +
    "                <li class=\"dropdown\">\r" +
    "\n" +
    "                    <a class=\"dropdown-toggle\" data-toggle=\"dropdown\">Testing stuff <b class=\"caret\"></b></a>\r" +
    "\n" +
    "                    <ul class=\"dropdown-menu\">\r" +
    "\n" +
    "                        <li class=\"header-dropdown-item\">\r" +
    "\n" +
    "                            <a data-anijs=\"if: mouseover, do: pulse animated\" ng-click=\"testDisplayError()\">Display error</a>\r" +
    "\n" +
    "                        </li>\r" +
    "\n" +
    "                        <li class=\"header-dropdown-item\">\r" +
    "\n" +
    "                            <a ng-click=\"getUserInfo()\">Get user info</a>\r" +
    "\n" +
    "                        </li>\r" +
    "\n" +
    "                        <li class=\"header-dropdown-item\">\r" +
    "\n" +
    "                            <a ng-click=\"toggleSocketDebugger()\">Toggle socket debugger</a>\r" +
    "\n" +
    "                        </li>\r" +
    "\n" +
    "                        <li class=\"header-dropdown-item\">\r" +
    "\n" +
    "                            <a ng-click=\"showLoginForm()\">Login form</a>\r" +
    "\n" +
    "                        </li>\r" +
    "\n" +
    "                    </ul>\r" +
    "\n" +
    "                </li>\r" +
    "\n" +
    "            </ul>\r" +
    "\n" +
    "            <div class=\"nav navbar-nav navbar-right\">\r" +
    "\n" +
    "                <div class=\"messages-toggle\" snap-toggle=\"right\">\r" +
    "\n" +
    "                    <i class=\"fa fa-comments\"></i>\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "                <div logged-user></div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "\r" +
    "\n" +
    "    <nav class=\"add-post visible-xs\" ng-show=\"showAddPostButton()\">\r" +
    "\n" +
    "        <a ng-click=\"goAddNewPost()\">\r" +
    "\n" +
    "            <i class=\"fa fa-plus\"></i>\r" +
    "\n" +
    "        </a>\r" +
    "\n" +
    "    </nav>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('login/loggedUser.html',
    "<div id=\"logged-user-info\">\r" +
    "\n" +
    "    <div ng-show=\"isLoggedIn()\">\r" +
    "\n" +
    "        <div class=\"btn-group\">\r" +
    "\n" +
    "            <button type=\"button\" class=\"btn btn-primary dropdown-toggle\" data-toggle=\"dropdown\">\r" +
    "\n" +
    "                <i class=\"fa fa-user\"></i>\r" +
    "\n" +
    "                {{user.FullName}}\r" +
    "\n" +
    "                <span class=\"caret\"></span>\r" +
    "\n" +
    "            </button>\r" +
    "\n" +
    "            <ul class=\"dropdown-menu\" role=\"menu\">\r" +
    "\n" +
    "                <li>\r" +
    "\n" +
    "                    <a href=\"/#/user\" ng-click=\"goToProfile()\">\r" +
    "\n" +
    "                        <i class=\"fa fa-edit\"></i>\r" +
    "\n" +
    "                        Profile\r" +
    "\n" +
    "                    </a>\r" +
    "\n" +
    "                </li>\r" +
    "\n" +
    "                <li>\r" +
    "\n" +
    "                    <a href=\"\" ng-click=\"logout()\">\r" +
    "\n" +
    "                        <i class=\"fa fa-lock\"></i>\r" +
    "\n" +
    "                        Logout\r" +
    "\n" +
    "                    </a>\r" +
    "\n" +
    "                </li>\r" +
    "\n" +
    "            </ul>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "    <div ng-show=\"!isLoggedIn()\">\r" +
    "\n" +
    "        <button class=\"btn btn-primary\" ng-click=\"launchLoginForm()\">Click here to log in or register</button>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('login/loginForm.html',
    "<form name=\"login-form\" class=\"login-form\" action=\"\" method=\"post\">\r" +
    "\n" +
    "    <div class=\"header\">\r" +
    "\n" +
    "        <h1>Bloggity Blog</h1>\r" +
    "\n" +
    "        <span>Fill out the form below to login to Bloggity.</span>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "    <div class=\"content\">\r" +
    "\n" +
    "        <label class=\"error-message\" ng-show=\"showErrorMessage()\">{{errorMessage}}</label>\r" +
    "\n" +
    "        <div>\r" +
    "\n" +
    "            <input name=\"username\" type=\"text\" class=\"input username\" ng-class=\"hasError()\" placeholder=\"Username\" ng-model=\"username\" ng-enter=\"login()\" />\r" +
    "\n" +
    "            <div class=\"user-icon\"></div>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "        <div>\r" +
    "\n" +
    "            <input name=\"password\" type=\"password\" class=\"input password\" ng-class=\"hasError()\" placeholder=\"Password\" ng-model=\"password\" ng-enter=\"login()\" />\r" +
    "\n" +
    "            <div class=\"pass-icon\"></div>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "        <input name=\"username\" type=\"checkbox\" ng-model=\"rememberMe\" name=\"rememberMe\">\r" +
    "\n" +
    "        <span>Remember me</span>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "    <div class=\"footer\">\r" +
    "\n" +
    "        <input type=\"button\" name=\"login\" value=\"Login\" class=\"button\" ng-click=\"login()\" />\r" +
    "\n" +
    "        <input type=\"button\" name=\"register\" value=\"Register\" class=\"register\" ng-show=\"isModal()\" ng-click=\"showRegisterForm()\"\r" +
    "\n" +
    "               data-placement=\"top\" title=\"{{registerPopover.title}}\" data-animation=\"am-flip-x\" data-content=\"{{registerPopover.content}}\" data-trigger=\"hover\" bs-popover />\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</form>"
  );


  $templateCache.put('login/loginFormModal.html',
    "<div id=\"login-form-modal\" class=\"modal\" tabindex=\"-1\" role=\"dialog\">\r" +
    "\n" +
    "    <div class=\"modal-dialog\">\r" +
    "\n" +
    "        <div class=\"modal-content\">\r" +
    "\n" +
    "            <div class=\"modal-body\">\r" +
    "\n" +
    "                <div class=\"flipper\">\r" +
    "\n" +
    "                    <login-form modal=\"true\"></login-form>\r" +
    "\n" +
    "                    <register-form modal=\"true\"></register-form>\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('login/registerForm.html',
    "<form name=\"register-form\" class=\"login-form register\" action=\"\" method=\"post\">\r" +
    "\n" +
    "    <div class=\"panel panel-default\">\r" +
    "\n" +
    "        <div class=\"panel-heading\">\r" +
    "\n" +
    "            <h3 class=\"panel-title\">Fill out the form below to register to Bloggity.</h3>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "        <div class=\"panel-body\">\r" +
    "\n" +
    "            <div class=\"content\">\r" +
    "\n" +
    "                <div class=\"row\">\r" +
    "\n" +
    "                    <div class=\"col-xs-12 col-sm-12 col-md-12 col-lg-12\">\r" +
    "\n" +
    "                        <div class=\"alert\" ng-class=\"messageDisplay.type\" ng-show=\"messageDisplay.show\">\r" +
    "\n" +
    "                            <button type=\"button\" class=\"close\" data-dismiss=\"alert\">\r" +
    "\n" +
    "                                <span aria-hidden=\"true\">&times;</span><span class=\"sr-only\"></span>\r" +
    "\n" +
    "                            </button>\r" +
    "\n" +
    "                            <p>{{messageDisplay.message}}</p>\r" +
    "\n" +
    "                        </div>\r" +
    "\n" +
    "                    </div>\r" +
    "\n" +
    "                    <div class=\"col-xs-12 col-sm-12 col-md-12 col-lg-12\">\r" +
    "\n" +
    "                        <p class=\"error-message\"></p>\r" +
    "\n" +
    "                        <input name=\"username\" type=\"text\" class=\"input\" ng-class=\"hasError('username')\" placeholder=\"Username\" ng-model=\"username\" ng-enter=\"register()\" />\r" +
    "\n" +
    "                    </div>\r" +
    "\n" +
    "                    <div class=\"col-xs-12 col-sm-12 col-md-12 col-lg-12\">\r" +
    "\n" +
    "                        <p class=\"error-message\"></p>\r" +
    "\n" +
    "                        <input name=\"password\" type=\"password\" class=\"input\" ng-class=\"hasError('password')\" placeholder=\"Password\" ng-model=\"password\" ng-enter=\"register()\" />\r" +
    "\n" +
    "                    </div>\r" +
    "\n" +
    "                    <div class=\"col-xs-12 col-sm-12 col-md-12 col-lg-12\">\r" +
    "\n" +
    "                        <p class=\"error-message\"></p>\r" +
    "\n" +
    "                        <input name=\"confirmpassword\" type=\"password\" class=\"input\" ng-class=\"hasError('confirmpassword')\" placeholder=\"Confirm Password\" ng-model=\"confirmPassword\" ng-enter=\"register()\" />\r" +
    "\n" +
    "                    </div>\r" +
    "\n" +
    "                    <div class=\"col-xs-12 col-sm-12 col-md-12 col-lg-12\">\r" +
    "\n" +
    "                        <p class=\"error-message\"></p>\r" +
    "\n" +
    "                        <input name=\"firstname\" type=\"text\" class=\"input\" ng-class=\"hasError('firstname')\" placeholder=\"First Name\" ng-model=\"firstName\" ng-enter=\"register()\" />\r" +
    "\n" +
    "                    </div>\r" +
    "\n" +
    "                    <div class=\"col-xs-12 col-sm-12 col-md-12 col-lg-12\">\r" +
    "\n" +
    "                        <p class=\"error-message\"></p>\r" +
    "\n" +
    "                        <input name=\"lastname\" type=\"text\" class=\"input\" ng-class=\"hasError('lastname')\" placeholder=\"Last Name\" ng-model=\"lastName\" ng-enter=\"register()\" />\r" +
    "\n" +
    "                    </div>\r" +
    "\n" +
    "                    <div class=\"col-xs-12 col-sm-12 col-md-12 col-lg-12\">\r" +
    "\n" +
    "                        <p class=\"error-message\"></p>\r" +
    "\n" +
    "                        <input name=\"email\" type=\"text\" class=\"input\" ng-class=\"hasError('email')\" placeholder=\"Email\" ng-model=\"email\" ng-enter=\"register()\" />\r" +
    "\n" +
    "                    </div>\r" +
    "\n" +
    "                    <div class=\"col-xs-12 col-sm-12 col-md-12 col-lg-12\">\r" +
    "\n" +
    "                        <p class=\"error-message\"></p>\r" +
    "\n" +
    "                        <input name=\"birthdate\" type=\"text\" class=\"input\" ng-class=\"hasError('birthdate')\" placeholder=\"Birthdate\"\r" +
    "\n" +
    "                               ng-model=\"birthDate\" ng-enter=\"register()\" bs-datepicker data-placement=\"top\" data-iconLeft=\"fa fa-calendar\" data-autoclose=\"true\" />\r" +
    "\n" +
    "                    </div>\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "\r" +
    "\n" +
    "    <div class=\"row\" ng-show=\"isModal()\">\r" +
    "\n" +
    "        <div class=\"col-xs-12 col-sm-12 col-md-12 col-lg-12\">\r" +
    "\n" +
    "            <div class=\"footer\">\r" +
    "\n" +
    "                <input type=\"button\" name=\"confirm\" class=\"button\" value=\"Register\" ng-click=\"register()\" />\r" +
    "\n" +
    "                <input type=\"button\" name=\"backToLogin\" class=\"back\" value=\"Go Back\" ng-click=\"showLoginForm()\" />\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</form>"
  );


  $templateCache.put('media/albumGroup.html',
    "<div class=\"album-group\">\r" +
    "\n" +
    "    <div class=\"header\">\r" +
    "\n" +
    "        <div class=\"btn-header left\" ng-click=\"toggleExpanded()\">\r" +
    "\n" +
    "            <i class=\"fa\" ng-class=\"toggleExpandClass()\"></i>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "\r" +
    "\n" +
    "        <h5 ng-show=\"!album.IsEditing\">{{album.AlbumName}}</h5>\r" +
    "\n" +
    "        <input type=\"text\" ng-model=\"album.AlbumName\" ng-show=\"album.IsEditing\"\r" +
    "\n" +
    "               placeholder=\"Enter new album name here...\" />\r" +
    "\n" +
    "\r" +
    "\n" +
    "        <label class=\"label label-primary hidden-xs\">{{album.CreatedDateDisplay}}</label>\r" +
    "\n" +
    "        <label class=\"label label-info hidden-xs\" ng-show=\"album.IsUserDefault\">Default Album</label>\r" +
    "\n" +
    "        \r" +
    "\n" +
    "        <div class=\"btn-header right pull-right\" ng-show=\"album.IsEditing\" ng-click=\"cancelEditAlbum()\"\r" +
    "\n" +
    "             data-placement=\"left\" data-type=\"info\" data-trigger=\"hover\" data-title=\"Cancel editing\" bs-tooltip=\"tooltip\">\r" +
    "\n" +
    "            <i class=\"fa fa-times\"></i>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "        <div class=\"btn-header right pull-right\" ng-show=\"album.IsEditing\" ng-click=\"saveAlbum()\"\r" +
    "\n" +
    "             data-placement=\"left\" data-type=\"info\" data-trigger=\"hover\" data-title=\"Save changes\" bs-tooltip=\"tooltip\">\r" +
    "\n" +
    "            <i class=\"fa fa-save\"></i>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "        <div class=\"btn-header right pull-right\" ng-show=\"!album.IsEditing\" ng-click=\"editAlbum()\"\r" +
    "\n" +
    "             data-placement=\"left\" data-type=\"info\" data-trigger=\"hover\" data-title=\"Edit name of this album\" bs-tooltip=\"tooltip\">\r" +
    "\n" +
    "            <i class=\"fa fa-edit\"></i>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "        <div class=\"btn-header right pull-right\" ng-show=\"!album.IsEditing\" ng-click=\"deleteAlbum()\"\r" +
    "\n" +
    "             data-placement=\"left\" data-type=\"info\" data-trigger=\"hover\" data-title=\"Delete this album\" bs-tooltip=\"tooltip\">\r" +
    "\n" +
    "            <i class=\"fa fa-trash\"></i>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "        <div class=\"btn-header right pull-right\" ng-show=\"!album.IsEditing\"\r" +
    "\n" +
    "             data-placement=\"left\" data-type=\"info\" data-trigger=\"hover\" data-title=\"Add new media to this album\" bs-tooltip=\"tooltip\">\r" +
    "\n" +
    "            <label class=\"fa fa-picture-o\">\r" +
    "\n" +
    "                <input nv-file-select type=\"file\" uploader=\"uploader\" />\r" +
    "\n" +
    "            </label>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "    <div class=\"body\" ng-show=\"isExpanded\">\r" +
    "\n" +
    "        <ul isotope-container isotope-item-resize reapply-layout-only=\"false\"\r" +
    "\n" +
    "            resize-large=\"24%\" resize-medium=\"48%\" resize-small=\"96%\">\r" +
    "\n" +
    "            <li ng-repeat=\"media in album.Media\" isotope-item class=\"media-item-container\">\r" +
    "\n" +
    "                <div class=\"animate fade-up\" media-item media=\"media\" user=\"user\" album-name=\"album.AlbumName\" \r" +
    "\n" +
    "                     data-mode=\"thumbnail\" data-allow-delete=\"true\" data-gallery-mode=\"true\"></div>\r" +
    "\n" +
    "                <p class=\"details\">\r" +
    "\n" +
    "                    Uploaded on {{media.CreatedDateDisplay}}\r" +
    "\n" +
    "                </p>\r" +
    "\n" +
    "            </li>\r" +
    "\n" +
    "        </ul>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('media/mediaDeleteDialog.html',
    "<div class=\"modal\" tabindex=\"-1\" role=\"dialog\">\r" +
    "\n" +
    "    <div class=\"modal-dialog\">\r" +
    "\n" +
    "        <div class=\"modal-content\">\r" +
    "\n" +
    "            <div class=\"modal-header\" ng-show=\"title\">\r" +
    "\n" +
    "                <button type=\"button\" class=\"close\" ng-click=\"$hide()\">&times;</button>\r" +
    "\n" +
    "                <h4 class=\"modal-title\" ng-bind=\"title\"></h4>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "            <div class=\"modal-body\" ng-bind=\"content\">\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "            <div class=\"modal-footer\">\r" +
    "\n" +
    "                <div class='btn btn-success' ng-click=\"confirmDelete()\">Yep! I'm positive.</div>\r" +
    "\n" +
    "                <div class='btn btn-danger' ng-click=\"$hide()\">Nope!</div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('media/mediaGallery.html',
    "<div id=\"media-gallery\" class=\"modal\" tabindex=\"-1\" role=\"dialog\">\r" +
    "\n" +
    "    <div class=\"modal-dialog\">\r" +
    "\n" +
    "        <div class=\"modal-content\">\r" +
    "\n" +
    "            <button type=\"button\" ng-click=\"closeGallery()\">&times;</button>\r" +
    "\n" +
    "            <div class=\"modal-body\">\r" +
    "\n" +
    "                <div ng-style=\"getWindowHeight()\" ng-show=\"showMediaGallery()\">\r" +
    "\n" +
    "                    <slick dots=\"true\" infinite=\"true\" speed=\"300\" slides-to-show=\"1\" slides-to-scroll=\"1\" arrows=\"true\">\r" +
    "\n" +
    "                        <div ng-repeat=\"mediaItem in mediaList track by $index\" media-item media=\"mediaItem\" data-gallery-mode=\"false\">\r" +
    "\n" +
    "                        </div>\r" +
    "\n" +
    "                    </slick>\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>\r" +
    "\n"
  );


  $templateCache.put('media/mediaGroupedList.html',
    "<div id=\"card default darken media-grouped-list\">\r" +
    "\n" +
    "    <div album-group album=\"album\" user=\"user\" add=\"false\" ng-repeat=\"album in albums\"></div>\r" +
    "\n" +
    "    <div class=\"album-group new\" ng-click=\"addAlbum()\" ng-show=\"!isAdding\">\r" +
    "\n" +
    "        <div class=\"header\">\r" +
    "\n" +
    "            <h5><i class=\"fa fa-plus-circle\"></i>Add new album</h5>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('media/mediaItem.html',
    "<div class=\"media-item\" ng-switch on=\"viewMode()\">\r" +
    "\n" +
    "    <div ng-switch-when=\"thumbnail\" ng-class=\"isCropped()\" ng-style=\"getThumbnailUrl()\">\r" +
    "\n" +
    "        <img ng-src=\"{{media.ThumbnailUrl}}\" />\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "    <div ng-switch-default ng-switch=\"getContentType(media.MediaType)\">\r" +
    "\n" +
    "        <video-player ng-switch-when=\"video\" media=\"media\"></video-player>\r" +
    "\n" +
    "        <img ng-switch-default ng-src=\"{{media.MediaUrl}}\" ng-class=\"isCropped()\" ng-style=\"getThumbnailUrl()\" />\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "    <button class=\"btn btn-danger\" ng-show=\"toggleDelete()\" ng-click=\"deleteMedia()\">\r" +
    "\n" +
    "        <i class=\"fa fa-trash\"></i>\r" +
    "\n" +
    "    </button>\r" +
    "\n" +
    "    <h5 ng-show=\"toggleGallery()\" ng-click=\"viewAsGallery()\" ng-class=\"isVideo()\">\r" +
    "\n" +
    "        <i class=\"fa fa-search-plus\"></i>View as gallery\r" +
    "\n" +
    "    </h5>\r" +
    "\n" +
    "</div>\r" +
    "\n"
  );


  $templateCache.put('media/mediaSelectionDialog.html',
    "<div id=\"media-selection-dialog\" class=\"modal\" tabindex=\"-1\" role=\"dialog\">\r" +
    "\n" +
    "    <div class=\"modal-dialog\">\r" +
    "\n" +
    "        <div class=\"modal-content\">\r" +
    "\n" +
    "            <div class=\"modal-header\" ng-show=\"title\">\r" +
    "\n" +
    "                <button type=\"button\" class=\"close\" ng-click=\"$hide()\">&times;</button>\r" +
    "\n" +
    "                <h4 class=\"modal-title\" ng-bind=\"title\"></h4>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "            <div class=\"modal-body\">\r" +
    "\n" +
    "                <ul>\r" +
    "\n" +
    "                    <li ng-repeat=\"album in albums\">\r" +
    "\n" +
    "                        <div class=\"header\">\r" +
    "\n" +
    "                            <h5>{{album.AlbumName}}</h5>\r" +
    "\n" +
    "                        </div>\r" +
    "\n" +
    "                        <div class=\"body\">\r" +
    "\n" +
    "                            <div ng-repeat=\"media in album.Media\" class=\"center-cropped\" ng-style=\"getThumbnailUrl(media)\"\r" +
    "\n" +
    "                                 ng-click=\"toggleMediaSelectionToExistingContents(media)\">\r" +
    "\n" +
    "                                <button class=\"btn\" ng-class=\"getMediaToggleButtonStyle(media)\">\r" +
    "\n" +
    "                                    <i class=\"fa\" ng-class=\"getMediaToggleButtonIcon(media)\"></i>\r" +
    "\n" +
    "                                </button>\r" +
    "\n" +
    "                                <img ng-src=\"{{media.ThumbnailUrl}}\" />\r" +
    "\n" +
    "                            </div>\r" +
    "\n" +
    "                        </div>\r" +
    "\n" +
    "                    </li>\r" +
    "\n" +
    "                </ul>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "            <div class=\"modal-footer\">\r" +
    "\n" +
    "                <div class='btn btn-success' ng-click=\"$hide()\">\r" +
    "\n" +
    "                    <i class=\"fa fa-thumbs-up\"></i>\r" +
    "\n" +
    "                    Done adding\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('messaging/chatWindow.html',
    "<div class=\"chat-window animate fade-right\" ng-show=\"chatWindowVisibility()\" ng-style=\"{ 'height' : elemHeight }\">\r" +
    "\n" +
    "    <div class=\"header\">\r" +
    "\n" +
    "        <h5>{{recipientName()}}</h5>\r" +
    "\n" +
    "        <div class=\"btn-header right pull-right\" ng-click=\"hideChatWindow()\">\r" +
    "\n" +
    "            <i class=\"fa fa-times\"></i>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "    <div class=\"body\" scroll-glue ng-style=\"{ 'height' : bodyHeight() }\">\r" +
    "\n" +
    "        <p ng-show=\"showViewMoreMessagesButton()\" ng-click=\"getMoreChatMessages()\">Show more messages</p>\r" +
    "\n" +
    "        <div ng-repeat=\"chatMessage in chatMessages\">\r" +
    "\n" +
    "            <div class=\"chat-message animate fade-down\" ng-class=\"isFromRecipient(chatMessage)\">\r" +
    "\n" +
    "                <p>{{chatMessage.Text}}</p>\r" +
    "\n" +
    "                <div class=\"details\">\r" +
    "\n" +
    "                    <i class=\"fa fa-clock-o\"></i>\r" +
    "\n" +
    "                    <span>{{chatMessage.CreatedDateDisplay}}</span>\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "\r" +
    "\n" +
    "    <div class=\"new-message\">\r" +
    "\n" +
    "        <textarea placeholder=\"Enter message...\" ng-model=\"newMessage\" ng-enter=\"sendChatMessage()\" />\r" +
    "\n" +
    "        <button class=\"btn btn-primary pull-right\" ng-click=\"sendChatMessage()\">\r" +
    "\n" +
    "            <i class=\"fa fa-send\"></i>\r" +
    "\n" +
    "        </button>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('messaging/messagesPanel.html',
    "<div id=\"messages-panel\" ng-style=\"{ 'height' : elemHeight }\">\r" +
    "\n" +
    "    <div class=\"header\">\r" +
    "\n" +
    "        <h5>Messages</h5>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "    <div class=\"body\" ng-style=\"{ 'height' : bodyHeight }\">\r" +
    "\n" +
    "        <div ng-show=\"!isLoggedIn()\" class=\"alert alert-warning\">\r" +
    "\n" +
    "            <h4>Not logged in</h4>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "        <div ng-show=\"isLoggedIn()\" class=\"messages-list\">\r" +
    "\n" +
    "            <div class=\"message-item\" data-user-id=\"{{messageItem.User.Id}}\" ng-repeat=\"messageItem in messagesList\" ng-click=\"launchChatWindow(messageItem)\">\r" +
    "\n" +
    "                <div class=\"chat-user-image\">\r" +
    "\n" +
    "                    <img ng-src=\"{{messageItem.User.Picture.MediaUrl}}\" />\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "                <div class=\"message-body\">\r" +
    "\n" +
    "                    <p>\r" +
    "\n" +
    "                        <span>{{messageItem.User.NameDisplay}}</span>\r" +
    "\n" +
    "                        {{messageItem.LastChatMessage.Text}}\r" +
    "\n" +
    "                    </p>\r" +
    "\n" +
    "                    <span>{{messageItem.LastChatMessage.CreatedDateDisplay}}</span>\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('navigation/navigationMenu.html',
    "<nav class=\"navigation-menu\" id=\"navigation-menu\">\r" +
    "\n" +
    "    <ul>\r" +
    "\n" +
    "        <li class=\"visible-xs\">\r" +
    "\n" +
    "            <a>\r" +
    "\n" +
    "                <div logged-user></div>\r" +
    "\n" +
    "            </a>\r" +
    "\n" +
    "        </li>\r" +
    "\n" +
    "        <li>\r" +
    "\n" +
    "            <a>\r" +
    "\n" +
    "                <form role=\"search\">\r" +
    "\n" +
    "                    <input type=\"text\" class=\"form-control\" placeholder=\"Search\">\r" +
    "\n" +
    "                    <i class=\"fa fa-search\">\r" +
    "\n" +
    "                        <input type=\"submit\" class=\"hidden\" />\r" +
    "\n" +
    "                    </i>\r" +
    "\n" +
    "                </form>\r" +
    "\n" +
    "            </a>\r" +
    "\n" +
    "        </li>\r" +
    "\n" +
    "        <li ng-repeat=\"navigationItem in navigationItems\">\r" +
    "\n" +
    "            <a class=\"icon\" href=\"{{navigationItem.href}}\" ng-click=\"toggleNavigation()\">\r" +
    "\n" +
    "                <i class=\"fa\" ng-class=\"navigationItem.icon\"></i>\r" +
    "\n" +
    "                {{navigationItem.text}}\r" +
    "\n" +
    "            </a>\r" +
    "\n" +
    "        </li>\r" +
    "\n" +
    "    </ul>\r" +
    "\n" +
    "</nav>"
  );


  $templateCache.put('posts/postContents.html',
    "<div>\r" +
    "\n" +
    "    <slick dots=\"true\" infinite=\"true\" speed=\"300\" slides-to-show=\"1\" slides-to-scroll=\"1\" arrows=\"true\">\r" +
    "\n" +
    "        <div class=\"post-item-content\" ng-repeat=\"content in contents\">\r" +
    "\n" +
    "            <div media-item media=\"content.Media\" data-mode=\"thumbnail\"></div>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </slick>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('posts/postHeader.html',
    "<div class=\"header big row\" data-user-id=\"{{post.CreatedBy}}\">\r" +
    "\n" +
    "    <div>\r" +
    "\n" +
    "        <img ng-src=\"{{user.Picture.MediaUrl}}\" />\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "    <div>\r" +
    "\n" +
    "        <h4>\r" +
    "\n" +
    "            <i class=\"fa fa-edit edit\" ng-show=\"isEditable()\" ng-click=\"editPost()\"></i>\r" +
    "\n" +
    "            <a href=\"{{post.Url}}\">{{post.PostTitle}}</a>\r" +
    "\n" +
    "        </h4>\r" +
    "\n" +
    "        <p>\r" +
    "\n" +
    "            <a user-info-popup user=\"user\">@{{user.UserName}}</a>\r" +
    "\n" +
    "            <span>{{post.DateDisplay}}</span>\r" +
    "\n" +
    "        </p>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('posts/postLikes.html',
    "<div class=\"post-likes\">\r" +
    "\n" +
    "    <div class=\"wrapper\" data-placement=\"right\" data-type=\"info\" data-animation=\"am-flip-x\" bs-tooltip=\"tooltip\" ng-click=\"likePost()\">\r" +
    "\n" +
    "        <span><i class=\"fa\" ng-class=\"isUserLiked()\"></i></span>\r" +
    "\n" +
    "        <span>{{postLikes.length}}</span>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('posts/postListItem.html',
    "<div id=\"post-item-{{post.Id}}\" ng-class=\"getPostSize()\" class=\"post-list-item card default\">\r" +
    "\n" +
    "    <div post-header post=\"post\" user=\"post.User\" />\r" +
    "\n" +
    "    <div class=\"content\">\r" +
    "\n" +
    "        <p ng-bind-html=\"post.PostMessage\" ellipsis></p>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "    <div class=\"content\">\r" +
    "\n" +
    "        <div post-contents contents=\"post.PostContents\"></div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "    <div class=\"content\">\r" +
    "\n" +
    "        <div post-likes list=\"postLikes\" post-id=\"post.Id\"></div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "    <div class=\"comments content\" ng-show=\"hasComments\">\r" +
    "\n" +
    "        <ul ticker data-enable-pause=\"true\" data-pause-element=\"popover\">\r" +
    "\n" +
    "            <li ng-repeat=\"comment in comments\">\r" +
    "\n" +
    "                <div post-list-item-comment comment=\"comment\"></div>\r" +
    "\n" +
    "            </li>\r" +
    "\n" +
    "        </ul>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "    <div class=\"tags content\" ng-show=\"hasTags\">\r" +
    "\n" +
    "        <div tag-item tag=\"tag\" ng-repeat=\"tag in post.Tags\"></div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('posts/postListItemComment.html',
    "<div data-comment-id=\"{{comment.Id}}\" class=\"post-item-comment\">\r" +
    "\n" +
    "    <p><a href=\"{{user.url}}\">{{user.name}}</a></p>\r" +
    "\n" +
    "    <p data-pause-trigger data-placement=\"top\" data-animation=\"am-flip-x\" id=\"post-comment-{{comment.Id}}\"\r" +
    "\n" +
    "           data-target=\".post-item-comment[data-comment-id='{{comment.Id}}']\" bs-popover=\"popover\">{{comment.CommentMessage}}</p>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('posts/postRelatedItem.html',
    "<div class=\"post-related-item card\">\r" +
    "\n" +
    "    <div class=\"header\">\r" +
    "\n" +
    "        <a href=\"{{post.Url}}\">{{post.PostTitle}}</a>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "    <div class=\"body\">\r" +
    "\n" +
    "        <div media-item media=\"post.PostContents[0].Media\" data-crop=\"true\" data-mode=\"thumbnail\"></div>\r" +
    "\n" +
    "        <div class=\"details\">\r" +
    "\n" +
    "            <p>\r" +
    "\n" +
    "                <span>{{post.PostContents.length}} images by</span>\r" +
    "\n" +
    "                <a>{{post.User.UserName}}</a>\r" +
    "\n" +
    "            </p>\r" +
    "\n" +
    "            <p>\r" +
    "\n" +
    "                <span>{{post.DateDisplay}}</span>\r" +
    "\n" +
    "            </p>\r" +
    "\n" +
    "            <div post-likes list=\"post.PostLikes\" post-id=\"post.Id\"></div>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('posts/postRelatedItems.html',
    "<div class=\"post-related-items-container card default darken\">\r" +
    "\n" +
    "    <div class=\"header\">\r" +
    "\n" +
    "        <h5>Related posts</h5>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "    <div class=\"content\">\r" +
    "\n" +
    "        <span>Filter</span>\r" +
    "\n" +
    "        <select ng-model=\"selectedCategory\" ng-options=\"category.name for category in relatedCategories\"></select><br>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "    <div class=\"content\">\r" +
    "\n" +
    "        <div class=\"alert\" ng-class=\"emptyPostsStyle()\" ng-show=\"displayEmptyPostsMessage()\">\r" +
    "\n" +
    "            <p>\r" +
    "\n" +
    "                {{getEmptyPostsMessage()}}\r" +
    "\n" +
    "                <div ng-show=\"hasError\">\r" +
    "\n" +
    "                    <a ng-click=\"getRelatedPosts()\">Click here</a> to reload\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "            </p>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "        <div post-related-item ng-show=\"displayUser()\" ng-repeat=\"post in postsByUser\" post=\"post\"></div>\r" +
    "\n" +
    "        <div post-related-item ng-show=\"displayTag()\" ng-repeat=\"post in postsByTag\" post=\"post\"></div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('posts/postViewCount.html',
    "<span class=\"post-view-count\">\r" +
    "\n" +
    "    <span>{{viewCount.length}} views</span>\r" +
    "\n" +
    "</span>"
  );


  $templateCache.put('posts/postViewNavigator.html',
    "<div class=\"post-view-navigator\" ng-show=\"isVisible()\">\r" +
    "\n" +
    "    <div class=\"col-xs-4 col-sm-6\">\r" +
    "\n" +
    "        <div class=\"btn btn-primary\" ng-click=\"previousPost()\">Prev</div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "    <div class=\"col-xs-4 col-sm-6\">\r" +
    "\n" +
    "        <div class=\"btn btn-primary\" ng-click=\"nextPost()\">Next</div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('shared/emptyRecordMessage.html',
    "<div class=\"empty-record-message\">\r" +
    "\n" +
    "    <p>{{message}}</p>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('shared/fileUpload.html',
    "<div>\r" +
    "\n" +
    "    <div class=\"file-upload\" ng-file-drop>\r" +
    "\n" +
    "        <label class=\"btn btn-primary\">\r" +
    "\n" +
    "            Choose from your computer..\r" +
    "\n" +
    "            <input nv-file-select type=\"file\" uploader=\"uploader\" multiple />\r" +
    "\n" +
    "        </label>\r" +
    "\n" +
    "        <div class=\"well dropzone\" nv-file-over=\"\" uploader=\"uploader\">\r" +
    "\n" +
    "            <h4>Drag files here...</h4>\r" +
    "\n" +
    "            <p>You have {{ uploader.queue.length }} items</p>\r" +
    "\n" +
    "            <div class=\"btn btn-primary upload-all\" ng-click=\"uploader.uploadAll()\">\r" +
    "\n" +
    "                <i class=\"fa fa-upload\"></i>Upload all\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "            <div>\r" +
    "\n" +
    "                <ul class=\"upload-items\" isotope-container isotope-item-resize>\r" +
    "\n" +
    "                    <li ng-repeat=\"item in uploader.queue\" isotope-item class=\"card\">\r" +
    "\n" +
    "                        <div data-media-id=\"{{item.mediaId}}\" file-upload-item item=\"item\" uploader=\"uploader\"></div>\r" +
    "\n" +
    "                    </li>\r" +
    "\n" +
    "                </ul>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('shared/fileUploadItem.html',
    "<div>\r" +
    "\n" +
    "    <div class=\"upload-details\">\r" +
    "\n" +
    "        <p class=\"filename\" bs-popover data-placement=\"bottom\" data-animation=\"am-flip-x\"\r" +
    "\n" +
    "           title=\"File Name\" data-content=\"{{item.file.name}}\">\r" +
    "\n" +
    "            {{ item.file.name }}\r" +
    "\n" +
    "        </p>\r" +
    "\n" +
    "        <p nowrap>{{ item.file.size/1024/1024|number:2 }} MB</p>\r" +
    "\n" +
    "\r" +
    "\n" +
    "        <div class=\"buttons\" nowrap>\r" +
    "\n" +
    "            <button type=\"button\" class=\"btn btn-success btn-xs\" ng-click=\"item.upload()\" ng-disabled=\"item.isReady || item.isUploading || item.isSuccess\">\r" +
    "\n" +
    "                <span class=\"fa fa-upload\"></span>\r" +
    "\n" +
    "            </button>\r" +
    "\n" +
    "            <button type=\"button\" class=\"btn btn-warning btn-xs\" ng-click=\"item.cancel()\" ng-disabled=\"!item.isUploading\">\r" +
    "\n" +
    "                <span class=\"fa fa-ban\"></span>\r" +
    "\n" +
    "            </button>\r" +
    "\n" +
    "            <button type=\"button\" class=\"btn btn-danger btn-xs\" ng-click=\"item.remove()\">\r" +
    "\n" +
    "                <span class=\"fa fa-trash-o\"></span>\r" +
    "\n" +
    "            </button>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "\r" +
    "\n" +
    "        <div class=\"status\">\r" +
    "\n" +
    "            <span ng-show=\"item.isSuccess\"><i class=\"fa fa-check\"></i></span>\r" +
    "\n" +
    "            <span ng-show=\"item.isCancel\"><i class=\"fa fa-stop\"></i></span>\r" +
    "\n" +
    "            <span ng-show=\"item.isError\"><i class=\"fa fa-exclamation-triangle\"></i></span>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "\r" +
    "\n" +
    "    <div class=\"thumbnail\" ng-switch=\"isNewContent(item.isExisting)\">\r" +
    "\n" +
    "        <div ng-switch-when=\"true\" file-upload-thumbnail=\"{ file: item._file, height: 100 }\"></div>\r" +
    "\n" +
    "        <div ng-switch-default>\r" +
    "\n" +
    "            <img ng-src=\"{{item.url}}\" />\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "\r" +
    "\n" +
    "    <div class=\"progress\" style=\"margin-bottom: 0;\">\r" +
    "\n" +
    "        <div class=\"progress-bar\" role=\"progressbar\" ng-style=\"{ 'width': item.progress + '%' }\"></div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "\r" +
    "\n" +
    "    <div class=\"captions\" ng-switch on=\"item.allowCaptions\">\r" +
    "\n" +
    "        <div ng-switch-when=\"true\">\r" +
    "\n" +
    "            <h5>Content Title</h5>\r" +
    "\n" +
    "            <input ng-model=\"item.postContentTitle\" placeholder=\"Enter content title here...\" maxlength=\"50\" />\r" +
    "\n" +
    "            <h5>Content Description</h5>\r" +
    "\n" +
    "            <textarea ng-model=\"item.postContentText\" placeholder=\"Enter a brief description of this content here...\" \r" +
    "\n" +
    "                      maxlength=\"140\"></textarea>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "        <div ng-switch-default></div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('shared/videoPlayer.html',
    "<div>\r" +
    "\n" +
    "    <videogular vg-player-ready=\"onPlayerReady\" vg-complete=\"onCompleteVideo\" vg-update-time=\"onUpdateTime\"\r" +
    "\n" +
    "                vg-update-size=\"onUpdateSize\" vg-update-volume=\"onUpdateVolume\" vg-update-state=\"onUpdateState\"\r" +
    "\n" +
    "                vg-width=\"config.width\" vg-height=\"config.height\" vg-theme=\"config.theme.url\"\r" +
    "\n" +
    "                vg-autoplay=\"config.autoPlay\" vg-stretch=\"config.stretch.value\" vg-responsive=\"true\">\r" +
    "\n" +
    "        <vg-video vg-src=\"config.sources\" preload='metadata'></vg-video>\r" +
    "\n" +
    "        <vg-poster-image vg-url='config.plugins.poster.url' vg-stretch=\"config.stretch.value\"></vg-poster-image>\r" +
    "\n" +
    "        <vg-buffering></vg-buffering>\r" +
    "\n" +
    "        <vg-overlay-play vg-play-icon=\"config.theme.playIcon\"></vg-overlay-play>\r" +
    "\n" +
    "        <vg-controls vg-autohide=\"config.autoHide\" vg-autohide-time=\"config.autoHideTime\" style=\"height: 50px;\">\r" +
    "\n" +
    "            <vg-play-pause-button></vg-play-pause-button>\r" +
    "\n" +
    "            <vg-scrubbar>\r" +
    "\n" +
    "                <vg-scrubbarcurrenttime></vg-scrubbarcurrenttime>\r" +
    "\n" +
    "            </vg-scrubbar>\r" +
    "\n" +
    "            <!--<vg-timedisplay>{{ currentTime }} / {{ totalTime }}</vg-timedisplay>-->\r" +
    "\n" +
    "            <vg-volume>\r" +
    "\n" +
    "                <vg-mutebutton></vg-mutebutton>\r" +
    "\n" +
    "                <vg-volumebar></vg-volumebar>\r" +
    "\n" +
    "            </vg-volume>\r" +
    "\n" +
    "            <vg-fullscreenbutton vg-enter-full-screen-icon=\"config.theme.enterFullScreenIcon\" vg-exit-full-screen-icon=\"config.theme.exitFullScreenIcon\"></vg-fullscreenbutton>\r" +
    "\n" +
    "        </vg-controls>\r" +
    "\n" +
    "    </videogular>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('sockets/socketDebugger.html',
    "<div id=\"socket-debugger\" class=\"card animate rotate-in-right ng-cloak\" ng-show=\"show\">\r" +
    "\n" +
    "    <div>\r" +
    "\n" +
    "        <input type=\"text\" ng-model=\"channelSubscription\" placeholder=\"Enter channel to subscribe...\"\r" +
    "\n" +
    "               ng-enter=\"subscribeToChannel()\" class=\"form-control\" />\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "\r" +
    "\n" +
    "    <div>\r" +
    "\n" +
    "        <input type=\"text\" ng-model=\"echoMessage\" placeholder=\"Enter message...\" ng-enter=\"doEcho()\"\r" +
    "\n" +
    "               class=\"form-control\" />\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "\r" +
    "\n" +
    "    <div class=\"empty\" ng-show=\"showEmptyMessage()\">\r" +
    "\n" +
    "        No messages currently..\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "\r" +
    "\n" +
    "    <div ng-repeat=\"message in messages\">\r" +
    "\n" +
    "        <p>\r" +
    "\n" +
    "            <strong>Function</strong> {{message.fn}}\r" +
    "\n" +
    "        </p>\r" +
    "\n" +
    "        <p>\r" +
    "\n" +
    "            {{message.data}}\r" +
    "\n" +
    "        </p>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('tags/tagItem.html',
    "<div class=\"tag-item\" data-tag-id=\"{{tag.TagId}}\">\r" +
    "\n" +
    "    <i class=\"fa fa-tags\"></i>\r" +
    "\n" +
    "    {{tag.TagName}}\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('user/userImage.html',
    "<div class=\"user-image\" data-user-id=\"{{user.Id}}\">\r" +
    "\n" +
    "    <div class=\"user-background-image\">\r" +
    "\n" +
    "        <img ng-src=\"{{backgroundImageUrl}}\" />\r" +
    "\n" +
    "        <label class=\"fa fa-picture-o edit\" ng-click=\"updateBackgroundImage()\" ng-show=\"showUpdateImages()\" data-placement=\"bottom-right\"\r" +
    "\n" +
    "               data-animation=\"am-flip-x\" title=\"Update background image?\" data-content=\"Click here to change your background image.\"\r" +
    "\n" +
    "               data-trigger=\"hover\" bs-popover>\r" +
    "\n" +
    "            <input nv-file-select type=\"file\" uploader=\"uploader\" />\r" +
    "\n" +
    "        </label>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "    <div class=\"details\">\r" +
    "\n" +
    "        <div class=\"user-profile-image\">\r" +
    "\n" +
    "            <img ng-src=\"{{profileImageUrl}}\" />\r" +
    "\n" +
    "            <label class=\"fa fa-camera-retro edit\" ng-click=\"updateProfileImage()\" ng-show=\"showUpdateImages()\" data-placement=\"bottom\"\r" +
    "\n" +
    "                   data-animation=\"am-flip-x\" title=\"Update profile image?\" data-content=\"Click here to change your profile image.\"\r" +
    "\n" +
    "                   data-trigger=\"hover\" bs-popover>\r" +
    "\n" +
    "                <input nv-file-select type=\"file\" uploader=\"uploader\" />\r" +
    "\n" +
    "            </label>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "        <div>\r" +
    "\n" +
    "            <h4>{{fullname}}</h4>\r" +
    "\n" +
    "            <p>\r" +
    "\n" +
    "                <a user-info-popup user=\"user\">@{{user.UserName}}</a>\r" +
    "\n" +
    "            </p>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "    <div class=\"clearfix\"></div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('user/userInfoPopup.html',
    "<div id=\"user-info-popup\" class=\"popover\">\r" +
    "\n" +
    "    <div class=\"arrow\"></div>\r" +
    "\n" +
    "    <h3 class=\"popover-title\" ng-show=\"title\">{{fullName()}}</h3>\r" +
    "\n" +
    "    <div class=\"popover-content\">\r" +
    "\n" +
    "        <div>\r" +
    "\n" +
    "            <p>\r" +
    "\n" +
    "                <span>{{birthdate()}}</span>\r" +
    "\n" +
    "            </p>\r" +
    "\n" +
    "            <div class=\"buttons\">\r" +
    "\n" +
    "                <div class=\"btn btn-primary\" data-trigger=\"hover\" data-type=\"info\" \r" +
    "\n" +
    "                     data-title=\"Send a message\" bs-tooltip ng-show=\"showSendMessage()\" ng-click=\"goToChat()\">\r" +
    "\n" +
    "                    <i class=\"fa fa-comments\"></i>\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "                <div class=\"btn btn-success\" data-trigger=\"hover\" data-type=\"info\" \r" +
    "\n" +
    "                     data-title=\"View profile\" bs-tooltip ng-click=\"viewProfile()\">\r" +
    "\n" +
    "                    <i class=\"fa fa-user\"></i>\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "                <div class=\"btn btn-danger\" data-trigger=\"hover\" data-type=\"info\" \r" +
    "\n" +
    "                     data-title=\"Close\" bs-tooltip ng-click=\"hide()\">\r" +
    "\n" +
    "                    <i class=\"fa fa-times\"></i>\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('user/userProfileComments.html',
    "<div class=\"user-profile-comments card\">\r" +
    "\n" +
    "    <div id=\"user-profile-comments-list\">\r" +
    "\n" +
    "        <div ng-repeat=\"comment in comments track by $index\" comment-item comment=\"comment\" user=\"user\" poster=\"user.UserName\"></div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('user/userProfileDetails.html',
    "<div id=\"user-profile-details\" class=\"user-profile-details\" data-user-id=\"{{user.Id}}\">\r" +
    "\n" +
    "    <div class=\"row\">\r" +
    "\n" +
    "        <div class=\"col-xs-12 col-sm-12 col-md-6 col-lg-6\">\r" +
    "\n" +
    "            <div user-profile-details-info user=\"user\"></div>\r" +
    "\n" +
    "\r" +
    "\n" +
    "            <div user-profile-details-address></div>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "\r" +
    "\n" +
    "        <div class=\"col-xs-12 col-sm-12 col-md-6 col-lg-6\">\r" +
    "\n" +
    "            <div user-profile-details-hobbies></div>\r" +
    "\n" +
    "\r" +
    "\n" +
    "            <div user-profile-details-education></div>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('user/userProfileDetailsAddress.html',
    "<div class=\"card default address\">\r" +
    "\n" +
    "    <div class=\"header\">\r" +
    "\n" +
    "        <h5>Address</h5>\r" +
    "\n" +
    "        <div class=\"btn-header right pull-right\" ng-show=\"isEditing\" ng-click=\"cancelEditAddress()\">\r" +
    "\n" +
    "            <i class=\"fa fa-times\"></i>\r" +
    "\n" +
    "            <span class=\"hidden-xs\">Cancel</span>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "        <div class=\"btn-header right pull-right\" ng-show=\"isEditing\" ng-click=\"saveAddress()\">\r" +
    "\n" +
    "            <i class=\"fa fa-save\"></i>\r" +
    "\n" +
    "            <span class=\"hidden-xs\">Save</span>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "        <div class=\"btn-header right pull-right\" ng-show=\"!isEditing\" ng-class=\"showButtons()\" ng-click=\"editAddress()\">\r" +
    "\n" +
    "            <i class=\"fa fa-edit\"></i>\r" +
    "\n" +
    "            <span class=\"hidden-xs\">Edit</span>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "    <form class=\"user-form\" name=\"user-address-form\" ng-submit=\"submitForm(userAddressForm.$valid)\" novalidate>\r" +
    "\n" +
    "        <div class=\"form-group\">\r" +
    "\n" +
    "            <div class=\"row\">\r" +
    "\n" +
    "                <div class=\"col-xs-12 col-sm-12 col-md-12 col-lg-12\">\r" +
    "\n" +
    "                    <p class=\"error-message\">{{error.Street}}</p>\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "            <div class=\"row\">\r" +
    "\n" +
    "                <div class=\"col-xs-12 col-sm-4 col-md-3 col-lg-3\">\r" +
    "\n" +
    "                    <label>Street</label>\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "                <div class=\"col-xs-12 col-sm-8 col-md-9 col-lg-9\">\r" +
    "\n" +
    "                    <label data-display ng-show=\"!isEditing\">{{address.StreetAddress}}</label>\r" +
    "\n" +
    "                    <input name=\"address-street\" type=\"text\" class=\"input form-control\" ng-show=\"isEditing\"\r" +
    "\n" +
    "                           ng-class=\"hasError('Street')\" placeholder=\"Street\" ng-model=\"address.StreetAddress\" ng-enter=\"register()\" />\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "            <div class=\"row\">\r" +
    "\n" +
    "                <div class=\"col-xs-12 col-sm-12 col-md-12 col-lg-12\">\r" +
    "\n" +
    "                    <p class=\"error-message\">{{error.State}}</p>\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "            <div class=\"row\">\r" +
    "\n" +
    "                <div class=\"col-xs-12 col-sm-4 col-md-3 col-lg-3\">\r" +
    "\n" +
    "                    <label>State</label>\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "                <div class=\"col-xs-12 col-sm-8 col-md-9 col-lg-9\">\r" +
    "\n" +
    "                    <label data-display ng-show=\"!isEditing\">{{address.State}}</label>\r" +
    "\n" +
    "                    <input name=\"address-state\" type=\"text\" class=\"input form-control\" ng-show=\"isEditing\"\r" +
    "\n" +
    "                           ng-class=\"hasError('State')\" placeholder=\"State\" ng-model=\"address.State\" ng-enter=\"register()\" />\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "            <div class=\"row\">\r" +
    "\n" +
    "                <div class=\"col-xs-12 col-sm-12 col-md-12 col-lg-12\">\r" +
    "\n" +
    "                    <p class=\"error-message\">{{error.City}}</p>\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "            <div class=\"row\">\r" +
    "\n" +
    "                <div class=\"col-xs-12 col-sm-4 col-md-3 col-lg-3\">\r" +
    "\n" +
    "                    <label>City</label>\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "                <div class=\"col-xs-12 col-sm-8 col-md-9 col-lg-9\">\r" +
    "\n" +
    "                    <label data-display ng-show=\"!isEditing\">{{address.City}}</label>\r" +
    "\n" +
    "                    <input name=\"address-city\" type=\"text\" class=\"input form-control\" ng-show=\"isEditing\" required\r" +
    "\n" +
    "                           ng-class=\"hasError('City')\" placeholder=\"City\" ng-model=\"address.City\" ng-enter=\"register()\" />\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "            <div class=\"row\">\r" +
    "\n" +
    "                <div class=\"col-xs-12 col-sm-12 col-md-12 col-lg-12\">\r" +
    "\n" +
    "                    <p class=\"error-message\">{{error.Country}}</p>\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "            <div class=\"row\">\r" +
    "\n" +
    "                <div class=\"col-xs-12 col-sm-4 col-md-3 col-lg-3\">\r" +
    "\n" +
    "                    <label>Country</label>\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "                <div class=\"col-xs-12 col-sm-8 col-md-9 col-lg-9\">\r" +
    "\n" +
    "                    <label data-display ng-show=\"!isEditing\">{{address.Country}}</label>\r" +
    "\n" +
    "                    <input name=\"address-country\" type=\"text\" class=\"input form-control\" ng-show=\"isEditing\"\r" +
    "\n" +
    "                           ng-class=\"hasError('Country')\" placeholder=\"Country\" ng-model=\"address.Country\" ng-enter=\"register()\" />\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "            <div class=\"row\">\r" +
    "\n" +
    "                <div class=\"col-xs-12 col-sm-12 col-md-12 col-lg-12\">\r" +
    "\n" +
    "                    <p class=\"error-message\">{{error.Zip}}</p>\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "            <div class=\"row\">\r" +
    "\n" +
    "                <div class=\"col-xs-12 col-sm-4 col-md-3 col-lg-3\">\r" +
    "\n" +
    "                    <label>Zip Code</label>\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "                <div class=\"col-xs-12 col-sm-8 col-md-9 col-lg-9\">\r" +
    "\n" +
    "                    <label data-display ng-show=\"!isEditing\">{{address.Zip}}</label>\r" +
    "\n" +
    "                    <input name=\"address-zipcode\" type=\"text\" class=\"input form-control\" ng-show=\"isEditing\"\r" +
    "\n" +
    "                           ng-class=\"hasError('Zip')\" placeholder=\"Zip Code\" ng-model=\"address.Zip\" ng-enter=\"register()\" />\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </form>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('user/userProfileDetailsEducation.html',
    "<div class=\"card default darken education\">\r" +
    "\n" +
    "    <div class=\"header\">\r" +
    "\n" +
    "        <h5>Education</h5>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "    <div class=\"body\">\r" +
    "\n" +
    "        <div class=\"row\">\r" +
    "\n" +
    "            <div class=\"col-xs-12 col-sm-12 col-md-12 col-lg-12\">\r" +
    "\n" +
    "                <div ng-repeat=\"educationGroup in educationGroups track by $index\" user-profile-details-education-group\r" +
    "\n" +
    "                     education-group=\"educationGroup\" user=\"user\"></div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('user/userProfileDetailsEducationGroup.html',
    "<div class=\"panel panel-primary\">\r" +
    "\n" +
    "    <div class=\"panel-heading\">\r" +
    "\n" +
    "        <h4 class=\"panel-title\">\r" +
    "\n" +
    "            <i class=\"fa fa-plus-circle\" ng-click=\"addEducation()\" ng-class=\"showButtons()\"></i>\r" +
    "\n" +
    "            {{educationGroup.Title}}\r" +
    "\n" +
    "        </h4>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "    <div id=\"{{educ.id}}\" class=\"panel-body\">\r" +
    "\n" +
    "        <div empty-record-message message=\"emptyRecordMessage\" ng-show=\"showNoRecordsMessage()\"></div>\r" +
    "\n" +
    "        \r" +
    "\n" +
    "        <div ng-show=\"!showNoRecordsMessage()\">\r" +
    "\n" +
    "            <div class=\"user-education-item\" ng-repeat=\"education in educationGroup.Content track by $index\"\r" +
    "\n" +
    "                 user-profile-details-education-item education=\"education\" user=\"user\"></div>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "        \r" +
    "\n" +
    "        <div class=\"user-education-item\" ng-show=\"isAdding\"\r" +
    "\n" +
    "             user-profile-details-education-item education=\"newEducation\" user=\"user\" is-adding=\"true\"></div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('user/userProfileDetailsEducationItem.html',
    "<div class=\"user-education-item animate fade-up\" data-educ-id=\"education.EducationId\">\r" +
    "\n" +
    "    <form class=\"user-form\" name=\"user-education-form\" ng-submit=\"submitForm(userEducationForm.$valid)\" novalidate>\r" +
    "\n" +
    "        <div class=\"form-group\">\r" +
    "\n" +
    "            <div class=\"row\">\r" +
    "\n" +
    "                <div class=\"col-xs-12 col-sm-12 col-md-12 col-lg-12\">\r" +
    "\n" +
    "                    <label ng-show=\"!isEditing\">{{education.SchoolName}}</label>\r" +
    "\n" +
    "                    <input name=\"firstname\" type=\"text\" class=\"input form-control\" ng-show=\"isEditing\"\r" +
    "\n" +
    "                           placeholder=\"School Name\" ng-model=\"education.SchoolName\" ng-enter=\"register()\" />\r" +
    "\n" +
    "                    <div class=\"buttons pull-right\">\r" +
    "\n" +
    "                        <i class=\"fa fa-save\" ng-show=\"isEditing\" ng-click=\"saveEducation()\"></i>\r" +
    "\n" +
    "                        <i class=\"fa fa-close\" ng-show=\"isEditing\" ng-click=\"cancelEditing()\"></i>\r" +
    "\n" +
    "                        <i class=\"fa fa-trash-o\" ng-show=\"!isEditing\" ng-click=\"deleteEducation()\" ng-class=\"showButtons()\"></i>\r" +
    "\n" +
    "                        <i class=\"fa fa-edit\" ng-show=\"!isEditing\" ng-click=\"editEducation()\" ng-class=\"showButtons()\"></i>\r" +
    "\n" +
    "                    </div>\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "            <div class=\"row\">\r" +
    "\n" +
    "                <div ng-show=\"!isEditing\">\r" +
    "\n" +
    "                    <div class=\"col-xs-12 col-sm-12 col-md-12 col-lg-12\">\r" +
    "\n" +
    "                        <label data-display>{{education.State}} {{education.City}}, {{education.Country}}</label>\r" +
    "\n" +
    "                    </div>\r" +
    "\n" +
    "                    <div class=\"col-xs-12 col-sm-12 col-md-12 col-lg-12\">\r" +
    "\n" +
    "                        <label data-display>{{educationCourseDisplay()}}</label>\r" +
    "\n" +
    "                        <label data-display>From {{education.YearAttendedDisplay}} to {{education.YearGraduatedDisplay}}</label>\r" +
    "\n" +
    "                    </div>\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "                <div ng-show=\"isEditing\">\r" +
    "\n" +
    "                    <div class=\"col-xs-12 col-sm-4 col-md-4 col-lg-4\">\r" +
    "\n" +
    "                        <input name=\"state\" type=\"text\" class=\"input form-control\"\r" +
    "\n" +
    "                               placeholder=\"State\" ng-model=\"education.State\" />\r" +
    "\n" +
    "                    </div>\r" +
    "\n" +
    "                    <div class=\"col-xs-12 col-sm-4 col-md-4 col-lg-4\">\r" +
    "\n" +
    "                        <input name=\"city\" type=\"text\" class=\"input form-control\"\r" +
    "\n" +
    "                               placeholder=\"City\" ng-model=\"education.City\" />\r" +
    "\n" +
    "                    </div>\r" +
    "\n" +
    "                    <div class=\"col-xs-12 col-sm-4 col-md-4 col-lg-4\">\r" +
    "\n" +
    "                        <input name=\"country\" type=\"text\" class=\"input form-control\"\r" +
    "\n" +
    "                               placeholder=\"Country\" ng-model=\"education.Country\" />\r" +
    "\n" +
    "                    </div>\r" +
    "\n" +
    "                    <div class=\"col-xs-12 col-sm-12 col-md-12 col-lg-12\">\r" +
    "\n" +
    "                        <input name=\"course\" type=\"text\" class=\"input form-control\"\r" +
    "\n" +
    "                               placeholder=\"Course\" ng-model=\"education.Course\" />\r" +
    "\n" +
    "                    </div>\r" +
    "\n" +
    "                    <div class=\"col-xs-12 col-sm-6 col-md-6 col-lg-6\">\r" +
    "\n" +
    "                        <input name=\"yearattended\" type=\"text\" class=\"input form-control\"\r" +
    "\n" +
    "                               placeholder=\"Date attended\" ng-model=\"education.YearAttended\" bs-datepicker data-placement=\"top\"\r" +
    "\n" +
    "                               data-date-format=\"MMMM yyyy\" data-iconleft=\"fa fa-calendar\" data-autoclose=\"true\" />\r" +
    "\n" +
    "                    </div>\r" +
    "\n" +
    "                    <div class=\"col-xs-12 col-sm-6 col-md-6 col-lg-6\">\r" +
    "\n" +
    "                        <input name=\"yeargraduated\" type=\"text\" class=\"input form-control\"\r" +
    "\n" +
    "                               placeholder=\"Date graduated\" ng-model=\"education.YearGraduated\" bs-datepicker data-placement=\"top\"\r" +
    "\n" +
    "                               data-date-format=\"MMMM yyyy\" data-iconleft=\"fa fa-calendar\" data-autoclose=\"true\" />\r" +
    "\n" +
    "                    </div>\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </form>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('user/userProfileDetailsHobbies.html',
    "<div class=\"card default hobbies\">\r" +
    "\n" +
    "    <div class=\"header\">\r" +
    "\n" +
    "        <h5>Hobbies</h5>\r" +
    "\n" +
    "        <div class=\"btn-header right pull-right\" ng-show=\"!isAdding\" ng-click=\"addHobby()\" ng-class=\"showButtons()\">\r" +
    "\n" +
    "            <i class=\"fa fa-plus\"></i>\r" +
    "\n" +
    "            <span class=\"hidden-xs\">Add</span>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "\r" +
    "\n" +
    "    <form class=\"user-form\" name=\"user-hobbies-form\" ng-submit=\"submitForm(userHobbiesForm.$valid)\" novalidate>\r" +
    "\n" +
    "        <div empty-record-message message=\"emptyRecordMessage\" ng-show=\"showNoRecordsMessage()\"></div>\r" +
    "\n" +
    "        <div ng-repeat=\"hobby in hobbies track by $index\" user-profile-details-hobby-item hobby=\"hobby\" user=\"user\"></div>\r" +
    "\n" +
    "\r" +
    "\n" +
    "        <div class=\"row\" ng-show=\"isAdding\">\r" +
    "\n" +
    "            <div class=\" col-xs-12 col-sm-12 col-md-12 col-lg-12\">\r" +
    "\n" +
    "                <p class=\"error-message\">{{error.HobbyName}}</p>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "        <div class=\"row animate fade-up\" ng-show=\"isAdding\">\r" +
    "\n" +
    "            <div class=\"col-xs-12 col-sm-12 col-md-12 col-lg-12 user-hobby-item\">\r" +
    "\n" +
    "                <input name=\"new-hobby\" type=\"text\" class=\"input form-control\"\r" +
    "\n" +
    "                       ng-class=\"hasError()\" placeholder=\"Enter hobby\" ng-model=\"newHobby.HobbyName\" />\r" +
    "\n" +
    "                <div class=\"buttons\">\r" +
    "\n" +
    "                    <i class=\"fa fa-save\" ng-show=\"isAdding\" ng-click=\"saveHobby()\"></i>\r" +
    "\n" +
    "                    <i class=\"fa fa-times\" ng-show=\"isAdding\" ng-click=\"cancelAdding()\"></i>\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </form>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('user/userProfileDetailsHobbyItem.html',
    "<div class=\"form-group\" data-hobby-id=\"hobby.HobbyId\">\r" +
    "\n" +
    "    <div class=\"row\">\r" +
    "\n" +
    "        <div class=\"col-xs-12 col-sm-12 col-md-12 col-lg-12\">\r" +
    "\n" +
    "            <p class=\"error-message\">{{error.HobbyName}}</p>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "    <div class=\"row\">\r" +
    "\n" +
    "        <div class=\"col-xs-12 col-sm-12 col-md-12 col-lg-12 user-hobby-item animate fade-up\">\r" +
    "\n" +
    "            <label ng-show=\"!isEditing\">{{hobby.HobbyName}}</label>\r" +
    "\n" +
    "            <input name=\"firstname\" type=\"text\" class=\"input form-control\" ng-show=\"isEditing\"\r" +
    "\n" +
    "                   ng-class=\"hasError()\" placeholder=\"Enter hobby\" ng-model=\"hobby.HobbyName\" />\r" +
    "\n" +
    "            <div class=\"buttons\">\r" +
    "\n" +
    "                <i class=\"fa fa-trash-o\" ng-show=\"!isEditing\" ng-click=\"deleteHobby()\" ng-class=\"showButtons()\"></i>\r" +
    "\n" +
    "                <i class=\"fa fa-edit\" ng-show=\"!isEditing\" ng-click=\"editHobby()\" ng-class=\"showButtons()\"></i>\r" +
    "\n" +
    "                <i class=\"fa fa-save\" ng-show=\"isEditing\" ng-click=\"saveHobby()\"></i>\r" +
    "\n" +
    "                <i class=\"fa fa-times\" ng-show=\"isEditing\" ng-click=\"cancelEditing()\"></i>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>\r" +
    "\n"
  );


  $templateCache.put('user/userProfileDetailsInfo.html',
    "<div class=\"card default information\">\r" +
    "\n" +
    "    <div class=\"header\">\r" +
    "\n" +
    "        <h5>Personal Details</h5>\r" +
    "\n" +
    "        <div class=\"btn-header right pull-right\" ng-show=\"isEditing\" ng-click=\"cancelEditDetails()\">\r" +
    "\n" +
    "            <i class=\"fa fa-times\"></i>\r" +
    "\n" +
    "            <span class=\"hidden-xs\">Cancel</span>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "        <div class=\"btn-header right pull-right\" ng-show=\"isEditing\" ng-click=\"saveDetails()\">\r" +
    "\n" +
    "            <i class=\"fa fa-save\"></i>\r" +
    "\n" +
    "            <span class=\"hidden-xs\">Save</span>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "        <div class=\"btn-header right pull-right\" ng-show=\"!isEditing\" ng-class=\"showButtons()\" ng-click=\"editDetails()\">\r" +
    "\n" +
    "            <i class=\"fa fa-edit\"></i>\r" +
    "\n" +
    "            <span class=\"hidden-xs\">Edit</span>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "    <form class=\"user-form\" name=\"user-details-form\" ng-submit=\"submitForm(userDetailsForm.$valid)\" novalidate>\r" +
    "\n" +
    "        <div class=\"form-group\">\r" +
    "\n" +
    "            <div class=\"row\">\r" +
    "\n" +
    "                <div class=\"col-xs-12 col-sm-12 col-md-12 col-lg-12\">\r" +
    "\n" +
    "                    <p class=\"error-message\">{{error.FirstName}}</p>\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "            <div class=\"row\">\r" +
    "\n" +
    "                <div class=\"col-xs-12 col-sm-4 col-md-3 col-lg-3\">\r" +
    "\n" +
    "                    <label>First Name</label>\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "                <div class=\"col-xs-12 col-sm-8 col-md-9 col-lg-9\">\r" +
    "\n" +
    "                    <label data-display ng-show=\"!isEditing\">{{user.FirstName}}</label>\r" +
    "\n" +
    "                    <input name=\"firstname\" type=\"text\" class=\"input form-control\" ng-show=\"isEditing\"\r" +
    "\n" +
    "                           ng-class=\"hasError('FirstName')\" placeholder=\"First Name\" ng-model=\"user.FirstName\" ng-enter=\"register()\" />\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "\r" +
    "\n" +
    "            <div class=\"row\">\r" +
    "\n" +
    "                <div class=\"col-xs-12 col-sm-12 col-md-12 col-lg-12\">\r" +
    "\n" +
    "                    <p class=\"error-message\">{{error.LastName}}</p>\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "            <div class=\"row\">\r" +
    "\n" +
    "                <div class=\"col-xs-12 col-sm-4 col-md-3 col-lg-3\">\r" +
    "\n" +
    "                    <label>Last Name</label>\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "                <div class=\"col-xs-12 col-sm-8 col-md-9 col-lg-9\">\r" +
    "\n" +
    "                    <label data-display ng-show=\"!isEditing\">{{user.LastName}}</label>\r" +
    "\n" +
    "                    <input name=\"lastname\" type=\"text\" class=\"input form-control\" ng-show=\"isEditing\"\r" +
    "\n" +
    "                           ng-class=\"hasError('LastName')\" placeholder=\"Last Name\" ng-model=\"user.LastName\" ng-enter=\"register()\" />\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "\r" +
    "\n" +
    "            <div class=\"row\">\r" +
    "\n" +
    "                <div class=\"col-xs-12 col-sm-12 col-md-12 col-lg-12\">\r" +
    "\n" +
    "                    <p class=\"error-message\">{{error.EmailAddress}}</p>\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "            <div class=\"row\">\r" +
    "\n" +
    "                <div class=\"col-xs-12 col-sm-4 col-md-3 col-lg-3\">\r" +
    "\n" +
    "                    <label>Email</label>\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "                <div class=\"col-xs-12 col-sm-8 col-md-9 col-lg-9\">\r" +
    "\n" +
    "                    <label data-display ng-show=\"!isEditing\">{{user.EmailAddress}}</label>\r" +
    "\n" +
    "                    <input name=\"email\" type=\"text\" class=\"input form-control\" ng-show=\"isEditing\" required\r" +
    "\n" +
    "                           ng-class=\"hasError('EmailAddress')\" placeholder=\"Email\" ng-model=\"user.EmailAddress\" ng-enter=\"register()\" />\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "\r" +
    "\n" +
    "            <div class=\"row\">\r" +
    "\n" +
    "                <div class=\"col-xs-12 col-sm-12 col-md-12 col-lg-12\">\r" +
    "\n" +
    "                    <p class=\"error-message\">{{error.BirthDate}}</p>\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "            <div class=\"row\">\r" +
    "\n" +
    "                <div class=\"col-xs-12 col-sm-4 col-md-3 col-lg-3\">\r" +
    "\n" +
    "                    <label>Birth Date</label>\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "                <div class=\"col-xs-12 col-sm-8 col-md-9 col-lg-9\">\r" +
    "\n" +
    "                    <label data-display ng-show=\"!isEditing\">{{user.BirthDateDisplay}}</label>\r" +
    "\n" +
    "                    <input name=\"birthdate\" type=\"text\" class=\"input form-control\" ng-show=\"isEditing\"\r" +
    "\n" +
    "                           ng-class=\"hasError('BirthDate')\" placeholder=\"Birthdate\" ng-model=\"user.BirthDate\" ng-enter=\"register()\"\r" +
    "\n" +
    "                           bs-datepicker data-date-format=\"MM/dd/yyyy\" data-placement=\"top\" data-iconleft=\"fa fa-calendar\" data-autoclose=\"true\" />\r" +
    "\n" +
    "                </div>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "        </div>\r" +
    "\n" +
    "    </form>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('user/userProfileFavorites.html',
    "<div class=\"user-profile-favorites\">\r" +
    "\n" +
    "    <div class=\"well\">\r" +
    "\n" +
    "        User profile favorites\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('user/userProfileMedia.html',
    "<div class=\"user-profile-media\">\r" +
    "\n" +
    "    <div media-grouped-list albums=\"albums\" user=\"user\"></div>\r" +
    "\n" +
    "    <div ui-view class=\"sticky top\"></div>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('user/userProfileNavigation.html',
    "<div class=\"user-profile-navigation\">\r" +
    "\n" +
    "    <nav>\r" +
    "\n" +
    "        <ul>\r" +
    "\n" +
    "            <li>\r" +
    "\n" +
    "                <!--<a href=\"{{aboutUrl}}\">About</a>-->\r" +
    "\n" +
    "                <a ui-sref=\".details\">About</a>\r" +
    "\n" +
    "            </li>\r" +
    "\n" +
    "            <li>\r" +
    "\n" +
    "                <!--<a href=\"{{postsUrl}}\">Posts</a>-->\r" +
    "\n" +
    "                <a ui-sref=\".posts\">Posts</a>\r" +
    "\n" +
    "            </li>\r" +
    "\n" +
    "            <li>\r" +
    "\n" +
    "                <!--<a href=\"{{commentsUrl}}\">Comments</a>-->\r" +
    "\n" +
    "                <a ui-sref=\".comments\">Comments</a>\r" +
    "\n" +
    "            </li>\r" +
    "\n" +
    "            <li>\r" +
    "\n" +
    "                <!--<a href=\"{{mediaUrl}}\">Media</a>-->\r" +
    "\n" +
    "                <a ui-sref=\".media\">Media</a>\r" +
    "\n" +
    "            </li>\r" +
    "\n" +
    "            <li>\r" +
    "\n" +
    "                <!--<a href=\"{{favoritesUrl}}\">Favorites</a>-->\r" +
    "\n" +
    "                <a ui-sref=\".favorites\">Favorites</a>\r" +
    "\n" +
    "            </li>\r" +
    "\n" +
    "        </ul>\r" +
    "\n" +
    "    </nav>\r" +
    "\n" +
    "</div>"
  );


  $templateCache.put('user/userProfilePosts.html',
    "<div class=\"user-profile-posts\">\r" +
    "\n" +
    "    <div id=\"user-profile-posts-list\" isotope-container isotope-item-resize resize-layout-only=\"false\" resize-container=\"user-profile-posts-list\"\r" +
    "\n" +
    "         resize-broadcast=\"updateUserPostsSize\">\r" +
    "\n" +
    "        <div ng-repeat=\"post in posts track by $index\" isotope-item post-list-item \r" +
    "\n" +
    "             data=\"{ Post: post, Width: size, User: user }\"></div>\r" +
    "\n" +
    "    </div>\r" +
    "\n" +
    "</div>"
  );

}]);
