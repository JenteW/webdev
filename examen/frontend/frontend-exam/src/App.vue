<template>
  <div id="app">
    <!-- <nav>
      <ul>
        <li v-for="(page,index) in pages" :key="index"
        @click ="changeActivePage(page)">
          {{page}}
        </li>
      </ul>
    </nav> -->
    <AdminPage v-if="activePage === 'admin'" @changeActivePage="changeActivePage"/>
    <AddUserPage v-if="activePage === 'adduser'" @changeActivePage="changeActivePage"/>
    <LoginPage v-if="activePage === 'login'" @changeActivePage="changeActivePage"/>
    <UserMainPage v-if="activePage === 'usermain'" @changeActivePage="changeActivePage"/>
  </div>
</template>

<script>
import AdminPage from "./pages/AdminPage.vue"
import AddUserPage from "./pages/AddUserPage.vue"
import LoginPage from "./pages/LoginPage.vue"
import UserMainPage from "./pages/UserMainPage.vue"
export default {
  name: 'App',
  components: {
    AdminPage,
    AddUserPage,
    LoginPage,
    UserMainPage

  },
  data(){
    return {
      activePage: "login",
      // pages: ["admin", "login"]
    }
  },
  methods: {
    changeActivePage(page){
      this.activePage = page;
    }
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
