$(function () {
    // Hobby item view model
    function hobbyViewModel(id, name, userId, createdBy, createdDate, modifiedBy, modifiedDate, isNew) {
        var self = this;

        self.HobbyId = ko.observable(id);

        self.HobbyName = ko.observable(name);

        self.UserId = ko.observable(userId);

        self.CreatedBy = ko.observable(createdBy);

        self.CreatedDate = ko.observable(createdDate);

        self.ModifiedBy = ko.observable(modifiedBy);

        self.ModifiedDate = ko.observable(modifiedDate);

        // Style of the hobby item view by checking the isNew flag
        self.getStyle = ko.pureComputed(function() {
            return self.isNew() ? "content-item-warning" : "content-item-default";
        }, self);

        // Flag to check if hobby is being added or updated
        self.isNew = ko.observable(isNew);

        // Flag to check if details are shown
        self.showHobbyDetails = ko.observable(false);

        // Pretty date display for created date
        self.createdDateDisplay = ko.computed(function () {
            return moment(self.CreatedDate()).format('MMM Do YYYY, h:mm:ss a');
        }, self);

        // Pretty date display for modified date
        self.modifiedDateDisplay = ko.computed(function () {
            return moment(self.ModifiedDate()).format('MMM Do YYYY, h:mm:ss a');
        }, self);

        // Show hobby details on info icon click
        self.toggleShowHobbyDetails = function () {
            var currentValue = self.showHobbyDetails();
            self.showHobbyDetails(!currentValue);
        };

        // Saves hobby
        self.saveHobby = function (parent) {
            if (self.isNew()) {
                $.ajax("/hobbies/create", {
                    data: ko.toJSON(self),
                    type: "post", contentType: "application/json",
                    success: function (result) {
                        parent.alertMessageClass("alert-success");
                        parent.alertMessageText("Successfully added new hobby! Yay!");
                        parent.alertMessageVisible(true);
                        parent.hasNew(false);

                        self.isNew(false);
                        self.CreatedBy(result.CreatedBy);
                        self.CreatedDate(result.CreatedDate);
                        self.ModifiedBy(result.ModifiedBy);
                        self.ModifiedDate(result.ModifiedDate);
                    },
                    error: function (err) {
                        parent.alertMessageClass("alert-danger");
                        parent.alertMessageText("Failed adding new hobby! " + err.statusText);
                        parent.alertMessageVisible(true);
                    }
                });
            } else {
                $.ajax("/hobbies/edit", {
                    data: ko.toJSON(self),
                    type: "post", contentType: "application/json",
                    success: function (result) {
                        parent.alertMessageClass("alert-success");
                        parent.alertMessageText("Successfully updated hobby! Yay!");
                        parent.alertMessageVisible(true);

                        self.CreatedBy(result.CreatedBy);
                        self.CreatedDate(result.CreatedDate);
                        self.ModifiedBy(result.ModifiedBy);
                        self.ModifiedDate(result.ModifiedDate);
                    },
                    error: function () {
                        parent.alertMessageClass("alert-danger");
                        parent.alertMessageText("Failed updating hobby!");
                        parent.alertMessageVisible(true);
                    }
                });
            }
        };

        // Deletes hobby
        self.deleteHobby = function (parent) {
            if (!self.isNew()) {
                $.ajax("/hobbies/delete/" + self.HobbyId(), {
                    type: "post", contentType: "application/json",
                    success: function () {
                        parent.alertMessageClass("alert-success");
                        parent.alertMessageText("Successfully deleted hobby! Yay? :/");
                        parent.alertMessageVisible(true);
                        parent.removeHobby(self);
                    },
                    error: function (err) {
                        parent.alertMessageClass("alert-danger");
                        parent.alertMessageText("Failed deleting hobby! " + err.statusText);
                        parent.alertMessageVisible(true);
                    }
                });
            } else {
                parent.hasNew(false);
                parent.removeHobby(self);
            }
        };
    }

    // Hobbies view model..hooray!
    function hobbiesViewModel() {
        var self = this;

        // User Id of the viewed user
        self.UserId = ko.observable(window.userId);

        // Flag to show the alert message
        self.alertMessageVisible = ko.observable(false);

        // Text to display in alert messages
        self.alertMessageText = ko.observable();

        // Determine the alert class/style
        self.alertMessageClass = ko.observable("alert-success");

        // Hobbies collection
        self.hobbies = ko.observableArray([]);

        // Flag that tells whether user has created a new hobby
        self.hasNew = ko.observable(false);

        // Hides alert messages whenver the alertMessageVisible value changes
        self.alertMessageVisible.subscribe(function () {
            setTimeout(function () {
                self.alertMessageVisible(false);
            }, 5000);
        });

        // Hides the alert message manually
        self.hideAlertMessage = function () {
            self.alertMessageVisible(false);
        };

        // Adds new hobby to collection
        self.addHobby = function () {
            self.hasNew(true);
            self.hobbies.push(new hobbyViewModel(0, "", self.UserId(), 0, moment().toDate(),
                0, moment().toDate(), true));
        };

        // Removes hobby from collection
        self.removeHobby = function(model) {
            var index = self.hobbies().indexOf(model);

            if (model.isNew()) {
                self.hasNew(false);
            }

            if (index >= 0) {
                self.hobbies.splice(index, 1);
            }
        };

        // Adds hobby model on load to the hobbies collection
        for (var i = 0; i < hobbies.length; i++) {
            self.hobbies.push(new hobbyViewModel(hobbies[i].HobbyId, hobbies[i].HobbyName,
                hobbies[i].UserId, hobbies[i].CreatedBy, hobbies[i].CreatedDate,
                hobbies[i].ModifiedBy, hobbies[i].ModifiedDate, false));
        }
    }

    ko.applyBindings(new hobbiesViewModel());
})