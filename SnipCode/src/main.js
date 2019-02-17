// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
import Store from './store/store'

Vue.config.productionTip = false

/* eslint-disable no-new */

Store.dispatch("validateToken").catch(() => {
    Store.dispatch("forgetToken");
  });

new Vue({
  el: '#app',
  store: Store,
  router,
  components: { App },
  template: '<App/>'
})