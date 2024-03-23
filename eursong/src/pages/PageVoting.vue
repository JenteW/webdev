<template>
    <div>
        <h1>
            Voting
        </h1>
       <div>
            <div v-for=" (song, index) in mappedSongs" :key="song.id">
                <div v-if="index == activeSongIndex">
                    {{song.artistObject.name}} - {{ song.title }}
                </div> 
            </div>
        </div>
        <button @click="prevSong()" :disabled="activeSongIndex == 0">
            prev
        </button>
        <button @click="nextSong()" :disabled="activeSongIndex >= mappedSongs.length -1">
            next
        </button>
        <br>
        <div v-for="(button,index) in buttons" :key="index">
            <button @click="vote(index, button.points)" :disabled="button.isVoted == true">
                Add {{ button.points }}
            </button>
        </div>

        <button @click="changePage">
        Ranking
        </button>
        <!-- <button @click="vote(2)">
            Add 2
        </button>
        <button @click="vote(4)">
           Add 4
        </button>
        <button @click="vote(6)">
           Add 6
        </button> -->
    </div>
</template>

<script>
    export default {
        name: "PageVote",
        mounted(){ this.fetchSongs()
        },
        data(){
            return{
                songs: [],
                artists:[],
                mappedSongs:[],
                activeSongIndex: 0,
                buttons: [
                    {
                        points: 2,
                        isVoted: false,
                    },
                    {
                        points: 4,
                        isVoted: false,
                    },
                    {
                        points: 6,
                        isVoted: false,
                    }
                ]
            }
        },

        methods: {
            changePage(){
                this.$emit("changeActivePage", "ranking"); // van bovenaf iets laten uitvoeren
            },
            vote(buttonIndex, amountOfPoints){
                console.log(amountOfPoints);
                let songId = this.mappedSongs[this.activeSongIndex].id;
                this.buttons[buttonIndex].isVoted = true;
                if(this.buttons.filter((button) => button.isVoted == false).length == 0){
                    setTimeout(() =>{
                        
                    })
                    this.$emit("changeActivePage", "ranking");
                }
                fetch("http://webservies.be/eurosong/Votes", {
                    method: "POST",
                    headers: {
                        'accept': 'application/json',
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify({
                        songID: songId, 
                        ip:"undefined", 
                        points: amountOfPoints})
                })
                .then((response) => response.json())
                .then((vote) => {
                    console.log(vote)
                })
            },
            prevSong(){
                this.activeSongIndex--;
            },
            nextSong(){
                this.activeSongIndex++;
            },
            fetchSongs(){
                fetch("http://webservies.be/eurosong/Songs", {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json"
                    }
                })
                .then((response) => response.json())
                .then((songs) => {
                    this.songs = songs;
                    this.fetchArtists();
                    
                });
            },
            fetchArtists(){
                fetch("http://webservies.be/eurosong/Artists", {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json"
                    }
                })
                .then((response) => response.json())
                .then((artists) => {
                    this.artists = artists;
                    this.mixSongs();
                });
            },
            mixSongs(){
                // let mappedSongs = this.songs;
                // for(let songIndex = 0; songIndex < mappedSongs.length; songIndex ++){
                //     let song = mappedSongs[songIndex];

                //     for(let artistIndex = 0; artistIndex < this.artists.length; artistIndex++){
                //         if(song.artist == this.artists[artistIndex].id){
                //             song.artist = this.artists[artistIndex];
                //             mappedSongs[songIndex] = song;
                //         }
                //     }
                // }
                // this.mappedSongs = mappedSongs;
                // console.log(mappedSongs)


                this.mappedSongs = this.songs.map((song) => {
                    song.artistObject = this.artists.find((artist) => {
                        return song.artist == artist.id
                    });
                    

                    return song;

                });
                console.log(this.mappedSongs);
            }



         
        }
    }
</script>