import Vue from 'vue'
import App from './App.vue'
import BootstrapVue from 'bootstrap-vue/dist/bootstrap-vue.esm';
import 'bootstrap-vue/dist/bootstrap-vue.css';
import 'bootstrap/dist/css/bootstrap.css';
import axios from 'axios';

axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';axios.defaults.headers.common['Content-Type'] = 'application/x-www-form-urlencoded'
axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
axios.defaults.headers.common['Access-Control-Allow-Origin'] = 'GET';

Vue.use(BootstrapVue);
Vue.config.productionTip = false

new Vue({
  render: h => h(App),
}).$mount('#app')
