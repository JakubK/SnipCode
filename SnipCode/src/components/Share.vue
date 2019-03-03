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
        <button @click="goBack()">Go back</button>
      </div>
    </div>
  </main>
</template>
<script>
import axios from 'axios'
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
    goBack()
    {
      this.$router.go(-1);
    },
    handleShareSnippet()
    {
      const data = JSON.stringify({
        hash: this.hash,
        userEmail: this.addEmail
      });
      axios.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage.getItem('token');
      axios.put("http://localhost:5000/api/snippet/share", data,{
        headers: {
            'Content-Type' : 'application/json'
          }
      })
    }
  }
}
</script>

<style src="./Share.scss" lang="scss" scoped>
  
</style>