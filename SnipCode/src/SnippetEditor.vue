<template>
  <main>
    <Navbar/>
    <router-view/>
    <Footer/>
  </main>
</template>
<script>
import Navbar from './components/Navbar.vue'
import Footer from './components/Footer.vue'
export default {
  name: "SnippetEditor",
  components: {Navbar, Footer}
}
</script>

