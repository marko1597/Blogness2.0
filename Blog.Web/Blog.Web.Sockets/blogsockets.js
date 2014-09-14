﻿var server = require('http').createServer();
var io = require('socket.io').listen(server);
var redis = require('redis');
var bloggityServer = {};
var redisSubscriber = redis.createClient('6379', process.env.redisServer);
var redisPublisher = redis.createClient('6379', process.env.redisServer);

var blogChannels = {
    viewPost: "post_",
    userLoggedIn: "user_"
};

var clientFunctions = {
    publishMessage: "PublishMessage",
    commentAdded: "CommentAdded",
    commentLikesUpdate: "CommentLikesUpdate",
    postLikesUpdate: "PostLikesUpdate",
    subscribeViewPost: "SubscribeViewPost",
    unsubscribeViewPost: "UnsubscribeViewPost"
};

var serverFunctions = {
    init: function (data) {
        if (data.fn !== undefined) {
            this[data.fn](data);
        }
    },
    
    PublishMessage: function (d) {
        io.sockets.in(blogChannels.viewPost + d.data.postId).emit(clientFunctions.publishMessage, d.data);
    },
    
    PostLikesUpdate: function (d) {
        if (d != null) {
            var data = {
                postId: d.PostId,
                postLikes: d.PostLikes
            };
            io.sockets.in(blogChannels.viewPost + data.postId).emit(clientFunctions.postLikesUpdate, data);
        }
    },
    
    CommentLikesUpdate: function (d) {
        if (d != null) {
            var data = {
                postId: d.PostId,
                commentId: d.CommentId,
                commentLikes: d.CommentLikes
            };
            io.sockets.in(blogChannels.viewPost + data.postId).emit(clientFunctions.commentLikesUpdate, data);
        }
    },
    
    CommentAdded: function (d) {
        if (d != null) {
            var data = {
                postId: d.PostId,
                comment: d.Comment
            };
            io.sockets.in(blogChannels.viewPost + data.postId).emit(clientFunctions.commentAdded, data);
        }
    }
};

server.listen(process.env.PORT || 4415);

io.sockets.on('connection', function (socket) {
    socket.on('echo', function (data) {
        socket.emit('echo', data);
    });
    
    socket.on(clientFunctions.subscribeViewPost, function (data) {
        socket.join(blogChannels.viewPost + data.postId);
        io.sockets.in(blogChannels.viewPost + data.postId).send('>>> Subscribing to post:' + data.postId);
    });
    
    socket.on(clientFunctions.unsubscribeViewPost, function (data) {
        io.sockets.in(blogChannels.viewPost + data.postId).send('>>> Un-Subscribing to post:' + data.postId);
        socket.leave(blogChannels.viewPost + data.postId);
    });
    
    socket.on(clientFunctions.publishMessage, function (message) {
        redisPublisher.publish('bloggity', message);
    });
});

redisSubscriber.subscribe('bloggity');
redisSubscriber.on("message", function (channel, message) {
    try {
        var data = JSON.parse(message);
        serverFunctions.init(data);
    } catch (err) {
        if (message.fn !== undefined) {
            serverFunctions.init(message);
        }
    }    ;
});

bloggityServer.displayInfo = function (socket) {
    socket.send("Your Transport: " + io.transports[socket.id].name);
};

bloggityServer.log2client = function (socket, msg) {
    socket.send("Transport: " + io.transports[socket.id].name + "// Redis Data:" + JSON.stringify(msg));
};

bloggityServer.listallrooms = function (socket) {
    socket.send("All Events: " + JSON.stringify(io.sockets.manager.rooms));
};

bloggityServer.listclientrooms = function (socket) {
    socket.send("All Client Events: " + JSON.stringify(io.sockets.manager.roomClients[socket.id]));
};