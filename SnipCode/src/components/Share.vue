<template>
  <main>
    <div class="share-frame">
       <div>
        <h2>Share a Snippet</h2>
        <p>Send a Snippet directly to SnipCode user</p>
        <input v-model="addEmail" type="text" placeholder="Email address of SnipCode user"/>
        <button @click="handleShareSnippet()">Add SnipCode User</button>
        <p>Or copy Snippet link to your clipboard</p>
        <button @click="handleCopy()">Copy link to clipboard</button>
        <p id="url">{{ url }}</p>
      </div>
    </div>
  </main>
</template>
<script>
export default {
  props: ['hash'],
  name: "Share",
  data()
  {
    return{
      addEmail: ''
    }
  },
  computed:
  {
    url()
    {
      return 'http://localhost:8080/' + this.hash;
    }
  },
  methods:
  {
    handleCopy()
    {
      const el = document.createElement('textarea');
      el.value = this.url;
      el.setAttribute('readonly', '');
      el.style.position = 'absolute';
      el.style.left = '-9999px';
      document.body.appendChild(el);
      el.select();
      document.execCommand('copy');
      document.body.removeChild(el);
    },
    handleShareSnippet()
    {

    }
  }
}
</script>

<style src="./Share.scss" lang="scss" scoped>
  
</style>