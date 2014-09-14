// ReSharper disable UseOfImplicitGlobalInFunctionScope
ngBlogSockets.factory('blogSocketsService', ["$rootScope", "configProvider", function ($rootScope, configProvider) {
    var address = configProvider.getSettings().BlogSockets;
    
    var details = {
        resource: address + "socket.io"
    };

    var socket = {};
    if (typeof io !== "undefined") {
        socket = io.connect(address, details);
    }

    socket.on('connect', function () {
        $rootScope.$broadcast(configProvider.getSocketClientFunctions().wsConnect);
    });

    socket.on('echo', function (data) {
        console.log(data);
    });

    socket.on(configProvider.getSocketClientFunctions().publishMessage, function (data) {
        $rootScope.$broadcast(configProvider.getSocketClientFunctions().publishMessage, data);
    });

    socket.on(configProvider.getSocketClientFunctions().postLikesUpdate, function (data) {
        $rootScope.$broadcast(configProvider.getSocketClientFunctions().postLikesUpdate, data);
    });

    socket.on(configProvider.getSocketClientFunctions().commentLikesUpdate, function (data) {
        $rootScope.$broadcast(configProvider.getSocketClientFunctions().commentLikesUpdate, data);
    });

    socket.on(configProvider.getSocketClientFunctions().commentAdded, function (data) {
        $rootScope.$broadcast(configProvider.getSocketClientFunctions().commentAdded, data);
    });

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