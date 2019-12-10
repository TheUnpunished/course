import 'babel-polyfill';
import Vue from 'vue';
import router from './router';
import store from './store';
import trendChart from 'vue-trend-chart'

const app = new Vue({
    router,
    store,
    el: "#app",
    render: h => h("router-view")
});
Vue.use(trendChart);
