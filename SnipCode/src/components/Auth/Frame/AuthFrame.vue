<template>
  <main>
    <div class="auth-frame">
      <div class="auth-buttons">
        <router-link class="auth-button" to="register" >Sign Up</router-link>
        <router-link class="auth-button" to="login">Sign In</router-link>
      </div>
      <router-view/>
    </div>
  </main>
</template>
<script>
export default {
  name: "AuthFrame",
  mounted()
  {
    if(this.$route.path === "/auth")
     this.$router.push("auth/register");
  }
}
</script>

<style src="./AuthFrame.scss" lang="scss" scoped>
  
</style>

