// ReSharper disable UseOfImplicitGlobalInFunctionScope
(function ($) {
    var address = null;

    var details = null;

    var socket = null;

    var clientFunctions = {
        publishMessage: "PublishMessage",
        commentAdded: "CommentAdded",
        commentLikesUpdate: "CommentLikesUpdate",
        postLikesUpdate: "PostLikesUpdate",
        subscribeViewPost: "SubscribeViewPost",
        unsubscribeViewPost: "UnsubscribeViewPost",
        subscribeAdmin: "SubscribeAdmin"
    };

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

    // Message sent to server for publishing
    function notificationMessage(data) {
        this.Message = ko.observable(data.message);
        this.Type = ko.observable(data.type);
        this.ChannelId = ko.observable(data.channelId);
    }

    // Message received from socket server
    function socketNotificationMessage(fn, message) {
        this.ClientFuntion = ko.observable(fn);
        this.Message = ko.observable(message);
    }

    // Message types displayed in the dropdown boxes
    function notificationMessageType(type, display) {
        this.Type = ko.observable(type);
        this.Display = ko.observable(display);
    }

    // Notifications view model..hooray!
    function notificationsViewModel() {
        var self = this;

        // Flag to show the error message
        self.showErrorMessage = ko.observable(false);

        // Flag to show the success message
        self.showSuccessMessage = ko.observable(false);

        // Message type object (dropdown boxes)
        self.messageType = ko.observable();

        // Message text sent to backend for publishing
        self.messageText = ko.observable();

        // Channel id to be sent to backend for publishing (this stands for the postId)
        self.messageChannelId = ko.observable();

        // Growl notification message
        self.growlMessage = ko.observable();

        // Growl notification title
        self.growlTitle = ko.observable();

        // Messages received from socket server
        self.notificationMessages = ko.observableArray([]);

        // Hides the success and error messages and clears the selection in message types
        self.messageType.subscribe(function () {
            self.showSuccessMessage(false);
            self.showErrorMessage(false);
            self.messageText("");
            self.messageChannelId("");
        });

        // Determine the message type selected to show in the success message
        self.successMessage = ko.computed(function () {
            if (self.messageType() == undefined) return "";
            return self.messageType().Type();
        });

        // Hides the error message
        self.hideErrorMessage = function () {
            self.showErrorMessage(false);
        };

        // Hides the success message
        self.hideSuccessMessage = function () {
            self.showSuccessMessage(false);
        };

        // Message types shown in dropdown box Type
        self.messageTypes = ko.observableArray([
            new notificationMessageType("CommentAdded", "Added new comment"),
            new notificationMessageType("CommentLikesUpdate", "Update comment likes"),
            new notificationMessageType("PostLikesUpdate", "Update post likes"),
            new notificationMessageType("Message", "New message")
        ]);

        // Send message to backend for publishing
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

        // Poll to connect to socket server and when connected, bind objects
        var timerId = 0;
        timerId = setInterval(function () {
            if (socket) {
                socket.on('connect', function () {
                    // Show that growl yo!
                    self.growlTitle("Connected");
                    self.growlMessage("Successfully connected to node server!");
                    self.notificationMessages.push(new socketNotificationMessage(self.growlTitle(), self.growlMessage()));

                    // Subscribe to admin in socket.io
                    socket.emit('SubscribeAdmin', {});

                    // Ugh..use some js/css3 animation shit here later!
                    $("#notification-message").removeClass("hidden");
                    setTimeout(function() { $("#notification-message").addClass("hidden"); }, 5000);

                    clearInterval(timerId);
                    return;
                });

                bindEventsFromSocket(self);
            }
        }, 200);
    }

    // Called when connected to socket and then bind events on socket emit to update view model
    function bindEventsFromSocket(viewModel) {
        socket.on(clientFunctions.commentAdded, function (message) {
            viewModel.notificationMessages.push(new socketNotificationMessage(clientFunctions.commentAdded, JSON.stringify(message)));
        });

        socket.on(clientFunctions.commentLikesUpdate, function (message) {
            viewModel.notificationMessages.push(new socketNotificationMessage(clientFunctions.commentLikesUpdate, JSON.stringify(message)));
        });

        socket.on(clientFunctions.postLikesUpdate, function (message) {
            viewModel.notificationMessages.push(new socketNotificationMessage(clientFunctions.postLikesUpdate, JSON.stringify(message)));
        });
    }

    ko.applyBindings(new notificationsViewModel());

    $(init);
})(jQuery);
// ReSharper restore UseOfImplicitGlobalInFunctionScope