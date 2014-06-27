angular.module('ngSignalr', [])
.constant('$', $)
.factory('signalrHub', ['$', function ($) {
    return function (connectionName, hubName, listeners, methods) {
        var hub = {};
        hub.isConnected = false;
        hub.connection = $.hubConnection(connectionName, { useDefaultPath: false });
        hub.proxy = hub.connection.createHubProxy(hubName);
        hub.on = function (event, fn) {
            hub.proxy.on(event, fn);
        };
        hub.invoke = function (method, args) {
            hub.proxy.invoke.apply(hub.proxy, arguments);
        };

        if (listeners) {
            angular.forEach(listeners, function (fn, event) {
                hub.on(event, fn);
            });
        }
        if (methods) {
            angular.forEach(methods, function (method) {
                hub[method] = function () {
                    var args = $.makeArray(arguments);
                    args.unshift(method);
                    hub.invoke.apply(hub, args);
                };
            });
        }
        hub.connection.start();
        hub.isConnected = true;
        return hub;
    };
}]);