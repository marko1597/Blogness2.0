ngShared.directive('isotopeItemResize', ["$window", "$timeout",
    function ($window, $timeout) {
        var linkFn = function (scope, elem, attrs) {
            scope.$on("windowSizeChanged", function (e, d) {
                resizeUploadItems(d.width);
            });

            scope.$on("resizeIsotopeItems", function () {
                resizeUploadItems($window.innerWidth);
            });

            var resizeUploadItems = function (w) {
                $timeout(function () {
                    if (attrs.reapplyLayoutOnly == undefined || attrs.reapplyLayoutOnly === "false") {
                        if (w >= 992) {
                            _.each($(elem).children(".isotope-item"), function (a) {
                                var large = attrs.resizeLarge == undefined ? "32%" : attrs.resizeLarge;
                                $(a).width(large);
                            });
                        } else if (w >= 767 && w < 992) {
                            _.each($(elem).children(".isotope-item"), function (a) {
                                var medium = attrs.resizeMedium == undefined ? "48%" : attrs.resizeMedium;
                                $(a).width(medium);
                            });
                        } else {
                            _.each($(elem).children(".isotope-item"), function (a) {
                                var small = attrs.resizeSmall == undefined ? "96%" : attrs.resizeSmall;
                                $(a).width(small);
                            });
                        }
                    }

                    $timeout(function () {
                        scope.$broadcast('iso-method', { name: null, params: null });
                        scope.$broadcast('iso-option', { layoutMode: 'masonry' });
                    }, 100);
                }, 200);
            };

            resizeUploadItems($window.innerWidth);
        };

        return {
            restrict: 'EA',
            link: linkFn
        };
    }
]);
