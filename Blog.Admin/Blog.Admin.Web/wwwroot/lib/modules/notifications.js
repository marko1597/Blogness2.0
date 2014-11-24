// ReSharper disable UseOfImplicitGlobalInFunctionScope
(function ($) {
    "use strict";

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
    function socketNotificationMessage(fn, message, data) {
        this.ClientFunction = ko.observable(fn);
        this.Message = ko.observable(message);
        this.Data = ko.observable(data);
    }

    // Message types displayed in the dropdown boxes
    function notificationMessageType(type, display) {
        this.Type = ko.observable(type);
        this.Display = ko.observable(display);
    }

    // Notifications view model..hooray!
    function notificationsViewModel() {
        var self = this;

        // Flag to check if connected to node js server
        self.isConnected = ko.observable(false);
        
        // Flag to show the alert message
        self.alertMessageVisible = ko.observable(false);

        // Text to display in alert messages
        self.alertMessageText = ko.observable();

        // Determine the alert class/style
        self.alertMessageClass = ko.observable("alert-success");

        // Message type object (dropdown boxes)
        self.messageType = ko.observable();

        // Message text sent to backend for publishing
        self.messageText = ko.observable();

        // Channel id to be sent to backend for publishing (this stands for the postId)
        self.messageChannelId = ko.observable();
        
        // Messages received from socket server
        self.notificationMessages = ko.observableArray([]);

        // Hides alert messages whenver the alertMessageVisible value changes
        self.alertMessageVisible.subscribe(function() {
            setTimeout(function() {
                self.alertMessageVisible(false);
            }, 5000);
        });

        // Hides the alert message and clears the selection in message types on change of 
        // message type selection in the dropdown boxes
        self.messageType.subscribe(function () {
            self.alertMessageVisible(false);
            self.messageText("");
            self.messageChannelId("");
        });
        
        // Hides the alert message manually
        self.hideAlertMessage = function () {
            self.alertMessageVisible(false);
        };

        // Message types shown in dropdown box Type
        self.messageTypes = ko.observableArray([
            new notificationMessageType("CommentAdded", "Added new comment"),
            new notificationMessageType("CommentLikesUpdate", "Update comment likes"),
            new notificationMessageType("PostLikesUpdate", "Update post likes"),
            new notificationMessageType("Message", "New message")
        ]);

        // Clear the notification messages displayed
        self.clearNotificationMessages = function() {
            self.notificationMessages([]);
        };

        // Send message to backend for publishing
        self.sendMessage = function () {
            if (!self.isConnected()) {
                self.alertMessageClass("alert-warning");
                self.alertMessageText("You're not yet connected bruh! Chill down and wait fer it..");
                self.alertMessageVisible(true);
                return;
            }

            var message = new notificationMessage({
                type: self.messageType().Type(),
                message: self.messageText(),
                channelId: self.messageChannelId()
            });

            $.ajax("/notifications/sendmessage", {
                data: ko.toJSON(message),
                type: "post", contentType: "application/json",
                success: function () {
                    self.alertMessageClass("alert-success");
                    self.alertMessageText("Successfully published " + self.messageType().Display() + " to client! Yay!");
                    self.alertMessageVisible(true);
                },
                error: function () {
                    self.alertMessageClass("alert-danger");
                    self.alertMessageText("Failed publishing " + self.messageType().Display() + " to client!");
                    self.alertMessageVisible(true);
                }
            });
        };

        // Poll to connect to socket server and when connected, bind objects
        var timerId = 0;
        timerId = setInterval(function () {
            if (typeof io === "undefined") {
                clearInterval(timerId);
                return;
            }

            if (socket) {
                socket.on('connect', function () {
                    self.isConnected(true);
                    self.alertMessageClass("alert-success");
                    self.alertMessageText("Successfully connected to node js! Do a chicken dance to celebrate!");
                    self.alertMessageVisible(true);
                    
                    // Subscribe to admin in socket.io
                    socket.emit('SubscribeAdmin', {});

                    clearInterval(timerId);
                    return;
                });

                bindEventsFromSocket(self);
            }
        }, 500);
    }

    // Called when connected to socket and then bind events on socket emit to update view model
    function bindEventsFromSocket(viewModel) {
        socket.on(clientFunctions.commentAdded, function (message) {
            viewModel.notificationMessages.push(new socketNotificationMessage(clientFunctions.commentAdded, "", message));
        });

        socket.on(clientFunctions.commentLikesUpdate, function (message) {
            viewModel.notificationMessages.push(new socketNotificationMessage(clientFunctions.commentLikesUpdate, "", message));
        });

        socket.on(clientFunctions.postLikesUpdate, function (message) {
            viewModel.notificationMessages.push(new socketNotificationMessage(clientFunctions.postLikesUpdate, "", message));
        });
    }

    ko.applyBindings(new notificationsViewModel());

    $(init);
})(jQuery);
// ReSharper restore UseOfImplicitGlobalInFunctionScope