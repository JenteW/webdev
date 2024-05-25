<template>
    <div class="div-flex">
        <div class="top-left-button">
            <button class="custom-button" @click="GoToUser()">Go to main page</button>
        </div>
        <h1 class="h1">Booking</h1>
        <div class="div-flex">
            <button class="custom-button" v-if="this.allbookings == 0" @click="GetBookings()">
                see all bookings
            </button>
            <button class="custom-button" v-else @click="GetFutureBookings()">
                see future bookings
            </button>
        </div>
        <div class="div-flex">
            <H2 class="h2">Booked CampingSpot</H2>
            <div v-for="booking in bookings" :key="booking.id" class="the-box-booking">
                <img :src="require('@/assets/campingspots/' + GetcampingspotsImage(booking.campingSpotId))" alt="campingspot image"
                    contain
                    class="mr-4"

                    height="200px"
                    width="300px"/>
                <div class="flex flex-col">
                    <h3 class="h3">{{GetCampingSpotsName(booking.campingSpotId)}}</h3>
                    <p class="mb-2">{{GetCampingspotsDescription(booking.campingSpotId)}}</p>
                    <h3 class="h3">{{GetAccomdationName(booking.accomodationId)}} was booked.</h3>
                    <p class="mb-2">{{GetAccomodationDescription(booking.accomodationId)}}</p>
                    <p class="mb-2">{{booking.startDate}}</p>
                    <p class="mb-2">{{booking.endDate}}</p>
                    <p class="mb-2">€{{booking.price}}</p>         
                </div>      
 
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: 'UserBooking',
    data(){
        return{
            bookings: [],
            Accomodations: [],
            CampingSpots: [],
            id: "",
            userId: "",
            spotId: "",
            allbookings: 0
        }
    },
    mounted(){
        this.userId = this.$route.params.userId;
        console.log(this.id + " is the id @ UserBooking");
        this.GetBookings();
        this.GetFutureBookings();
        this.GetAllAccomodations();
        this.GetAllCampingSpots();
    },
    methods:{
        GoToUser(){
            this.$router.push({name: "UserMainPage", params: {id: this.userId}});
            this.$emit("changeActivePage", "usermain");
        },
        GetBookings(){
            this.allbookings = 1;
            fetch("https://localhost:5162/Booking/userId/" + this.userId, {
                method: "GET",
                headers: {
                    "Content-Type": "application/json",
                }
            })
            .then(response => {
                if(!response.ok){
                    throw new Error("Network response was not ok at GetBookings");
                }
                return response.json();
            })
            .then(data => {
                console.log(data);
                this.bookings = data;
            })
            .catch(error => {
                console.error("There was a problem with your fetch operation:", error);
            });
        },
        GetAccomdationName(id){
            const accomodation = this.Accomodations.find(accomodation => accomodation.id == id);
            return accomodation ? accomodation.name : "Unknown Accomodation";
        },
        GetAccomodationDescription(id){
            const accomodation = this.Accomodations.find(accomodation => accomodation.id == id);
            return accomodation ? accomodation.description : "Unknown Accomodation";
        },
        GetCampingSpotsName(id){
            const campingspot = this.CampingSpots.find(campingspot => campingspot.id == id);
            return campingspot ? campingspot.name : "Unknown CampingSpot";
        },
        GetCampingspotsDescription(id){
            const campingspot = this.CampingSpots.find(campingspot => campingspot.id == id);
            return campingspot ? campingspot.description : "Unknown CampingSpot";
        },
        GetcampingspotsImage(id){
            const campingspot = this.CampingSpots.find(campingspot => campingspot.id == id);
            return campingspot ? campingspot.image : "Unknown CampingSpot";
        },
        GetAllAccomodations(){
            fetch("https://localhost:5162/Accomodation", {
                method: "GET",
                headers: {
                    "Content-Type": "application/json",
                }
            })
            .then(response => {
                if(!response.ok){
                    throw new Error("Network response was not ok at GetAllAccomodations");
                }
                return response.json();
            })
            .then(data => {
                console.log(data);
                this.Accomodations = data;
            })
            .catch(error => {
                console.error("There was a problem with your fetch operation:", error);
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
                    throw new Error("Network response was not ok at GetAllCampingSpots");
                }
                return response.json();
            })
            .then(data => {
                console.log(data);
                this.CampingSpots = data;
            })
            .catch(error => {
                console.error("There was a problem with your fetch operation:", error);
            });
        },
        GetFutureBookings(){
            this.allbookings = 0;
            fetch("https://localhost:5162/Booking/userId/" + this.userId, {
                method: "GET",
                headers: {
                    "Content-Type": "application/json",
                }
            })
            .then(response => {
                if(!response.ok){
                    throw new Error("Network response was not ok at GetFutureBookings");
                }
                return response.json();
            })
            .then(data => {
               console.log(data);
                const currentDate = new Date();
                this.bookings = data.filter(booking => new Date(booking.endDate) > currentDate);
            })
            .catch(error => {
                console.error("There was a problem with your fetch operation:", error);
            });

        }        
        
    }
}
</script>