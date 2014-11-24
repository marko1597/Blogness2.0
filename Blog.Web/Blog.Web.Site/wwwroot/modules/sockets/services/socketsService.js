// ReSharper disable UseOfImplicitGlobalInFunctionScope
ngBlogSockets.factory('blogSocketsService', ["$rootScope", "$timeout", "$interval", "configProvider",
    function ($rootScope, $timeout, $interval, configProvider) {
        var address = configProvider.getSettings().BlogSockets === "" ?
            window.blogConfiguration.blogSockets :
            configProvider.getSettings().BlogSockets;

        var details = {
            resource: address + "socket.io"
        };

        var socket = {};
        if (typeof io !== "undefined") {
            socket = io.connect(address, details);
        }

        var broadcastMessage = function(topic, data) {
            var stop;
            stop = $interval(function () {
                if ($rootScope.$$listeners[topic] && $rootScope.$$listeners[topic].length > 0) {
                    $rootScope.$broadcast(topic, data);
                    $interval.cancel(stop);
                    stop = undefined;
                }
            }, 250);
        };

        var isBlogSocketsAvailable = window.blogConfiguration.blogSocketsAvailable;

        if (isBlogSocketsAvailable || isBlogSocketsAvailable === 'true') {
            var socketReady;

            socketReady = $interval(function() {
                if (socket && socket.on) {
                    $interval.cancel(socketReady);
                    socketReady = undefined;

                    socket.on('connect', function () {
                        $rootScope.$broadcast(configProvider.getSocketClientFunctions().wsConnect);
                    });

                    socket.on('echo', function (data) {
                        console.log(data);
                    });

                    socket.on(configProvider.getSocketClientFunctions().publishMessage, function (data) {
                        $timeout(function () {
                            $rootScope.$broadcast(configProvider.getSocketClientFunctions().publishMessage, data);
                        }, 250);
                    });

                    socket.on(configProvider.getSocketClientFunctions().getPostLikes, function (data) {
                        var topic = configProvider.getSocketClientFunctions().getPostLikes;
                        broadcastMessage(topic, data);
                    });

                    socket.on(configProvider.getSocketClientFunctions().viewCountUpdate, function (data) {
                        var topic = configProvider.getSocketClientFunctions().viewCountUpdate;
                        broadcastMessage(topic, data);
                    });

                    socket.on(configProvider.getSocketClientFunctions().getPostTopComments, function (data) {
                        var topic = configProvider.getSocketClientFunctions().getPostTopComments;
                        broadcastMessage(topic, data);
                    });

                    socket.on(configProvider.getSocketClientFunctions().postLikesUpdate, function (data) {
                        var topic = configProvider.getSocketClientFunctions().postLikesUpdate;
                        broadcastMessage(topic, data);
                    });

                    socket.on(configProvider.getSocketClientFunctions().commentLikesUpdate, function (data) {
                        var topic = configProvider.getSocketClientFunctions().commentLikesUpdate;
                        broadcastMessage(topic, data);
                    });

                    socket.on(configProvider.getSocketClientFunctions().commentAdded, function (data) {
                        var topic = configProvider.getSocketClientFunctions().commentAdded;
                        broadcastMessage(topic, data);
                    });

                    socket.on(configProvider.getSocketClientFunctions().sendChatMessage, function (data) {
                        var topic = configProvider.getSocketClientFunctions().sendChatMessage;
                        broadcastMessage(topic, data);
                    });
                }
            }, 250);
        }

        return {
            emit: function (eventName, data, callback) {
                if (socket.connected) {
                    if (typeof io !== "undefined") {
                        socket.emit(eventName, data, function () {
                            var args = arguments;
                            $rootScope.$apply(function () {
                                if (callback) {
                                    callback.apply(socket, args);
                                }
                            });
                            return true;
                        });
                    }
                }
                return false;
            }
        };
    }]);

// ReSharper restore UseOfImplicitGlobalInFunctionScope