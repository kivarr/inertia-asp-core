declare global {
    interface Window { MyWebApp: MyWebAppType; }
}

class MyWebAppType  {
    Home: any;
    newJsPage: () => void
}

window.MyWebApp = window.MyWebApp || new MyWebAppType;

type routes = {
    Home: object
}

type homeRoutes = {
    init: () => void
}

const Routes : routes = {
    Home: {
        init() {
            window.MyWebApp.newJsPage = function () : void {
                
            };
        }
    }
};

const Router = {
    exec: function (controller : string = "Home", action : string = undefined) {
        action = action === undefined ? "init" : action;

        // @ts-ignore
        if (controller !== "" && Routes[controller] && typeof Routes[controller][action] === "function") {
            // @ts-ignore
            Routes[controller][action]();
        }
    },

    init: function () {
        let body = document.body;
        let controller = body.getAttribute("data-controller");
        let action = body.getAttribute("data-action");

        Router.exec(controller);
        Router.exec(controller, action);

    }
};

//run this immediately
Router.init();

export {};