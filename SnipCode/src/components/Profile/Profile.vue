<template>
  <main>
    <div class="profile-details">
      <div>
        <label>example@example.com</label>
        <button>Edit</button>
      </div>
      <button>Change Password</button>
      <button>Log out</button>
    </div>
    <div class="profile-snippets">
      <h2>List of Snippets</h2>
      <table>
        <thead>
          <tr><th>Name</th><th>Files</th><th>Last modified</th><th>Created</th><th>Actions</th></tr>
        </thead>
        <tbody>
          <tr><td>My first snippet</td><td>3</td><td>Yesterday</td><td>Yestarday</td><td><button>Open</button><button>Delete</button><button>Share</button></td></tr>
          <tr><td>My first snippet</td><td>3</td><td>Yesterday</td><td>Yestarday</td><td><button>Open</button><button>Delete</button><button>Share</button></td></tr>
          <tr><td>My first snippet</td><td>3</td><td>Yesterday</td><td>Yestarday</td><td><button>Open</button><button>Delete</button><button>Share</button></td></tr>
          <tr><td>My first snippet</td><td>3</td><td>Yesterday</td><td>Yestarday</td><td><button>Open</button><button>Delete</button><button>Share</button></td></tr>
          <tr><td>My first snippet</td><td>3</td><td>Yesterday</td><td>Yestarday</td><td><button>Open</button><button>Delete</button><button>Share</button></td></tr>
        </tbody>
      </table>
      <h2>List of Shared Snippets</h2>
      <table>
        <thead>
          <tr><th>Name</th><th>Files</th><th>Last modified</th><th>Created</th><th>Actions</th></tr>
        </thead>
          <tbody>
          <tr><td>My first snippet</td><td>3</td><td>Yesterday</td><td>Yestarday</td><td><button>Open</button><button>Delete</button></td></tr>
          <tr><td>My first snippet</td><td>3</td><td>Yesterday</td><td>Yestarday</td><td><button>Open</button><button>Delete</button></td></tr>
          <tr><td>My first snippet</td><td>3</td><td>Yesterday</td><td>Yestarday</td><td><button>Open</button><button>Delete</button></td></tr>
          <tr><td>My first snippet</td><td>3</td><td>Yesterday</td><td>Yestarday</td><td><button>Open</button><button>Delete</button></td></tr>
        </tbody>
      </table>
    </div>
  </main>
</template>
<script>
export default {
  name: "Profile",
}
</script>

<style lang="scss" src="./Profile.scss" scoped></style>