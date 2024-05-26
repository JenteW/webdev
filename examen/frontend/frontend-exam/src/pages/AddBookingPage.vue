<template>
    <div class="div-flex">
        <h1 class="h1">Book the accommodation</h1>
        <br>
        <div class="top-left-button">
            <button class="custom-button" @click="ToUser()">
                Return to main page
            </button>
        </div>
        <br>
        <h2 class="h2">Details of the spot</h2>
        <div :key="Spot.id" class="div-flex">
            <h3 class="h3 text-center">{{Spot.name}}</h3>
            <p>{{Spot.description}}</p>
            <p>The price per day is: €{{Spot.price}}</p>
            <img :src="require('@/assets/campingspots/' + Spot.image)" alt="campingspot image"
            contain
            height="200px"
            width="300px"/>
            <br>
        </div>


        <h2 class="h2">Accommodation</h2>
        <div :key="accomodation.id" class="div-flex">
            <h3 class="h3 text-center">{{accomodation.name}}</h3>
            <p>{{accomodation.description}}</p>
        </div>
        <div class="div-flex">
            <br>
            <h2 class="h2">Booking</h2>
            <input class="custom-input" type="date" v-model="startDate" placeholder="StartDate">
            <br>
            <input class="custom-input" type="date" v-model="endDate" placeholder="EndDate" >
            <br>
            <p>The total sum is €{{ this.price }}</p>
            <button class="custom-button" @click="CheckIfAvailable()">
                Add Booking
            </button>
        </div>
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
                price: 0,
                isAvailable: false
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
                if(this.startDate == "" || this.endDate == ""){
                    return;
                }
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
            CheckIfAvailable(){
            if(this.startDate == this.endDate){
                alert("Please select a start and end date");
                return;
            }
            fetch("https://localhost:5162/Booking/checkavailability", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({
                    startDate: this.startDate,
                    endDate: this.endDate,
                    campingSpotId: this.spotId,
                    accomodationId: this.accomodation.id,

                })
            })
            .then(response => {
                if(!response.ok){
                    throw new Error("Network response was not ok at CheckIfAvailable");
                }
                return response.json();
            })
            .then(data => {
                console.log(data);
                this.isAvailable = data;
                console.log(this.isAvailable + " is the availability");

                if(this.isAvailable == false){
                    console.log("This spot is not available for the selected dates");
                    alert("This spot is not available for the selected dates");
                    return;
                }
                if(this.isAvailable == true){
                    console.log("This spot is available for the selected dates");
                    alert("This spot is available for the selected dates");
                    this.AddBooking();
                }else{
                    alert("This spot is not available for the selected dates");
                }
            })
            },
            AddBooking(){
                fetch("https://localhost:5162/Booking", {
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
                        price: this.price,
                        OwnerId: this.Spot.ownerId
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
            GetAccomdationById(id){
                fetch("https://localhost:5162/Accomodation/" + id, {
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