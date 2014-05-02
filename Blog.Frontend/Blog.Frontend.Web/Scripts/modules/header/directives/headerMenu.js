ngHeader.directive('headerMenu', function () {
    var ctrlFn = function ($scope) {
        $scope.userLoggedIn = false;
    };
    ctrlFn.$inject = ["$scope"];

    
    return {
        restrict: 'EA',
        scope: { data: '=' },
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
			            '<a class="navbar-brand" href="#" snap-toggle>' +
                            'Bloggity Blog' +
	                    '</a>' +
			        '</div>' +
                '</div>' +
			'</div>',
        controller: ctrlFn
    };
});
