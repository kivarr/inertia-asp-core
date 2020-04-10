import './styles/main.css';

import './asp-router';

import PortalVue from 'portal-vue';
import Vue from 'vue';
import VueMeta from "vue-meta";
import {InertiaApp} from "@inertiajs/inertia-vue";

// @ts-ignore
Vue.use(InertiaApp);
Vue.use(PortalVue);
Vue.use(VueMeta);

const app = document.getElementById('app');

export class App {
    constructor(){
        new Vue({
            render: (createElement) => {
                console.log(app);
                return createElement(InertiaApp, {
                    props: {
                        // @ts-ignore
                        initialPage: JSON.parse(app.dataset.page),
                        resolveComponent: (name: string) => require(`./Pages/${name}`).default
                    }
                });
            }
        }).$mount(app)
    }
}

new App();