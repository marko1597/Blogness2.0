// ReSharper disable UseOfImplicitGlobalInFunctionScope
(function ($) {
    var address = null;
    var details = null;
    var socket = null;

    var init = function () {
        $(".alert > button > span:first-child").on("click", function (e) {
            e.preventDefault();
            return false;
        });

        address = window.configuration.blogSockets;
        details = {
            resource: address + "socket.io"
        };

        if (typeof io !== "undefined") {
            socket = io.connect(address, details);
        }
    };

    function notificationMessage(data) {
        this.Message = ko.observable(data.message);
        this.Type = ko.observable(data.type);
        this.ChannelId = ko.observable(data.channelId);
    }

    function socketNotificationMessage(message, fn) {
        this.ClientFuntion = ko.observable(fn);
        this.Message = ko.observable(message);
    }

    function notificationMessageType(type, display) {
        this.Type = ko.observable(type);
        this.Display = ko.observable(display);
    }

    function notificationsViewModel() {
        var self = this;

        self.showErrorMessage = ko.observable(false);

        self.showSuccessMessage = ko.observable(false);

        self.messageType = ko.observable();

        self.messageText = ko.observable();

        self.messageChannelId = ko.observable();

        self.growlMessage = ko.observable();

        self.growlTitle = ko.observable();

        self.notificationMessages = ko.observableArray([]);

        self.messageType.subscribe(function () {
            self.showSuccessMessage(false);
            self.showErrorMessage(false);
            self.messageText("");
            self.messageChannelId("");
        });

        self.successMessage = ko.computed(function () {
            if (self.messageType() == undefined) return "";
            return self.messageType().Type();
        });

        self.hideErrorMessage = function () {
            self.showErrorMessage(false);
        };

        self.hideSuccessMessage = function () {
            self.showSuccessMessage(false);
        };

        self.messageTypes = ko.observableArray([
            new notificationMessageType("CommentAdded", "Added new comment"),
            new notificationMessageType("CommentLikesUpdate", "Update comment likes"),
            new notificationMessageType("PostLikesUpdate", "Update post likes"),
            new notificationMessageType("Message", "New message")
        ]);

        self.sendMessage = function () {
            var message = new notificationMessage({
                type: self.messageType().Type(),
                message: self.messageText(),
                channelId: self.messageChannelId()
            });

            $.ajax("/notifications/sendmessage", {
                data: ko.toJSON(message),
                type: "post", contentType: "application/json",
                success: function () {
                    self.messageText("");
                    self.showSuccessMessage(true);
                },
                error: function () {
                    self.showErrorMessage(true);
                }
            });
        };

        var timerId = 0;
        timerId = setInterval(function () {
            if (socket) {
                socket.on('connect', function () {
                    self.growlTitle("Connected");
                    self.growlMessage("Successfully connected to node server!");
                    self.notificationMessages.push(new socketNotificationMessage(self.growlMessage(), self.growlTitle()));

                    $("#notification-message").removeClass("hidden");
                    setTimeout(function() { $("#notification-message").addClass("hidden"); }, 5000);

                    clearInterval(timerId);
                    return;
                });
            }
        }, 50);
    }

    ko.applyBindings(new notificationsViewModel());

    $(init);
})(jQuery);
// ReSharper restore UseOfImplicitGlobalInFunctionScope