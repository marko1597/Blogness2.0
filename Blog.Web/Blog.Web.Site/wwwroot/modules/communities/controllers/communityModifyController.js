// ReSharper disable InconsistentNaming

ngCommunities.controller('communityModifyController', ["$scope", "$rootScope", "$location", "$timeout", "$window",
	"$templateCache", "$modal", "FileUploader", "localStorageService", "communitiesService", "userService",
	"albumService", "errorService", "dateHelper", "configProvider", "authenticationService",
	function ($scope, $rootScope, $location, $timeout, $window, $templateCache, $modal, FileUploader, localStorageService,
		communitiesService, userService, albumService, errorService, dateHelper, configProvider,
		authenticationService) {
        
	    var mediaSelectionDialog = $modal({
	        title: 'Select emblem for this community',
	        scope: $scope,
	        template: "media/mediaSelectionDialog.html",
	        show: false
	    });
        
	    $scope.user = $rootScope.user;

	    $scope.isAdding = false;

	    $scope.albums = [];

	    $scope.community = {
	        Name: "",
	        Description: "",
            Leader: $scope.user,
	        LeaderUserId: $scope.user ? $scope.user.Id : null,
            EmblemId: null
	    };

	    $scope.selectedEmblemUrl = null;

	    $scope.username = localStorageService.get("username");

	    $scope.authData = localStorageService.get("authorizationData");

	    $scope.getCommunity = function () {
	        communitiesService.getById($rootScope.$stateParams.communityId).then(function (resp) {
	            if ($scope.username === resp.Leader.UserName) {
	                if (resp.Error == undefined) {
	                    $scope.isAdding = false;
	                    $scope.community = resp;

	                    if ($scope.community.Emblem) {
	                        $scope.selectedEmblemUrl = $scope.community.Emblem.MediaUrl;
	                    }
	                } else {
	                    errorService.displayError(resp.Error);
	                }
	            } else {
	                errorService.displayError({ Message: "Oh you sneaky bastard! This community is not yours to edit." });
	            }
	        }, function (e) {
	            errorService.displayError(e);
	        });
	    };

	    $scope.saveCommunity = function () {
	        if ($scope.authData && $scope.user) {
	            if ($scope.isAdding) {
	                communitiesService.addCommunity($scope.community).then(function (resp) {
	                    if (resp.Error == undefined) {
	                        $location.path("/");
	                    } else {
	                        errorService.displayError(resp.Error);
	                    }
	                }, function (e) {
	                    errorService.displayError(e);
	                });
	            } else {
	                communitiesService.updateCommunity($scope.community).then(function (resp) {
	                    if (resp.Error == undefined) {
	                        $location.path("/");
	                    } else {
	                        errorService.displayError(resp.Error);
	                    }
	                }, function (e) {
	                    errorService.displayError(e);
	                });
	            }
	        }
	    };

	    $scope.cancelCommunity = function () {
	        $location.path("/communities");
	    };

	    $scope.init = function () {
	        authenticationService.getUserInfo().then(function (response) {
	            if (response.Message == undefined || response.Message == null) {
	                if (!isNaN($rootScope.$stateParams.communityId)) {
	                    $scope.getCommunity();
	                }
	            } else {
	                errorService.displayErrorRedirect(response.Message);
	            }
	        });
	    };

	    $rootScope.$watch('user', function () {
	        if ($rootScope.user) {
	            $scope.user = $rootScope.user;
	            $scope.community.Leader = $scope.user;
	            $scope.community.LeaderUserId = $scope.user.Id;
	        }
	    });

	    $scope.$on("userLoggedIn", function () {
	        $scope.username = localStorageService.get("username");
	        $scope.authData = localStorageService.get("authorizationData");
	    });
        
	    $scope.init();

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
	                mediaSelectionDialog.show();
	            }, function (e) {
	                errorService.displayError(e);
	            });
	        } else {
	            mediaSelectionDialog.show();
	        }
	    };

	    $scope.getThumbnailUrl = function (media) {
	        return {
	            "background-image": "url(" + media.ThumbnailUrl + ")"
	        };
	    };

	    $scope.toggleMediaSelectionToExistingContents = function (media) {
	        var isSelectedResult = !media.IsSelected;

	        $scope.selectedEmblemUrl = null;

	        _.each($scope.albums, function (album) {
	            _.each(album.Media, function (m) {
	                m.IsSelected = false;
	            });
	        });

	        if (!$scope.isImage(media.MediaType)) {
	            return false;
	        }

	        media.IsSelected = isSelectedResult;
	        if (isSelectedResult) {
	            $scope.community.EmblemId = media.Id;
	            $scope.selectedEmblemUrl = media.MediaUrl;
	        }
	    };

	    $scope.getMediaToggleButtonStyle = function (media) {
	        return media.IsSelected ? 'btn-danger' : 'btn-success';
	    };

	    $scope.getMediaToggleButtonIcon = function (media) {
	        return media.IsSelected ? 'fa-times' : 'fa-check';
	    };

	    $scope.isImage = function (type) {
	        var str = type.split('/');
            
	        if (str[0] !== 'image') {
	            return false;
	        }

	        var acceptedImages = ['png', 'jpg', 'jpeg', 'bmp'];

	        return _.contains(acceptedImages, str[1]);
	    };

	    // #endregion
	}
]);

// ReSharper restore InconsistentNaming