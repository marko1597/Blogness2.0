ngShared.directive('isotopeItemResize', ["$window", "$timeout", "$interval",
    function ($window, $timeout, $interval) {
        var linkFn = function (scope, elem, attrs) {
            scope.columnCount = 0;
            scope.$emit('iso-option', { 'animationEngine' : 'best-available' });

            scope.applyLayout = function () {
                $interval(function () {
                    resizeItems($window.innerWidth);
                    scope.$broadcast('iso-method', { name: 'layout', params: null });

                    // TODO: temporarily removed and to be verified if it works!
                    //var isotopeElements = elem.children();
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
                        _.each($(elem).children(), function (a) {
                            var large = attrs.resizeLarge == undefined ? "32%" : attrs.resizeLarge;
                            $(a).width(large);
                        });
                    } else if (w >= 767 && w < 992) {
                        scope.columnCount = getColumnCount(w, attrs.resizeLarge, "48%");
                        _.each($(elem).children(), function (a) {
                            var medium = attrs.resizeMedium == undefined ? "48%" : attrs.resizeMedium;
                            $(a).width(medium);
                        });
                    } else {
                        scope.columnCount = getColumnCount(w, attrs.resizeSmall, "96%");
                        _.each($(elem).children(), function (a) {
                            var small = attrs.resizeSmall == undefined ? "96%" : attrs.resizeSmall;
                            $(a).width(small);
                        });
                    }
                } else {
                    var container = $("#" + attrs.resizeContainer);
                    var containerWidth = container.outerWidth();

                    if (containerWidth > 1200) {
                        scope.columnCount = getColumnCount(containerWidth, attrs.resizeXlarge, "19%");
                        _.each($(elem).children(), function (a) {
                            var xlarge = attrs.resizeXlarge == undefined ? "19%" : attrs.resizeXlarge;
                            $(a).width(xlarge);
                        });
                        if (attrs.resizeBroadcast != undefined)
                            scope.$emit(attrs.resizeBroadcast, "xlarge");
                    } else if (containerWidth > 992) {
                        scope.columnCount = getColumnCount(containerWidth, attrs.resizeLarge, "23.5%");
                        _.each($(elem).children(), function (a) {
                            var large = attrs.resizeLarge == undefined ? "23.5%" : attrs.resizeLarge;
                            $(a).width(large);
                        });
                        if (attrs.resizeBroadcast != undefined)
                            scope.$emit(attrs.resizeBroadcast, "large");
                    } else if (containerWidth > 768) {
                        scope.columnCount = getColumnCount(containerWidth, attrs.resizeMedium, "31.5%");
                        _.each($(elem).children(), function (a) {
                            var medium = attrs.resizeMedium == undefined ? "31.5%" : attrs.resizeMedium;
                            $(a).width(medium);
                        });
                        if (attrs.resizeBroadcast != undefined)
                            scope.$emit(attrs.resizeBroadcast, "medium");
                    } else if (containerWidth > 568) {
                        scope.columnCount = getColumnCount(containerWidth, attrs.resizeSmall, "48%");
                        _.each($(elem).children(), function (a) {
                            var medium = attrs.resizeSmall == undefined ? "48%" : attrs.resizeSmall;
                            $(a).width(medium);
                        });
                        if (attrs.resizeBroadcast != undefined)
                            scope.$emit(attrs.resizeBroadcast, "small");
                    } else {
                        scope.columnCount = getColumnCount(containerWidth, attrs.resizeXsmall, "98%");
                        _.each($(elem).children(), function (a) {
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
