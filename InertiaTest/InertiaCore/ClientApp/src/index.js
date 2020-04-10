"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
require("./styles/main.css");
require("./asp-router");
var portal_vue_1 = __importDefault(require("portal-vue"));
var vue_1 = __importDefault(require("vue"));
var vue_meta_1 = __importDefault(require("vue-meta"));
var inertia_vue_1 = require("@inertiajs/inertia-vue");
// @ts-ignore
vue_1.default.use(inertia_vue_1.InertiaApp);
vue_1.default.use(portal_vue_1.default);
vue_1.default.use(vue_meta_1.default);
var app = document.getElementById('app');
var App = /** @class */ (function () {
    function App() {
        new vue_1.default({
            render: function (createElement) { return createElement(inertia_vue_1.InertiaApp, {
                props: {
                    // @ts-ignore
                    initialPage: JSON.parse(app.dataset.page),
                    resolveComponent: function (name) { return require("./Pages/" + name).default; }
                }
            }); }
        }).$mount(app);
    }
    return App;
}());
exports.App = App;
new App();
