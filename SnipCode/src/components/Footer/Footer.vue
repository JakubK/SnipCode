<template>
  <footer>
    <div class="project-group">
      <button>
        <a target="_blank" href="https://github.com/JakubK/SnipCode">Github</a>
      </button>
    </div>
    <div class="project-text">
      Made with &lt;3 by Jakub and Deadline 
    </div>
    <button @click="handleProfileAccess()">
      Account
    </button>
  </footer>
</template>
<script>
export default {
  name : "Footer",
  methods:
  {
    handleProfileAccess()
    {
      if(this.token !== undefined && this.token !== null)
      {
          this.$router.push("/profile");
      }
      else
      {
          this.$router.push("/auth/login");
      }
    }
  },
  computed:
  {
    token()
    {
      return this.$store.getters.token;
    }
  }
}
</script>

<style lang="scss" src="./Footer.scss" scoped/>