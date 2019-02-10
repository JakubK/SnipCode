<template>
  <div>
    <h2>Sign Up</h2>
    <p>By creating SnipCode account you will get access to new features</p>
    <form class="register-form">
      <input v-model="email" type="text" placeholder="Email Address"/>
      <input v-model="password" type="password" placeholder="Password"/>
      <input v-model="confirmPassword" type="password" placeholder="Confirm Password"/>
      <button @click="TryToRegister">Send verification E-mail</button>
    </form>
  </div>
</template>
<style lang="scss" src="./Register.scss" scoped></style>
<script>
export default {
  name: "Register",
  data(){
    return{
      email: '',
      password: '',
      confirmPassword: ''
    }
  },
  methods:
  {
    TryToRegister(e)
    {
      e.preventDefault();
      this.$store.dispatch("createAccount", {email: this.email, password: this.password}).then(() => {
      })     
    }
  }
}
</script>