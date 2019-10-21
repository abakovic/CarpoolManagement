import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import VueMonthlyPicker from 'vue-monthly-picker'
import Multiselect from 'vue-multiselect'
import Datepicker from 'vuejs-datepicker'
import axios from 'axios'
import vueMoment from 'vue-moment'
import "@/assets/site.css"

Vue.config.productionTip = false

Vue.component('vue-monthly-picker', VueMonthlyPicker);
Vue.component('multiselect', Multiselect);
Vue.component('datepicker', Datepicker);
Vue.prototype.$axios = axios;
Vue.prototype.$vueMoment = vueMoment;
new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
