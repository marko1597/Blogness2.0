var server = require('http').createServer()
    , io = require('socket.io').listen(server)
    , redis = require('redis')
    , bloggityServer = {}
    , redisSubscriber = redis.createClient('6379', process.env.redisServer)
    , redisPublisher = redis.createClient('6379', process.env.redisServer)
	, connectedUsers = []
    , blogChannels = {
        viewPost: "post_",
        userChat: "userchat_",
        adminApp: "adminApp"
    }
    , clientFunctions = {
        publishMessage: "PublishMessage",
        getPostTopComments: "GetPostTopComments",
        viewCountUpdate: "ViewCountUpdate",
        getPostLikes: "GetPostLikes",
        commentAdded: "CommentAdded",
        commentLikesUpdate: "CommentLikesUpdate",
        postLikesUpdate: "PostLikesUpdate",
        userChatOnline: "UserChatOnline",
        userChatOffline: "UserChatOffline",
        sendChatMessage: "SendChatMessage",
        subscribeViewPost: "SubscribeViewPost",
        unsubscribeViewPost: "UnsubscribeViewPost",
        subscribeAdmin: "SubscribeAdmin"
    };

io.set('transports', ['polling']);

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
    
    GetPostTopComments: function (d) {
        if (d != null) {
            var data = {
                postId: d.PostId,
                comments: d.Comments
            };
            io.sockets.emit(clientFunctions.getPostTopComments, data);
            io.sockets.in(blogChannels.adminApp).emit(clientFunctions.getPostTopComments, data);
        }
    },
    
    GetPostLikes: function (d) {
        if (d != null) {
            var data = {
                postId: d.PostId,
                postLikes: d.PostLikes
            };
            io.sockets.emit(clientFunctions.getPostLikes, data);
            io.sockets.in(blogChannels.adminApp).emit(clientFunctions.getPostLikes, data);
        }
        
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
    
    ViewCountUpdate: function (d) {
        if (d != null) {
            var data = {
                postId: d.PostId,
                viewCounts: d.ViewCounts
            };
            io.sockets.emit(clientFunctions.viewCountUpdate, data);
            io.sockets.in(blogChannels.adminApp).emit(clientFunctions.viewCountUpdate, data);
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
    },
    
    SendChatMessage: function (d) {
        if (d != null && d.ChatMessage != null) {
            var chat = d.ChatMessage;
            var data = {
                FromUser: chat.FromUser,
                ToUser: chat.ToUser,
                Text: chat.Text,
                CreatedDate: chat.CreatedDate
            };
            io.sockets.in(blogChannels.userChat + d.RecipientUserId).emit(clientFunctions.sendChatMessage, data);
            io.sockets.in(blogChannels.adminApp).emit(clientFunctions.sendChatMessage, data);
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
    
    socket.on(clientFunctions.userChatOnline, function (data) {
		connectedUsers.push[data.userId];
        socket.join(blogChannels.userChat + data.userId);
        io.sockets.in(blogChannels.userChat + data.userId).send('>>> User ' + data.userId + ' is online');
    });
    
    socket.on(clientFunctions.userChatOffline, function (data) {
        var index = connectedUsers.indexOf(data.userId);
		connectedUsers.splice(index, 1);
		
		io.sockets.in(blogChannels.userChat + data.userId).send('>>> User ' + data.userId + ' is now offline');
        socket.leave(blogChannels.userChat + data.userId);
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
    }
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