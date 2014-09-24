(function () {
    "use strict";

    // Profile image view model..hooray!
    function profileImageViewModel() {
        var self = this;

        // Selected image src
        self.selectedImage = ko.observable(window.mediaUrl);

        // Updates selected image src when user selects an image from computer
        self.updateSelectedImage = function (element) {
            if (element.files && element.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    self.selectedImage(e.target.result);
                };

                reader.readAsDataURL(element.files[0]);
            }
        };
    }

    ko.applyBindings(new profileImageViewModel());
})();