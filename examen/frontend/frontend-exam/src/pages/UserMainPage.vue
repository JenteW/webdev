<template>
    <div class="div-flex">
        <h1 class="h1">User Main Page</h1>
        <H2 class="h2">Welcome {{ user.username }}</H2>
        <div>
            <button class="custom-button" @click="GoToLogin()">
                back to login
            </button>
            <button class="custom-button" @click="GoToUpdate()">
                Update Account
            </button>
            <button class="custom-button" @click="GoToBookings()">
                Go to bookings
            </button>
        </div>
        <h2 class="h2">Available Camping Spots</h2>
        <div class="div-flex">
            <h3 class="text-xl">Filter by:</h3>
            <label for="country">Country:</label>
                <select v-model="country">
                    <option v-for="country in countries" :key=" 'country-' + country.id" :value="country.id">
                        {{country.name}}
                    </option>
                </select>
            <button class= "custom-button" @click="RemoveFilters()">
                remove filters
            </button>
        </div>
        <div class="div-flex">
            <div v-for="campingspot in availableCampingSpots" :key="campingspot.id" class="the-box">
                <img :src="require('@/assets/campingspots/' + campingspot.image)" alt="campingspot image"
                    class="mr-4 h-48 w-64" 
                    contain/>
                <div class="flex flex-col">
                    <h3 class="h3">{{campingspot.name}}</h3>
                    <p class="mb-2">{{campingspot.description}}</p>
                    <p class="mb-2">€{{campingspot.price}}</p>
                    <button class="custom-button" @click="GoToAccomodations(campingspot.id)">see more</button>                
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        name: 'UserMainPage',
        mounted(){
            this.id = this.$route.params.id;
            console.log(this.id + " is the id @ UserMainPage");
            this.GetUser(this.id);
            this.GetAllCampingSpots();
            this.GetAllCountries();
        },
        data(){
            return{
                user: {},
                id: "",
                CampingSpots: [],
                countries: [],
                locations: [],
                country: ""


            }
        },
        watch:{
            country: function(){
                console.log(this.country);
                this.GetLocationsByCountry(this.country);
            }
        },
        computed:{
            //available campings spots
            availableCampingSpots(){
                return this.CampingSpots.filter(campingspot => campingspot.availability === true);
            }
        },
        
        methods:{
            GoToBookings(){
                this.$router.push({name: "UserBookingPage", params: {userId: this.id}});

                this.$emit("changeActivePage", "userbooking");
            },
            GoToLogin(){
                this.$router.push({name: "LoginPage"});
                this.$emit("changeActivePage", "login");
            },
            GoToUpdate(){
                this.$router.push({name: "UpdateUserPage", params: {id: this.id}});
                this.$emit("changeActivePage", "updateuser");
            },
            GoToAccomodations(id){
                this.$router.push({name: "AccomodationsPage", params: {id: id, userId: this.user.id}});
                this.$emit("changeActivePage", "accomodations");
            },
            RemoveFilters(){
                this.GetAllCampingSpots();
            },
            GetLocationsByCountry(countryId){
                fetch("https://localhost:5162/Location/Country/" + countryId, {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => {
                    if(!response.ok){
                        throw new Error("Network response was not ok at GETLOCATIONSBYCOUNTRY");
                    }
                    else{
                        return response.json();
                    }
                })
                .then(data => {
                    console.log(data);
                    console.log("is fecking data");
                    this.locations = data;
                    this.CampingSpots = [];
                    for(let i = 0; i < this.locations.length; i++){
                        console.log(this.locations[i].id);
                        this.GetCampingspotsByLocationId(this.locations[i].id);
                    }
                })
                .catch(error => {
                    console.error("There was an error!", error);
                });
                
            },
            GetCampingspotsByLocationId(id){
                fetch("https://localhost:5162/CampingSpot/Location/" + id, {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => {
                    if(!response.ok){
                        throw new Error("Network response was not ok at GETCAMPINGSPOTSBYLOCATIONID");
                    }
                    else{
                        return response.json();
                    }
                })
                .then(data => {
                    console.log(data);
                    console.log("is fecking data");
                    this.CampingSpots.push(...data);
                })
                .catch(error => {
                    console.error("There was an error!", error);
                });
                
            },
            GetAllLocations(){
                fetch("https://localhost:5162/Location", {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => {
                    if(!response.ok){
                        throw new Error("Network response was not ok at GETALLLOCATIONS");
                    }
                    else{
                        return response.json();
                    }
                })
                .then(data => {
                    console.log(data);
                    console.log("is fecking data");
                    this.locations = data;
                })
                .catch(error => {
                    console.error("There was an error!", error);
                });
            
            
            },
            GetAllCountries(){
                fetch("https://localhost:5162/Country", {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => {
                    if(!response.ok){
                        throw new Error("Network response was not ok at GETALLCOUNTRIES");
                    }
                    else{
                        return response.json();
                    }
                })
                .then(data => {
                    console.log(data);
                    console.log("is fecking data");
                    this.countries = data;
                })
                .catch(error => {
                    console.error("There was an error!", error);
                });
            
            },
            GetAllCampingSpots(){
                fetch("https://localhost:5162/CampingSpot", {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => {
                    if(!response.ok){
                        throw new Error("Network response was not ok at GETALLCAMPINGSPOTS");
                    }
                    else{
                        return response.json();
                    }
                })
                .then(data => {
                    console.log(data);
                    console.log("is fecking data");
                    this.CampingSpots = data;
                })
                .catch(error => {
                    console.error("There was an error!", error);
                });
            
            },
            GetUser(id){
                fetch("https://localhost:5162/User/" + id, {
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
                         throw new Error("Network response was not ok at GETUSER");
                    }
                    else{
                        return response.json();
                    }
                })
                .then(data => {
                    console.log(data);
                    console.log("is fecking data");
                    this.user = data;
                })
                .catch(error => {
                    console.error("There was an error!", error);
                });
            }
            
        }
        
    }
</script>