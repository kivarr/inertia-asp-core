"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
var __importStar = (this && this.__importStar) || function (mod) {
    if (mod && mod.__esModule) return mod;
    var result = {};
    if (mod != null) for (var k in mod) if (Object.hasOwnProperty.call(mod, k)) result[k] = mod[k];
    result["default"] = mod;
    return result;
};
Object.defineProperty(exports, "__esModule", { value: true });
var inertia_vue_1 = require("@inertiajs/inertia-vue");
var vue_1 = __importDefault(require("vue"));
// @ts-ignore
vue_1.default.use(inertia_vue_1.InertiaApp);
var app = document.getElementById('app');
var App = /** @class */ (function () {
    function App(selector) {
        new vue_1.default({
            render: function (createElement) { return createElement(inertia_vue_1.InertiaApp, {
                props: {
                    initialPage: JSON.parse(app.dataset.page),
                    resolveComponent: function (name) { return Promise.resolve().then(function () { return __importStar(require("@/Pages/" + name)); }).then(function (module) { return module.default; }); },
                }
            }); }
        }).$mount(app);
    }
    return App;
}());
exports.App = App;
exports.default = new App('#app');
