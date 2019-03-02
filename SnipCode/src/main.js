// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
import Store from './store/store'
import axios from 'axios'

Vue.config.productionTip = false

/* eslint-disable no-new */

Store.dispatch("validateToken").catch((error) => {
    if(error.response.status === 401)
    {
      Store.dispatch("forgetToken");
      //try to use refreshToken
      axios.post("http://localhost:5000/api/Auth/refresh",null,{
        headers:{
          'Content-Type' : 'application/json',
          'refreshToken' : localStorage.getItem('refresh-token')
        }
      }).then((response) => 
      {
        if(response)
        {
          commit("updateAuthToken", response.data);
        }
      }).catch(() =>
      {
        Store.dispatch("forgetToken");
        //next('auth/login');
      });
    }
  });

new Vue({
  el: '#app',
  store: Store,
  router,
  components: { App },
  template: '<App/>'
})