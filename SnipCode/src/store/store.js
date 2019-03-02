import Vuex from 'vuex'
import Vue from 'vue';
import axios from 'axios';

Vue.use(Vuex);
export default new Vuex.Store({
  state: {
    snippetContent: '',
    snippets: [],
    snippet: {},
    sharedSnippets: [],
    email: localStorage.getItem('email'),
    token: localStorage.getItem("token").length > 9 ? localStorage.getItem("token") : null
  },
  getters: {
    canEdit: state =>
    {
      return (state.email === state.snippet.creatorEmail || state.snippet.creatorEmail === undefined || state.snippet.creatorEmail === null);
    },
    email: state =>
    {
      return state.email == '' ? localStorage.getItem('email') : state.email;
    },
    snippet: state =>
    {
      return state.snippet;
    },
    snippets: state => 
    {
      return state.snippets
    },
    sharedSnippets: state =>
    {
      return state.sharedSnippets
    },
    token: state => {
      return state.token;
    }
  },
  mutations: {
    updateSnippetContent: (state, newContent) => {
      state.snippetContent = newContent;
    },
    email: (state, newEmail) =>
    {
      state.email = newEmail;
      localStorage.setItem('email', newEmail);
    },
    snippet: (state, snippet) =>
    {
      state.snippet = snippet;
    },
    updateAuthToken:(state, authData) => {
      if(authData !== undefined)
      {
        state.token = authData.accessToken;
        localStorage.setItem("token", state.token);
        localStorage.setItem("refresh-token", authData.refreshToken);
      }
      else
      {
        state.token = null;
        localStorage.setItem("token", null);

        localStorage.setItem("email", '');
        state.email = '';
      }
    },
    deleteSnippet:(state, hash) => 
    {
     state.snippets = state.snippets.filter(snippet => snippet.hash !== hash);
    },
    userSnippets:(state, snippets) =>
    {
      state.snippets = snippets
    },
    sharedSnippets:(state, snippets) =>
    {
      state.sharedSnippets = snippets;
    },
    removeSharedSnippet:(state, hash) => 
    {
      state.sharedSnippets = state.sharedSnippets.filter(snippet => snippet.hash !== hash);
    }
  },
  actions: {
    snippetByHash: async({commit}, hash) =>
    {
      const snippet = await axios.get("http://localhost:5000/api/snippet/" + hash);
      commit("snippet", snippet.data);
    },
    deleteSnippet: async ({commit}, hash) =>
    {
      axios.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage.getItem('token');
      axios.delete("http://localhost:5000/api/snippet/" + hash, {
        headers: {
          'Content-Type': 'application/json',
        }
      }).then(() => 
      {
        commit("deleteSnippet", hash);
      });
    },
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
    uploadSnippetContent: async(state, {content, name}) =>
    {
      const data = JSON.stringify({
        name: '',
        content: content,
        name: name,
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
        commit("email", credentials.email);
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
    changePassword: async({commit}, passwords) =>
    {
      axios.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage.getItem('token');
      const data = JSON.stringify({
        oldPassword: passwords.oldPassword,
        newPassword: passwords.newPassword
      });

      return axios.put("http://localhost:5000/api/Auth/change/password", data, {
          headers: {
            'Content-Type' : 'application/json'
          }
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
    },
    userSnippets: async({commit}) => 
    {
      
      axios.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage.getItem('token');
      return await axios.get("http://localhost:5000/api/snippet/user").then(response => {
         commit("userSnippets", response.data);
      });
    }, 
    sharedSnippets: async ({commit}) =>
    {
      axios.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage.getItem('token');
      return await axios.get("http://localhost:5000/api/snippet/user/shared").then(response => {
         commit("sharedSnippets", response.data);
      });
    },
    removeSharedSnippet: async ({commit}, hash) =>
    {
      const data = JSON.stringify({
        hash: hash,
        userEmail: localStorage.getItem("email")
      });
      axios.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage.getItem('token');
      axios.put("http://localhost:5000/api/snippet/removeShared", data,{
        headers: {
            'Content-Type' : 'application/json'
          }
      })
      .then(() => {
         commit("removeSharedSnippet", hash);
      });
    }
  }
})