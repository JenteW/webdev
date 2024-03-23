<template>
  <div id="app">
    <!--Navigation-->
    <nav>
      <ul>
        <!-- <li @click="changeActivePage('home')">
          Home
        </li>
        <li @click="changeActivePage('artist')">
          Artist
        </li>
        <li @click="changeActivePage('voting')">
          Voting
        </li> -->

        <li v-for="(page,index) in pages" :key="index"
        @click="changeActivePage(page)"
        :style="getActiveStyling(page)">
          {{ page }}
        </li>
      </ul>
    </nav>
    <!-- PAGES-->
    <!-- {{ activePage }} -->
    <PageHome v-if="activePage == 'home'"/>
    <PageArtists v-if="activePage == 'artists'"/>
    <PageVoting v-if="activePage == 'voting'" @changeActivePage="changeActivePage"/>

  </div>
</template>

<script>
import PageHome from "./pages/PageHome.vue";
import PageVoting from "./pages/PageVoting.vue";
import PageArtists from "./pages/PageArtists.vue";
import PageRanking from "./pages/PageRanking.vue";

export default {
  name: 'App',
  components: {
    PageHome,
    PageArtists,
    PageVoting,
    PageRanking,
  },
  data() {
    //internal use, can be overwritten
    return{
      activePage:"voting",
      pages:[
        "home",
        "artists",
        "voting",
        "ranking"
      ]
    }
  },
  // all the functions needed
  methods: {
    changeActivePage(page){
      this.activePage = page;
    },
    getActiveStyling(page){
      if (page==this.activePage) {
      return "text-decoration: underline; font-weight:bold";
      }
      return "";
    }
  }
}
</script>
