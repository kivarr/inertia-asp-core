"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var MyWebAppType = /** @class */ (function () {
    function MyWebAppType() {
    }
    return MyWebAppType;
}());
window.MyWebApp = window.MyWebApp || new MyWebAppType;
var Routes = {
    Home: {
        init: function () {
            window.MyWebApp.newJsPage = function () {
            };
        }
    }
};
var Router = {
    exec: function (controller, action) {
        if (controller === void 0) { controller = "Home"; }
        if (action === void 0) { action = undefined; }
        action = action === undefined ? "init" : action;
        // @ts-ignore
        if (controller !== "" && Routes[controller] && typeof Routes[controller][action] === "function") {
            // @ts-ignore
            Routes[controller][action]();
        }
    },
    init: function () {
        var body = document.body;
        var controller = body.getAttribute("data-controller");
        var action = body.getAttribute("data-action");
        Router.exec(controller);
        Router.exec(controller, action);
    }
};
//run this immediately
Router.init();
