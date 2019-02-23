<template>
  <main>
    <div class="profile-details">
      <svg class="icon-account" aria-hidden="true" focusable="false" data-prefix="fas" data-icon="user-circle" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 496 512"><path fill="currentColor" d="M248 8C111 8 0 119 0 256s111 248 248 248 248-111 248-248S385 8 248 8zm0 96c48.6 0 88 39.4 88 88s-39.4 88-88 88-88-39.4-88-88 39.4-88 88-88zm0 344c-58.7 0-111.3-26.6-146.5-68.2 18.8-35.4 55.6-59.8 98.5-59.8 2.4 0 4.8.4 7.1 1.1 13 4.2 26.6 6.9 40.9 6.9 14.3 0 28-2.7 40.9-6.9 2.3-.7 4.7-1.1 7.1-1.1 42.9 0 79.7 24.4 98.5 59.8C359.3 421.4 306.7 448 248 448z"></path></svg>
      <div>
        <label>example@example.com</label>
        <button class="btn-email">Edit</button>
      </div>
      <button class="btn-password">Change Password</button>
    </div>
    <div class="profile-snippets">
      <h2>List of Snippets</h2>
      <table>
        <thead>
          <tr><th>Name</th><th>Last modified</th><th>Created</th><th>Actions</th></tr>
        </thead>
        <tbody>
          <tr v-for="(snippet, key) in snippets" :key="key">
            <td>{{ snippet.name }}</td><td>{{ snippet.lastModified }}</td><td>{{  snippet.creationTime}}</td>
            <td>
              <router-link :to="{ name: 'HashSnippet', params: { hash: snippet.hash } }"><svg class="icon-open" aria-hidden="true" focusable="false" data-prefix="fas" data-icon="external-link-square-alt" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512"><path fill="currentColor" d="M448 80v352c0 26.51-21.49 48-48 48H48c-26.51 0-48-21.49-48-48V80c0-26.51 21.49-48 48-48h352c26.51 0 48 21.49 48 48zm-88 16H248.029c-21.313 0-32.08 25.861-16.971 40.971l31.984 31.987L67.515 364.485c-4.686 4.686-4.686 12.284 0 16.971l31.029 31.029c4.687 4.686 12.285 4.686 16.971 0l195.526-195.526 31.988 31.991C358.058 263.977 384 253.425 384 231.979V120c0-13.255-10.745-24-24-24z"></path></svg></router-link>
              <button @click="handleDelete(snippet.hash)"><svg class="icon-delete" aria-hidden="true" focusable="false" data-prefix="fas" data-icon="trash-alt" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512"><path fill="currentColor" d="M32 464a48 48 0 0 0 48 48h288a48 48 0 0 0 48-48V128H32zm272-256a16 16 0 0 1 32 0v224a16 16 0 0 1-32 0zm-96 0a16 16 0 0 1 32 0v224a16 16 0 0 1-32 0zm-96 0a16 16 0 0 1 32 0v224a16 16 0 0 1-32 0zM432 32H312l-9.4-18.7A24 24 0 0 0 281.1 0H166.8a23.72 23.72 0 0 0-21.4 13.3L136 32H16A16 16 0 0 0 0 48v32a16 16 0 0 0 16 16h416a16 16 0 0 0 16-16V48a16 16 0 0 0-16-16z"></path></svg></button>
              <router-link :to="{ name: 'Share', params: {hash: snippet.hash}}"><svg class="icon-share" aria-hidden="true" data-prefix="fas" data-icon="share" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512" ><path fill="currentColor" d="M503.691 189.836L327.687 37.851C312.281 24.546 288 35.347 288 56.015v80.053C127.371 137.907 0 170.1 0 322.326c0 61.441 39.581 122.309 83.333 154.132 13.653 9.931 33.111-2.533 28.077-18.631C66.066 312.814 132.917 274.316 288 272.085V360c0 20.7 24.3 31.453 39.687 18.164l176.004-152c11.071-9.562 11.086-26.753 0-36.328z"></path></svg></router-link>
            </td>
          </tr>
        </tbody>
      </table>
      <h2>List of Shared Snippets</h2>
      <table>
        <thead>
          <tr><th>Name</th><th>Last modified</th><th>Created</th><th>Actions</th></tr>
        </thead>
          <tbody>
            <tr v-for="(snippet, key) in sharedSnippets" :key="key">
              <td>{{ snippet.name }}</td><td>{{snippet.lastModified}}</td><td>{{snippet.creationTime}}</td>
              <td>
                <router-link :to="{ name: 'HashSnippet', params: { hash: snippet.hash } }"><svg class="icon-open" aria-hidden="true" focusable="false" data-prefix="fas" data-icon="external-link-square-alt" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512"><path fill="currentColor" d="M448 80v352c0 26.51-21.49 48-48 48H48c-26.51 0-48-21.49-48-48V80c0-26.51 21.49-48 48-48h352c26.51 0 48 21.49 48 48zm-88 16H248.029c-21.313 0-32.08 25.861-16.971 40.971l31.984 31.987L67.515 364.485c-4.686 4.686-4.686 12.284 0 16.971l31.029 31.029c4.687 4.686 12.285 4.686 16.971 0l195.526-195.526 31.988 31.991C358.058 263.977 384 253.425 384 231.979V120c0-13.255-10.745-24-24-24z"></path></svg></router-link>
                <button @click="handleRemoveShared(snippet.hash)"><svg class="icon-delete" aria-hidden="true" focusable="false" data-prefix="fas" data-icon="trash-alt" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512"><path fill="currentColor" d="M32 464a48 48 0 0 0 48 48h288a48 48 0 0 0 48-48V128H32zm272-256a16 16 0 0 1 32 0v224a16 16 0 0 1-32 0zm-96 0a16 16 0 0 1 32 0v224a16 16 0 0 1-32 0zm-96 0a16 16 0 0 1 32 0v224a16 16 0 0 1-32 0zM432 32H312l-9.4-18.7A24 24 0 0 0 281.1 0H166.8a23.72 23.72 0 0 0-21.4 13.3L136 32H16A16 16 0 0 0 0 48v32a16 16 0 0 0 16 16h416a16 16 0 0 0 16-16V48a16 16 0 0 0-16-16z"></path></svg></button>
              </td>
            </tr>
        </tbody>
      </table>
    </div>
  </main>
</template>
<script>
export default {
  name: "Profile",
  methods:
  {
    handleDelete(hash)
    {
      this.$store.dispatch("deleteSnippet", hash);
    },
    handleRemoveShared(hash)
    {
      this.$store.dispatch("removeSharedSnippet",hash);
    }
  },
  mounted()
  {
    this.$store.dispatch("userSnippets");
    this.$store.dispatch("sharedSnippets");
  },
  computed:
  {
    snippets()
    {
      return this.$store.getters.snippets;
    },
    sharedSnippets()
    {
      return this.$store.getters.sharedSnippets;
    }
  }
}
</script>

<style lang="scss" src="./Profile.scss" scoped></style>