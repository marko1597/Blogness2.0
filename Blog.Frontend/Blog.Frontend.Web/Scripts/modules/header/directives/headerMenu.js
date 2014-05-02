headerModule.directive('headerMenu', function ($timeout) {
    var ctrlFn = function ($scope) {
        $scope.userLoggedIn = false;

        $scope.openNavigationMenu = function () {
            $timeout(function() {
                $scope.isNavigationOpen = !$scope.isNavigationOpen;
            }, 300);
        };
    };
    ctrlFn.$inject = ["$scope", "$timeout"];

    var linkFn = function (scope, element) {
        var isNavigationOpen = false;
        var container = document.getElementById('main-container');

        var resetMenu = function () {
            isNavigationOpen = false;
            classie.remove(container, 'navigation-menu-open');
        };

        var hasParentClass = function (e, classname) {
            if (e === document) return false;
            if (classie.has(e, classname)) {
                return true;
            }
            return e.parentNode && hasParentClass(e.parentNode, classname);
        };

        /* Opens side menu
         * ============================================== */
        $(element).find(".navbar-brand").click(function(ev) {
            container.className = 'main-container';

            if (isNavigationOpen) {
                if (!hasParentClass(ev.target, 'navigation-menu')) {
                    resetMenu();
                }
            } else {
                classie.add(container, "navigation-effect");
                isNavigationOpen = true;
                setTimeout(function () {
                    classie.add(container, 'navigation-menu-open');
                }, 25);
            }
        });
    };

    return {
        restrict: 'EA',
        scope: { data: '=' },
        link: linkFn,
        replace: true,
        template:
            '<div id="blog-header">' +
        	    '<div class="navbar navbar-default navbar-fixed-top" role="navigation">' +
			        '<div class="navbar-header">' +
			            '<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#sidebar">' +
			                '<span class="icon-bar"></span>' +
			                '<span class="icon-bar"></span>' +
			                '<span class="icon-bar"></span>' +
			            '</button>' +
                        '<div class="navmenu-toggle">' +
                            '<span class="icon-bar"></span>' +
			                '<span class="icon-bar"></span>' +
			                '<span class="icon-bar"></span>' +
                        '</div>' +
			            '<a class="navbar-brand" href="#">' +
                            'Bloggity Blog' +
	                    '</a>' +
			        '</div>' +
                '</div>' +
			'</div>',
        controller: ctrlFn
    };
});
