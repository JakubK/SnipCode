<template>
  <div class="password-background">
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
      newPasswordRepeat: ''
    }
  },
  methods:
  {
    handleSubmit()
    {
       if(this.newPassword === this.newPasswordRepeat)
       {
        this.$store.dispatch("changePassword",{oldPassword: this.oldPassword,newPassword: this.newPassword}).then(result =>
        {
          this.$router.go(-1)
        });
       }
    }
  }
}
</script>
<style lang="scss" src='./ChangePassword.scss' scoped></style>