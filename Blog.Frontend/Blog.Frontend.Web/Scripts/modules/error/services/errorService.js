ngError.factory('errorService', ["$location", "$rootScope", "blockUiService",
    function ($location, $rootScope, blockUiService) {
        return {
            displayErrorUnblock: function (d) {
                blockUiService.unblockIt();
                $rootScope.$broadcast("displayError", { Message: d.Message });
            },

            displayErrorRedirect: function (d) {
                $rootScope.$broadcast("displayError", { Message: d.Message });
                $location.path("/404");
            }
        };
    }
]);