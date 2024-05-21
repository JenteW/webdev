<template>
    <div>
        <h1>Owner Main Page</h1>
        <button @click="GoToLogin()">
            back to login
        </button>
        <H2>Welcome {{ owner.username }}</H2>
        <button @click="GoToUpdate()">
            Update Account
        </button>
        <br>
        <button @click="GoToCampingSpots()">
            Go to your camping spots
        </button>
        <br>
        <h2>Your campingspots</h2>
        <div v-for="campingspot in campingSpots" :key="campingspot.id">
            <h3>{{campingspot.name}}</h3>
            <p>{{campingspot.description}}</p>
            <p>€{{campingspot.price}}</p>
            <p>{{campingspot.availability}}</p>
            <img :src="require('@/assets/campingspots/' + campingspot.image)" alt="campingspot image"
            contain
            height="200px"
            width="300px"/>
            <br>
            <!-- <button @click="GetTagsBySpotId(campingspot.id)">see Tags</button>
            <br>
             <p>TAGS:</p>
            <div v-for="tag in campingspot.tags" :key="tag.id">
                <p>{{ tag.name }}</p>
            </div> -->
            
            <br>
            <button @click="GoToAccomodations(campingspot.id)">
                see more
            </button>
            <br>
        </div>


    </div>
</template>

<script>

    export default {
        name: 'OwnerMainPage',
        mounted(){
            this.id = this.$route.params.id;
            console.log(this.$route.params.id + " is the id @ OwnerMainPage");
            console.log(this.id + " is the id @ OwnerMainPage");
            this.GetOwner(this.id);
            this.GetCampingSpots(this.id);
        },
        data(){
            return{
                owner: {},
                id: "",
                campingSpots: [],
                //SpotTags: [],
                //SingleTag: {},
                //tags: []

            }
        },

        methods:{
            GoToLogin(){
                this.$router.push({name: "LoginPage"});
                this.$emit("changeActivePage", "login");
            },
            GoToUpdate(){
                this.$router.push({name: "UpdateOwnerPage", params: {id: this.id}});
                this.$emit("changeActivePage", "updateowner");
            },
            GoToCampingSpots(){
                this.$router.push({name: "MyCampingspotPage", params: {id: this.id}});
                this.$emit("changeActivePage", "addcampingspot");
            },
            GoToAccomodations(id){
                this.$router.push({name: "AddAccomodationPage", params: {id: id}});
                this.$emit("changeActivePage", "addaccomodation");
            },
            // GetTagById(id){
            //     fetch("http://localhost:5162/Tag/" + id, {
            //         method: "GET",
            //         headers: {
            //             "Content-Type": "application/json",
            //         }
            //     })
            //     .then(response => {
            //         if(response.status === 404){
            //             alert("No tags found!");
            //             throw new Error("No tags found");
            //         }
            //         else if(!response.ok){
            //              throw new Error("Network response was not ok at GETUSER");
            //         }
            //         else{
            //             return response.json();
            //         }
            //     })
            //     // .then(data => {
            //     //     console.log(data);
            //     //     console.log(data.name)
            //     //     console.log("is fecking data");
            //     //     console.log("name " + data.name);
            //     //     this.SingleTag = data;
            //     //     this.tags.push(data);

            //     // })
            //     .catch(error => {
            //         console.error("There was an error!", error);
            //         return null;
            //     });
            // },
            // GetTagsBySpotId(campingspot){
            //     fetch("http://localhost:5162/SpotTag/Spot/" + campingspot.id, {
            //         method: "GET",
            //         headers: {
            //             "Content-Type": "application/json",
            //         }
            //     })
            //     .then(response => {
            //         if(response.status === 404){
            //             alert("No tags found!");
            //             throw new Error("No tags found");
            //         }
            //         else if(!response.ok){
            //              throw new Error("Network response was not ok at GETUSER");
            //         }
            //         else{
            //             return response.json();
            //         }
            //     })
            //     .then(data => {
            //         console.log(data);
            //         campingspot.tags = [];
            //         const tagPromises = data.map(spotTag => {
            //             this.GetTagById(spotTag.tagId);
            //         });
            //         return Promise.all(tagPromises);
            //     })
            //     .then(tags => {
            //         campingspot.tags = tags.filter(tag => tag !== null);
            //     })
            //         // console.log("is fecking data");
            //         // console.log(data.length + " is the length of data");
            //         // this.tags = [];
            //         // this.SpotTags = data;
            //         // this.SpotTags.forEach(tag => {
            //         //     console.log(tag);
            //         //     this.GetTagById(tag.tagId);
            //         // });
            //         //})
                
            //     .catch(error => {
            //         console.error("There was an error!", error);
            //     });
            // },
            GetCampingSpots(id){
                fetch("http://localhost:5162/CampingSpot/Owner/" + id, {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => {
                    if(response.status === 404){
                        alert("No campingspots found!");
                        throw new Error("user not found at GETUSER");
                    }
                    else if(!response.ok){
                         throw new Error("Network response was not ok at campingspot");
                    }
                    else{
                        return response.json();
                    }
                })
                .then(data => {
                    console.log(data);
                    console.log("is fecking data");
                    this.campingSpots = data;
                    // this.campingSpots.forEach(campingspot => {
                    //     console.log(campingspot);
                    //     this.GetTagsBySpotId(campingspot);
                    // });
                })
                .catch(error => {
                    console.error("There was an error!", error);
                });
            },
            GetOwner(id){
                fetch("http://localhost:5162/Owner/" + id, {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => {
                    if(response.status === 404){
                        alert("Username not found!");
                        throw new Error("user not found at GETUSER");
                    }
                    else if(!response.ok){
                         throw new Error("Network response was not ok at getowner");
                    }
                    else{
                        return response.json();
                    }
                })
                .then(data => {
                    console.log(data);
                    console.log("is fecking data");
                    this.owner = data;
                })
                .catch(error => {
                    console.error("There was an error!", error);
                });
            }
            
        }
        
    }
</script>