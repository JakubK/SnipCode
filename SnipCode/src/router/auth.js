import store from '../store/store';

export default  (to, from, next) => {
  if (store.getters.token) {
     store.dispatch("validateToken").then(response => 
      {
        if(response.status === 200)
        {
          next();
        }
        else
        {
          store.dispatch("forgetToken");
          next('/auth/register');
        }
      }).catch(() => 
      {
        store.dispatch("forgetToken");
        next('/auth/register');
      })
  } else {
    next('/auth/register');
  }
}