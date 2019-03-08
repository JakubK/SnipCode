<template>
  <div>
    <h2>Sign Up</h2>
    <p>By creating SnipCode account you will get access to new features</p>
          <p class="error-text" v-show="errorText">{{ errorText}}</p>
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
      confirmPassword: '',
      errorText: ''
    }
  },
  methods:
  {
    TryToRegister(e)
    {
      e.preventDefault();
      if(this.password && this.confirmPassword && this.email)
      {
        if(this.password === this.confirmPassword)
        {
          try
          {
            const response = this.$store.dispatch("createAccount", {email: this.email, password: this.password});
            this.$router.push('/auth/login');
          }
          catch(error)
          {
            this.errorText = "Provided email address is already registered";
          }
        }
        else
        {
          this.errorText = "Passwords don't match";
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