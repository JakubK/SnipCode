import Vue from 'vue'
import Router from 'vue-router'
import SnipCode from '@/components/SnipCode'
import Share from '@/components/Share'
import Snippet from '@/components/Snippet'
import SubmitSnippet from '@/components/SubmitSnippet'
import AuthFrame from '@/components/Auth/Frame/AuthFrame'
import Register from '@/components/Auth/Register'
import Login from '@/components/Auth/Login'
import Profile from '@/components/Profile/Profile'
import Auth from './auth'
import ChangePassword from '@/components/Profile/ChangePassword/ChangePassword'

Vue.use(Router)

export default new Router({
  mode: 'history',
  routes: [
    {
      path: '/share/:hash',
      name: 'Share',
      component: Share,
      props: true
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
    },
    {
      path: '/',
      component: SnipCode,
      children:[
        {
          path: '/',
          name: "Snippet",
          component: Snippet,
          children:[
            {
              path: '/submit',
              name: "SubmitSnippet",
              component: SubmitSnippet,
              props: true
            }
          ]
        },
        {
          path: '/profile',
          name: 'Profile',
          component: Profile,
          beforeEnter: Auth, 
          children:[
            {
              path:'/change/password',
              name: "ChangePassword",
              component: ChangePassword
            }
          ]
        },
        {
          path: '/:hash',
          name: 'HashSnippet',
          component: Snippet,
          props: true
        } 
      ]
    }
  ]
})