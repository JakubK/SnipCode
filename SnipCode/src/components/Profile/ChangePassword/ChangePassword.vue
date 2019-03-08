<template>
  <div class="password-background" @click.self="handleClose()">
    <div class="password-panel">
      <h1>Changing Password</h1>
      <p>You're about to change your password</p>
      <form>
        <label>Old Password</label>
        <input v-model="oldPassword"  type="password"/>
        <br/>
        <label>New Password</label>
        <input v-model="newPassword"  type="password"/>
        <br/>
        <label>Repeat new Password</label>
        <input v-model="newPasswordRepeat"  type="password"/>
        <br/>
        <p class="error-text" v-show="errorText">{{ errorText }}</p>
        <button @click="handleSubmit" type="button">Change password</button>
      </form>
    </div>
  </div>
</template>
<script>
export default {
  name: 'ChangePassword',
  data()
  {
    return{
      oldPassword: '',
      newPassword: '',
      newPasswordRepeat: '',

      errorText: ''
    }
  },
  methods:
  {
    handleSubmit()
    {
       
       if(this.newPassword === this.newPasswordRepeat && this.oldPassword)
       {
        this.$store.dispatch("changePassword",{oldPassword: this.oldPassword,newPassword: this.newPassword}).then(result =>
        {
          this.$router.go(-1)
        });
       }
       else if(this.newPassword !== this.newPasswordRepeat)
       {
         this.errorText = "Passwords don't match";
       }
       else
       {
         this.errorText = "Please fill every field";
       }
    },
    handleClose()
    {
      this.$router.go(-1);
    }
  }
}
</script>
<style lang="scss" src='./ChangePassword.scss' scoped></style>