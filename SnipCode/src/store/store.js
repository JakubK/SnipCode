import Vuex from 'vuex'
import Vue from 'vue';
import axios from 'axios';

Vue.use(Vuex);
export default new Vuex.Store({
  state:
  {
    snippetContent: ''
  },
  getters:
  {
    snippetByHash: state => hash =>
    {
      return axios.get("http://localhost:5000/api/snippet/" + hash);
    }
  },
  mutations:
  {
    updateSnippetContent: (state, newContent) =>
    {
      state.snippetContent = newContent;
    }
  },
  actions: 
  {
    updateSnippetContent: async({commit}, newContent) =>
    {
      await commit('updateSnippetContent', newContent);
    }
  }
})