angular.module('ngSignalr', [])
.constant('$', $)
.factory('signalrHub', ['$', function ($) {
    return function (hubName, listeners, methods) {
        var hub = this;
        hub.connection = $.hubConnection();
        hub.proxy = hub.connection.createHubProxy(hubName);
        hub.connection.start();
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
        return hub;
    };
}]);