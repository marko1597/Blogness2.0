(function () {
    "use strict";

    // Identity mapping view model..hooray!
    function identityMappingViewModel() {
        var self = this;

        // Selected identity text
        self.selectedIdentityId = ko.observable();
        
        // Updates selected identity text when changing selection on Identity Users dropdown
        self.updateSelectedIdentityId = function(identityId) {
            self.selectedIdentityId(identityId);
        };
    }

    ko.applyBindings(new identityMappingViewModel());
})();