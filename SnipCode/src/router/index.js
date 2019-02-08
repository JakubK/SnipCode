import Vue from 'vue'
import Router from 'vue-router'
import SnipCode from '@/components/SnipCode'
import Share from '@/components/Share'
import Snippet from '@/components/Snippet'
import AuthFrame from '@/components/Auth/Frame/AuthFrame'
import Register from '@/components/Auth/Register'
import Login from '@/components/Auth/Login'
import Profile from '@/components/Profile/Profile'


Vue.use(Router)

export default new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      component: SnipCode,
      children:[
        {
          path: '/',
          name: "Snippet",
          component: Snippet
        },
        {
          path: '/profile',
          name: 'Profile',
          component: Profile
        },
        {
          path: '/:hash',
          component: Snippet,
          props: true
        } 
      ]
    },
    {
      path: '/share',
      name: 'Share',
      component: Share
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