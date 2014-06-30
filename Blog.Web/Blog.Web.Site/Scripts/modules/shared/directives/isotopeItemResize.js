ngShared.directive('isotopeItemResize', ["$window", "$timeout", "$interval",
    function ($window, $timeout, $interval) {
        var linkFn = function (scope, elem, attrs) {
            scope.applyLayout = function () {
                $interval(function () {
                    resizeItems($window.innerWidth);
                    scope.$broadcast('iso-method', { name: null, params: null });
                }, 2000, 5);
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

            var resizeItems = function (w) {
                if (attrs.resizeContainer == undefined) {
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
                } else {
                    var container = $("#" + attrs.resizeContainer);
                    var containerWidth = container.outerWidth();

                    if (containerWidth > 1200) {
                        _.each($(elem).children(".isotope-item"), function (a) {
                            var xlarge = attrs.resizeXlarge == undefined ? "19%" : attrs.resizeXlarge;
                            $(a).width(xlarge);
                        });
                        if (attrs.resizeBroadcast != undefined)
                            scope.$emit(attrs.resizeBroadcast, "xlarge");
                    } else if (containerWidth > 992) {
                        _.each($(elem).children(".isotope-item"), function (a) {
                            var large = attrs.resizeLarge == undefined ? "24%" : attrs.resizeLarge;
                            $(a).width(large);
                        });
                        if (attrs.resizeBroadcast != undefined)
                            scope.$emit(attrs.resizeBroadcast, "large");
                    } else if (containerWidth > 768) {
                        _.each($(elem).children(".isotope-item"), function (a) {
                            var medium = attrs.resizeMedium == undefined ? "31.5%" : attrs.resizeMedium;
                            $(a).width(medium);
                        });
                        if (attrs.resizeBroadcast != undefined)
                            scope.$emit(attrs.resizeBroadcast, "medium");
                    } else if (containerWidth > 568) {
                        _.each($(elem).children(".isotope-item"), function (a) {
                            var medium = attrs.resizeMedium == undefined ? "48%" : attrs.resizeSmall;
                            $(a).width(medium);
                        });
                        if (attrs.resizeBroadcast != undefined)
                            scope.$emit(attrs.resizeBroadcast, "small");
                    } else {
                        _.each($(elem).children(".isotope-item"), function (a) {
                            var xsmall = attrs.resizeXsmall == undefined ? "98%" : attrs.resizeXsmall;
                            $(a).width(xsmall);
                        });
                        if (attrs.resizeBroadcast != undefined)
                            scope.$emit(attrs.resizeBroadcast, "xsmall");
                    }
                }
            };

            resizeItems($window.innerWidth);
        };

        return {
            restrict: 'EA',
            link: linkFn
        };
    }
]);
