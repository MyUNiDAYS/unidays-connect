import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import authApp from "./app-auth";

import "./assets/scss/style.scss";

Vue.config.productionTip = false;
Vue.use(authApp);

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app");
