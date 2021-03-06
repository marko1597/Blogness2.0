﻿// ReSharper disable InconsistentNaming

ngPosts.controller('postsModifyController', ["$scope", "$rootScope", "$location", "$timeout", "$window",
	"$templateCache", "$modal", "FileUploader", "localStorageService", "postsService", "userService",
	"albumService", "tagsService", "errorService", "dateHelper", "configProvider", "authenticationService",
	function ($scope, $rootScope, $location, $timeout, $window, $templateCache, $modal, FileUploader, localStorageService,
		postsService, userService, albumService, tagsService, errorService, dateHelper, configProvider,
		authenticationService) {

		var mediaSelectionDialog = $modal({
			title: 'Select media to add',
			scope: $scope,
			template: "media/mediaSelectionDialog.html",
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
			Communities: [],
			Tags: []
		};

	    $scope.emptyCommunitiesMessage = "No communities selected for this post to show..";

		$scope.uploadUrl = configProvider.getSettings().BlogApi == "" ?
			window.blogConfiguration.blogApi + "media?username=" + $scope.username + "&album=default" :
			configProvider.getSettings().BlogApi + "media?username=getTagsSource" + $scope.username + "&album=default";

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

	    $scope.hasCommunities = function() {
	        return $scope.post.Communities && $scope.post.Communities.length > 0;
	    };

	    $scope.communityCreatedByLoggedUser = function (community) {
	        if (community && community.Leader) {
	            return $scope.username === community.Leader.UserName;
	        }
	        return false;
	    };

	    $scope.removeCommunityFromPost = function(community) {
	        var index = $scope.post.Communities.indexOf(community);
	        $scope.post.Communities.splice(index);
	    };

		$scope.showCommunitySelection = function () {
			$scope.$broadcast("launchCommunitySelectionDialog", { canClose: true });
		};

		$scope.$on("doneSelectingCommunities", function (ev, data) {
		    $scope.post.Communities = [];

            if (data && data.items) {
                _.each(data.items, function(community) {
                    $scope.post.Communities.push(community);
                });
            } else {
                errorService.displayError("Something went wrong when you selected communities. :P");
            }
		});

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