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
                    url: window.blogConfiguration.blogRoot + "/content/plugins/videogular/themes/default/videogular.css"
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