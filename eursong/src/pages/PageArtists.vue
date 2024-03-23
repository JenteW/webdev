<template>
    <div>
        <h1>
            Artists
        </h1>
      <button @click="fetchArtists()">
        fetch all artists
      </button>
      <p>
        amount of artists {{ artists.length }}
      </p>
      <ul>
        <li v-for="artist in artists" :key="artist.id">
            {{ artist.name }} {{ artist.id }}
            <button v-if="artist.name == null" @click="deleteArtist(artist.id)">
                delete
            </button>
        </li>
    </ul>
    <hr>
    <input type="text" v-model="newArtist">
    <button @click="addArtist()">
        Add Artist
    </button>
    </div>
</template>

<script>
    export default {
        name: "PageArtists",
        //mounted betekent dat het iets direct uit gaat voeren, als je op de pagina komt
        mounted(){
            this.fetchArtists();
        },
        data(){
            return{
                artists: [],
                newArtist : "",
            }
        },
        methods:{
            addArtist(){
                console.log("Add new artist " + this.newArtist);
                fetch("http://webservies.be/eurosong/Artists", {
                    method: "POST",
                    headers: {
                        'accept': 'application/json',
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify({name: this.newArtist})
                })
                .then((response) => response.json())
                .then(() => {
                    this.fetchArtists();
                })
            },
            fetchArtists(){
                fetch("http://webservies.be/eurosong/Artists", {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json"
                    }
                })
                .then((response) => response.json())
                .then((data) => {
                    this.artists = data;
                })
                //console.log("I wanna fetch a list of dem artists");
            },
            deleteArtist(id){
                fetch("http://webservies.be/eurosong/Artists?id=" + id, {
                    method: "DELETE",
                    headers: {
                        "Content-Type": "application/json"
                    }
                })
                .then((response) => response.json())
                .then((data) => {
                    console.log(data)
                  this.fetchArtists();
                })
            }
        }
    }
</script>