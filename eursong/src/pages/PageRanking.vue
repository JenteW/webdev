<template>
    <div>
        <h1>
            Ranking
        </h1>
       <div>
            <div v-for=" (song, index) in mappedSongs" :key="song.id">
                <div v-if="index == activeSongIndex">
                    {{song.artistObject.name}} - {{ song.title }} - {{ song.points }}
                </div> 
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        name: "PageRanking",
        mounted(){ this.fetchSongs()
        },
        data(){
            return{
                songs: [],
                artists:[],
                mappedSongs:[],
               
            }
        },

        methods: {

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
            fecthVotes(){
                fetch("http://webservies.be/eurosong/Votes", {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json"
                    }
                })
                .then((response) => response.json())
                .then((votes) => {
                    this.votes = votes;
                    this.mixSongs();
                });
            },
            mixSongs(){
               
                this.mappedSongs = this.songs.map((song) => {
                    song.artistObject = this.artists.find((artist) => {
                        return song.artist == artist.id
                    });
                    let points = 0;
                    this.votes.forEach((vote) => {
                        if(vote.songID == song.id){
                            points += vote.points;
                        }
                    });

                    return song;

                });
                console.log(this.mappedSongs);
                this.mappedSongs.sort((a,b) => b.points - a.points);
            }



         
        }
    }
</script>