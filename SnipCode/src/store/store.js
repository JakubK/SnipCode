import Vuex from 'vuex'
import Vue from 'vue';

Vue.use(Vuex);
export default new Vuex.Store({
  state:
  {
    snippetContent: ''
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