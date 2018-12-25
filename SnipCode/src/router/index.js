import Vue from 'vue'
import Router from 'vue-router'
import HelloWorld from '@/components/HelloWorld'
import SnippetEditor from '@/SnippetEditor'
import AuthFrame from '@/AuthFrame'
import Register from '@/Register'
import Login from '@/Login'


Vue.use(Router)

export default new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      name: 'SnippetEditor',
      component: SnippetEditor,
      children:[
        {
          path: '/',
          name: "HelloWorld",
          component: HelloWorld
        }
      ]
    },
    {
      path: '/auth',
      name: 'Auth',
      component: AuthFrame,
      children:[
        {
          path: 'register',
          name: "Register",
          component: Register
        },
        {
          path: 'login',
          name: "Login",
          component: Login
        }
      ]
    }
  ]
})