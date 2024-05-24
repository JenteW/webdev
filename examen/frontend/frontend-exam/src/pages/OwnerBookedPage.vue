<template>
    <div class="flex flex-col items-center">
        <H1>bookings</H1>
        <button class="custom-button" @click="GoToOwnerMain()">
            Go to homepage
        </button>
        <div v-for="booking in bookings" :key="booking.id">
            <h3>{{GetSpotName(booking.campingSpotId)}} was booked.</h3>
            <p>booker: {{GetNameOfBooker(booking.userId)}}</p>
            <p>Booked for {{ days(booking.id) }} days, coming to a total cost of €{{booking.price}}</p>
            <p>{{booking.startDate}}</p>
            <p>{{booking.endDate}}</p>
            <img :src="require('@/assets/campingspots/' + GetSpotImage(booking.campingSpotId))" alt="campingspot image"
            contain
            height="200px"
            width="300px"/>
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
        }
    },
    mounted(){
        this.userId = this.$route.params.userId;
        console.log(this.id + " is the id @ OwnerBookedPage");
        this.GetBookings();
        this.GetCampingSpotsByOwnerId();
        this.GetBookingsOfSpots();
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
        GetBookings(){
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