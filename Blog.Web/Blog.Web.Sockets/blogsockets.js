var server = require('http').createServer()
    , io = require('socket.io').listen(server)
    , redis = require('redis')
    , bloggityServer = {}
    , redisSubscriber = redis.createClient('6379', process.env.redisServer)
    , redisPublisher = redis.createClient('6379', process.env.redisServer)
    , blogChannels = {
            viewPost: "post_",
            userLoggedIn: "user_",
            adminApp: "adminApp"
        }
    , clientFunctions = {
            publishMessage: "PublishMessage",
            commentAdded: "CommentAdded",
            commentLikesUpdate: "CommentLikesUpdate",
            postLikesUpdate: "PostLikesUpdate",
            subscribeViewPost: "SubscribeViewPost",
            unsubscribeViewPost: "UnsubscribeViewPost",
            subscribeAdmin: "SubscribeAdmin"
        };

var serverFunctions = {
    init: function (data) {
        if (data.fn !== undefined) {
            this[data.fn](data);
        }
    },
    
    PublishMessage: function (d) {
        io.sockets.emit(clientFunctions.publishMessage, d.data);
        io.sockets.in(blogChannels.adminApp).emit(clientFunctions.publishMessage, d.data);
    },
    
    PostLikesUpdate: function (d) {
        if (d != null) {
            var data = {
                postId: d.PostId,
                postLikes: d.PostLikes
            };
            io.sockets.emit(clientFunctions.postLikesUpdate, data);
            io.sockets.in(blogChannels.adminApp).emit(clientFunctions.postLikesUpdate, data);
        }
    },
    
    CommentLikesUpdate: function (d) {
        if (d != null) {
            var data = {
                postId: d.PostId,
                commentId: d.CommentId,
                commentLikes: d.CommentLikes
            };
            io.sockets.emit(clientFunctions.commentLikesUpdate, data);
            io.sockets.in(blogChannels.adminApp).emit(clientFunctions.commentLikesUpdate, data);
        }
    },
    
    CommentAdded: function (d) {
        if (d != null) {
            var data = {
                postId: d.PostId,
                commentId: d.CommentId,
                comment: d.Comment
            };
            io.sockets.in(blogChannels.viewPost + data.postId).emit(clientFunctions.commentAdded, data);
            io.sockets.in(blogChannels.adminApp).emit(clientFunctions.commentAdded, data);
        }
    }
};

server.listen(process.env.PORT || 4415);

io.sockets.on('connection', function (socket) {
    socket.on('echo', function (data) {
        socket.emit('echo', data);
    });
    
    socket.on(clientFunctions.subscribeAdmin, function () {
        socket.join(blogChannels.adminApp);
        io.sockets.in(blogChannels.adminApp).send('>>> Subscribing admin app');
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