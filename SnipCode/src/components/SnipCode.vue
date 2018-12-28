<template>
  <main>
    <Navbar/>
    <router-view/>
    <Footer/>
  </main>
</template>
<script>

import Navbar from './Navbar/Navbar.vue'
import Footer from './Footer/Footer.vue'
export default {
  name: "SnippetEditor",
  components: {Navbar, Footer}
}
</script>

