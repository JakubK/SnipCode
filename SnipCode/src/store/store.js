import Vuex from 'vuex'
import Vue from 'vue';
import axios from 'axios';

Vue.use(Vuex);
export default new Vuex.Store({
  state: {
    snippetContent: '',
    email: localStorage.getItem("email"),
    token: localStorage.getItem("token").length > 9 ? localStorage.getItem("token") : null
  },
  getters: {
    token: state => {
      return state.token;
    },
    snippetByHash: state => hash => {
      return axios.get("http://localhost:5000/api/snippet/" + hash);
    },
    userSnippets: state => 
    {
      axios.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage.getItem('token');
      return axios.get("http://localhost:5000/api/snippet/user");
    }
    
  },
  mutations: {
    updateSnippetContent: (state, newContent) => {
      state.snippetContent = newContent;
    },
    updateAuthToken:(state, authData) => {
      if(authData !== undefined)
      {
        state.token = authData.accessToken;
        localStorage.setItem("token", state.token);
        state.email = authData.email;
      }
      else
      {
        state.token = null;
        state.email = null;
        localStorage.setItem("token", null);
      }
    }
  },
  actions: {
    updateSnippetContent: async ({
      commit
    }, {hash, newContent}) => {
      const data = JSON.stringify({
        hash: hash,
        newContent: newContent
      });

      axios.put("http://localhost:5000/api/snippet/", data, {
        headers: {
          'Content-Type': 'application/json',
        }
      });
      await commit('updateSnippetContent', newContent);
    },
    uploadSnippetContent: async(state, newContent) =>
    {
      const data = JSON.stringify({
        name: '',
        content: newContent,
        creatorEmail: localStorage.getItem('email')
      });
      
      return axios.post("http://localhost:5000/api/snippet/", data, {
        headers: {
          'Content-Type': 'application/json',
        }
      });    
    },
    loginWithCredentials: async({commit}, credentials ) =>
    {
      const data = JSON.stringify({
        email: credentials.email,
        password: credentials.password
      });

         return await axios.post("http://localhost:5000/api/Auth/login", data, {
          headers: {
            'Content-Type' : 'application/json'
          }
        }).then((response) => 
        {
          localStorage.setItem('email', credentials.email);
           commit("updateAuthToken", response.data);
        }).catch((error) => 
        {
          console.log("bad credentials");
        });
    },
    createAccount: async({commit}, credentials) =>
    {
      const data = JSON.stringify({
        email: credentials.email,
        password: credentials.password
      });

        return axios.post("http://localhost:5000/api/Auth/register", data, {
          headers: {
            'Content-Type' : 'application/json'
          }
        }).then((response) => 
        {
        }).catch((error) => 
        {
          console.log("something went wrong");
        });
    },
    forgetToken: async({commit}) => 
    {
      await commit("updateAuthToken");
    },
    validateToken: async({commit}) =>
    {
      axios.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage.getItem('token');
      return await axios.get("http://localhost:5000/api/auth/validate");
    }
  }
})