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
    <LoginPage v-if="activePage === 'login'" @changeActivePage="changeActivePage"/>
    
    <AdminPage v-if="activePage === 'admin'" @changeActivePage="changeActivePage"/>
    <AdminLoginPage v-if="activePage === 'adminlogin'" @changeActivePage="changeActivePage"/>
    
    <AddUserPage v-if="activePage === 'adduser'" @changeActivePage="changeActivePage"/>
    <UserMainPage v-if="activePage === 'usermain'" @changeActivePage="changeActivePage"/>
    <UpdateUserPage v-if="activePage === 'updateuser'" @changeActivePage="changeActivePage"/>
    <AccomodationsPage v-if="activePage === 'accomodations'" @changeActivePage="changeActivePage"/>
    <AddBookingPage v-if="activePage === 'addbooking'" @changeActivePage="changeActivePage"/>


    <OwnerMainPage v-if="activePage === 'ownermain'" @changeActivePage="changeActivePage"/>
    <UpdateOwnerPage v-if="activePage === 'updateowner'" @changeActivePage="changeActivePage"/>
    
    <CountriesPage v-if="activePage === 'countries'" @changeActivePage="changeActivePage"/>
    <MyCampingSpotPage v-if="activePage === 'addcampingspot'" @changeActivePage="changeActivePage"/>
    <AddAccomodationPage v-if="activePage === 'addaccomodation'" @changeActivePage="changeActivePage"/>
 
 </div>
</template>

<script>
//STARTPAGE
import LoginPage from "./pages/LoginPage.vue"

// ADMIN PAGES
import AdminPage from "./pages/AdminPage.vue"
import AdminLoginPage from "./pages/AdminLoginPage.vue"


//USER PAGES
import AddUserPage from "./pages/AddUserPage.vue"
import UserMainPage from "./pages/UserMainPage.vue"
import UpdateUserPage from "./pages/UpdateUserPage.vue"
import AccomodationsPage from "./pages/AccomodationsPage.vue"
import AddBookingPage from "./pages/AddBookingPage.vue"

//OWNER PAGES
import OwnerMainPage from "./pages/OwnerMainPage.vue"
import UpdateOwnerPage from "./pages/UpdateOwnerPage.vue"

//COUNTRIES
import CountriesPage from "./pages/CountriesPage.vue"
//CAMPINGSPOT
import MyCampingSpotPage from "./pages/MyCampingspotPage.vue"

//ACCOMODATION PAGES
import AddAccomodationPage from "./pages/AddAccomodationPage.vue"


export default {
  name: 'App',
  components: {
    LoginPage,

    AdminPage,
    AdminLoginPage,

    AddUserPage,
    UserMainPage,
    UpdateUserPage,
    AccomodationsPage,
    AddBookingPage,

    OwnerMainPage,
    UpdateOwnerPage,

    CountriesPage,
    AddAccomodationPage,
    MyCampingSpotPage

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
