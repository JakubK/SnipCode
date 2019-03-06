import Vue from 'vue'
import Router from 'vue-router'
import Auth from './auth'

Vue.use(Router)

export default new Router({
  mode: 'history',
  routes: [
    {
      path: '/share/:hash',
      name: 'Share',
      component: () => import('@/components/Share'),
      props: true
    },
    {
      path: '/auth',
      name: 'Auth',
      component: () => import('@/components/Auth/Frame/AuthFrame'),
      children:[
        {
          path: 'register',
          name: "Register",
          component: () => import('@/components/Auth/Register')
        },
        {
          path: 'login',
          name: "Login",
          component: () => import('@/components/Auth/Login')
        }
      ]
    },
    {
      path: '/',
      component: () => import('@/components/SnipCode'),
      children:[
        {
          path: '/',
          name: "Snippet",
          component: () => import('@/components/Snippet'),
          children:[
            {
              path: '/submit',
              name: "SubmitSnippet",
              component: () => import('@/components/SubmitSnippet'),
              props: true
            }
          ]
        },
        {
          path: '/profile',
          name: 'Profile',
          component: () => import('@/components/Profile/Profile'),
          beforeEnter: Auth, 
          children:[
            {
              path:'/change/password',
              name: "ChangePassword",
              component: () => import('@/components/Profile/ChangePassword/ChangePassword')
            }
          ]
        },
        {
          path: '/:hash',
          name: 'HashSnippet',
          component: () => import('@/components/Snippet'),
          props: true
        } 
      ]
    }
  ]
})