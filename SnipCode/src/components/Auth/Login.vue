<template>
  <div>
    <h2>Sign In</h2>
    <p>Log In to your SnipCode Account to manage your Snippets</p>
    <form class="login-form">
      <input v-model="email" type="text" placeholder="Email Address"/>
      <input v-model="password" type="password" placeholder="Password"/>
      <p class="error-text" v-show="errorText">{{ errorText }}</p>
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
      password: '',
      errorText: ''
    }
  },
  methods:
  {
    async TryToLogin(e)
    {
      e.preventDefault();
      if(this.email && this.password)
      {
        try
        {
          const response = await this.$store.dispatch("loginWithCredentials", {email: this.email, password: this.password});
          this.$router.push("/profile");
        }
        catch(error){
          this.errorText = "Bad credentials were given";
        }
      }
      else
      {
        this.errorText = "Please fill every field";
      }
    }
  }
}
</script>
