ngShared.directive('fileUploadItemResize', ["$window",
    function ($window) {
        var linkFn = function (scope) {
            scope.$on("windowSizeChanged", function (e, d) {
                resizeUploadItems(d.width);
            });

            scope.$on("resizeUploadThumbnails", function () {
                resizeUploadItems($window.innerWidth);
            });

            var resizeUploadItems = function (w) {
                console.log(w);
                if (w >= 992) {
                    _.each($("ul.upload-items > li.isotope-item"), function (a) {
                        $(a).width("32%");
                    });
                } else if (w >= 767 && w < 992) {
                    _.each($("ul.upload-items > li.isotope-item"), function (a) {
                        $(a).width("48%");
                    });
                } else {
                    _.each($("ul.upload-items > li.isotope-item"), function (a) {
                        $(a).width("96%");
                    });
                }
                scope.$broadcast('iso-method', { name: null, params: null });
                scope.$broadcast('iso-option', { layoutMode: 'masonry' });
            };
            resizeUploadItems($window.innerWidth);
        };

        return {
            restrict: 'EA',
            link: linkFn
        };
    }
]);
