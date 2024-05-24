<template>
    <div class="flex flex-col items-center">
        <H1 class="h1">Accomodation</H1>
        <div>
            <button class="custom-button" @click="GoToCampingSpots()">
                Go Back
            </button>
        </div>
        <H2 class="h2">Spot</H2>
        <div>
            <h3>{{ Spot.name }}</h3>
            <p>{{ Spot.description }}</p>
            <p>€{{ Spot.price }}</p>
            <img :src="require('@/assets/campingspots/' + Spot.image)" alt="campingspot image"
            contain
            height="300px"
            width="300px">
        </div>
        <H2>Accomodations</H2>
        <div v-for="accomodation in accomodations" :key="accomodation.id">
           
            <p><strong>Name: </strong>{{ accomodation.name }}</p>
            <p><strong>Description:</strong>{{ accomodation.description }}</p>
            <button class="custom-button" @click="GoToBooking(accomodation.id)">Book the accomodation</button>
        </div>

    </div>
</template>

<script>
    export default {
        name: 'AccomodationsPage',
        data(){
            return{
                accomodations: [],
                Spot: {},
                id: "",
                userId: ""
            }
        },
        mounted(){
            this.id = this.$route.params.id;
            this.userId = this.$route.params.userId;
            console.log(this.id + " is the id @ AccomodationsPage");
            this.GetSpotById(this.id);
            this.GetAccomodationBySpotId(this.id);
        },
        methods:{
            GoToBooking(id){
                this.$router.push({name: "AddBookingPage", params: {id: id, userId: this.userId, spotId: this.id}});
                this.$emit("changeActivePage", "addbooking");
            },
            GoToCampingSpots(){
                this.$router.push({name: "UserMainPage", params: {id: this.userId}});
                this.$emit("changeActivePage", "usermain");
            },
            GetSpotById(id){
                fetch("https://localhost:5162/CampingSpot/" + id, {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => {
                    if(!response.ok){
                        throw new Error("Network response was not ok at GetSpotById");
                    }
                    return response.json();
                })
                .then(data => {
                    console.log(data);
                    this.Spot = data;
                })
                .catch(error => {
                    console.error("There was a problem with your fetch operation:", error);
                });
            },
            GetAccomodationBySpotId(id){
                fetch("https://localhost:5162/Accomodation/CampingSpotId/" + id, {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => {
                    if(!response.ok){
                        throw new Error("Network response was not ok at GetAccomodationBySpotId");
                    }
                    else{
                        return response.json();
                    }
                })
                .then(data => {
                    console.log(data);
                    this.accomodations = data;
                })
                .catch(error => {
                    console.error("There was a problem with your fetch operation:", error);
                });
            }
        }
    }
</script>