<template>
    <div>
        <button @click="GoToUser()">Go to main page</button>
        <h1>Booking</h1>
        <div v-for="booking in bookings" :key="booking.id">
            
            <H2>Booked CampingSpot</H2>
            <div>
                <h3>{{GetCampingSpotsName(booking.campingSpotId)}}</h3>
                <p>{{GetCampingspotsDescription(booking.campingSpotId)}}</p>
                <img :src="require('@/assets/campingspots/' + GetcampingspotsImage(booking.campingSpotId))" alt="campingspot image"
                contain
                height="200px"
                width="300px"/>
            </div>
            <h2>With accomodation</h2>
            <div>
                <h3>{{GetAccomdationName(booking.accomodationId)}}</h3>
                <p>{{GetAccomodationDescription(booking.accomodationId)}}</p>
            </div>      
            <H3>{{booking.spotName}}</h3>
            <h3>{{booking.startDate}}</h3>
            <p>{{booking.endDate}}</p>
            <p>€{{booking.price}}</p>          
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
            spotId: ""
        }
    },
    mounted(){
        this.userId = this.$route.params.userId;
        console.log(this.id + " is the id @ UserBooking");
        this.GetBookings();
        this.GetAllAccomodations();
        this.GetAllCampingSpots();
    },
    methods:{
        GoToUser(){
            this.$router.push({name: "UserMainPage", params: {id: this.userId}});
            this.$emit("changeActivePage", "usermain");
        },
        GetBookings(){
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
        }
        
        
    }
}
</script>