headerModule.directive('headerMenu', function () {
    var ctrlFn = function ($scope) {
        $scope.userLoggedIn = false;
    };
    ctrlFn.$inject = ["$scope"];

    var linkFn = function (scope, element) {
        var container = document.getElementById('main-container');
        var resetMenu = function () {
            classie.remove(container, 'navigation-menu-open');
        };
        var hasParentClass = function (e, classname) {
            if (e === document) return false;
            if (classie.has(e, classname)) {
                return true;
            }
            return e.parentNode && hasParentClass(e.parentNode, classname);
        };

        $(element).find(".navbar-brand").click(function() {
            container.className = 'main-container';
            classie.add(container, "navigation-effect");
            setTimeout(function () {
                classie.add(container, 'navigation-menu-open');
            }, 25);
        });

        $(document).on("click", function (evt) {
            if (!hasParentClass(evt.target, 'navigation-menu')) {
                resetMenu();
                document.removeEventListener("click", this);
            }
        });
    };

    return {
        restrict: 'EA',
        scope: { data: '=' },
        link: linkFn,
        replace: true,
        template:
        	'<div class="navbar navbar-default navbar-fixed-top" role="navigation">' +
			        '<div class="navbar-header">' +
			            '<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#sidebar">' +
			                '<span class="icon-bar"></span>' +
			                '<span class="icon-bar"></span>' +
			                '<span class="icon-bar"></span>' +
			            '</button>' +
			            '<a class="navbar-brand" href="#">Bloggity Blog</a>' +
			        '</div>' +
			'</div>',
        controller: ctrlFn
    };
});
