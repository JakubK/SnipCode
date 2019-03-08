import store from '../store/store'
import axios from 'axios'

export default  (to, from, next) => {
  if (store.getters.token) {
     store.dispatch("validateToken").then(response => 
      {
        if(response.status === 200)
        {
          next();
        }
      }).catch((error) => 
      {
        if(error.response.status === 401)
        {
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
              if(response.status === 200)
              {
                store.commit("updateAuthToken", response.data);
                next();
              }
            }
            else
            {
              store.dispatch("forgetToken");
              next('/auth/login');
            }
          });
        }
      })
  }
}