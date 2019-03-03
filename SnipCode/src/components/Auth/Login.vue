<template>
  <div>
    <h2>Sign In</h2>
    <p>Log In to your SnipCode Account to manage your Snippets</p>
    <form class="login-form">
      <input v-model="email" type="text" placeholder="Email Address"/>
      <input v-model="password" type="password" placeholder="Password"/>
      <button @click="TryToLogin">Sign in</button>
    </form>
  </div>
</template>
<style lang="scss" src="./Login.scss" scoped></style>
<script>
export default {
  name: "Login",
  data(){
    return{
      email: '',
      password: ''
    }
  },
  methods:
  {
    TryToLogin(e)
    {
      e.preventDefault();
      this.$store.dispatch("loginWithCredentials", {email: this.email, password: this.password}).then(() => {
        //token acquired
        this.$router.push("/profile");
      })
    }
  }
}
</script>
