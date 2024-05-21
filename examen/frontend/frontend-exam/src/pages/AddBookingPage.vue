<template>
    <div>
        <h1>Add Booking</h1>

        <button @click="ToUser()">
            return to main page
        </button>
        <h2>SpotDetails</h2>
        <div :key="Spot.id">
            <h3>{{Spot.name}}</h3>
            <p>{{Spot.description}}</p>
            <p>€{{Spot.price}}</p>
            <p>{{Spot.availability}}</p>
            <img :src="require('@/assets/campingspots/' + Spot.image)" alt="campingspot image"
            contain
            height="200px"
            width="300px"/>
            <br>
        </div>


        <h2>Accomodation</h2>
        <div :key="accomodation.id">
            <h3>{{accomodation.name}}</h3>
            <p>{{accomodation.description}}</p>
        </div>
            <br>
            <h2>Booking</h2>
            <input type="date" v-model="startDate" placeholder="StartDate">
            <br>
            <input type="date" v-model="endDate" placeholder="EndDate" >
            <br>
            <p>The total sum is {{ this.price }}</p>
            <button @click="AddBooking()">
                Add Booking
            </button>
    </div>
</template>

<script>
    export default{
        name: 'AddBookingPage',
        data(){
            return{
                id: "",
                userId: "",
                accomodation: {},
                spotId: "",
                Spot: {},
                startDate: "",
                endDate: "",
                price: 0
            }
        },
        watch:{
            startDate: function(){
                this.CalculatePrice();
            },
            endDate: function(){
                this.CalculatePrice();
            }
        },
        mounted(){
            this.id = this.$route.params.id;
            this.userId = this.$route.params.userId;
            this.spotId = this.$route.params.spotId;
            console.log(this.id + " is the id @ AddBookingPage");
            this.GetAccomdationById(this.id);
            this.GetSpotById(this.spotId);
        },
        methods:{
            
            changeActivePage(page){
                this.$emit("changeActivePage", page);
            },
            ToUser(){
                this.$router.push({name: "UserMainPage", params: {id: this.userId}});
                this.$emit("changeActivePage", "usermain");
            },
            ToBookings(){
                this.$router.push({name: "UserBookingPage", params: {id: this.id, userId: this.userId, spotId: this.spotId}});
                this.$emit("changeActivePage", "userbooking");
            },
            CalculatePrice(){
                let start = new Date(this.startDate);
                console.log(start);
                let end = new Date(this.endDate);
                console.log(end);
                let diff = Math.abs(end - start);
                console.log(diff);
                let days = Math.ceil(diff / (1000 * 60 * 60 * 24));
                console.log(days);
                this.price = days * this.Spot.price;
            },
            AddBooking(){
                fetch("http://localhost:5162/Booking", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify({
                        startDate: this.startDate,
                        endDate: this.endDate,
                        campingspotId: this.spotId,
                        accomodationId: this.id,
                        userId: this.userId,
                        price: this.price
                    })
                })
                .then(response => {
                    if(!response.ok){
                        throw new Error("Network response was not ok at AddBooking");
                    }
                    return response;
                })
                .then(data => {
                    console.log(data);
                    alert("Booking added");
                    this.ToBookings();
                })
                .catch(error => {
                    console.error("There was a problem with your fetch operation:", error);
                });
            },
            GetSpotById(id){
                fetch("http://localhost:5162/CampingSpot/" + id, {
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
            GetAccomdationById(id){
                fetch("http://localhost:5162/Accomodation/" + id, {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => {
                    if(!response.ok){
                        throw new Error("Network response was not ok at GetAccomdationById");
                    }
                    return response.json();
                })
                .then(data => {
                    console.log(data);
                    this.accomodation = data;
                })
                .catch(error => {
                    console.error("There was a problem with your fetch operation:", error);
                });
            },
        }
    }
</script>