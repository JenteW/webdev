<template>
    <div class="div-flex">
        <div class="top-left-button"> 
            <button class="custom-button" @click="GoToOwnerMain()">
            Go to homepage
            </button>
        </div>
        <H1 class="h1">bookings</H1>
        <div class="div-flex">
            <button class="custom-button" v-if="this.allbookings == 0" @click="GetAllBookings()">
                see all bookings
            </button>
            <button class="custom-button" v-else @click="GetFutureBookings()">
                see future bookings
            </button>
        </div>
        <div class="div-flex">
            <div v-for="booking in bookings" :key="booking.id" class="the-box-booking">
                <img :src="require('@/assets/campingspots/' + GetSpotImage(booking.campingSpotId))" alt="campingspot image"
                contain
                class="mr-4"
                height="200px"
                width="300px"/>
                <div class="flex flex-col">
                    <h3 class="h2">{{GetSpotName(booking.campingSpotId)}} was booked.</h3>
                    <p class="mb-2">Booked by: {{GetNameOfBooker(booking.userId)}}</p>
                    <p class="mb-2">Booked for {{ days(booking.id) }} day(s), coming to a total cost of €{{booking.price}}</p>
                    <p class="mb-2">Arrives on: {{booking.startDate}}</p>
                    <p class="mb-2">Leaving on: {{booking.endDate}}</p>
                </div>
            </div>
        </div>
    </div>
</template>

<script>

export default {
    name: 'OwnerBookedPage',
    data(){
        return{
            bookings: [],
            Accomodations: [],
            CampingSpots: [],
            users: [],
            id: "",
            userId: "",
            allbookings: 0,
        }
    },
    mounted(){
        this.userId = this.$route.params.userId;
        console.log(this.id + " is the id @ OwnerBookedPage");
        this.GetCampingSpotsByOwnerId();
        this.GetBookingsOfSpots();
        this.GetFutureBookings();
        this.GetUsers();
    },
    methods:{
        GoToOwnerMain(){
            this.$router.push({name: "OwnerMainPage", params: {id: this.userId}});
            this.$emit("changeActivePage", "ownermain");

        },
        GetBookingsOfSpots(){
            const spotIds = this.CampingSpots.map(spot => spot.id);
            this.bookings = this.bookings.filter(booking => spotIds.includes(booking.campingSpotId));
        },
        GetNameOfBooker(userId){
            const user = this.users.find(user => user.id === userId);
            return user.fn + " " + user.ln;
        },
        GetSpotName(spotId){
            const spot = this.CampingSpots.find(spot => spot.id === spotId);
            return spot.name;
        },
        GetSpotImage(spotId){
            const spot = this.CampingSpots.find(spot => spot.id === spotId);
            console.log(spot.image + " is the image")
            return spot.image;
        },
        days(bookingId){
            const booking = this.bookings.find(booking => booking.id === bookingId);
            const startDate = new Date(booking.startDate);
            const endDate = new Date(booking.endDate);
            const timeDifference = endDate.getTime() - startDate.getTime();
            const days = timeDifference / (1000 * 3600 * 24);
            return days;
        },
        GetAllBookings(){
            this.allbookings = 1;
            fetch("https://localhost:5162/Booking/", {
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
        GetFutureBookings(){
            this.allbookings = 0;
            fetch("https://localhost:5162/Booking/", {
                method: "GET",
                headers: {
                    "Content-Type": "application/json",
                }
            })
            .then(response => {
                if (!response.ok) {
                    throw new Error("Network response was not ok at GetBookings");
                }
                return response.json();
            })
            .then(data => {
                console.log(data);
                const currentDate = new Date();
                this.bookings = data.filter(booking => new Date(booking.endDate) > currentDate);
                this.GetBookingsOfSpots();
            })
            .catch(error => {
                console.error("There was a problem with your fetch operation:", error);
            });
        },
        GetUsers(){
            fetch("https://localhost:5162/User/", {
                method: "GET",
                headers: {
                    "Content-Type": "application/json",
                }
            })
            .then(response => {
                if(!response.ok){
                    throw new Error("Network response was not ok at GetUsers");
                }
                return response.json();
            })
            .then(data => {
                console.log(data);
                this.users = data;
            })
            .catch(error => {
                console.error("There was a problem with your fetch operation:", error);
            });
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
        GetCampingSpotsByOwnerId(){
            fetch("https://localhost:5162/CampingSpot/Owner/" + this.userId, {
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
                this.CampingSpots = data;
            })
            .catch(error => {
                console.error("There was an error!", error);
            });
        },
    }
}
</script>