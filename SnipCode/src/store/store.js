import Vuex from 'vuex'
import Vue from 'vue';
import axios from 'axios';

Vue.use(Vuex);
export default new Vuex.Store({
  state: {
    snippetContent: '',
    token: localStorage.getItem("token")
  },
  getters: {
    token: state => {
      return state.token;
    },
    snippetByHash: state => hash => {
      return axios.get("http://localhost:5000/api/snippet/" + hash);
    }
  },
  mutations: {
    updateSnippetContent: (state, newContent) => {
      state.snippetContent = newContent;
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
    uploadSnippetContent: async({commit}, newContent) =>
    {
      const data = JSON.stringify({
        name: '',
        content: newContent,
        creatorEmail: ''
      });
      return axios.post("http://localhost:5000/api/snippet/", data, {
        headers: {
          'Content-Type': 'application/json',
        }
      });    
    }
  }
})
