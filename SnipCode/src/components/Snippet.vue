<template>
  <main>
    <div class="snippet-panel">
      <span>Snippet Name</span>
      <button @click="shareSnippet()">Share Snippet... / Save</button>
    </div>
    <textarea v-model="content"></textarea>
  </main>
</template>
<script>
export default {
  name: "Snippet",
  data()
  {
    return{
      content: ""
    }
  },
  methods:
  {
    shareSnippet()
    {
      this.$store.dispatch("updateSnippetContent",this.content);
    }
  },
  mounted()
  {
    this.content = this.$store.state.snippetContent;
  }
}
</script>

<style src="./Snippet.scss" lang="scss" scoped>

</style>