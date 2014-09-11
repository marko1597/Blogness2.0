ngSockets.factory('socketsService', ["$rootScope", "configProvider", function ($rootScope, configProvider) {
    var address = configProvider.getSettings().BlogSockets == "" ?
            window.blogConfiguration.blogSockets :
            configProvider.getSettings().BlogSockets;
    
    var details = {
        resource: address + "socket.io"
    };

    // ReSharper disable UseOfImplicitGlobalInFunctionScope
    var socket = {};
    if (typeof io !== "undefined") {
        socket = io.connect(address, details);
    }

    return {
        on: function (eventName, callback) {
            if (typeof io !== "undefined") {
                socket.on(eventName, function () {
                    var args = arguments;
                    $rootScope.$apply(function () {
                        callback.apply(socket, args);
                    });
                });
            }

        },
        emit: function (eventName, data, callback) {
            if (typeof io !== "undefined") {
                socket.emit(eventName, data, function () {
                    var args = arguments;
                    $rootScope.$apply(function () {
                        if (callback) {
                            callback.apply(socket, args);
                        }
                    });
                });
            }

        }
    };
    // ReSharper restore UseOfImplicitGlobalInFunctionScope
}]);