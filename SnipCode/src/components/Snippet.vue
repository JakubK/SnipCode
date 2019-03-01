<template>
  <main>
    <div class="snippet-submit">
      <router-view></router-view>
    </div>
    <div class="snippet-panel">
      <span>{{ snippet.name }}</span>
      <button v-if="canEdit" class="btn-share" @click="shareSnippet()">
        <svg aria-hidden="true" data-prefix="fas" data-icon="share" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512" ><path fill="currentColor" d="M503.691 189.836L327.687 37.851C312.281 24.546 288 35.347 288 56.015v80.053C127.371 137.907 0 170.1 0 322.326c0 61.441 39.581 122.309 83.333 154.132 13.653 9.931 33.111-2.533 28.077-18.631C66.066 312.814 132.917 274.316 288 272.085V360c0 20.7 24.3 31.453 39.687 18.164l176.004-152c11.071-9.562 11.086-26.753 0-36.328z"></path></svg>
        Save snippet
      </button>
    </div>
    <textarea :readonly="readonly" v-model="snippet.content"></textarea>
  </main>
</template>
<script>
export default {
  props: ['hash'],
  name: "Snippet",
  data()
  {
    return{
      content: '',
      readonly: false
    };
  },
  methods:
  {
    shareSnippet()
    {
      if(this.hash)
        this.$store.dispatch("updateSnippetContent", {hash: this.hash,newContent: this.snippet.content});
      else
      {
        if(this.$store.getters.token)
        {
          this.$router.push({name: 'SubmitSnippet', params: {content: this.snippet.content}});
        }
        else
        {
          this.$store.dispatch("uploadSnippetContent",{content: this.snippet.content}).then(result =>
          {
            console.log(this.content);
            this.$router.push(result.data.hash);
          });
        }
      }
    }
  },
  computed:
  {
    canEdit()
    {
      return this.$store.getters.canEdit;
    },
    snippet()
    {
      return this.$store.getters.snippet;
    }
  },
  mounted()
  {
    if(!this.hash)
    {
      this.content = this.$store.state.snippetContent;
    }
    else
    {
      this.$store.dispatch("snippetByHash", this.hash);
    }
  }
}
</script>

<style src="./Snippet.scss" lang="scss" scoped>

</style>