$(function () {
    "use strict";

    // Default date format for moment js
    var defaultDateFormat = 'MMMM YYYY';

    // Education type view model
    function educationTypeViewModel(id, name) {
        var self = this;

        self.EducationTypeId = ko.observable(id);

        self.EducationTypeName = ko.observable(name);
    };

    // Education item view model
    function educationViewModel(data, isNew, educationTypes) {
        var self = this;

        // Education types collection
        self.EducationTypes = ko.observableArray(educationTypes);

        // Selected education type (defaults to Grade School)
        self.EducationType = ko.observable(data.EducationType === null ? 
            self.EducationTypes.find("EducationTypeId", 1) : self.EducationTypes.find("EducationTypeId", data.EducationType));
        
        self.EducationId = ko.observable(data.EducationId);

        self.SchoolName = ko.observable(data.SchoolName);

        self.Course = ko.observable(data.Course);

        self.State = ko.observable(data.State);

        self.City = ko.observable(data.City);

        self.Country = ko.observable(data.Country);

        self.CreatedDate = ko.observable(moment(data.CreatedDate).format(defaultDateFormat));

        self.CreatedBy = ko.observable(data.CreatedBy);

        self.YearAttended = ko.observable(moment(data.YearAttended).format(defaultDateFormat));

        self.YearGraduated = ko.observable(moment(data.YearAttended).format(defaultDateFormat));

        self.UserId = ko.observable(data.UserId);

        // Education type error message
        self.EducationTypeError = ko.observable();

        // School name error message
        self.SchoolNameError = ko.observable();

        // Education item style
        self.cssStyle = ko.observable(isNew ? "content-item-warning" : "content-item-default");

        // Changes the cssStyle to default whenever it changes value
        self.cssStyle.subscribe(function () {
            setTimeout(function () {
                self.cssStyle("content-item-default");
            }, 5000);
        });

        // Display text for selected education type
        self.selectedEducationTypeDisplay = ko.computed(function () {
            return self.EducationType() !== undefined && self.EducationType() !== null ? self.EducationType().EducationTypeName() : '';
        }, self);

        // Style of the education item view by checking the isNew flag
        self.getStyle = ko.pureComputed(function () {
            return self.isNew() ? "content-item-warning" : "content-item-default";
        }, self);

        // Flag to check if education is being added or updated
        self.isNew = ko.observable(isNew);

        // Saves education
        self.saveEducation = function (parent) {
            if (self.isNew()) {
                $.ajax("/education/create", {
                    data: ko.toJSON(self),
                    type: "post", contentType: "application/json",
                    success: function () {
                        parent.alertMessageClass("alert-success");
                        parent.alertMessageText("Successfully added new education! Yay!");
                        parent.alertMessageVisible(true);
                        parent.hasNew(false);

                        self.isNew(false);
                        self.EducationTypeError("");
                        self.SchoolNameError("");
                        self.cssStyle("content-item-success");
                    },
                    error: function (err) {
                        parent.alertMessageClass("alert-danger");
                        parent.alertMessageText("Failed adding new education! " + err.statusText);
                        parent.alertMessageVisible(true);

                        self.SchoolNameError(err.responseJSON.SchoolName[0]);
                        self.cssStyle("content-item-warning");
                    }
                });
            } else {
                $.ajax("/education/edit", {
                    data: ko.toJSON(self),
                    type: "post", contentType: "application/json",
                    success: function () {
                        parent.alertMessageClass("alert-success");
                        parent.alertMessageText("Successfully updated education! Yay!");
                        parent.alertMessageVisible(true);

                        self.EducationTypeError("");
                        self.SchoolNameError("");
                        self.cssStyle("content-item-success");
                    },
                    error: function (err) {
                        parent.alertMessageClass("alert-danger");
                        parent.alertMessageText("Failed updating education!");
                        parent.alertMessageVisible(true);

                        self.SchoolNameError(err.responseJSON.SchoolName[0]);
                        self.cssStyle("content-item-warning");
                    }
                });
            }
        };

        // Deletes education
        self.deleteEducation = function (parent) {
            if (!self.isNew()) {
                $.ajax("/education/delete/" + self.EducationId(), {
                    type: "post", contentType: "application/json",
                    success: function () {
                        parent.alertMessageClass("alert-success");
                        parent.alertMessageText("Successfully deleted education! Yay? :/");
                        parent.alertMessageVisible(true);
                        parent.removeEducation(self);
                    },
                    error: function (err) {
                        parent.alertMessageClass("alert-danger");
                        parent.alertMessageText("Failed deleting education! " + err.statusText);
                        parent.alertMessageVisible(true);

                        self.cssStyle("content-item-warning");
                    }
                });
            } else {
                parent.hasNew(false);
                parent.removeEducation(self);
            }
        };
    }

    // Education view model..hooray!
    function educationListViewModel() {
        var self = this;

        // User Id of the viewed user
        self.UserId = ko.observable(window.userId);

        // Flag to show the alert message
        self.alertMessageVisible = ko.observable(false);

        // Text to display in alert messages
        self.alertMessageText = ko.observable();

        // Determine the alert class/style
        self.alertMessageClass = ko.observable("alert-success");

        // Education collection
        self.education = ko.observableArray([]);

        // Education types collection
        self.educationTypes = ko.observableArray([]);

        // Flag that tells whether user has created a new education
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

        // Adds new education to collection
        self.addEducation = function () {
            self.hasNew(true);

            var data = {
                EducationId: 0,
                Schoolname: '',
                Course: '',
                State: '',
                City: '',
                Country: '',
                YearAttended: moment().format(defaultDateFormat),
                YearGraduated: moment().format(defaultDateFormat),
                UserId: self.UserId(),
                EducationType: null
            };

            self.education.push(new educationViewModel(data, true, self.educationTypes()));
            $('.datepicker').datepicker();
            window.scrollTo(0, (document.body.scrollHeight - 980));
        };

        // Removes education from collection
        self.removeEducation = function (model) {
            var index = self.education().indexOf(model);

            if (model.isNew()) {
                self.hasNew(false);
            }

            if (index >= 0) {
                self.education.splice(index, 1);
            }
        };
        
        // Adds education types on load to the education collection
        self.educationTypes.push(new educationTypeViewModel(1, 'Grade School'));
        self.educationTypes.push(new educationTypeViewModel(2, 'High School'));
        self.educationTypes.push(new educationTypeViewModel(3, 'College Education'));
        self.educationTypes.push(new educationTypeViewModel(4, 'Post Graduate'));

        // Adds education model on load to the education collection
        for (var i = 0; i < education.length; i++) {
            self.education.push(new educationViewModel(education[i], false, self.educationTypes()));
        }
    }

    // Extension for observable array to pre-select an item in the education type dropdown
    ko.observableArray.fn.find = function (prop, data) {
        var valueToMatch = data[prop];
        return ko.utils.arrayFirst(this(), function (item) {
            return item[prop]() === valueToMatch;
        });
    };

    ko.applyBindings(new educationListViewModel());
})