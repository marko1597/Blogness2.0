﻿///#source 1 1 /Scripts/modules/init.js
window.blogInit = {};

window.blogInit =
{
    start: function () {
        var navigationItems = [
            { text: "Home", icon: "fa-home", href: "/#/" },
            { text: "People", icon: "fa-user", href: "/#/user" },
            { text: "Friends", icon: "fa-comments", href: "/#/friends" },
            { text: "Groups", icon: "fa-users", href: "/#/groups" },
            { text: "Events", icon: "fa-calendar", href: "/#/events" }
        ];

        var settings = angular.element(document.querySelector('[ng-app]')).injector().get("configProvider");
        settings.setBlogSockets(window.blogConfiguration.blogSockets);
        settings.setBlogApiEndpoint(window.blogConfiguration.blogApi);
        settings.setBlogRoot(window.blogConfiguration.blogRoot);
        settings.setBlogSocketsAvailability(window.blogConfiguration.blogSocketsAvailable);
        settings.setDimensions(window.innerWidth, window.innerHeight);
        settings.setNavigationItems(navigationItems);
        settings.setDefaultProfilePicture(window.blogConfiguration.blogApi + "media/defaultprofilepicture");
        settings.setDefaultBackgroundPicture(window.blogConfiguration.blogApi + "media/defaultbackgroundpicture");
        settings.setSocketClientFunctions(window.socketClientFunctions);

        // TODO: This is a temporary hack. It should be in its respective module
        ngLogger.provider("$exceptionHandler", {
            $get: function (errorLogService) {
                return (errorLogService);
            }
        });
    }
}
///#source 1 1 /Scripts/modules/comments/comments.js
var ngComments = angular.module("ngComments",
    [
        "ngShared",
        "iso.directives",
        "ngConfig",
        "LocalStorageModule"
    ]);
///#source 1 1 /Scripts/modules/comments/directives/commentItem.js
ngComments.directive('commentItem', [function () {
    var ctrlFn = function ($scope, $rootScope, commentsService, errorService, configProvider) {
        $scope.canExpandComment = function () {
            if (!$scope.allowExpand) {
                return false;
            }
            if ($scope.comment.Comments == undefined || $scope.comment.Comments === null || $scope.comment.Comments.length < 1) {
                return false;
            }
            return true;
        };

        $scope.toggleReplies = function () {
            var state = !$scope.comment.ShowReplies;
            $scope.comment.ShowReplies = state;

            if (!state) {
                $rootScope.$broadcast("hideAddReply");
            }
        };

        $scope.isExpanded = function () {
            if ($scope.comment.ShowReplies) {
                return "fa-minus";
            }
            return "fa-plus";
        };

        $scope.canReplyToComment = function () {
            if (!$scope.allowReply) {
                return "hidden";
            }
            if ($scope.comment.PostId === null || $scope.comment.PostId == 0) {
                return "hidden";
            }

            return "";
        };

        $scope.showAddReply = function () {
            $scope.comment.ShowAddReply = true;

            if (!$scope.comment.ShowReplies) {
                $scope.toggleReplies();
                $scope.isExpanded();
            }
        };

        $scope.isUserLiked = function () {
            var isLiked = false;
            _.each($scope.comment.CommentLikes, function (c) {
                if ($scope.user && c.UserId == $scope.user.Id) {
                    isLiked = true;
                }
            });

            return isLiked ? "fa-star" : "fa-star-o";
        };

        $scope.isFromPostOwner = function () {
            if ($scope.comment.User && $scope.poster && $scope.comment.User.UserName == $scope.poster) {
                return "";
            }
            return "hidden";
        };

        $scope.likeComment = function () {
            commentsService.likeComment($scope.comment.Id, $scope.user.UserName).then(function () { },
                function (err) {
                    errorService.displayError(err);
                });;
        };

        $scope.$on(configProvider.getSocketClientFunctions().commentLikesUpdate, function (e, d) {
            if ($scope.comment.Id == d.commentId) {
                $scope.comment.CommentLikes = d.commentLikes;
                $(".comment-likes-count[data-comment-id='" + d.commentId + "']").effect("highlight", { color: "#B3C833" }, 1500);
                $scope.isUserLiked();
            }
        });

        $rootScope.$on("hideAddReply", function () {
            $scope.comment.ShowAddReply = false;
        });
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "commentsService", "errorService", "configProvider"];

    var linkFn = function(scope, elem, attrs) {
        scope.allowReply = attrs.allowReply === 'true' ? true : false;
        scope.allowExpand = attrs.allowExpand === 'true' ? true : false;
    };

    return {
        restrict: 'EA',
        scope: {
            comment: '=',
            user: '=',
            poster: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "comments/commentItem.html",
        controller: ctrlFn,
        link: linkFn
    };
}]);

///#source 1 1 /Scripts/modules/comments/directives/commentsAddNew.js
ngComments.directive('commentsAddNew', [function () {
    var ctrlFn = function ($scope, $rootScope, commentsService, errorService) {
        $scope.comment = {
            CommentMessage: "",
            PostId: $scope.postid,
            ParentCommentId: $scope.commentid,
            User: $scope.user
        };

        $scope.hasError = false;

        $scope.commentMessageStyle = function() {
            if ($scope.hasError) {
                return errorService.highlightField();
            }
            return "";
        };

        $scope.removeCommentMessageError = function() {
            $scope.hasError = false;
        };

        $scope.hideAddComment = function () {
            if ($scope.commentid == undefined || $scope.commentid == null) {
                $rootScope.$broadcast("hideAddComment");
            } else {
                $rootScope.$broadcast("hideAddReply");
            }
        };

        $scope.saveComment = function () {
            if ($scope.comment.CommentMessage != "") {
                commentsService.addComment($scope.createCommentForAdding()).then(function(resp) {
                    if (resp.Error == undefined) {
                        $scope.comment.CommentMessage = "";
                        $scope.hideAddComment();
                    } else {
                        $scope.hasError = true;
                        errorService.displayError(resp.Error);
                    }
                }, function(e) {
                    errorService.displayError(e);
                });
            } else {
                $scope.hasError = true;
                errorService.displayError({ Message: "Your comment message is empty. Please don't be that stupid." });
            }
        };

        $scope.createCommentForAdding = function () {
            if ($scope.comment.ParentCommentId) {
                $scope.comment.PostId = $scope.parentpostid;
                return $scope.comment;
            }
            return $scope.comment;
        };

        $rootScope.$watch('user', function () {
            if ($rootScope.user) {
                $scope.comment.User = $rootScope.user;
            }
        });
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "commentsService", "errorService"];

    return {
        restrict: 'EA',
        scope: {
            commentid: '=',
            postid: '=',
            user: '=',
            parentpostid: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "comments/commentsAddNew.html",
        controller: ctrlFn
    };
}]);

///#source 1 1 /Scripts/modules/comments/directives/commentsContainer.js
ngComments.directive('commentsContainer', [function () {
    var ctrlFn = function ($scope) {
        $scope.showAddComment = false;

        $scope.toggleShowAddComment = function() {
            $scope.showAddComment = !$scope.showAddComment;
        };

        $scope.$on("hideAddComment", function () {
            $scope.showAddComment = false;
        });
    };
    ctrlFn.$inject = ["$scope"];

    return {
        restrict: 'EA',
        scope: {
            user: '=',
            postid: '=',
            poster: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "comments/commentsContainer.html",
        controller: ctrlFn
    };
}]);

///#source 1 1 /Scripts/modules/comments/directives/commentsList.js
ngComments.directive('commentsList', [function () {
    var ctrlFn = function ($scope, $rootScope, postsService, commentsService, userService, errorService, configProvider) {
        $scope.comments = [];

        $scope.emptyCommentsMessage = "";

        $scope.hasError = false;

        $scope.getComments = function () {
            if (!isNaN($scope.postid)) {
                commentsService.getCommentsByPost($scope.postid).then(function (comments) {
                    $scope.hasError = false;
                    $scope.comments = comments;
                    postsService.subscribeToPost($scope.postid);
                }, function(err) {
                    $scope.hasError = true;
                    errorService.displayError(err);
                });
            } else {
                $scope.hasError = true;
            }
        };

        $scope.showEmptyCommentsMessage = function () {
            if ($scope.comments.length != 0) {
                return false;
            }
            return true;
        };

        $scope.emptyMessageStyle = function() {
            return $scope.hasError ? "alert-danger" : "alert-warning";
        };

        $scope.getEmptyCommentsMessage = function() {
            return $scope.hasError ?
                "Something went wrong with loading the comments! :(" :
                "There are no comments yet.";
        };

        $scope.$on(configProvider.getSocketClientFunctions().commentAdded, function (e, d) {
            d.comment = commentsService.addViewProperties(d.comment);

            if (d.commentId !== null && d.commentId != undefined) {
                var comment = _.where($scope.comments, { Id: d.commentId })[0];
                if (comment.Comments === null) comment.Comments = [];

                comment.Comments.unshift(d.comment);

                $(".comment-item[data-comment-id='" + d.comment.Id + "']").effect("highlight", { color: "#B3C833" }, 1500);
                $(".comment-item[data-comment-id='" + d.comment.ParentCommentId + "']").effect("highlight", { color: "#B3C833" }, 1500);
            } else {
                $scope.comments.unshift(d.comment);
                $(".comment-item[data-comment-id='" + d.comment.Id + "']").effect("highlight", { color: "#B3C833" }, 1500);
            }
        });

        $rootScope.$on(configProvider.getSocketClientFunctions().wsConnect, function () {
            postsService.subscribeToPost($scope.postid);
        });

        $scope.getComments();
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "postsService", "commentsService", "userService", "errorService", "configProvider"];

    return {
        restrict: 'EA',
        scope: {
            user: '=',
            postid: '=',
            poster: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "comments/commentsList.html",
        controller: ctrlFn
    };
}]);

///#source 1 1 /Scripts/modules/comments/services/commentsService.js
ngComments.factory('commentsService', ["$http", "$q", "configProvider", "dateHelper",
    function ($http, $q, configProvider, dateHelper) {
        var commentsApi = configProvider.getSettings().BlogApi == "" ?
            window.blogConfiguration.blogApi :
            configProvider.getSettings().BlogApi;

        return {
            getCommentsByPost: function (id) {
                var deferred = $q.defer();
                var that = this;

                $http({
                    url: commentsApi + "Posts/" + id + "/Comments",
                    method: "GET"
                }).success(function (response) {
                    _.each(response, function (c) {
                        c = that.addViewProperties(c, false, false);

                        _.each(c.Comments, function (r) {
                            that.addViewProperties(r);
                        });
                    });
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            getCommentsByUser: function (userId) {
                var userCommentsUrl = configProvider.getSettings().BlogApi == "" ?
                    window.blogConfiguration.blogApi + "user/" :
                    configProvider.getSettings().BlogApi + "user/";

                var deferred = $q.defer();
                var that = this;

                $http({
                    url: userCommentsUrl + userId + "/comments",
                    method: "GET"
                }).success(function (response) {
                    _.each(response, function (c) {
                        c = that.addViewProperties(c, false, false);

                        _.each(c.Comments, function (r) {
                            that.addViewProperties(r);
                        });
                    });
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            likeComment: function (commentId, username) {
                var deferred = $q.defer();

                $http({
                    url: commentsApi + "comments/likes?username=" + username + "&commentId=" + commentId,
                    method: "POST"
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            addComment: function (comment) {
                var deferred = $q.defer();

                $http({
                    url: commentsApi + "comments",
                    method: "POST",
                    data: comment
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            addViewProperties: function(comment, showReplies, showAddReply) {
                comment.DateDisplay = dateHelper.getDateDisplay(comment.CreatedDate);
                comment.NameDisplay = comment.User != null ? comment.User.FirstName + " " + comment.User.LastName : "";
                comment.Url = "/#/user/" + (comment.User != null ? comment.User.UserName : "");

                if (showReplies != undefined) {
                    comment.ShowReplies = false;
                }

                if (showAddReply != undefined) {
                    comment.ShowAddReply = false;
                }

                return comment;
            }
        };
    }
]);
///#source 1 1 /Scripts/modules/config/config.js
var ngConfig = angular.module("ngConfig", []);
///#source 1 1 /Scripts/modules/config/provider/configProvider.js
ngConfig.provider('configProvider', [function () {
    var windowDimensions = {
        width: 0,
        height: 0,
        mode: ""
    };

    var settings = {
        "BlogApi": "",
        "BlogRoot": "",
        "BlogSockets": "",
        "BlogSocketsAvailable": true,
        "HubUrl": "",
        "AlertTimer": 5000
    };

    var pageState = {
        POPULAR: "Popular",
        RECENT: "Recent",
        USEROWNED: "UserOwned"
    };

    var defaults = {
        profilePictureUrl: "",
        backgroundPictureUrl: ""
    };

    var navigationItems = [];

    var socketClientFunctions = {};

    this.$get = [function () {
        return {
            /* Getters */
            getSettings: function () {
                return settings;
            },

            getNavigationItems: function () {
                return navigationItems;
            },

            getDefaults: function () {
                return defaults;
            },

            getSocketClientFunctions: function () {
                return socketClientFunctions;
            },

            getBlogSocketsAvailability: function () {
                return settings.BlogSocketsAvailable;
            },

            /* Setters */
            setDimensions: function (w, h) {
                windowDimensions.width = w;
                windowDimensions.height = h;

                if (w >= 320 && w <= 767) {
                    windowDimensions.mode = "mobile";
                } else if (w >= 768 && w <= 1024) {
                    windowDimensions.mode = "tablet";
                } else if (w > 1024) {
                    windowDimensions.mode = "desktop";
                }
            },

            setSocketClientFunctions: function (val) {
                socketClientFunctions = val;
            },

            setBlogApiEndpoint: function (val) {
                settings.BlogApi = val;
            },

            setBlogRoot: function (val) {
                settings.BlogRoot = val;
            },

            setBlogSockets: function (val) {
                settings.BlogSockets = val;
            },

            setBlogSocketsAvailability: function (val) {
                settings.BlogSocketsAvailable = val;
            },

            setDefaultProfilePicture: function (val) {
                defaults.profilePictureUrl = val;
            },

            setDefaultBackgroundPicture: function (val) {
                defaults.backgroundPictureUrl = val;
            },

            setNavigationItems: function (items) {
                navigationItems = items;
            },

            /* Constants */
            pageState: pageState,
            windowDimensions: windowDimensions
        };
    }];
}]);
///#source 1 1 /Scripts/modules/error/error.js
var ngError = angular.module("ngError", ["ngConfig", "ngLogin"]);
///#source 1 1 /Scripts/modules/error/controllers/errorPageController.js
ngError.controller('errorPageController', ["$scope", "errorService", "configProvider",
    function ($scope, errorService, configProvider) {
        $scope.errorImage = configProvider.getSettings().BlogRoot + "/content/images/error-pages/servererror_bg2.png";
        $scope.error = errorService.getError();
    }
]);
///#source 1 1 /Scripts/modules/error/directives/errorDisplay.js
ngError.directive('errorDisplay', [
    function () {
        var ctrlFn = function ($scope, errorService) {
            $scope.errorMessage = "";

            $scope.$on("displayError", function (e, d) {
                errorService.setError(d);
            });
        };
        ctrlFn.$inject = ["$scope", "errorService"];

        var linkFn = function (scope, element) {
            scope.$on("displayError", function (e, d) {
                scope.errorMessage = d.Message != undefined ? d.Message : d;
                $(element).removeClass("hidden");
            });

            $("#blog-error-global .close-error").on("click", function (ev) {
                ev.preventDefault();
                $(element).addClass('hidden');
            });
        };

        return {
            restrict: 'EA',
            scope: { data: '=' },
            replace: true,
            templateUrl: window.blogConfiguration.templatesModulesUrl + "error/errorDisplay.html",
            controller: ctrlFn,
            link: linkFn
        };
    }]);

///#source 1 1 /Scripts/modules/error/services/errorService.js
ngError.factory('errorService', ["$location", "$rootScope", "$window", "configProvider", "authenticationService",
    function ($location, $rootScope, $window, configProvider, authenticationService) {
        var error = {};

        var isAuthorized = function (d) {
            if (d.error == "invalid_grant" || d.Message == "Authorization has been denied for this request.") {
                return false;
            } else {
                return true;
            }
        };

        var logoutUser = function () {
            authenticationService.logout().then(function () {
                $window.location.href = configProvider.getSettings().BlogRoot + '/account';
            }, function (e) {
                $rootScope.$broadcast("displayError", e);
            });
        };

        return {
            displayError: function (d) {
                error = d;
                if (isAuthorized(d)) {
                    $rootScope.$broadcast("displayError", d);
                }
            },

            displayErrorRedirect: function (d) {
                error = d;
                if (isAuthorized(d)) {
                    $location.path("/error");
                } else {
                    logoutUser();
                }
            },

            highlightField: function () {
                return "field-error";
            },

            setError: function (e) {
                error = e;
            },

            getError: function () {
                return error;
            }
        };
    }
]);
///#source 1 1 /Scripts/modules/header/header.js
var ngHeader = angular.module("ngHeader", ["ngConfig", "ngLogin"]);
///#source 1 1 /Scripts/modules/header/directives/headerMenu.js
ngHeader.directive('headerMenu', function () {
    var ctrlFn = function ($scope, $location, $rootScope, snapRemote, $http, $window, configProvider, authenticationService) {
        $scope.userLoggedIn = false;

        $scope.toggleClass = "nav-close";

        $scope.goAddNewPost = function () {
            $('#blog-header-collapsible').collapse("hide");
            $location.path("/post/create/new");
        };

        $scope.$on('toggleNavigation', function (ev, d) {
            snapRemote.toggle(d.direction, undefined);
        });

        $scope.testDisplayError = function () {
            $('#blog-header-collapsible').collapse("hide");
            $rootScope.$broadcast("displayError", { Message: "This is a test error message." });
        };

        $scope.getUserInfo = function () {
            $('#blog-header-collapsible').collapse("hide");

            authenticationService.getUserInfo().then(function (response) {
                if (response.Message != undefined || response.Message != null) {
                    $rootScope.$broadcast("launchLoginForm", { canClose: true });
                } else {
                    $rootScope.$broadcast("displayError", { Message: JSON.stringify(response)});
                }
            });
        };

        $scope.showLoginForm = function() {
            $rootScope.$broadcast("launchLoginForm", { canClose: true });
        };

        $scope.logout = function() {
            authenticationService.logout();
            $window.location.href = configProvider.getSettings().BlogRoot + "/account";
        };

        $scope.toggleSocketDebugger = function() {
            $rootScope.$broadcast("toggleSocketDebugger");
        };

        snapRemote.getSnapper().then(function (snapper) {
            var checkNav = function () {
                if ($scope.toggleClass == "nav-open") {
                    $scope.toggleClass = "nav-close";
                } else {
                    $scope.toggleClass = "nav-open";
                }
                $('#blog-header-collapsible').collapse("hide");
            };

            snapper.on('open', function () {
                checkNav();
            });

            snapper.on('close', function () {
                checkNav();
            });
        });
    };
    ctrlFn.$inject = ["$scope", "$location", "$rootScope", "snapRemote", "$http", "$window", "configProvider", "authenticationService"];

    var linkFn = function () {
    };

    return {
        link: linkFn,
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "header/headerMenu.html",
        controller: ctrlFn
    };
});
///#source 1 1 /Scripts/modules/logger/logger.js
var ngLogger = angular.module("ngLogger", ["ngConfig"]);
///#source 1 1 /Scripts/modules/logger/services/errorLogService.js
ngLogger.factory("errorLogService", ["$log", "$window", "configProvider", "stacktraceService", function ($log, $window, configProvider, stacktraceService) {
    function log(exception, cause) {
        var logApi = configProvider.getSettings().BlogApi == "" ?
            window.blogConfiguration.blogApi + "log" :
            configProvider.getSettings().BlogApi + "log";

        $log.error.apply($log, arguments);

        try {
            var errorMessage = exception.toString();
            var stackTrace = stacktraceService.print({ e: exception });

            $.ajax({
                type: "POST",
                url: logApi,
                contentType: "application/json",
                data: JSON.stringify({
                    ErrorUrl: $window.location.href,
                    ErrorMessage: errorMessage,
                    StackTrace: stackTrace,
                    Cause: (cause || "")
                }),
                success: function (d) {
                    $log.error.apply(d);
                }
            });

        } catch (loggingError) {
            $log.warn("Error logging failed");
            $log.log(loggingError);
        }
    }
    return (log);
}]);
///#source 1 1 /Scripts/modules/logger/services/stacktraceService.js
ngLogger.factory('stacktraceService', [function() {
    return ({
        print: printStackTrace
    });
}]);
///#source 1 1 /Scripts/modules/login/login.js
var ngLogin = angular.module("ngLogin",
    [
        "ngRoute",
        "ngConfig",
        "LocalStorageModule",
        "mgcrea.ngStrap"
    ]);
///#source 1 1 /Scripts/modules/login/directives/loggedUser.js
ngLogin.directive('loggedUser', function () {
    var ctrlFn = function ($scope, $rootScope, $location, $window, userService, configProvider, localStorageService, authenticationService) {
        $scope.user = {};

        $scope.authData = localStorageService.get("authorizationData");

        $scope.isLoggedIn = function () {
            if ($scope.authData) {
                return true;
            }
            return false;
        };

        $scope.goToProfile = function() {
            $location.path("/user");
        };

        $scope.logout = function () {
            authenticationService.logout();
            $window.location.href = configProvider.getSettings().BlogRoot + "/account";
        };

        $scope.launchLoginForm = function () {
            $rootScope.$broadcast("launchLoginForm", { canClose: true });
        };

        $rootScope.$watch('user', function () {
            if ($rootScope.user) {
                $scope.user = $rootScope.user;
                $scope.user.FullName = $scope.user.FirstName + " " + $scope.user.LastName;
            }
        });

        $rootScope.$on("userLoggedIn", function () {
            $scope.authData = localStorageService.get("authorizationData");
        });
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$location", "$window", "userService", "configProvider", "localStorageService", "authenticationService"];

    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        controller: ctrlFn,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "login/loggedUser.html",
    };
});

///#source 1 1 /Scripts/modules/login/directives/loginForm.js
ngLogin.directive('loginForm', function () {
    var ctrlFn = function ($scope, $rootScope, $timeout, $location, $window, errorService, localStorageService, configProvider, authenticationService) {
        $scope.username = "";

        $scope.password = "";

        $scope.rememberMe = false;

        $scope.errorMessage = "";

        $scope.registerPopover = {
            title: "Don't have an account?",
            content: "Create an account with Bloggity so you bloggity-bliggity-blog away!"
        };

        $scope.showErrorMessage = function() {
            if ($scope.errorMessage == "") {
                return false;
            }
            return true;
        };

        $scope.hasError = function () {
            if ($scope.errorMessage == "") {
                return "";
            }
            return "has-error";
        };

        $scope.login = function () {
            authenticationService.login($scope.username, $scope.password).then(function (response) {
                if (response.error == undefined || response.error == null) {

                    if (!$scope.isModal()) {
                        $window.location.href = configProvider.getSettings().BlogRoot;
                    } else {
                        $rootScope.$broadcast("hideLoginForm");
                        $rootScope.$broadcast("userLoggedIn", { username: $scope.username });
                        $location.path("/");
                    }
                } else {
                    $scope.errorMessage = response.error_description;
                }
            }, function (error) {
                $scope.errorMessage = error.Message;
            });
        };

        $scope.isModal = function() {
            if ($scope.modal == undefined) {
                return false;
            } else {
                return $scope.modal ? true : false;
            }
        };
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$timeout", "$location", "$window", "errorService", "localStorageService", "configProvider", "authenticationService"];

    var linkFn = function(scope, elem) {
        scope.showRegisterForm = function() {
            $(elem).closest(".modal-body").addClass("hover");
        };
    };

    return {
        restrict: 'EA',
        scope: { modal: '=' },
        link: linkFn,
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "login/loginform.html",
        controller: ctrlFn
    };
});

///#source 1 1 /Scripts/modules/login/directives/loginFormModal.js
ngLogin.directive('loginFormModal', function () {
    var ctrlFn = function ($scope, $rootScope, $modal) {
        $scope.loginModal = $modal({
            template: window.blogConfiguration.templatesModulesUrl + "login/loginformmodal.html",
            show: false,
            keyboard: false,
            backdrop: 'static'
        });

        $rootScope.$on("launchLoginForm", function (ev, data) {
            try {
                if ($scope.loginModal.$options.show) return;

                if (data.canClose) {
                    $scope.loginModal.$options.keyboard = true;
                    $scope.loginModal.$options.backdrop = true;
                } else {
                    $scope.loginModal.$options.keyboard = false;
                    $scope.loginModal.$options.backdrop = 'static';
                }
                $scope.loginModal.$promise.then($scope.loginModal.show);
            } catch (ex) {
                $scope.loginModal.$options.keyboard = false;
                $scope.loginModal.$options.backdrop = 'static';
                $scope.loginModal.$promise.then($scope.loginModal.show);
            }
        });

        $rootScope.$on("hideLoginForm", function () {
            $scope.loginModal.hide();
        });
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$modal"];

    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        controller: ctrlFn
    };
});

///#source 1 1 /Scripts/modules/login/directives/registerForm.js
ngLogin.directive('registerForm', function () {
    var ctrlFn = function ($scope, $rootScope, $timeout, $window, errorService, configProvider, authenticationService, blockUiService) {
        $scope.username = "";
        $scope.password = "";
        $scope.confirmPassword = "";
        $scope.firstName = "";
        $scope.lastName = "";
        $scope.email = "";
        $scope.birthDate = "";
        $scope.errors = [];
        $scope.messageDisplay = {
            show: false,
            type: "alert-success",
            message: ""
        };

        $scope.register = function () {
            blockUiService.blockIt({
                html: '<h4><img src="content/images/loader-girl.gif" height="128" /></h4>',
                css: {
                    border: 'none',
                    padding: '5px',
                    backgroundColor: '#000',
                    opacity: .5,
                    color: '#fff'
                }
            });

            var registrationInfo = {
                Username: $scope.username,
                Password: $scope.password,
                ConfirmPassword: $scope.confirmPassword,
                FirstName: $scope.firstName,
                LastName: $scope.lastName,
                Email: $scope.email,
                BirthDate: $scope.birthDate
            };

            authenticationService.saveRegistration(registrationInfo).then(function (response) {
                if (response.error == undefined || response.error == null) {
                    blockUiService.unblockIt();
                    blockUiService.blockIt({
                        html: '<div class="alert alert-success"><p>Hooray! You have successfully registered an account to Bloggity! Let\'s sign you in now.</p></div>',
                        css: {
                            border: 'none',
                            padding: '0',
                            backgroundColor: '#000',
                            color: '#fff'
                        }
                    });

                    authenticationService.login($scope.username, $scope.password).then(function (loginResponse) {
                        if (loginResponse.error == undefined || loginResponse.error == null) {
                            if (!$scope.isModal()) {
                                $window.location.href = configProvider.getSettings().BlogRoot;
                            } else {
                                $rootScope.$broadcast("hideLoginForm");
                                $rootScope.$broadcast("userLoggedIn");
                            }
                        } else {
                            $scope.showMessageDisplay("alert-warning",
                                "Oops! We've managed to create your account but there was a problem logging you in. (" + response.error_description + ")");
                        }
                        blockUiService.unblockIt();
                    }, function (error) {
                        $scope.showMessageDisplay("alert-warning",
                                "Oops! We've managed to create your account but there was a problem logging you in. (" + error.Message + ")");
                        blockUiService.unblockIt();
                    });
                } else {
                    blockUiService.unblockIt();
                    $scope.showMessageDisplay("alert-danger", error.Message);
                }
            }, function (error) {
                try {
                    for (var key in error.ModelState) {
                        var errorItem = {
                            field: key == "" ? "username" : key.split('.')[1].toLowerCase(),
                            message: error.ModelState[key][0]
                        };
                        $scope.errors.push(errorItem);
                    }
                    blockUiService.unblockIt();
                } catch (ex) {
                    blockUiService.unblockIt();
                }
            });
        };

        $scope.hasError = function (name) {
            var classStr = "";

            _.each($scope.errors, function (e) {
                if (e.field == name) {
                    classStr = "has-error";
                    $(".login-form.register").find(".content input[name='" + e.field + "']").prev('p').text(e.message);
                }
            });

            return classStr;
        };

        $scope.showMessageDisplay = function(type, message) {
            $scope.messageDisplay.type = type;
            $scope.messageDisplay.message = message;
            $scope.messageDisplay.show = true;
        };

        $scope.isModal = function () {
            if ($scope.modal == undefined) {
                return false;
            } else {
                return $scope.modal ? true : false;
            }
        };
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$timeout", "$window", "errorService", "configProvider", "authenticationService", "blockUiService"];

    var linkFn = function (scope, elem) {
        scope.showLoginForm = function () {
            $(elem).closest(".modal-body").removeClass("hover");
        };
    };

    return {
        restrict: 'EA',
        scope: { modal: '=' },
        replace: true,
        link: linkFn,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "login/registerform.html",
        controller: ctrlFn
    };
});

///#source 1 1 /Scripts/modules/login/services/authenticationInterceptorService.js
ngLogin.factory('authenticationInterceptorService', ['$q', '$rootScope', '$location', 'localStorageService',
    function ($q, $rootScope, $location, localStorageService) {
        return {
            request: function(config) {
                config.headers = config.headers || {};
                var authData = localStorageService.get('authorizationData');
                if (authData) {
                    config.headers.Authorization = 'Bearer ' + authData.token;
                }

                return config;
            },

            responseError: function(rejection) {
                if (rejection.status === 401) {
                    $location.path("/");
                    $rootScope.$broadcast("launchLoginForm", { canClose: true });
                    localStorageService.remove('username');
                    localStorageService.remove('authorizationData');
                }
                return $q.reject(rejection);
            }
        };
    }
]);
///#source 1 1 /Scripts/modules/login/services/authenticationService.js
ngLogin.factory('authenticationService', ['$http', '$q', 'configProvider', 'localStorageService',
    function ($http, $q, configProvider, localStorageService) {
        var authenticationApi = configProvider.getSettings().BlogApi == "" ?
            window.blogConfiguration.blogApi + "Account/" : configProvider.getSettings().BlogApi + "Account/";

        var authentication = {
            isAuthenticated: false,
            username: ""
        };

        return {
            saveRegistration: function (registerInfo) {
                this.logout();

                var deferred = $q.defer();

                $http({
                    url: authenticationApi + "register?trNsUm3GtEwsLe='allow'",
                    method: "POST",
                    data: registerInfo
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (err) {
                    deferred.reject(err);
                });

                return deferred.promise;
            },

            getUserInfo: function () {
                var deferred = $q.defer();

                $http({
                    url: authenticationApi + "userinfo",
                    method: "GET"
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (error) {
                    deferred.reject(error);
                });

                return deferred.promise;
            },

            getAuthenticationData: function () {
                return authentication;
            },

            createAuthenticationData: function () {
                var authData = localStorageService.get('authorizationData');
                if (authData) {
                    authentication.isAuthenticated = true;
                    authentication.username = authData.userName;
                }
            },

            login: function (username, password) {
                var deferred = $q.defer();
                var credentials = "grant_type=password&Username=" + username + "&Password=" + password;

                $http.post(authenticationApi + "login", credentials, {
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                }).success(function (response) {
                    localStorageService.add("username", username);
                    localStorageService.set('authorizationData', {
                         token: response.access_token, username: username
                    });

                    authentication.isAuthenticated = true;
                    authentication.username = username;

                    deferred.resolve(response);
                }).error(function (response) {
                    deferred.reject({ Message: response.error_description });
                });

                return deferred.promise;
            },

            logout: function () {
                localStorageService.remove('username');
                localStorageService.remove('authorizationData');
                authentication.isAuthenticated = false;
                authentication.username = "";
            }
        };
    }
]);
///#source 1 1 /Scripts/modules/login/services/loginService.js
ngLogin.factory('loginService', ["$http", "$q", "$window", "configProvider", function ($http, $q, $window, configProvider) {
    var sessionApi = configProvider.getSettings().BlogRoot == "" ? window.blogConfiguration.blogRoot + "Authentication" : configProvider.getSettings().BlogRoot + "Authentication";
    var authApi = configProvider.getSettings().BlogApi == "" ? window.blogConfiguration.blogApi + "Authenticate" : configProvider.getSettings().BlogApi + "Authenticate";

    return {
        login: function (username, password, rememberMe) {
            var deferred = $q.defer();
            var credentials = {
                Username: username,
                Password: password,
                RememberMe: rememberMe
            };

            $http({
                url: sessionApi + "/Login",
                method: "POST",
                data: credentials
            }).success(function (response) {
                if (response.Session != null && response.User != null) {
                    deferred.resolve(response);
                } else {
                    deferred.reject(response);
                }
            }).error(function () {
                deferred.reject({ Message: "Error communicating with login server!" });
            });

            return deferred.promise;
        },

        logout: function (username) {
            var deferred = $q.defer();
            var credentials = {
                Username: username
            };

            $http({
                url: sessionApi + "/Logout",
                method: "POST",
                data: credentials
            }).success(function (response) {
                if (response == null || response == "") {
                    deferred.resolve(response);
                } else {
                    deferred.reject(response);
                }
            }).error(function () {
                deferred.reject({ Message: "Error communicating with login server!" });
            });

            return deferred.promise;
        },

        loginApi: function (username, password, rememberMe) {
            var deferred = $q.defer();
            var credentials = {
                Username: username,
                Password: password,
                RememberMe: rememberMe
            };

            $http({
                url: authApi,
                method: "POST",
                data: credentials
            }).success(function (response) {
                deferred.resolve(response);
            }).error(function () {
                deferred.reject({ Message: "Error authenticating in the API!" });
            });

            return deferred.promise;
        },

        logoutApi: function (username) {
            var deferred = $q.defer();
            var credentials = {
                Username: username
            };

            $http({
                url: authApi,
                method: "PUT",
                data: credentials
            }).success(function (response) {
                deferred.resolve(response);
            }).error(function () {
                deferred.reject({ Message: "Error logging out in the API!" });
            });

            return deferred.promise;
        }
    };
}]);
///#source 1 1 /Scripts/modules/main/app.js
var blog = angular.module("blog",
    [
        "ngRoute",
        "ngAnimate",
        "ngCookies",
        "mgcrea.ngStrap",
        "snap",
        "ngConfig",
        "ngLogger",
        "ngHeader",
        "ngLogin",
        "ngPosts",
        "ngComments",
        "ngError",
        "ngNavigation",
        "ngUser",
        "ngTags",
        "ui.router"
    ]);

blog.run([
    '$rootScope', '$state', '$stateParams',
    function ($rootScope, $state, $stateParams) {
        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;
    }
]);
///#source 1 1 /Scripts/modules/main/config/blogConfig.js
blog.config(["$routeProvider", "$httpProvider", "$provide", "$stateProvider", "$urlRouterProvider",
    function ($routeProvider, $httpProvider, $provide, $stateProvider, $urlRouterProvider) {
        $provide.factory('httpInterceptor', ["$q", "$location", "blockUiService", function ($q, $location, blockUiService) {
            return {
                request: function (config) {
                    blockUiService.blockIt();

                    return config || $q.when(config);
                },

                requestError: function (rejection) {
                    blockUiService.blockIt();
                    return $q.reject(rejection);
                },

                response: function (response) {
                    blockUiService.unblockIt();

                    return response || $q.when(response);
                },

                responseError: function (rejection) {
                    blockUiService.unblockIt();

                    return $q.reject(rejection);
                }
            };
        }]);

        $httpProvider.interceptors.push('httpInterceptor');
        $httpProvider.interceptors.push('authenticationInterceptorService');

        $urlRouterProvider.otherwise("/");

        $stateProvider
            .state('posts', {
                url: "/",
                templateUrl: window.blogConfiguration.templatesUrl + 'posts.html',
                controller: 'postsController'
            })
            .state('viewpost', {
                url: "/post/:postId",
                templateUrl: window.blogConfiguration.templatesUrl + 'viewpost.html',
                controller: 'postsViewController'
            })
                .state('viewpost.gallery', {
                    url: "/gallery",
                    controller: 'mediaGalleryController'
                })
            .state('friends', {
                url: "/friends",
                templateUrl: window.blogConfiguration.templatesUrl + 'friends.html'
            })
            .state('groups', {
                url: "/groups",
                templateUrl: window.blogConfiguration.templatesUrl + 'groups.html'
            })
            .state('events', {
                url: "/events",
                templateUrl: window.blogConfiguration.templatesUrl + 'events.html'
            })
            .state('newpost', {
                url: "/post/create/new",
                templateUrl: window.blogConfiguration.templatesUrl + 'modifypost.html',
                controller: 'postsModifyController'
            })
            .state('editpost', {
                url: "/post/edit/:postId",
                templateUrl: window.blogConfiguration.templatesUrl + 'modifypost.html',
                controller: 'postsModifyController'
            })
            .state('ownprofile', {
                url: "/user",
                templateUrl: window.blogConfiguration.templatesUrl + 'users.html',
                controller: 'userProfileController',
                'abstract': true
            })
                .state('ownprofile.details', {
                    url: '',
                    templateUrl: window.blogConfiguration.templatesUrl + 'modules/user/userprofiledetails.html',
                    controller: 'userProfileController'
                })
                .state('ownprofile.posts', {
                    url: '/posts',
                    templateUrl: window.blogConfiguration.templatesUrl + 'modules/user/userprofileposts.html',
                    controller: 'userProfilePostsController'
                })
                .state('ownprofile.comments', {
                    url: '/comments',
                    templateUrl: window.blogConfiguration.templatesUrl + 'modules/user/userprofilecomments.html',
                    controller: 'userProfileCommentsController'
                })
                .state('ownprofile.favorites', {
                    url: '/favorites',
                    templateUrl: window.blogConfiguration.templatesUrl + 'modules/user/userprofilefavorites.html',
                    controller: 'userProfileFavoritesController'
                })
                .state('ownprofile.media', {
                    url: '/media',
                    templateUrl: window.blogConfiguration.templatesUrl + 'modules/user/userprofilemedia.html',
                    controller: 'userProfileMediaController'
                })
                    .state('ownprofile.media.gallery', {
                        url: '/gallery/:albumName',
                        controller: 'mediaGalleryController'
                    })
            .state('othersprofile', {
                url: "/user/:username",
                templateUrl: window.blogConfiguration.templatesUrl + 'users.html',
                controller: 'userProfileController',
                'abstract': true
            })
                .state('othersprofile.details', {
                    url: '',
                    templateUrl: window.blogConfiguration.templatesUrl + 'modules/user/userprofiledetails.html',
                    controller: 'userProfileController'
                })
                .state('othersprofile.posts', {
                    url: '/posts',
                    templateUrl: window.blogConfiguration.templatesUrl + 'modules/user/userprofileposts.html',
                    controller: 'userProfilePostsController'
                })
                .state('othersprofile.comments', {
                    url: '/comments',
                    templateUrl: window.blogConfiguration.templatesUrl + 'modules/user/userprofilecomments.html',
                    controller: 'userProfileCommentsController'
                })
                .state('othersprofile.favorites', {
                    url: '/favorites',
                    templateUrl: window.blogConfiguration.templatesUrl + 'modules/user/userprofilefavorites.html',
                    controller: 'userProfileFavoritesController'
                }).state('othersprofile.media', {
                    url: '/media',
                    templateUrl: window.blogConfiguration.templatesUrl + 'modules/user/userprofilemedia.html',
                    controller: 'userProfileMediaController'
                })
                    .state('othersprofile.media.gallery', {
                        url: '/gallery/:albumName',
                        controller: 'mediaGalleryController'
                    })
            .state('error', {
                url: "/error",
                templateUrl: window.blogConfiguration.templatesUrl + 'errorpage.html',
                controller: 'errorPageController'
            });
    }
]);
///#source 1 1 /Scripts/modules/main/controllers/blogMainController.js
blog.controller('blogMainController', ["$scope", "$location", "$rootScope", "$log", "$timeout", "configProvider",
    "localStorageService", "postsService", "userService", "authenticationService", 
    function ($scope, $location, $rootScope, $log, $timeout, configProvider, localStorageService, postsService,
        userService, authenticationService) {

        $scope.authData = localStorageService.get('authorizationData');

        $scope.username = null;

        $rootScope.$on("$locationChangeStart", function (event, next, current) {
            $log.info("location changing from " + current + " to " + next);

            if (current !== configProvider.getSettings().BlogRoot + "/#/") {
                postsService.getRecentPosts()
                    .then(function (response) {
                        $log.info(response);
                    }, function (error) {
                        console.log(error);
                    });
            }

            if ($rootScope.user) {
                $rootScope.$broadcast("loggedInUserInfo", $rootScope.user);
            }
        });

        $scope.init = function() {
            if ($scope.authData != null) {
                $scope.username = localStorageService.get('username');

                authenticationService.getUserInfo().then(function(response) {
                    if (response.Message == undefined || response.Message == null) {
                        $scope.getUserInfo($scope.username);
                    }
                }, function() {
                    authenticationService.logout();
                });
            } else {
                authenticationService.logout();
            }
        };

        $scope.getUserInfo = function (username) {
            userService.getUserInfo(username).then(function (user) {
                if (user.Error == null) {
                    $rootScope.user = user;
                    $rootScope.authData = $scope.authData;
                    $timeout(function () {
                        $rootScope.$broadcast("loggedInUserInfo", user);
                    }, 1500);
                }
            });
        };

        $rootScope.$on("userLoggedIn", function (ev, data) {
            $scope.getUserInfo(data.username);
        });

        $scope.init();
    }
]);
///#source 1 1 /Scripts/modules/main/directives/windowResize.js
blog.directive("windowResize", ["$window", "$rootScope", "$timeout", function ($window, $rootScope, $timeout) {
    return {
        restrict: 'EA',
        link: function postLink(scope) {
            scope.onResizeFunction = function () {
                scope.windowHeight = $window.innerHeight;
                scope.windowWidth = $window.innerWidth;
                $rootScope.$broadcast("windowSizeChanged", {
                    height: scope.windowHeight,
                    width: scope.windowWidth
                });
            };

            scope.onResizeFunction();

            angular.element($window).bind('resize', function () {
                $timeout(function () {
                    scope.onResizeFunction();
                    scope.$apply();
                }, 500);
            });
        }
    };
}]);

///#source 1 1 /Scripts/modules/media/media.js
var ngMedia = angular.module("ngMedia",
    [
        "ngShared",
        "iso.directives",
        "angularFileUpload",
        "slick"
    ]);
///#source 1 1 /Scripts/modules/media/controllers/mediaGalleryController.js
ngMedia.controller('mediaGalleryController', ["$scope", "$rootScope", "$interval", "$timeout",
    function ($scope, $rootScope, $interval, $timeout) {
        $scope.init = function () {
            var stop;
            var topic = "launchMediaGallery";

            stop = $interval(function () {
                if ($rootScope.$$listeners[topic] && $rootScope.$$listeners[topic].length > 0) {
                    $timeout(function () {
                        $rootScope.$broadcast(topic, {});
                        $interval.cancel(stop);
                        stop = undefined;
                    }, 250);
                }
            }, 250);
        };

        $scope.init();
    }
]);
///#source 1 1 /Scripts/modules/media/directives/albumGroup.js
// ReSharper disable InconsistentNaming

ngMedia.directive('albumGroup', function () {
    var ctrlFn = function ($scope, $rootScope, $window, albumService, mediaService, errorService, dateHelper, configProvider,
        $modal, FileUploader, localStorageService) {
        $scope.uploadUrl = configProvider.getSettings().BlogApi == "" ?
           $window.blogConfiguration.blogApi + "media?username=" + $scope.user.UserName + "&album=" + encodeURIComponent($scope.album.AlbumName) :
           configProvider.getSettings().BlogApi + "media?username=" + $scope.user.UserName + "&album=" + encodeURIComponent($scope.album.AlbumName);

        $scope.isExpanded = !$scope.album.IsNew ? true : false;

        $scope.authData = localStorageService.get("authorizationData");

        $scope.toggleExpandClass = function () {
            if ($scope.isExpanded) {
                return "fa-minus";
            }
            return "fa-plus";
        };

        $scope.toggleExpanded = function () {
            if (!$scope.album.IsNew) $scope.isExpanded = !$scope.isExpanded;
        };

        $scope.editAlbum = function () {
            $scope.album.IsEditing = true;
        };

        $scope.cancelEditAlbum = function () {
            if ($scope.album.IsNew) {
                $scope.$emit('cancelledAddingOfAlbum', $scope.album);
                $scope.album.IsEditing = false;
            }
            $scope.album.IsEditing = false;
        };

        $scope.saveAlbum = function () {
            var album = {
                IsUserDefault: false,
                AlbumName: $scope.album.AlbumName,
                AlbumId: !$scope.album.AlbumId ? 0 : $scope.album.AlbumId
            };

            if ($rootScope.authData) {
                album.User = $rootScope.user;
            }

            if ($scope.album.IsNew) {
                addAlbum(album);
            } else {
                updateAlbum(album);
            }
        };

        $scope.deleteAlbum = function () {
            mediaDeleteDialog.$promise.then(mediaDeleteDialog.show);
        };

        $scope.confirmDelete = function () {
            albumService.deleteAlbum($scope.album.AlbumId).then(function (response) {
                mediaDeleteDialog.hide();

                if (response.Error != null) {
                    errorService.displayError(response.Error);
                    return;
                }

                $scope.$emit("successDeletingAlbum", $scope.album);
            }, function (err) {
                $scope.setModelStateErrors(err.ModelState);
                mediaDeleteDialog.hide();
            });
        };

        $scope.$on("successDeletingMedia", function(ev, data) {
            var index = $scope.album.Media.indexOf(data);
            $scope.album.Media.splice(index, 1);
        });

        $scope.init = function() {
            mediaService.addViewedMediaListFromAlbum($scope.album.Media, $scope.user.UserName, $scope.album.AlbumName);
        };

        $scope.init();

        var mediaDeleteDialog = $modal({
            title: 'Delete?',
            content: "Are you sure you want to delete this album? Doing so will also delete all the media in it.",
            scope: $scope,
            template: $window.blogConfiguration.templatesModulesUrl + "media/mediaDeleteDialog.html",
            show: false
        });

        var addAlbum = function (album) {
            albumService.addAlbum(album).then(function (response) {
                if (response.Error != null) {
                    errorService.displayError(response.Error);
                    return;
                }
                response.IsEdit = false;
                response.IsNew = false;
                $scope.album = response;
            }, function (err) {
                errorService.displayError(err);
            });
        };

        var updateAlbum = function (album) {
            albumService.updateAlbum(album).then(function (response) {
                if (response.Error != null) {
                    errorService.displayError(response.Error);
                    return;
                }
                response.IsEdit = false;
                response.IsNew = false;
                $scope.album = response;
            }, function (err) {
                errorService.displayError(err);
            });
        };

        // #region angular-file-upload
        
        var uploader = $scope.uploader = new FileUploader({
            scope: $rootScope,
            url: $scope.uploadUrl,
            autoUpload: true,
            headers: { Authorization: 'Bearer ' + ($scope.authData ? $scope.authData.token : "") }
        });

        uploader.filters.push({
            name: 'imageFilter',
            fn: function (item) {
                var type = uploader.isHTML5 ? item.type : '/' + item.value.slice(item.value.lastIndexOf('.') + 1);
                type = '|' + type.toLowerCase().slice(type.lastIndexOf('/') + 1) + '|';
                return '|jpg|png|jpeg|bmp|gif|mp4|flv|webm|'.indexOf(type) !== -1;
            }
        });

        uploader.onSuccessItem = function (fileItem, response) {
            response.CreatedDateDisplay = dateHelper.getDateDisplay(response.CreatedDate);
            $scope.album.Media.push(response);
            $rootScope.$broadcast("resizeIsotopeItems", {});
        };

        // #endregion
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$window", "albumService", "mediaService", "errorService", 
        "dateHelper", "configProvider", "$modal", "FileUploader", "localStorageService"];

    return {
        restrict: 'EA',
        scope: {
            album: '=',
            user: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "media/albumGroup.html",
        controller: ctrlFn
    };
});

// ReSharper restore InconsistentNaming
///#source 1 1 /Scripts/modules/media/directives/mediaGalleryView.js
ngMedia.directive('mediaGalleryView', function () {
    var ctrlFn = function ($scope, $rootScope, $window, $location, $modal, mediaService, localStorageService) {
        $scope.mediaList = [];

        $scope.username = localStorageService.get("username");

        var mediaSelectionDialog = $modal({
            title: 'Gallery view',
            scope: $scope,
            template: window.blogConfiguration.templatesModulesUrl + "media/mediaGallery.html",
            show: false
        });

        $scope.closeGallery = function () {
            mediaSelectionDialog.hide();
            $scope.mediaList = [];

            if ($rootScope.$stateParams.postId) {
                $location.path("/post/" + $rootScope.$stateParams.postId);
            } else {
                if ($rootScope.$stateParams.username) {
                    $location.path("/user/" + $scope.user.UserName + '/media');
                } else {
                    $location.path("/user/media");
                }
            }
        };

        $rootScope.$on("launchMediaGallery", function () {
            if ($rootScope.$stateParams.postId) {
                mediaService.getViewMediaListFromPost($rootScope.$stateParams.postId)
                    .then(function (response) {
                        $scope.mediaList = response;
                    }, function (error) {
                        console.log(error);
                    });
            } else {
                if ($rootScope.$stateParams.username) {
                    mediaService.getViewMediaListFromAlbum($rootScope.$stateParams.username, $rootScope.$stateParams.albumName)
                        .then(function (response) {
                            $scope.mediaList = response;
                        }, function (error) {
                            console.log(error);
                        });
                } else {
                    mediaService.getViewMediaListFromAlbum($scope.username, $rootScope.$stateParams.albumName)
                        .then(function (response) {
                            $scope.mediaList = response;
                        }, function (error) {
                            console.log(error);
                        });
                }
            }

            mediaSelectionDialog.$promise.then(mediaSelectionDialog.show);
        });

        $scope.getWindowHeight = function () {
            return { height: $window.innerHeight - 30 + 'px' };
        };

        $scope.showMediaGallery = function () {
            if ($scope.mediaList && $scope.mediaList.length > 0) {
                return true;
            }
            return false;
        };
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$window", "$location", "$modal", "mediaService", "localStorageService"];

    return {
        restrict: 'EA',
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "media/mediaGallery.html",
        controller: ctrlFn
    };
});

///#source 1 1 /Scripts/modules/media/directives/mediaGroupedList.js
ngMedia.directive('mediaGroupedList', function () {
    var ctrlFn = function ($scope) {
        $scope.isAdding = false;

        $scope.addAlbum = function () {
            var newAlbum = {
                AlbumName: '',
                IsNew: true,
                IsEditing: true
            };
            $scope.albums.push(newAlbum);
            $scope.isAdding = true;
        };

        $scope.$on('cancelledAddingOfAlbum', function (ev, data) {
            var index = $scope.albums.indexOf(data);
            $scope.albums.splice(index, 1);
            $scope.isAdding = false;
        });

        $scope.$on('successDeletingAlbum', function (ev, data) {
            var index = $scope.albums.indexOf(data);
            $scope.albums.splice(index, 1);
        });
    };
    ctrlFn.$inject = ["$scope"];

    return {
        restrict: 'EA',
        scope: {
            albums: '=',
            user: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "media/mediaGroupedList.html",
        controller: ctrlFn
    };
});

///#source 1 1 /Scripts/modules/media/directives/mediaItem.js
ngMedia.directive('mediaItem', function () {
    var ctrlFn = function ($scope, $rootScope, $location, localStorageService, $modal, mediaService, errorService) {
        var mediaDeleteDialog = $modal({
            title: 'Delete?',
            content: "Are you sure you want to delete this item?",
            scope: $scope,
            template: window.blogConfiguration.templatesModulesUrl + "media/mediaDeleteDialog.html",
            show: false
        });

        $scope.username = localStorageService.get("username");

        $scope.deleteButtonVisible = false;

        $scope.viewMode = function () {
            if ($scope.mode && $scope.mode === 'thumbnail') {
                return "thumbnail";
            }
            return "";
        };

        $scope.getThumbnailUrl = function () {
            if ($scope.crop && $scope.crop === 'true') {
                if ($scope.media) {
                    return {
                        "background-image": "url(" + $scope.media.ThumbnailUrl + ")"
                    };
                }
                return {
                    "background-image": "url(/content/images/warning.png)"
                };
            }
            return {};
        };

        $scope.toggleDelete = function () {
            if ($scope.allowDelete && $scope.allowDelete === 'true' && $rootScope.authData) {
                if ($scope.user && $scope.username === $scope.user.UserName) {
                    return true;
                }
            }
            return false;
        };

        $scope.deleteMedia = function () {
            mediaDeleteDialog.$promise.then(mediaDeleteDialog.show);
        };

        $scope.toggleGallery = function () {
            if ($scope.galleryMode && $scope.galleryMode === 'true') {
                return true;
            }
            return false;
        };

        $scope.viewAsGallery = function () {
            if ($rootScope.$stateParams.postId) {
                $location.path("/post/" + $rootScope.$stateParams.postId + '/gallery');
            } else {
                if ($rootScope.$stateParams.username) {
                    $location.path("/user/" + $scope.user.UserName + "/media/gallery/" + $scope.albumName.toLowerCase());
                } else {
                    $location.path("/user/media/gallery/" + $scope.albumName.toLowerCase());
                }
            }
        };

        $scope.confirmDelete = function () {
            mediaService.deleteMedia($scope.media.Id).then(function (response) {
                mediaDeleteDialog.hide();

                if (response.Error != null) {
                    errorService.displayError(response.Error);
                    return;
                }

                if (response) {
                    $scope.$emit("successDeletingMedia", $scope.media);
                } else {
                    errorService.displayError(response);
                }
            }, function (err) {
                $scope.setModelStateErrors(err.ModelState);
                mediaDeleteDialog.hide();
            });
        };

        $scope.getContentType = function (content) {
            if (content == undefined) return "image";

            var contentType = content.split('/');
            if (contentType[0] == "video") {
                return "video";
            } else {
                return "image";
            }
        };

        $scope.isVideo = function () {
            var supportedVideos = [
               "video/avi",
               "video/quicktime",
               "video/mpeg",
               "video/mp4",
               "video/x-flv"
            ];

            var isVideo = _.contains(supportedVideos, $scope.media.MediaType);
            return isVideo ? "hidden" : "";
        };
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$location", "localStorageService", "$modal", "mediaService", "errorService"];

    var linkFn = function (scope, elem, attrs) {
        scope.mode = attrs.mode;

        scope.crop = attrs.crop;

        scope.galleryMode = attrs.galleryMode;

        scope.allowDelete = attrs.allowDelete;

        scope.isCropped = function () {
            if (attrs.crop && attrs.crop === 'true') {
                return "center-cropped";
            }
            return "";
        };
    };

    return {
        restrict: 'EA',
        scope: {
            media: '=',
            user: '=',
            albumName: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "media/mediaItem.html",
        controller: ctrlFn,
        link: linkFn
    };
});

///#source 1 1 /Scripts/modules/media/services/albumService.js
ngMedia.factory('albumService', ["$http", "$q", "configProvider", "dateHelper",
    function ($http, $q, configProvider, dateHelper) {
        var albumApi = configProvider.getSettings().BlogApi == "" ?
            window.blogConfiguration.blogApi :
            configProvider.getSettings().BlogApi;

        return {
            getAlbumsByUser: function (userId) {
                var deferred = $q.defer();

                $http({
                    url: albumApi + "users/" + userId + "/albums",
                    method: "GET"
                }).success(function (response) {
                    _.each(response, function (a) {
                        a.CreatedDateDisplay = dateHelper.getDateDisplay(a.CreatedDate);
                        a.IsNew = false;
                        a.IsEditing = false;

                        _.each(a.Media, function (m) {
                            m.CreatedDateDisplay = dateHelper.getDateDisplay(m.CreatedDate);
                        });
                    });
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            getUserDefaultAlbum: function (userId) {
                var deferred = $q.defer();

                $http({
                    url: albumApi + "users/" + userId + "/albums/default",
                    method: "GET"
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            addAlbum: function (album) {
                var deferred = $q.defer();

                $http({
                    url: albumApi + "album",
                    method: "POST",
                    data: album
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            updateAlbum: function (album) {
                var deferred = $q.defer();

                $http({
                    url: albumApi + "album",
                    method: "PUT",
                    data: album
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            deleteAlbum: function (albumId) {
                var deferred = $q.defer();

                $http({
                    url: albumApi + "album/" + albumId,
                    method: "DELETE",
                }).success(function (response) {
                    if (response) {
                        deferred.resolve(response);
                    } else {
                        deferred.reject(response);
                    }
                }).error(function (error) {
                    deferred.reject(error);
                });

                return deferred.promise;
            }
        };
    }
]);
///#source 1 1 /Scripts/modules/media/services/mediaService.js
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
///#source 1 1 /Scripts/modules/messaging/messaging.js
var ngMessaging = angular.module("ngMessaging", ["ngConfig"]);
///#source 1 1 /Scripts/modules/navigation/navigation.js
var ngNavigation = angular.module("ngNavigation", ["ngConfig"]);
///#source 1 1 /Scripts/modules/navigation/directives/navigationMenu.js
ngNavigation.directive('navigationMenu', function () {
    var ctrlFn = function ($scope, $rootScope, $window, userService, configProvider, localStorageService, authenticationService) {
        $scope.navigationItems = configProvider.getNavigationItems();

        $scope.user = {};

        $scope.authData = localStorageService.get("authorizationData");

        $scope.isLoggedIn = function() {
            if ($scope.authData) {
                return true;
            }
            return false;
        };

        $scope.logout = function () {
            authenticationService.logout();
            $window.location.href = configProvider.getSettings().BlogRoot + "/account";
        };
        
        $scope.launchLoginForm = function() {
            $rootScope.$broadcast("launchLoginForm", { canClose: true });
        };
        
        $scope.toggleNavigation = function() {
            $rootScope.$broadcast("toggleNavigation", { direction: 'left' });
        };

        $rootScope.$watch('user', function () {
            if ($rootScope.user) {
                $scope.user = $rootScope.user;
                $scope.user.FullName = $scope.user.FirstName + " " + $scope.user.LastName;
            }
        });

        $rootScope.$on("userLoggedIn", function () {
            $scope.authData = localStorageService.get("authorizationData");
        });
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$window", "userService", "configProvider", "localStorageService", "authenticationService"];

    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "navigation/navigationMenu.html",
        controller: ctrlFn
    };
});

///#source 1 1 /Scripts/modules/posts/posts.js
var ngPosts = angular.module("ngPosts",
    [
        "ngSanitize",
        "ngComments",
        "ngTags",
        "ngUser",
        "ngMedia",
        "ngError",
        "ngMedia",
        "ngBlogSockets",
        "ngCkeditor",
        "ngTagsInput",
        "iso.directives",
        "ngConfig",
        "LocalStorageModule",
        "angularFileUpload",
        "slick"
    ]);
///#source 1 1 /Scripts/modules/posts/controllers/postsController.js
ngPosts.controller('postsController', ["$scope", "$rootScope", "$location", "$timeout", "$interval", "localStorageService", "postsService", "errorService",
    function ($scope, $rootScope, $location, $timeout, $interval, localStorageService, postsService, errorService) {
        $scope.posts = [];
        $scope.size = "";
        $scope.isBusy = false;

        $scope.init = function() {
            $scope.getRecentPosts();
            $rootScope.$broadcast("updateScrollTriggerWatch", "posts-main");
        };
        
        $scope.getRecentPosts = function () {
            if ($scope.isBusy) {
                return;
            }
            $scope.isBusy = true;

            postsService.getRecentPosts().then(function (resp) {
                $scope.posts = resp;
                $scope.isBusy = false;
                $scope.$broadcast("resizeIsotopeItems");
            }, function (e) {
                errorService.displayError(e);
            });
        };

        $scope.getMoreRecentPosts = function () {
            if ($scope.isBusy) {
                return;
            }
            $scope.isBusy = true;
            
            postsService.getMoreRecentPosts($scope.posts.length).then(function (resp) {
                _.each(resp, function (p) {
                    $scope.posts.push(p);
                });
                $scope.isBusy = false;
                $scope.$broadcast("resizeIsotopeItems");
            }, function (e) {
                errorService.displayError(e);
            });
        };

        $scope.$on("updatePostsSize", function (ev, size) {
            $scope.size = size;
        });

        $scope.$on("scrollBottom", function () {
            $scope.getMoreRecentPosts();
        });

        $scope.init();
    }
]);
///#source 1 1 /Scripts/modules/posts/controllers/postsModifyController.js
// ReSharper disable InconsistentNaming

ngPosts.controller('postsModifyController', ["$scope", "$rootScope", "$location", "$timeout", "$window", "$modal",
    "FileUploader", "localStorageService", "postsService", "userService", "albumService", "tagsService", "errorService",
    "dateHelper", "configProvider", "authenticationService",
    function ($scope, $rootScope, $location, $timeout, $window, $modal, FileUploader, localStorageService,
        postsService, userService, albumService, tagsService, errorService, dateHelper, configProvider,
        authenticationService) {

        var mediaSelectionDialog = $modal({
            title: 'Select media to add',
            scope: $scope,
            template: window.blogConfiguration.templatesModulesUrl + "media/mediaSelectionDialog.html",
            show: false
        });

        $scope.user = $rootScope.user;

        $scope.isAdding = true;

        $scope.existingContents = [];

        $scope.albums = [];

        $scope.username = localStorageService.get("username");

        $scope.authData = localStorageService.get("authorizationData");

        $scope.dimensionMode = configProvider.windowDimensions.mode == "" ?
            window.getDimensionMode() : configProvider.windowDimensions.mode;

        $scope.post = {
            PostTitle: "",
            PostMessage: "",
            PostContents: [],
            Tags: []
        };

        $scope.uploadUrl = configProvider.getSettings().BlogApi == "" ?
            window.blogConfiguration.blogApi + "media?username=" + $scope.username + "&album=default" :
            configProvider.getSettings().BlogApi + "media?username=" + $scope.username + "&album=default";

        $scope.onTagAdded = function (t) {
            var tag = { TagName: t.text };
            $scope.post.Tags.push(tag);
        };

        $scope.onTagRemoved = function (t) {
            var tag = { TagName: t.text };
            var index = $scope.post.Tags.indexOf(tag);
            $scope.post.Tags.splice(index);
        };

        $scope.getTagsSource = function (t) {
            return tagsService.getTagsByName(t);
        };

        $scope.getPost = function () {
            postsService.getPost($rootScope.$stateParams.postId).then(function (resp) {
                if ($scope.username === resp.User.UserName) {
                    if (resp.Error == undefined) {
                        $scope.isAdding = false;
                        $scope.post = resp;

                        _.each(resp.Tags, function (t) {
                            $scope.Tags.push({ text: t.TagName });
                        });

                        _.each(resp.PostContents, function (t) {
                            addMediaToUploaderQueue(t.Media, t.PostContentTitle, t.PostContentText);
                        });
                    } else {
                        errorService.displayError(resp.Error);
                    }
                } else {
                    errorService.displayError({ Message: "Oh you sneaky bastard! This post is not yours to edit." });
                }
            }, function (e) {
                errorService.displayError(e);
            });
        };

        $scope.savePost = function () {
            if ($scope.authData && $scope.user) {
                if (uploader.getNotUploadedItems().length === 0) {
                    $scope.post.User = $scope.user;
                    setPostContentsFromUploader();

                    if ($scope.isAdding) {
                        postsService.addPost($scope.post).then(function(resp) {
                            if (resp.Error == undefined) {
                                $location.path("/");
                            } else {
                                errorService.displayError(resp.Error);
                            }
                        }, function(e) {
                            errorService.displayError(e);
                        });
                    } else {
                        postsService.updatePost($scope.post).then(function(resp) {
                            if (resp.Error == undefined) {
                                $location.path("/");
                            } else {
                                errorService.displayError(resp.Error);
                            }
                        }, function(e) {
                            errorService.displayError(e);
                        });
                    }
                } else {
                    errorService.displayError("There are some contents not yet uploaded.");
                }
            } else {
                $rootScope.$broadcast("launchLoginForm");
            }
        };

        $scope.cancelPost = function () {
            $location.path("/");
        };

        $scope.init = function () {
            authenticationService.getUserInfo().then(function (response) {
                if (response.Message == undefined || response.Message == null) {
                    if (!isNaN($rootScope.$stateParams.postId)) {
                        $scope.getPost();
                    }
                } else {
                    errorService.displayErrorRedirect(response.Message);
                }
            });
        };

        $rootScope.$watch('user', function () {
            if ($rootScope.user) {
                $scope.user = $rootScope.user;
                $scope.username = $scope.user.UserName;
                $scope.uploadUrl = configProvider.getSettings().BlogApi + "media?username=" + $scope.username + "&album=default";
            }
        });

        $scope.$on("userLoggedIn", function () {
            $scope.username = localStorageService.get("username");
            $scope.authData = localStorageService.get("authorizationData");
        });

        $scope.$on("windowSizeChanged", function (e, d) {
            configProvider.setDimensions(d.width, d.height);
        });

        // #region media selection dialog

        $scope.launchMediaSelectionDialog = function () {
            if ($scope.albums.length == 0) {
                albumService.getAlbumsByUser($scope.user.Id).then(function (resp) {
                    _.each(resp, function (a) {
                        _.each(a.Media, function (m) {
                            m.IsSelected = false;
                        });
                    });

                    $scope.albums = resp;
                    mediaSelectionDialog.$promise.then(mediaSelectionDialog.show);
                }, function (e) {
                    errorService.displayError(e);
                });
            } else {
                mediaSelectionDialog.$promise.then(mediaSelectionDialog.show);
            }
        };

        $scope.getThumbnailUrl = function (media) {
            return {
                "background-image": "url(" + media.ThumbnailUrl + ")"
            };
        };

        $scope.toggleMediaSelectionToExistingContents = function (media) {
            media.IsSelected = !media.IsSelected;

            if (media.IsSelected) {
                media.IsSelected = addMediaToUploaderQueue(media, '', '');
            } else {
                removeMediaFromUploaderQueue(media);
            }
        };

        $scope.getMediaToggleButtonStyle = function (media) {
            return media.IsSelected ? 'btn-danger' : 'btn-success';
        };

        $scope.getMediaToggleButtonIcon = function (media) {
            return media.IsSelected ? 'fa-times' : 'fa-check';
        };

        // #endregion

        // #region angular-file-upload

        var addMediaToUploaderQueue = function (media, title, text) {
            var item = getUploaderItem(media, title, text);

            var isValid = validateVideoUpload(item);
            if (!isValid) {
                errorService.displayError("You cannot upload more than one video in a post.");
                return false;
            }

            $timeout(function () {
                uploader.queue.push(item);
                $scope.$broadcast("resizeIsotopeItems");
            }, 500);

            return true;
        };

        var removeMediaFromUploaderQueue = function (media) {
            var item = _.where(uploader.queue, { media: media, mediaId: media.Id })[0];

            $timeout(function () {
                var index = uploader.queue.indexOf(item);
                uploader.queue.splice(index, 1);
                $scope.$broadcast("resizeIsotopeItems");
            }, 500);
        };

        var setPostContentsFromUploader = function () {
            $scope.post.PostContents = [];

            _.each(uploader.queue, function (item) {
                var postContent = {
                    PostId: 0,
                    Media: item.media,
                    PostContentTitle: item.postContentTitle,
                    PostContentText: item.postContentText
                };
                $scope.post.PostContents.push(postContent);
            });
        };

        var getUploaderItem = function (media, title, text) {
            var item = {
                file: {
                    name: media.FileName,
                    size: 1e6,
                    type: media.MediaType,
                },
                mediaId: media.Id,
                media: media,
                progress: 100,
                isUploaded: true,
                isSuccess: true,
                isExisting: true,
                url: media.ThumbnailUrl,
                base: media,
                allowCaptions: true,
                postContentTitle: title,
                postContentText: text,
                remove: function () {
                    var index = uploader.queue.indexOf(item);
                    uploader.queue.splice(index, 1);

                    if ($scope.albums.length > 0) {
                        var album = _.where($scope.albums, { AlbumId: item.media.AlbumId })[0];
                        var albumMedia = _.where(album.Media, { Id: item.media.Id })[0];
                        albumMedia.IsSelected = false;
                    }
                }
            };

            return item;
        };

        var uploader = $scope.uploader = new FileUploader({
            scope: $rootScope,
            url: $scope.uploadUrl,
            autoUpload: false,
            headers: { Authorization: 'Bearer ' + ($scope.authData ? $scope.authData.token : "") }
        });

        uploader.filters.push({
            name: 'imageFilter',
            fn: function (item /*{File|HTMLInputElement}*/) {
                var type = uploader.isHTML5 ? item.type : '/' + item.value.slice(item.value.lastIndexOf('.') + 1);
                type = '|' + type.toLowerCase().slice(type.lastIndexOf('/') + 1) + '|';

                if (!validateVideoUpload(item)) {
                    errorService.displayError("You cannot upload more than one video in a post.");
                    return false;
                }
                
                return '|jpg|png|jpeg|bmp|gif|mp4|flv|'.indexOf(type) !== -1;
            }
        });

        uploader.onSuccessItem = function (fileItem, response) {
            response.IsSelected = true;

            if ($scope.albums.length > 0) {
                var album = _.where($scope.albums, { IsUserDefault: true })[0];
                album.Media.push(response);
            }

            fileItem.media = response;
            fileItem.mediaId = response.Id;
            fileItem.mediaId = response;
        };

        uploader.onAfterAddingFile = function (fileItem) {
            fileItem.allowCaptions = true;
            fileItem.postContentTitle = "";
            fileItem.postContentText = "";
        };

        var validateVideoUpload = function(fileItem) {
            var supportedVideos = [
                "video/avi",
                "video/quicktime",
                "video/mpeg",
                "video/mp4",
                "video/x-flv"
            ];

            var isVideo = _.contains(supportedVideos, (fileItem.file ? fileItem.file.type : fileItem.type));
            if (isVideo) {
                var videoUploads = _.filter(uploader.queue, function (upload) {
                    return _.contains(supportedVideos, upload.file.type);
                });

                if (videoUploads.length > 0) {
                    return false;
                }
                return true;
            };
            return true;
        };

        // #endregion

        $scope.init();
    }
]);

// ReSharper restore InconsistentNaming
///#source 1 1 /Scripts/modules/posts/controllers/postsViewController.js
ngPosts.controller('postsViewController', ["$scope", "$rootScope", "$location", "postsService",
    "userService", "mediaService", "configProvider", "errorService", "localStorageService",
    function ($scope, $rootScope, $location, postsService, userService, mediaService, configProvider,
        errorService, localStorageService) {

        $scope.postId = parseInt($rootScope.$stateParams.postId);

        $scope.post = null;

        $scope.user = null;

        $scope.viewCount = [];

        $scope.postsList = [];

        $scope.postLikes = [];

        $scope.isBusy = false;

        $scope.isEditable = false;

        $scope.authData = localStorageService.get("authorizationData");

        $scope.username = localStorageService.get("username");

        $scope.toggleIsEditable = function () {
            if ($scope.user && $scope.post && $scope.post.User.UserName === $scope.username) {
                $scope.isEditable = true;
                return;
            }
            $scope.isEditable = false;
        };

        $scope.$on("loggedInUserInfo", function (ev, data) {
            if (data) {
                $scope.username = data.UserName;
                $scope.toggleIsEditable();
            }
        });

        $rootScope.$watch('user', function () {
            if ($rootScope.user) {
                $scope.user = $rootScope.user;
                $scope.username = $rootScope.user.UserName;
                $scope.toggleIsEditable();
            }
        });

        $scope.editPost = function () {
            $location.path("/post/edit/" + $scope.post.Id);
        };

        $scope.getContentType = function (content) {
            if (content == undefined) return "image";

            var contentType = content.split('/');
            if (contentType[0] == "video") {
                return "video";
            } else {
                return "image";
            }
        };

        $scope.getPostsList = function () {
            postsService.getPopularPosts().then(function (list) {
                $scope.postsList = list;
            }, function (e) {
                errorService.displayError({ Message: e });
            });
        };

        $scope.getViewedPost = function () {
            if (!isNaN($rootScope.$stateParams.postId)) {
                postsService.getPost($scope.postId).then(function (post) {
                    if (post.Error == undefined) {
                        $scope.post = post;
                        $scope.isBusy = false;
                        $scope.toggleIsEditable();

                        $scope.$broadcast("resizeIsotopeItems");

                        // update post likes and view counts directive
                        $scope.postLikes = $scope.post.PostLikes;
                        $scope.viewCount = $scope.post.ViewCounts;

                        // subscribe to post socket.io events
                        postsService.subscribeToPost($scope.post.Id);

                        // update mediaservice viewed gallery
                        var mediaList = _.pluck(post.PostContents, 'Media');
                        mediaService.addViewedMediaListFromPost(mediaList, post.Id);
                        $rootScope.$broadcast("updateMediaGalleryFromPost", {});
                    } else {
                        errorService.displayError({ Message: e });
                    }
                }, function (e) {
                    errorService.displayError({ Message: e });
                });
            } else {
                errorService.displayErrorRedirect({ Message: "You're missing the post to edit bruh! Don't be stupid!" });
            }
        };

        $scope.hasContents = function () {
            if ($scope.post && $scope.post.PostContents && $scope.post.PostContents.length > 0) {
                return true;
            }
            return false;
        };

        $scope.nextPost = function () {
            if (!$scope.post) return;

            var nextPost = postsService.getNextPostIdFromCache($scope.post.Id);

            if (nextPost) {
                $location.path("/post/" + nextPost);
            } else {
                postsService.getMoreRecentPosts()
                    .then(function () {
                        nextPost = postsService.getNextPostIdFromCache($scope.post.Id);

                        if (nextPost) {
                            $location.path("/post/" + nextPost);
                        } else {
                            $location.path("/");
                        }
                    }, function () {
                        $location.path("/");
                    });
            }
        };

        $scope.previousPost = function () {
            if (!$scope.post) return;

            var previousPost = postsService.getPreviousPostIdFromCache($scope.post.Id);

            if (previousPost) {
                $location.path("/post/" + previousPost);
            } else {
                $location.path("/");
            }
        };

        $scope.init = function () {
            if ($scope.isBusy) {
                return;
            }
            $scope.isBusy = true;

            if ($scope.authData) {
                if ($rootScope.user) {
                    $scope.user = $rootScope.user;
                }
            }

            $scope.getViewedPost();
        };

        $scope.init();
    }
]);
///#source 1 1 /Scripts/modules/posts/directives/postContents.js
ngPosts.directive('postContents', function () {
    var ctrlFn = function ($scope) {
    };
    ctrlFn.$inject = ["$scope"];

    return {
        restrict: 'EA',
        scope: { contents: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "posts/postContents.html",
        controller: ctrlFn
    };
});

///#source 1 1 /Scripts/modules/posts/directives/postLikes.js
ngPosts.directive('postLikes', [function () {
    var linkFn = function (scope, elem) {
        scope.highlight = function() {
            $(elem).effect("highlight", { color: "#B3C833" }, 1500);
        };
    };

    var ctrlFn = function ($scope, $rootScope, postsService, userService, errorService, localStorageService, configProvider) {
        $scope.postLikes = $scope.list;

        $scope.user = null;

        $scope.username = localStorageService.get("username");

        $scope.authData = localStorageService.get("authorizationData");

        $scope.tooltip = { "title": "Click to favorite this post." };

        $scope.$on(configProvider.getSocketClientFunctions().postLikesUpdate, function (e, d) {
            if (d.postId == $scope.postId) {
                $scope.postLikes = d.postLikes;
                $scope.highlight();
                $scope.isUserLiked();
            }
        });
        
        $scope.$on("loggedInUserInfo", function (ev, data) {
            $scope.user = data;
            $scope.isUserLiked();
        });

        $rootScope.$watch('user', function () {
            $scope.user = $rootScope.user;
            $scope.isUserLiked();
        });

        $scope.$on("viewPostLoadedLikes", function (e, d) {
            $scope.postId = d.postId;
            $scope.postLikes = d.postLikes;
            $scope.isUserLiked();
        });

        $scope.$watch('list', function() {
            $scope.postLikes = $scope.list;
        });

        $scope.likePost = function () {
            postsService.likePost($scope.postId, $scope.username).then(function () { },
            function (err) {
                errorService.displayError(err);
            });
        };

        $scope.isUserLiked = function () {
            var isLiked = false;
            if ($scope.authData && $scope.user) {
                _.each($scope.postLikes, function (p) {
                    if (p.UserId == $scope.user.Id) {
                        isLiked = true;
                    }
                });
            }

            return isLiked ? "fa-star" : "fa-star-o";
        };
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "postsService", "userService", "errorService", "localStorageService", "configProvider"];

    return {
        restrict: 'EA',
        scope: {
            list: '=',
            postId: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "posts/postlikes.html",
        controller: ctrlFn,
        link: linkFn
    };
}]);

///#source 1 1 /Scripts/modules/posts/directives/postViewCount.js
ngPosts.directive('postViewCount', [function () {
    var ctrlFn = function ($scope, $rootScope, configProvider) {
        $scope.viewCount = $scope.list;

        $scope.$on(configProvider.getSocketClientFunctions().viewCountUpdate, function (e, d) {
            if (d.postId == $scope.postId) {
                $scope.viewCount = d.viewCount;
            }
        });
        
        $scope.$watch('list', function () {
            $scope.viewCount = $scope.list;
        });
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "configProvider"];

    return {
        restrict: 'EA',
        scope: {
            list: '=',
            postId: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "posts/postViewCount.html",
        controller: ctrlFn
    };
}]);

///#source 1 1 /Scripts/modules/posts/directives/postListItem.js
ngPosts.directive('postListItem', [function () {
    var ctrlFn = function ($scope, $rootScope, $location, localStorageService, configProvider) {

        $scope.post = $scope.data.Post;

        $scope.user = $scope.data.Post.User;

        $scope.comments = $scope.data.Post.Comments && $scope.data.Post.Comments.length > 0 ? 
            $scope.data.Post.Comments : [];

        $scope.postLikes = $scope.data.Post.PostLikes && $scope.data.Post.PostLikes.length > 0 ?
            $scope.data.Post.PostLikes : [];

        $scope.username = localStorageService.get("username");

        $scope.hasComments = $scope.data.Post.Comments && $scope.data.Post.Comments.length > 0 ? true : false;

        $scope.hasTags = $scope.data.Post.Tags && $scope.data.Post.Tags.length > 0 ? true : false;

        $scope.isEditable = ($scope.user && $scope.user.UserName === $scope.username) ? true : false;
        
        $scope.getCommentPopover = function(commentId) {
            var comment = _.where($scope.comments, { CommentId: commentId });
            var user = comment.User.FirstName + " " + comment.User.LastName;
            return { "title": user, "content": comment.CommentMessage };
        };

        $scope.getPostSize = function() {
            return $scope.data.Width;
        };

        $scope.toggleIsEditable = function () {
            if ($scope.user && $scope.user.UserName === $scope.username) {
                $scope.isEditable = true;
            }
            $scope.isEditable = false;
        };

        $rootScope.$on(configProvider.getSocketClientFunctions().getPostTopComments, function (e, d) {
            if (d.postId == $scope.post.Id) {
                $scope.comments = d.comments;
                $scope.hasComments = d.comments && d.comments.length > 0 ? true : false;
            }
        });

        $rootScope.$on(configProvider.getSocketClientFunctions().getPostLikes, function (e, d) {
            if (d.postId == $scope.post.Id) {
                $scope.postLikes = d.postLikes;
            }
        });

        $scope.$on("loggedInUserInfo", function (ev, data) {
            if (data) {
                $scope.username = data.UserName;
                $scope.toggleIsEditable();
            }
        });

        $rootScope.$watch('user', function () {
            if ($rootScope.user) {
                $scope.username = $rootScope.user.UserName;
                $scope.toggleIsEditable();
            }
        });

        $scope.editPost = function() {
            $location.path("/post/edit/" + $scope.post.Id);
        };
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$location", "localStorageService", "configProvider"];

    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "posts/postListItem.html",
        controller: ctrlFn
    };
}]);

///#source 1 1 /Scripts/modules/posts/directives/postListItemComment.js
ngPosts.directive('postListItemComment', [function () {
    var ctrlFn = function ($scope) {
        $scope.user = {
            "name": $scope.comment.User.FirstName + " " + $scope.comment.User.LastName,
            "url": "#"
        };

        $scope.popover = {
            "title": $scope.user.name,
            "content": $scope.comment.CommentMessage
        };
    };
    ctrlFn.$inject = ["$scope"];

    return {
        restrict: 'EA',
        scope: { comment: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "posts/postListItemComment.html",
        controller: ctrlFn
    };
}]);

///#source 1 1 /Scripts/modules/posts/directives/postRelatedItem.js
ngPosts.directive('postRelatedItem', [function () {
    return {
        restrict: 'EA',
        scope: { post: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "posts/postrelateditem.html"
    };
}]);

///#source 1 1 /Scripts/modules/posts/directives/postRelatedItems.js
ngPosts.directive('postRelatedItems', [function () {
    var ctrlFn = function ($scope, $rootScope, postsService, errorService) {
        $scope.hasError = false;

        $scope.postsByTag = [];

        $scope.postsByUser = [];
        
        $scope.relatedCategories = [
            { name: 'By user', id: "user" },
            { name: 'By similar tags', id: "tags" }
        ];

        $scope.selectedCategory = $scope.relatedCategories[0];

        $scope.emptyPostsMessage = "There are no related posts.";

        $scope.getRelatedPosts = function () {
            if (!isNaN($scope.parentpostid)) {
                postsService.getRelatedPosts($scope.parentpostid).then(function (response) {
                    $scope.hasError = false;
                    $scope.postsByTag = response.PostsByTags;
                    $scope.postsByUser = response.PostsByUser;
                }, function(e) {
                    errorService.displayError(e);
                    $scope.hasError = true;
                });
            } else {
                $scope.hasError = true;
            }
        };

        $scope.displayEmptyPostsMessage = function() {
            if ($scope.selectedCategory.id == "user") {
                if ($scope.postsByUser.length > 0) {
                    return false;
                }
                return true;
            } else {
                if ($scope.postsByTag.length > 0) {
                    return false;
                }
                return true;
            }
        };

        $scope.emptyPostsStyle = function() {
            return $scope.hasError ? "alert-danger" : "alert-warning";
        };

        $scope.getEmptyPostsMessage = function () {
            return $scope.hasError ?
                "Something went wrong with loading the related posts! :(" :
                "There are no related posts yet.";
        };

        $scope.displayUser = function() {
            if ($scope.selectedCategory.id == "user") {
                if ($scope.postsByUser.length > 0) {
                    return true;
                }
                return false;
            }
            return false;
        };

        $scope.displayTag = function () {
            if ($scope.selectedCategory.id == "tags") {
                if ($scope.postsByTag.length > 0) {
                    return true;
                }
                return false;
            }
            return false;
        };

        $scope.getRelatedPosts();
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "postsService", "errorService"];

    return {
        restrict: 'EA',
        scope: { parentpostid: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "posts/postrelateditems.html",
        controller: ctrlFn
    };
}]);

///#source 1 1 /Scripts/modules/posts/services/postsService.js
ngPosts.factory('postsService', ["$http", "$q", "blogSocketsService", "configProvider", "dateHelper",
    function ($http, $q, blogSocketsService, configProvider, dateHelper) {
        var postsApi = configProvider.getSettings().BlogApi == "" ?
            window.blogConfiguration.blogApi + "Posts/" :
            configProvider.getSettings().BlogApi + "Posts/";

        var addPostViewData = function (post) {
            post.DateDisplay = dateHelper.getDateDisplay(post.CreatedDate);
            post.Url = "/#/post/" + post.Id;

            return post;
        };

        var cachedPostsList = [];

        var getCachedPostId = function (currentPostId, isNext) {
            if (cachedPostsList && cachedPostsList.length > 0) {
                var cachedPostIds = _.pluck(cachedPostsList, 'Id');
                var isCurrentPostInCache = _.contains(cachedPostIds, currentPostId);

                if (!isCurrentPostInCache) return null;
                
                var index = _.indexOf(cachedPostIds, currentPostId);
                if (index < 0) return cachedPostIds[0];

                if (index === 0 && !isNext) {
                    return null;
                } else {
                    index = isNext ? index + 1 : index - 1;

                    if (cachedPostIds.length < index + 1) {
                        return null;
                    }

                    return cachedPostIds[index];
                }
            } else {
                return null;
            }
        };

        return {
            getPost: function (id) {
                var deferred = $q.defer();

                $http({
                    url: postsApi + id,
                    method: "GET"
                }).success(function (response) {
                    var post = addPostViewData(response);
                    deferred.resolve(post);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            getRelatedPosts: function (id) {
                var deferred = $q.defer();

                $http({
                    url: postsApi + id + "/related",
                    method: "GET"
                }).success(function (response) {
                    _.each(response.PostsByUser, function (p) {
                        addPostViewData(p);
                    });
                    _.each(response.PostsByTags, function (p) {
                        addPostViewData(p);
                    });

                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            getPopularPosts: function () {
                var deferred = $q.defer();

                $http({
                    url: postsApi + "popular",
                    method: "GET"
                }).success(function (response) {
                    _.each(response, function (p) {
                        addPostViewData(p);
                    });
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            getRecentPosts: function () {
                var self = this;
                var deferred = $q.defer();

                $http({
                    url: postsApi + "recent",
                    method: "GET"
                }).success(function (response) {
                    _.each(response, function (p) {
                        addPostViewData(p);
                        self.addToCachedPostsList([p]);
                    });
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            getMoreRecentPosts: function (currentPostsCount) {
                var self = this;
                var deferred = $q.defer();

                if (!currentPostsCount || currentPostsCount === 0) currentPostsCount = cachedPostsList.length;

                $http({
                    url: postsApi + "recent/more/" + currentPostsCount,
                    method: "GET"
                }).success(function (response) {
                    _.each(response, function (p) {
                        addPostViewData(p);
                        self.addToCachedPostsList([p]);
                    });
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            getPostsByUser: function (userId) {
                var userPostsUrl = configProvider.getSettings().BlogApi == "" ?
                    window.blogConfiguration.blogApi + "user/" :
                    configProvider.getSettings().BlogApi + "user/";

                var deferred = $q.defer();

                $http({
                    url: userPostsUrl + userId + "/posts",
                    method: "GET"
                }).success(function (response) {
                    _.each(response, function (p) {
                        addPostViewData(p);
                    });
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            getMorePostsByUser: function (userId, skip) {
                var userPostsUrl = configProvider.getSettings().BlogApi == "" ?
                    window.blogConfiguration.blogApi + "user/" :
                    configProvider.getSettings().BlogApi + "user/";

                var deferred = $q.defer();

                $http({
                    url: userPostsUrl + userId + "/posts/more/" + skip,
                    method: "GET"
                }).success(function (response) {
                    _.each(response, function (p) {
                        addPostViewData(p);
                    });
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            subscribeToPost: function (id) {
                blogSocketsService.emit(configProvider.getSocketClientFunctions().unsubscribeViewPost, { postId: id });
                blogSocketsService.emit(configProvider.getSocketClientFunctions().subscribeViewPost, { postId: id });
            },

            addPost: function (post) {
                var deferred = $q.defer();

                $http({
                    url: postsApi,
                    method: "POST",
                    data: post
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            updatePost: function (post) {
                var deferred = $q.defer();

                $http({
                    url: postsApi,
                    method: "PUT",
                    data: post
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            likePost: function (postId, username) {
                var deferred = $q.defer();

                $http({
                    url: postsApi + "likes?username=" + username + "&postId=" + postId,
                    method: "POST"
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (e) {
                    deferred.reject(e);
                });

                return deferred.promise;
            },

            addToCachedPostsList: function (postsList) {
                var cachedPostIds = _.pluck(cachedPostsList, 'Id');

                _.each(postsList, function (post) {
                    if (!_.contains(cachedPostIds, post.Id)) {
                        cachedPostsList.push(post);
                        return;
                    }
                });
            },

            getNextPostIdFromCache: function (currentPostId) {
                return getCachedPostId(currentPostId, true);
            },

            getPreviousPostIdFromCache: function (currentPostId) {
                return getCachedPostId(currentPostId, false);
            },
        };
    }
]);
///#source 1 1 /Scripts/modules/shared/shared.js
var ngShared = angular.module("ngShared",
    [
        'angularFileUpload',
        "com.2fdevs.videogular",
		"com.2fdevs.videogular.plugins.controls",
		"com.2fdevs.videogular.plugins.overlayplay",
		"com.2fdevs.videogular.plugins.buffering",
		"com.2fdevs.videogular.plugins.poster",
		"com.2fdevs.videogular.plugins.imaads"
    ]);
///#source 1 1 /Scripts/modules/shared/directives/ellipsis.js
ngShared.directive('ellipsis', [function () {
    var filterFn;
    filterFn = function (scope, element, attrs) {
        scope.$on("reapplyEllipsis", function () {
            scope.applyEllipsis();
        });

        scope.applyEllipsis = function() {
            var height = parseInt(attrs.wrapHeight == undefined ? 180 : attrs.wrapHeight);
            $(element).dotdotdot({
                ellipsis: "...",
                height: height
            });
        };

        scope.applyEllipsis();
    };

    return {
        restrict: 'EA',
        link: filterFn
    };
}]);
///#source 1 1 /Scripts/modules/shared/directives/emptyRecordMessage.js
ngShared.directive("emptyRecordMessage", [
    function () {
        var ctrlFn = function ($scope) {
        };
        ctrlFn.$inject = ["$scope"];

        return {
            restrict: 'EA',
            scope: { message: '=' },
            replace: true,
            templateUrl: window.blogConfiguration.templatesModulesUrl + "shared/emptyRecordMessage.html",
            controller: ctrlFn
        };
    }
]);
///#source 1 1 /Scripts/modules/shared/directives/fileUpload.js
ngShared.directive("fileUpload", [
    function () {
        return {
            restrict: 'EA',
            scope: { uploader: '='},
            replace: true,
            templateUrl: window.blogConfiguration.templatesModulesUrl + "shared/fileUpload.html"
        };
    }
]);
///#source 1 1 /Scripts/modules/shared/directives/fileUploadItem.js
ngShared.directive("fileUploadItem", [
    function () {
        var linkFn = function(scope) {
            scope.isNewContent = function (exists) {
                var response = true;
                if (exists) {
                    response = false;
                }
                return response;
            };
        };

        return {
            link: linkFn,
            restrict: 'EA',
            scope: {
                item: '=',
                uploader: '='
            },
            replace: true,
            templateUrl: window.blogConfiguration.templatesModulesUrl + "shared/fileUploadItem.html"
        };
    }
]);
///#source 1 1 /Scripts/modules/shared/directives/fileUploadThumbnail.js
ngShared.directive('fileUploadThumbnail', ['$window', "$rootScope", "blockUiService",
    function ($window, $rootScope, blockUiService) {
        var helper = {
            support: !!($window.FileReader && $window.CanvasRenderingContext2D),
            isFile: function (item) {
                return angular.isObject(item) && item instanceof $window.File;
            },
            isImage: function (file) {
                var type = '|' + file.type.slice(file.type.lastIndexOf('/') + 1) + '|';
                return '|jpg|png|jpeg|bmp|gif|'.indexOf(type) !== -1;
            }
        };

        return {
            restrict: 'A',
            template: '<canvas/>',
            link: function (scope, element, attributes) {
                if (!helper.support) return;

                var params = scope.$eval(attributes.fileUploadThumbnail);

                if (!helper.isFile(params.file)) return;
                if (!helper.isImage(params.file)) return;

                var canvas = element.find('canvas');
                var reader = new FileReader();

                reader.onload = onLoadFile;
                reader.readAsDataURL(params.file);

                function onLoadFile(event) {
                    var img = new Image();
                    img.onload = onLoadImage;
                    img.src = event.target.result;
                }

                function onLoadImage() {
                    blockUiService.blockIt();
                    var width = params.width || this.width / this.height * params.height;
                    var height = params.height || this.height / this.width * params.width;
                    canvas.attr({ width: width, height: height });
                    canvas[0].getContext('2d').drawImage(this, 0, 0, width, height);
                    $rootScope.$broadcast("resizeIsotopeItems", {});
                    blockUiService.unblockIt();
                }
            }
        };
    }
]);
///#source 1 1 /Scripts/modules/shared/directives/isotopeItemResize.js
ngShared.directive('isotopeItemResize', ["$window", "$timeout", "$interval",
    function ($window, $timeout, $interval) {
        var linkFn = function (scope, elem, attrs) {
            scope.columnCount = 0;
            scope.$emit('iso-option', { 'animationEngine' : 'best-available' });

            scope.applyLayout = function () {
                $interval(function () {
                    resizeItems($window.innerWidth);
                    scope.$broadcast('iso-method', { name: null, params: null });

                    // TODO: temporarily removed and to be verified if it works!
                    //var isotopeElements = elem.children(".isotope-item");
                    //for (var i = 0; i < isotopeElements.length; i++) {
                    //    if ((i + 1) % scope.columnCount == 0) {
                    //        $(isotopeElements[i]).css({ "margin-right": "0"});
                    //    }
                    //}
                }, 500, 5);
            };

            scope.$on("windowSizeChanged", function (e, d) {
                if (attrs.resizeLayoutOnly == undefined || attrs.resizeLayoutOnly === "false") {
                    resizeItems(d.width);
                }
                scope.applyLayout();
            });

            scope.$on("resizeIsotopeItems", function () {
                scope.applyLayout();
            });

            var getColumnCount = function(containerWidth, columnSize, defaultSize) {
                var columnPercentage = columnSize == undefined ? parseFloat(defaultSize) : parseFloat(columnSize);
                var columnWidth = (containerWidth / 100) * columnPercentage;
                var columnCount = parseInt(containerWidth / columnWidth);

                return columnCount;
            };

            var resizeItems = function (w) {
                if (attrs.resizeContainer == undefined) {
                    if (w >= 992) {
                        scope.columnCount = getColumnCount(w, attrs.resizeLarge, "32%");
                        _.each($(elem).children(".isotope-item"), function (a) {
                            var large = attrs.resizeLarge == undefined ? "32%" : attrs.resizeLarge;
                            $(a).width(large);
                        });
                    } else if (w >= 767 && w < 992) {
                        scope.columnCount = getColumnCount(w, attrs.resizeLarge, "48%");
                        _.each($(elem).children(".isotope-item"), function (a) {
                            var medium = attrs.resizeMedium == undefined ? "48%" : attrs.resizeMedium;
                            $(a).width(medium);
                        });
                    } else {
                        scope.columnCount = getColumnCount(w, attrs.resizeSmall, "96%");
                        _.each($(elem).children(".isotope-item"), function (a) {
                            var small = attrs.resizeSmall == undefined ? "96%" : attrs.resizeSmall;
                            $(a).width(small);
                        });
                    }
                } else {
                    var container = $("#" + attrs.resizeContainer);
                    var containerWidth = container.outerWidth();

                    if (containerWidth > 1200) {
                        scope.columnCount = getColumnCount(containerWidth, attrs.resizeXlarge, "19%");
                        _.each($(elem).children(".isotope-item"), function (a) {
                            var xlarge = attrs.resizeXlarge == undefined ? "19%" : attrs.resizeXlarge;
                            $(a).width(xlarge);
                        });
                        if (attrs.resizeBroadcast != undefined)
                            scope.$emit(attrs.resizeBroadcast, "xlarge");
                    } else if (containerWidth > 992) {
                        scope.columnCount = getColumnCount(containerWidth, attrs.resizeLarge, "23.5%");
                        _.each($(elem).children(".isotope-item"), function (a) {
                            var large = attrs.resizeLarge == undefined ? "23.5%" : attrs.resizeLarge;
                            $(a).width(large);
                        });
                        if (attrs.resizeBroadcast != undefined)
                            scope.$emit(attrs.resizeBroadcast, "large");
                    } else if (containerWidth > 768) {
                        scope.columnCount = getColumnCount(containerWidth, attrs.resizeMedium, "31.5%");
                        _.each($(elem).children(".isotope-item"), function (a) {
                            var medium = attrs.resizeMedium == undefined ? "31.5%" : attrs.resizeMedium;
                            $(a).width(medium);
                        });
                        if (attrs.resizeBroadcast != undefined)
                            scope.$emit(attrs.resizeBroadcast, "medium");
                    } else if (containerWidth > 568) {
                        scope.columnCount = getColumnCount(containerWidth, attrs.resizeSmall, "48%");
                        _.each($(elem).children(".isotope-item"), function (a) {
                            var medium = attrs.resizeSmall == undefined ? "48%" : attrs.resizeSmall;
                            $(a).width(medium);
                        });
                        if (attrs.resizeBroadcast != undefined)
                            scope.$emit(attrs.resizeBroadcast, "small");
                    } else {
                        scope.columnCount = getColumnCount(containerWidth, attrs.resizeXsmall, "98%");
                        _.each($(elem).children(".isotope-item"), function (a) {
                            var xsmall = attrs.resizeXsmall == undefined ? "98%" : attrs.resizeXsmall;
                            $(a).width(xsmall);
                        });
                        if (attrs.resizeBroadcast != undefined)
                            scope.$emit(attrs.resizeBroadcast, "xsmall");
                    }
                }
            };

            scope.applyLayout();
        };

        return {
            restrict: 'EA',
            link: linkFn
        };
    }
]);

///#source 1 1 /Scripts/modules/shared/directives/keypress.js
ngShared.directive('ngEnter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.ngEnter);
                });

                event.preventDefault();
            }
        });
    };
});
///#source 1 1 /Scripts/modules/shared/directives/scrollTrigger.js
ngShared.directive('scrollTrigger', ["$rootScope", function ($rootScope) {
    return {
        link: function (scope, element, attrs) {
            scope.scrollTriggerWatch = null;

            $rootScope.$on("updateScrollTriggerWatch", function (event, data) {
                scope.scrollTriggerWatch = "#" + data;
            });

            angular.element(element).bind("scroll", function () {
                if (scope.scrollTriggerWatch != null) {
                    var scroll = $(element).scrollTop();
                    if (scroll + $(window).height() >= $(scope.scrollTriggerWatch).outerHeight()) {
                        $rootScope.$broadcast("scrollBottom");
                    }
                }
            });
        }
    };
}]);
///#source 1 1 /Scripts/modules/shared/directives/ticker.js
ngShared.directive('ticker', function () {
    var filterFn;
    filterFn = function (scope, element, attrs) {
        var ticker = $(element).newsTicker({
            row_height: 40,
            max_rows: 1,
            duration: 5000
        });

        if (attrs.enablePause) {
            $(element).on("click", $(element).find("[data-pause-trigger]"), function (ev) {
                ticker.newsTicker('toggle');
            });
        }
    };

    return {
        restrict: 'EA',
        link: filterFn
    };
});
///#source 1 1 /Scripts/modules/shared/directives/videoPlayer.js
ngShared.directive("videoPlayer", [
    function () {
        var ctrlFn = function ($scope, $sce) {
            $scope.currentTime = 0;
            $scope.totalTime = 0;
            $scope.state = null;
            $scope.volume = 1;
            $scope.isCompleted = false;
            $scope.API = null;

            $scope.onPlayerReady = function (API) {
                $scope.API = API;
            };

            $scope.onCompleteVideo = function () {
                $scope.currentTime = 0;
                $scope.isCompleted = true;
            };

            $scope.onUpdateState = function (state) {
                $scope.state = state;
            };

            $scope.onUpdateTime = function (currentTime, totalTime) {
                $scope.currentTime = currentTime;
                $scope.totalTime = totalTime;
            };

            $scope.onUpdateVolume = function (newVol) {
                $scope.volume = newVol;
            };

            $scope.onUpdateSize = function (width, height) {
                $scope.config.width = width;
                $scope.config.height = height;
            };

            $scope.stretchModes = [
                { label: "None", value: "none" },
                { label: "Fit", value: "fit" },
                { label: "Fill", value: "fill" }
            ];

            $scope.config = {
                width: 740,
                height: 380,
                autoHide: false,
                autoHideTime: 3000,
                autoPlay: false,
                responsive: false,
                stretch: $scope.stretchModes[2],
                sources: [
                    {
                        src: $sce.trustAsResourceUrl($scope.media.MediaUrl),
                        type: $scope.media.MediaType
                    }
                ],
                transclude: true,
                theme: {
                    url: window.blogConfiguration.blogRoot + "/scripts/bower_components/videogular-themes-default/videogular.css"
                },
                plugins: {
                    poster: {
                        url: $scope.media.ThumbnailUrl
                    }
                }
            };
        };
        ctrlFn.$inject = ["$scope", "$sce"];

        return {
            restrict: 'EA',
            scope: { media: '=' },
            controller: ctrlFn,
            replace: true,
            templateUrl: window.blogConfiguration.templatesModulesUrl + "shared/videoPlayer.html"
        };
    }
]);
///#source 1 1 /Scripts/modules/shared/services/blockUi.js
ngShared.factory('blockUiService', [function () {
    return {
        blockIt: function (properties) {
            if (properties == undefined) properties = {};

            if (properties.html == undefined) {
                properties.html = '<h4><img src="content/images/loader-girl.gif" height="128" /></h4>';
            }

            if (properties.css == undefined) {
                properties.css = {
                    border: 'none',
                    padding: '5px',
                    backgroundColor: '#000',
                    opacity: .5,
                    color: '#fff'
                };
            }

            if (properties.elem == undefined) {
                $.blockUI({
                    message: properties.html,
                    css: properties.css
                });
            } else {
                $(properties.elem).block({
                    message: properties.html,
                    css: properties.css
                });
            }
        },

        unblockIt: function (elem) {
            if (elem == undefined) {
                $.unblockUI();
            } else {
                $(elem).unblock();
            }
        }
    };
}]);
///#source 1 1 /Scripts/modules/shared/services/dateHelper.js
ngShared.factory('dateHelper', [function () {
    return {
        getJsFullDate: function (jsonDate) {
            return moment(jsonDate);
        },
        
        getJsDate: function (jsonDate) {
            var date = moment(jsonDate).format("MMM D, YYYY");
            return date;
        },

        getMonthYear: function(jsonDate) {
            var date = moment(jsonDate).format("MMMM YYYY");
            return date;
        },

        getJsTime: function (jsonDate) {
            var time = moment(jsonDate).format("hh:mm A");
            return time;
        },

        getDateDisplay: function (jsonDate) {
            var itemDate = moment(jsonDate);
            var currDate = moment();
            
            return itemDate.from(currDate) + " at " + this.getJsTime(jsonDate);
        }
    };
}]);
///#source 1 1 /Scripts/modules/sockets/socket.js
var ngBlogSockets = angular.module("ngBlogSockets",
    [
        "ngConfig",
        "ngAnimate"
    ]);
///#source 1 1 /Scripts/modules/sockets/directives/socketDebugger.js
ngBlogSockets.directive("socketDebugger", [
    function () {
        var ctrlFn = function ($scope, $rootScope, blogSocketsService, configProvider) {
            $scope.messages = [];

            $scope.show = false;

            $scope.channelSubscription = null;

            $scope.echoMessage = null;

            $scope.showEmptyMessage = function() {
                if ($scope.messages.length > 0) {
                    return false;
                }
                return true;
            };

            $scope.doEcho = function () {
                blogSocketsService.emit("echo", { message: $scope.echoMessage });
            };

            $scope.subscribeToChannel = function () {
                var id = parseInt($scope.channelSubscription);
                blogSocketsService.emit(configProvider.getSocketClientFunctions().subscribeViewPost, { postId: id });
            };

            $rootScope.$on("toggleSocketDebugger", function () {
                $scope.show = !$scope.show;
            });

            $rootScope.$on(configProvider.getSocketClientFunctions().publishMessage, function (ev, data) {
                $scope.addToMessages(configProvider.getSocketClientFunctions().publishMessage, data);
            });

            $rootScope.$on(configProvider.getSocketClientFunctions().commentLikesUpdate, function (ev, data) {
                $scope.addToMessages(configProvider.getSocketClientFunctions().commentLikesUpdate, data);
            });

            $rootScope.$on(configProvider.getSocketClientFunctions().postTopComments, function (ev, data) {
                $scope.addToMessages(configProvider.getSocketClientFunctions().postTopComments, data);
            });

            $rootScope.$on(configProvider.getSocketClientFunctions().commentAdded, function (ev, data) {
                $scope.addToMessages(configProvider.getSocketClientFunctions().commentAdded, data);
            });

            $rootScope.$on(configProvider.getSocketClientFunctions().postLikesUpdate, function (ev, data) {
                $scope.addToMessages(configProvider.getSocketClientFunctions().postLikesUpdate, data);
            });

            $scope.addToMessages = function(fn, data) {
                var message = {
                    fn: fn,
                    data: JSON.stringify(data)
                };
                $scope.messages.push(message);
            };
        };
        ctrlFn.$inject = ["$scope", "$rootScope", "blogSocketsService", "configProvider"];

        return {
            controller: ctrlFn,
            restrict: 'EA',
            replace: true,
            templateUrl: window.blogConfiguration.templatesModulesUrl + "sockets/socketDebugger.html"
        };
    }
]);
///#source 1 1 /Scripts/modules/sockets/services/socketsService.js
// ReSharper disable UseOfImplicitGlobalInFunctionScope
ngBlogSockets.factory('blogSocketsService', ["$rootScope", "$timeout", "$interval", "configProvider",
    function ($rootScope, $timeout, $interval, configProvider) {
        var address = configProvider.getSettings().BlogSockets;

        var details = {
            resource: address + "socket.io"
        };

        var socket = {};
        if (typeof io !== "undefined") {
            socket = io.connect(address, details);
        }

        var broadcastMessage = function(topic, data) {
            var stop;

            stop = $interval(function () {
                if ($rootScope.$$listeners[topic] && $rootScope.$$listeners[topic].length > 0) {
                    $rootScope.$broadcast(topic, data);
                    $interval.cancel(stop);
                    stop = undefined;
                }
            }, 250);
        };

        if (configProvider.getSettings().BlogSocketsAvailable === "true") {
            var socketReady;

            socketReady = $interval(function() {
                if (socket && socket.on) {
                    $interval.cancel(socketReady);
                    socketReady = undefined;

                    socket.on('connect', function () {
                        $rootScope.$broadcast(configProvider.getSocketClientFunctions().wsConnect);
                    });

                    socket.on('echo', function (data) {
                        console.log(data);
                    });

                    socket.on(configProvider.getSocketClientFunctions().publishMessage, function (data) {
                        $timeout(function () {
                            $rootScope.$broadcast(configProvider.getSocketClientFunctions().publishMessage, data);
                        }, 250);
                    });

                    socket.on(configProvider.getSocketClientFunctions().getPostLikes, function (data) {
                        var topic = configProvider.getSocketClientFunctions().getPostLikes;
                        broadcastMessage(topic, data);
                    });

                    socket.on(configProvider.getSocketClientFunctions().viewCountUpdate, function (data) {
                        var topic = configProvider.getSocketClientFunctions().viewCountUpdate;
                        broadcastMessage(topic, data);
                    });

                    socket.on(configProvider.getSocketClientFunctions().getPostTopComments, function (data) {
                        var topic = configProvider.getSocketClientFunctions().getPostTopComments;
                        broadcastMessage(topic, data);
                    });

                    socket.on(configProvider.getSocketClientFunctions().postLikesUpdate, function (data) {
                        var topic = configProvider.getSocketClientFunctions().postLikesUpdate;
                        broadcastMessage(topic, data);
                    });

                    socket.on(configProvider.getSocketClientFunctions().commentLikesUpdate, function (data) {
                        var topic = configProvider.getSocketClientFunctions().commentLikesUpdate;
                        broadcastMessage(topic, data);
                    });

                    socket.on(configProvider.getSocketClientFunctions().commentAdded, function (data) {
                        var topic = configProvider.getSocketClientFunctions().commentAdded;
                        broadcastMessage(topic, data);
                    });
                }
            }, 250);
        }

        return {
            emit: function (eventName, data, callback) {
                if (socket.connected) {
                    if (typeof io !== "undefined") {
                        socket.emit(eventName, data, function () {
                            var args = arguments;
                            $rootScope.$apply(function () {
                                if (callback) {
                                    callback.apply(socket, args);
                                }
                            });
                            return true;
                        });
                    }
                }
                return false;
            }
        };
    }]);

// ReSharper restore UseOfImplicitGlobalInFunctionScope
///#source 1 1 /Scripts/modules/tags/tags.js
var ngTags = angular.module("ngTags", []);
///#source 1 1 /Scripts/modules/tags/directives/tagItem.js
ngTags.directive('tagItem', [function () {
    var ctrlFn = function ($scope) {
        
    };
    ctrlFn.$inject = ["$scope"];

    return {
        restrict: 'EA',
        scope: { tag: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "tags/tagItem.html",
        controller: ctrlFn
    };
}]);

///#source 1 1 /Scripts/modules/tags/services/tagsService.js
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
///#source 1 1 /Scripts/modules/user/user.js
var ngUser = angular.module("ngUser",
    [
        "LocalStorageModule",
        "ngSanitize",
        "ngShared",
        "ngComments",
        "ngPosts",
        "ngLogin",
        "ngConfig",
        "angularFileUpload"
    ]);
///#source 1 1 /Scripts/modules/user/controllers/userProfileCommentsController.js
ngUser.controller('userProfileCommentsController', ["$scope", "$rootScope", "$stateParams", "commentsService", "userService", "errorService", "localStorageService",
    function ($scope, $rootScope, $stateParams, commentsService, userService, errorService, localStorageService) {
        $scope.user = null;
        
        $scope.loggedUser = null;

        $scope.comments = [];

        $scope.isBusy = false;

        $scope.username = ($rootScope.$stateParams.username == null || $rootScope.$stateParams.username === "undefined") ?
                localStorageService.get("username") : $rootScope.$stateParams.username;

        $scope.init = function () {
            if ($rootScope.$stateParams.username != null || $rootScope.$stateParams.username !== "undefined") {
                $scope.getUserInfo();
            }
            $rootScope.$broadcast("updateScrollTriggerWatch", "user-profile-comments-list");
        };

        $scope.getUserInfo = function () {
            if ($scope.username) {
                userService.getUserInfo($scope.username).then(function (response) {
                    if (response.Error == null) {
                        $scope.user = response;
                        $scope.getCommentsByUser();
                    } else {
                        errorService.displayError(response.Error);
                    }
                }, function (err) {
                    errorService.displayError(err);
                });
            } else {
                errorService.displayError({ Message: "User lookup failed. Sorry. :(" });
            }
        };

        $scope.getCommentsByUser = function () {
            if ($scope.isBusy) {
                return;
            }
            $scope.isBusy = true;

            commentsService.getCommentsByUser($scope.user.Id).then(function (resp) {
                _.each(resp, function(c) {
                    c.User = $scope.user;
                    c.NameDisplay = $scope.user.FirstName + " " + $scope.user.LastName;
                });
                $scope.comments = resp;
                $scope.isBusy = false;
            }, function (e) {
                errorService.displayError(e);
            });
        };
        
        $scope.init();
    }
]);
///#source 1 1 /Scripts/modules/user/controllers/userProfileController.js
ngUser.controller('userProfileController', ["$scope", "$location", "$rootScope", "localStorageService", "userService", "errorService", "authenticationService",
    function ($scope, $location, $rootScope, localStorageService, userService, errorService, authenticationService) {
        $scope.user = null;

        $scope.userFullName = null;

        $scope.username = null;

        $scope.init = function () {
            if ($rootScope.$stateParams.username || $rootScope.$stateParams.username == null || $rootScope.$stateParams.username === "") {
                $scope.username = localStorageService.get("username");

                if ($scope.username == undefined || $scope.username == null) {
                    errorService.displayErrorRedirect({ Message: "You are not logged in. Try logging in or maybe create an account and join us." });
                } else {
                    authenticationService.getUserInfo().then(function (response) {
                        if (response.Message != undefined || response.Message != null) {
                            errorService.displayError(response.Message);
                        }
                        $scope.getUserInfo();
                    });
                }
            } else {
                $scope.username = $rootScope.$stateParams.username;
                $scope.getUserInfo();
            }
        };

        $scope.getUserInfo = function () {
            userService.getUserInfo($scope.username).then(function (user) {
                if (user.Error == null) {
                    $scope.user = user;
                    $scope.userFullName = $scope.user.FirstName + " " + $scope.user.LastName;
                    $rootScope.$broadcast("viewedUserLoaded", user);

                    delete user.Education;
                    delete user.Address;
                    delete user.Hobbies;
                } else {
                    errorService.displayError(user.Error);
                }
            }, function (err) {
                errorService.displayErrorRedirect(err);
            });
        };

        $scope.init();
    }
]);
///#source 1 1 /Scripts/modules/user/controllers/userProfileFavoritesController.js
ngUser.controller('userProfileFavoritesController', ["$scope", "$stateParams", "userService", "blockUiService", "errorService",
    function ($scope, $stateParams, userService, blockUiService, errorService) {
    }
]);
///#source 1 1 /Scripts/modules/user/controllers/userProfileMediaController.js
ngUser.controller('userProfileMediaController', ["$scope", "$rootScope", "$stateParams", "userService",
    "albumService", "errorService", "localStorageService", 
    function ($scope, $rootScope, $stateParams, userService, albumService, errorService, localStorageService) {
        $scope.user = null;

        $scope.albums = [];

        $scope.isBusy = false;

        $scope.username = ($rootScope.$stateParams.username == null || $rootScope.$stateParams.username === "undefined") ?
                localStorageService.get("username") : $rootScope.$stateParams.username;
        
        $scope.init = function () {
            if ($rootScope.$stateParams.username != null || $rootScope.$stateParams.username !== "undefined") {
                $scope.getUserInfo();
            }
        };

        $scope.getUserInfo = function () {
            if ($scope.username) {
                userService.getUserInfo($scope.username).then(function (response) {
                    if (response.Error == null) {
                        $scope.user = response;
                        $scope.getMediaByUser();
                    } else {
                        errorService.displayError(response.Error);
                    }
                }, function (err) {
                    errorService.displayError(err);
                });
            } else {
                errorService.displayError({ Message: "User lookup failed. Sorry. :(" });
            }
        };

        $scope.getMediaByUser = function () {
            if ($scope.isBusy) {
                return;
            }
            $scope.isBusy = true;

            albumService.getAlbumsByUser($scope.user.Id).then(function (resp) {
                $scope.albums = resp;
                $scope.isBusy = false;
            }, function (e) {
                errorService.displayError(e);
            });
        };
        
        $scope.init();
    }
]);
///#source 1 1 /Scripts/modules/user/controllers/userProfilePostsController.js
ngUser.controller('userProfilePostsController', ["$scope", "$rootScope", "$stateParams", "userService", "postsService", "errorService", "localStorageService",
    function ($scope, $rootScope, $stateParams, userService, postsService, errorService, localStorageService) {
        $scope.user = null;

        $scope.posts = [];

        $scope.isBusy = false;

        $scope.username = ($rootScope.$stateParams.username == null || $rootScope.$stateParams.username === "undefined") ?
                localStorageService.get("username") : $rootScope.$stateParams.username;

        $scope.size = "";

        $scope.init = function () {
            if ($rootScope.$stateParams.username != null || $rootScope.$stateParams.username !== "undefined") {
                $scope.getUserInfo();
            }
            $rootScope.$broadcast("updateScrollTriggerWatch", "user-profile-posts-list");
        };

        $scope.getUserInfo = function () {
            if ($scope.isBusy) {
                return;
            }
            $scope.isBusy = true;

            if ($scope.username) {
                userService.getUserInfo($scope.username).then(function (response) {
                    if (response.Error == null) {
                        $scope.user = response;
                        $scope.isBusy = false;
                        $scope.getPostsByUser();
                    } else {
                        errorService.displayError(response.Error);
                    }
                }, function (err) {
                    errorService.displayError(err);
                });
            } else {
                errorService.displayError({ Message: "User lookup failed. Sorry. :(" });
            }
        };

        $scope.getPostsByUser = function () {
            if ($scope.isBusy) {
                return;
            }
            $scope.isBusy = true;

            postsService.getPostsByUser($scope.user.Id).then(function (resp) {
                $scope.posts = resp;
                $scope.isBusy = false;
                $scope.$broadcast("resizeIsotopeItems");
            }, function(e) {
                errorService.displayError(e);
            });
        };

        $scope.getMorePostsByUser = function () {
            if ($scope.isBusy) {
                return;
            }
            $scope.isBusy = true;

            postsService.getMorePostsByUser($scope.user.Id, $scope.posts.length).then(function (resp) {
                _.each(resp, function (p) {
                    $scope.posts.push(p);
                });
                $scope.isBusy = false;
                $scope.$broadcast("resizeIsotopeItems");
            }, function (e) {
                errorService.displayError(e);
            });
        };

        $scope.$on("scrollBottom", function () {
            $scope.getMorePostsByUser();
        });
        
        $scope.$on("updateUserPostsSize", function (ev, size) {
            $scope.size = size;
        });
        
        $scope.init();
    }
]);
///#source 1 1 /Scripts/modules/user/directives/userImage.js
ngUser.directive('userImage', [function () {
    var ctrlFn = function ($scope, $rootScope, userService, configProvider, FileUploader, localStorageService) {
        $scope.authData = null;

        $scope.username = null;

        $scope.albumName = null;

        $scope.profileImageUrl = null;

        $scope.backgroundImageUrl = null;

        $scope.uploadUrl = null;

        $scope.showUpdateImages = function () {
            if ($scope.authData && $scope.user) {
                if ($scope.user.UserName === $scope.username) {
                    return true;
                }
            }
            return false;
        };

        $scope.updateProfileImage = function () {
            $scope.albumName = 'profile';
        };

        $scope.updateBackgroundImage = function () {
            $scope.albumName = 'background';
        };

        $scope.$watch('user', function () {
            if ($scope.user !== null && $scope.user !== undefined) {
                $scope.profileImageUrl = $scope.user.Picture.MediaUrl;
                $scope.backgroundImageUrl = $scope.user.Background.MediaUrl;
            }
        });

        $scope.$watch('username', function () {
            if ($scope.username !== null && $scope.username !== undefined) {
                $scope.uploadUrl = configProvider.getSettings().BlogApi == "" ?
                    window.blogConfiguration.blogApi + "media?username=" + $scope.username :
                    configProvider.getSettings().BlogApi + "media?username=" + $scope.username;
            }
        });

        $scope.init = function () {
            $scope.authData = localStorageService.get("authorizationData");
            $scope.username = localStorageService.get("username");
        };

        $scope.init();

        // #region image uploader object

        var uploader = $scope.uploader = new FileUploader({
            scope: $rootScope,
            url: $scope.uploadUrl,
            headers: { Authorization: 'Bearer ' + ($scope.authData ? $scope.authData.token : "") },
            autoUpload: true
        });

        uploader.filters.push({
            name: 'imageFilter',
            fn: function (item /*{File|HTMLInputElement}*/) {
                var type = uploader.isHTML5 ? item.type : '/' + item.value.slice(item.value.lastIndexOf('.') + 1);
                type = '|' + type.toLowerCase().slice(type.lastIndexOf('/') + 1) + '|';
                return '|jpg|png|jpeg|bmp|'.indexOf(type) !== -1;
            }
        });

        uploader.onSuccessItem = function (fileItem, response) {
            if ($scope.albumName === 'profile') {
                $scope.profileImageUrl = response.MediaUrl;
            } else if ($scope.albumName === 'background') {
                $scope.backgroundImageUrl = response.MediaUrl;
            }
        };

        uploader.onAfterAddingFile = function (item) {
            item.url = $scope.uploadUrl + '&album=' + $scope.albumName;
        };

        // #endregion
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "userService", "configProvider", "FileUploader", "localStorageService"];

    return {
        restrict: 'EA',
        scope: {
            user: '=',
            fullname: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "user/userImage.html",
        controller: ctrlFn
    };
}]);

///#source 1 1 /Scripts/modules/user/directives/userProfileDetails.js
ngUser.directive('userProfileDetails', [function () {
    var ctrlFn = function ($scope) {
    };
    ctrlFn.$inject = ["$scope"];

    return {
        restrict: 'EA',
        scope: {
            user: '=',
            fullname: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "user/userProfileDetails.html",
        controller: ctrlFn
    };
}]);

///#source 1 1 /Scripts/modules/user/directives/userProfileDetailsAddress.js
ngUser.directive('userProfileDetailsAddress', [function () {
    var ctrlFn = function ($scope, $rootScope, errorService, userService, localStorageService) {
        $scope.isEditing = false;

        $scope.address = {};

        $scope.error = {};

        $scope.username = localStorageService.get("username");

        $scope.editAddress = function () {
            $scope.isEditing = true;
        };

        $scope.cancelEditAddress = function () {
            $scope.isEditing = false;
        };

        $scope.saveAddress = function () {
            userService.updateUserAddress($scope.address).then(function (response) {
                if (response.Error == null) {
                    $scope.address = response;
                } else {
                    errorService.displayError(response.Error);
                }
                $scope.isEditing = false;
            }, function (err) {
                $scope.setModelStateErrors(err.ModelState);
            });
        };

        $scope.setModelStateErrors = function (error) {
            for (var name in error) {
                var tmp = name.toString().split('.')[1];
                $scope.error[tmp] = error[name][0];
            }
        };

        $scope.hasError = function (errorProperty) {
            if ($scope.error[errorProperty] == undefined) {
                return "";
            }
            return "has-error";
        };

        $scope.showButtons = function () {
            if ($scope.username == undefined || $scope.username == null || $scope.username == "") {
                return "hidden";
            } else {
                if ($scope.user == undefined) {
                    return "hidden";
                } else {
                    if ($scope.username !== $scope.user.UserName) {
                        return "hidden";
                    }
                    return "";
                }
            }
        };

        $scope.$on("viewedUserLoaded", function (ev, data) {
            $scope.address = data.Address;
            $scope.user = data;
        });
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "errorService", "userService", "localStorageService"];

    return {
        restrict: 'EA',
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "user/userProfileDetailsAddress.html",
        controller: ctrlFn
    };
}]);

///#source 1 1 /Scripts/modules/user/directives/userProfileDetailsEducation.js
ngUser.directive('userProfileDetailsEducation', [function () {
    var ctrlFn = function ($scope, $rootScope, dateHelper) {
        $scope.$on("viewedUserLoaded", function (ev, data) {
            $scope.educationGroups = data.EducationGroups;
            $scope.user = data;

            _.each($scope.educationGroups, function (g) {
                g.isAdding = false;

                _.each(g.Content, function (e) {
                    e.YearAttendedDisplay = dateHelper.getMonthYear(e.YearAttended);
                    e.YearGraduatedDisplay = dateHelper.getMonthYear(e.YearGraduated);
                });
            });
        });
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "dateHelper"];

    return {
        restrict: 'EA',
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "user/userProfileDetailsEducation.html",
        controller: ctrlFn
    };
}]);

///#source 1 1 /Scripts/modules/user/directives/userProfileDetailsEducationGroup.js
ngUser.directive('userProfileDetailsEducationGroup', [function () {
    var ctrlFn = function ($scope, localStorageService) {
        $scope.username = localStorageService.get("username");

        $scope.isAdding = false;

        $scope.newEducation = {
            City: "",
            Country: "",
            Course: "",
            EducationType: {
                EducationTypeId: $scope.educationGroup.EducationType,
                EducationTypeName: $scope.educationGroup.Title
            },
            SchoolName: "",
            State: "",
            UserId: $scope.user.Id,
            YearAttended: "",
            YearAttendedDisplay: "",
            YearGraduated: "",
            YearGraduatedDisplay: ""
        };

        $scope.emptyRecordMessage = "This person has no " + $scope.educationGroup.Title + " education..pathetic right?";

        $scope.addEducation = function() {
            $scope.isAdding = true;
        };

        $scope.showNoRecordsMessage = function() {
            if ($scope.educationGroup.Content.length > 0 || $scope.isAdding) {
                return false;
            }
            return true;
        };

        $scope.showButtons = function () {
            if ($scope.username == undefined || $scope.username == null || $scope.username == "") {
                return "hidden";
            } else {
                if ($scope.user == undefined) {
                    return "hidden";
                } else {
                    if ($scope.username !== $scope.user.UserName) {
                        return "hidden";
                    }
                    return "";
                }
            }
        };

        $scope.$on("cancelAddingUserEducation", function () {
            $scope.newEducation = {
                City: "",
                Country: "",
                Course: "",
                EducationType: {
                    EducationTypeId: $scope.educationGroup.type,
                    EducationTypeName: $scope.educationGroup.title
                },
                SchoolName: "",
                State: "",
                UserId: $scope.user.Id,
                YearAttended: "",
                YearAttendedDisplay: "",
                YearGraduated: "",
                YearGraduatedDisplay: ""
            };
            $scope.isAdding = false;
        });

        $scope.$on("successAddingUserEducation", function (ev, data) {
            $scope.educationGroup.Content.push(data);
        });

        $scope.$on("successDeletingUserEducation", function(ev, data) {
            var educationIndex = $scope.educationGroup.Content.indexOf(data);
            $scope.educationGroup.Content.splice(educationIndex, 1);
        });
    };
    ctrlFn.$inject = ["$scope", "localStorageService"];

    return {
        restrict: 'EA',
        scope: {
            educationGroup: '=',
            user: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "user/userProfileDetailsEducationGroup.html",
        controller: ctrlFn
    };
}]);

///#source 1 1 /Scripts/modules/user/directives/userProfileDetailsEducationItem.js
ngUser.directive('userProfileDetailsEducationItem', [function () {
    var ctrlFn = function ($scope, userService, dateHelper, blockUiService, errorService, localStorageService) {
        $scope.username = localStorageService.get("username");

        $scope.isEditing = $scope.isAdding == undefined ? false : ($scope.isAdding === "false" ? false : true);

        $scope.editEducation = function () {
            $scope.isEditing = true;
        };

        $scope.cancelEditing = function () {
            var isAdding = $scope.isAdding == undefined ? false : ($scope.isAdding === "false" ? false : true);
            if (isAdding) {
                $scope.$emit("cancelAddingUserEducation");
                return;
            }
            $scope.isEditing = false;
        };

        $scope.saveEducation = function () {
            var isAdding = $scope.isAdding == undefined ? false : ($scope.isAdding === "false" ? false : true);
            if (isAdding) {
                $scope.addEducation();
            } else {
                $scope.updateEducation();
            }
        };

        $scope.deleteEducation = function () {
            userService.deleteUserEducation($scope.education.EducationId).then(function (response) {
                if (response.Error != null) {
                    errorService.displayError(response.Error);
                }

                $scope.$emit("successDeletingUserEducation", $scope.education);
            }, function (err) {
                $scope.setModelStateErrors(err.ModelState);
            });
        };

        $scope.addEducation = function () {
            $scope.education.UserId = $scope.user.Id;

            userService.addUserEducation($scope.education).then(function (response) {
                if (response.Error != null) {
                    errorService.displayError(response.Error);
                }

                response.YearAttendedDisplay = dateHelper.getMonthYear(response.YearAttended);
                response.YearGraduatedDisplay = dateHelper.getMonthYear(response.YearGraduated);

                $scope.$emit("successAddingUserEducation", response);
                $scope.$emit("cancelAddingUserEducation");
            }, function (err) {
                $scope.setModelStateErrors(err.ModelState);
            });
        };

        $scope.updateEducation = function () {
            userService.updateUserEducation($scope.education).then(function (response) {
                if (response.Error != null) {
                    errorService.displayError(response.Error);
                }

                $scope.isEditing = false;
            }, function (err) {
                $scope.setModelStateErrors(err.ModelState);
            });
        };

        $scope.showButtons = function () {
            if ($scope.username == undefined || $scope.username == null || $scope.username == "") {
                return "hidden";
            } else {
                if ($scope.user == undefined) {
                    return "hidden";
                } else {
                    if ($scope.username !== $scope.user.UserName) {
                        return "hidden";
                    }
                    return "";
                }
            }
        };

        $scope.educationCourseDisplay = function() {
            if (!$scope.education.Course || $scope.education.Course === '') {
                return 'No course selected';
            } else {
                return $scope.education.Course;
            }
        };
    };
    ctrlFn.$inject = ["$scope", "userService", "dateHelper", "blockUiService", "errorService", "localStorageService"];

    return {
        restrict: 'EA',
        scope: {
            education: '=',
            isAdding: '=',
            user: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "user/userProfileDetailsEducationItem.html",
        controller: ctrlFn
    };
}]);

///#source 1 1 /Scripts/modules/user/directives/userProfileDetailsHobbies.js
ngUser.directive('userProfileDetailsHobbies', [function () {
    var ctrlFn = function ($scope, $rootScope, blockUiService, errorService, userService, localStorageService) {
        $scope.isAdding = false;

        $scope.hobbies = [];

        $scope.error = {};

        $scope.newHobby = { HobbyName: "" };

        $scope.emptyRecordMessage = "Uhhh..a no lifer..";

        $scope.username = localStorageService.get("username");

        $scope.addHobby = function () {
            $scope.isAdding = true;
        };

        $scope.cancelAdding = function () {
            $scope.isAdding = false;
        };

        $scope.saveHobby = function () {
            $scope.newHobby.UserId = $scope.user.Id;

            userService.addUserHobby($scope.newHobby).then(function (response) {
                if (response.Error == null) {
                    $scope.hobbies.push(response);
                    $scope.newHobby = { HobbyName: "", UserId: $scope.user };
                } else {
                    errorService.displayError(response.Error);
                }

                $scope.isAdding = false;
            }, function (err) {
                $scope.setModelStateErrors(err.ModelState);
            });
        };

        $scope.$on("successDeletingUserHobby", function (ev, data) {
            var hobbyIndex = $scope.hobbies.indexOf(data);
            $scope.hobbies.splice(hobbyIndex, 1);
        });

        $scope.showNoRecordsMessage = function () {
            if ($scope.hobbies.length > 0) {
                return false;
            }
            return true;
        };

        $scope.setModelStateErrors = function (error) {
            for (var name in error) {
                var tmp = name.toString().split('.')[1];
                $scope.error[tmp] = error[name][0];
            }
        };

        $scope.hasError = function () {
            if ($scope.error.HobbyName == undefined || $scope.error.HobbyName == "") {
                return "";
            }
            return "has-error";
        };

        $scope.showButtons = function () {
            if ($scope.username == undefined || $scope.username == null || $scope.username == "") {
                return "hidden";
            } else {
                if ($scope.user == undefined) {
                    return "hidden";
                } else {
                    if ($scope.username !== $scope.user.UserName) {
                        return "hidden";
                    }
                    return "";
                }
            }
        };

        $scope.$on("viewedUserLoaded", function(ev, data) {
            $scope.hobbies = data.Hobbies;
            $scope.user = data;
        });
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "blockUiService", "errorService", "userService", "localStorageService"];

    return {
        restrict: 'EA',
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "user/userProfileDetailsHobbies.html",
        controller: ctrlFn
    };
}]);

///#source 1 1 /Scripts/modules/user/directives/userProfileDetailsHobbyItem.js
ngUser.directive('userProfileDetailsHobbyItem', [function () {
    var ctrlFn = function ($scope, blockUiService, errorService, userService, localStorageService) {
        $scope.isEditing = false;
        $scope.error = {};
        $scope.hobbyNameStore = "";
        $scope.username = localStorageService.get("username");

        $scope.cancelEditing = function () {
            $scope.hobby.HobbyName = $scope.hobbyNameStore;
            $scope.isEditing = false;
        };

        $scope.editHobby = function () {
            $scope.hobbyNameStore = $scope.hobby.HobbyName;
            $scope.isEditing = true;
        };

        $scope.saveHobby = function () {
            userService.updateUserHobby($scope.hobby).then(function (response) {
                if (response.Error != null) {
                    errorService.displayError(response.Error);
                }

                $scope.isEditing = false;
            }, function (err) {
                $scope.setModelStateErrors(err.ModelState);
            });
        };

        $scope.deleteHobby = function() {
            userService.deleteUserHobby($scope.hobby.HobbyId).then(function (response) {
                if (response.Error != null) {
                    errorService.displayError(response.Error);
                }

                $scope.$emit("successDeletingUserHobby", $scope.hobby);
            }, function (err) {
                $scope.setModelStateErrors(err.ModelState);
            });
        };

        $scope.setModelStateErrors = function (error) {
            for (var name in error) {
                var tmp = name.toString().split('.')[1];
                $scope.error[tmp] = error[name][0];
            }
        };

        $scope.hasError = function () {
            if ($scope.error.HobbyName == undefined || $scope.error.HobbyName == "") {
                return "";
            }
            return "has-error";
        };

        $scope.showButtons = function () {
            if ($scope.username == undefined || $scope.username == null || $scope.username == "") {
                return "hidden";
            } else {
                if ($scope.user == undefined) {
                    return "hidden";
                } else {
                    if ($scope.username !== $scope.user.UserName) {
                        return "hidden";
                    }
                    return "";
                }
            }
        };
    };
    ctrlFn.$inject = ["$scope", "blockUiService", "errorService", "userService", "localStorageService"];

    return {
        restrict: 'EA',
        scope: {
            hobby: '=',
            user: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "user/userProfileDetailsHobbyItem.html",
        controller: ctrlFn
    };
}]);

///#source 1 1 /Scripts/modules/user/directives/userProfileDetailsInfo.js
ngUser.directive('userProfileDetailsInfo', [function () {
    var ctrlFn = function ($scope, errorService, userService, localStorageService) {
        $scope.isEditing = false;
        $scope.userFullName = null;
        $scope.error = {};
        $scope.username = localStorageService.get("username");

        $scope.editDetails = function () {
            $scope.isEditing = true;
        };

        $scope.cancelEditDetails = function() {
            $scope.isEditing = false;
        };

        $scope.saveDetails = function () {
            userService.updateUser($scope.user).then(function (response) {
                if (response.Error == null) {
                    delete response.Education;
                    delete response.Address;
                    delete response.Hobbies;

                    $scope.user = response;
                    $scope.userFullName = $scope.user.FirstName + " " + $scope.user.LastName;

                    $scope.isEditing = false;
                } else {
                    errorService.displayError(response.Error);
                    $scope.isEditing = false;
                }
            }, function (err) {
                $scope.setModelStateErrors(err.ModelState);
            });
        };

        $scope.setModelStateErrors = function (error) {
            for (var name in error) {
                var tmp = name.toString().split('.')[1];
                $scope.error[tmp] = error[name][0];
            }
        };

        $scope.hasError = function (errorProperty) {
            if ($scope.error[errorProperty] == undefined) {
                return "";
            }
            return "has-error";
        };

        $scope.showButtons = function () {
            if ($scope.username == undefined || $scope.username == null || $scope.username == "") {
                return "hidden";
            } else {
                if ($scope.user == undefined) {
                    return "hidden";
                } else {
                    if ($scope.username !== $scope.user.UserName) {
                        return "hidden";
                    }
                    return "";
                }
            }
        };
    };
    ctrlFn.$inject = ["$scope", "errorService", "userService", "localStorageService"];

    return {
        restrict: 'EA',
        scope: {
            user: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "user/userProfileDetailsInfo.html",
        controller: ctrlFn
    };
}]);

///#source 1 1 /Scripts/modules/user/directives/userProfileNavigation.js
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

///#source 1 1 /Scripts/modules/user/services/userService.js
ngUser.factory('userService', ["$http", "$q", "configProvider", "dateHelper",
    function ($http, $q, configProvider, dateHelper) {
        var userApi = configProvider.getSettings().BlogApi == "" ? window.blogConfiguration.blogApi + "Users" : configProvider.getSettings().BlogApi + "Users";
        var addressApi = configProvider.getSettings().BlogApi == "" ? window.blogConfiguration.blogApi + "Address" : configProvider.getSettings().BlogApi + "Address";
        var hobbyApi = configProvider.getSettings().BlogApi == "" ? window.blogConfiguration.blogApi + "Hobbies" : configProvider.getSettings().BlogApi + "Hobbies";
        var educationApi = configProvider.getSettings().BlogApi == "" ? window.blogConfiguration.blogApi + "Education" : configProvider.getSettings().BlogApi + "Education";

        var applyUserModelDefaults = function(user) {
            user.BirthDateDisplay = dateHelper.getJsDate(user.BirthDate);
            user.BirthDate = dateHelper.getJsFullDate(user.BirthDate);

            if (user.FirstName == null) user.FirstName = "n/a";
            if (user.LastName == null) user.LastName = "n/a";
            if (user.Picture == null) user.Picture = { MediaUrl: configProvider.getDefaults().profilePictureUrl };
            if (user.Background == null) user.Background = { MediaUrl: configProvider.getDefaults().backgroundPictureUrl };

            return user;
        };

        return {
            getUserInfo: function (username) {
                var deferred = $q.defer();

                $http({
                    url: userApi + "/" + username,
                    method: "GET"
                }).success(function (response) {
                    deferred.resolve(applyUserModelDefaults(response));
                }).error(function (err) {
                    deferred.reject(err);
                });

                return deferred.promise;
            },

            updateUser: function (user) {
                var deferred = $q.defer();
                
                $http({
                    url: userApi,
                    method: "PUT",
                    data: user
                }).success(function (response) {
                    deferred.resolve(applyUserModelDefaults(response));
                }).error(function (error) {
                    deferred.reject(error);
                });

                return deferred.promise;
            },

            addUserAddress: function (address) {
                var deferred = $q.defer();

                $http({
                    url: addressApi,
                    method: "POST",
                    data: address
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (error) {
                    deferred.reject(error);
                });

                return deferred.promise;
            },

            updateUserAddress: function(address) {
                var deferred = $q.defer();

                $http({
                    url: addressApi,
                    method: "PUT",
                    data: address
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (error) {
                    deferred.reject(error);
                });

                return deferred.promise;
            },

            addUserHobby: function(hobby) {
                var deferred = $q.defer();

                $http({
                    url: hobbyApi,
                    method: "POST",
                    data: hobby
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (error) {
                    deferred.reject(error);
                });

                return deferred.promise;
            },

            updateUserHobby: function(hobby) {
                var deferred = $q.defer();

                $http({
                    url: hobbyApi,
                    method: "PUT",
                    data: hobby
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (error) {
                    deferred.reject(error);
                });

                return deferred.promise;
            },

            deleteUserHobby: function (hobbyId) {
                var deferred = $q.defer();

                $http({
                    url: hobbyApi + "/" + hobbyId,
                    method: "DELETE"
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (error) {
                    deferred.reject(error);
                });

                return deferred.promise;
            },

            addUserEducation: function (education) {
                var deferred = $q.defer();

                $http({
                    url: educationApi,
                    method: "POST",
                    data: education
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (error) {
                    deferred.reject(error);
                });

                return deferred.promise;
            },

            updateUserEducation: function (education) {
                var deferred = $q.defer();

                $http({
                    url: educationApi,
                    method: "PUT",
                    data: education
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (error) {
                    deferred.reject(error);
                });

                return deferred.promise;
            },

            deleteUserEducation: function (educationId) {
                var deferred = $q.defer();

                $http({
                    url: educationApi + "/" + educationId,
                    method: "DELETE",
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (error) {
                    deferred.reject(error);
                });

                return deferred.promise;
            }
        };
    }
]);
