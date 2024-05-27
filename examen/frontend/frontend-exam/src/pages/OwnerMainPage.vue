<template>
    <div class="flex flex-col items-center">
        <div class="top-left-button">
            <button class="custom-button" @click="GoToLogin()">
                Back to login
            </button>
        </div>
        <h1 class="h1">Owner Main Page</h1>
        <div>

            <H2 class="h2">Welcome {{ owner.username }}</H2>
            <button class="custom-button" @click="GoToUpdate()">
                Update Account
            </button>
            <br>
            <button class="custom-button" @click="GoToCampingSpots()">
                Add a new campingspot
            </button>
            <br>
            <button class="custom-button" @click="GoToBookings()">
                Manage bookings
            </button>
        </div>
        <h2 class="h2 m-3">Your campingspots</h2>
        <div class="flex flex-col items-center">
            <div v-for="campingspot in campingSpots" :key="campingspot.id" class="the-box">
                <img :src="require('@/assets/campingspots/' + campingspot.image)" alt="campingspot image"
                class="mr-4 h-48 w-64"
                contain/>
                <div class="flex flex-col">
                    <h3 class="h3">{{campingspot.name}}</h3>
                    <p class="mb-2">{{campingspot.description}}</p>
                    <p class="mb-2">€{{campingspot.price}}</p>
                    <button class="custom-button" @click="GoToAccomodations(campingspot.id)">
                        See more
                    </button>
                </div> 
            </div>
        </div>  
    </div>
</template>

<script>

    export default {
        name: 'OwnerMainPage',
        mounted(){
            this.id = this.$route.params.id;
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
            GoToBookings(){
                this.$router.push({name: "OwnerBookedPage", params: {userId: this.id}});
                this.$emit("changeActivePage", "ownerbooked");
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

            GetCampingSpots(id){
                fetch("https://localhost:5162/CampingSpot/Owner/" + id, {
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

                    this.campingSpots = data;

                })
                .catch(error => {
                    console.error("There was an error!", error);
                });
            },
            GetOwner(id){
                fetch("https://localhost:5162/Owner/" + id, {
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

                    this.owner = data;
                })
                .catch(error => {
                    console.error("There was an error!", error);
                });
            }
            
        }
        
    }
</script>