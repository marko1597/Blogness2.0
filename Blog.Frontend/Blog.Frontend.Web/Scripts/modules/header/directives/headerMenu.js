headerMenu.directive('headerMenu', function () {
    var ctrlFn = function ($scope, $rootScope) {
    };
    ctrlFn.$inject = ["$scope", "$rootScope"];

    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        template:
        	'<div class="navbar navbar-default navbar-fixed-top" role="navigation">' +
			    '<div class="container">' +
			        '<div class="navbar-header">' +
			            '<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#sidebar">' +
			                '<span class="icon-bar"></span>' +
			                '<span class="icon-bar"></span>' +
			                '<span class="icon-bar"></span>' +
			            '</button>' +
			            '<a class="navbar-brand" href="#">Bloggity Blog</a>' +
			        '</div>' +
			    '</div>' +
			'</div>',
        controller: ctrlFn
    };
});
