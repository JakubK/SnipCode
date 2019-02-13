import store from '../store/store';

export default  (to, from, next) => {
  if (store.getters.token) {
    next();
  } else {
    next('/auth/register');
  }
}