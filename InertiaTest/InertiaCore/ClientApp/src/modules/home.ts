import { InertiaApp } from '@inertiajs/inertia-vue'
import Vue from "vue";

// @ts-ignore
Vue.use(InertiaApp);

const app = document.getElementById('app');

export class App {
    constructor(selector: string){
        new Vue({
            render: (createElement) => createElement(InertiaApp, {
                props: {
                    initialPage: JSON.parse(app.dataset.page),
                    resolveComponent: name => import(`@/Pages/${name}`).then(module => module.default),
                }
            })
        }).$mount(app)
    }
}

export default new App('#app');