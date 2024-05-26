<template>
    <div class="flex flex-col items-center">
        <H1 class="h1">Detailed View</H1>
        <div class="top-left-button">
            <button class="custom-button" @click="GoToCampingSpots()">
                Return to main page
            </button>
        </div>
        <H2 class="h2">Spot</H2>
        <div class="div-flex bg-blue-200 rounded-md w-10/12">
            <h3 class="h3 text-center mr-2 p-4">{{ Spot.name }}</h3>
            <p class=" mr-2 p-2">{{ Spot.description }}</p>
            <p class=" mr-2 p-2">€{{ Spot.price }} for a night</p>
            <img :src="require('@/assets/campingspots/' + Spot.image)" alt="campingspot image"
            contain
            class="mr-4 p-2"
            height="300px"
            width="300px">
        </div>
        <H2 class="h2 m-2">Accomodations</H2>
        <div class="div-flex">
            <div v-for="accomodation in accomodations" :key="accomodation.id">
                <h3 class="h3 text-center">{{ accomodation.name }}</h3>
                <p class="text-center m-4"><strong>Description: </strong>{{ accomodation.description }}</p>
                <button class="custom-button" @click="GoToBooking(accomodation.id)">Book the accommodation</button>
            </div>
        </div>
        <div class="div-flex" v-if="allow == true">
            <H2 class="h2">Review the Spot here to help other adventurers find quality spots!</H2>
            <select v-model="rating" class="w-1/2">
                <option disabled value="">Please select your rating here</option>
                <option v-for="rating in ratings" :key="rating" :value="rating">
                    {{ rating }}
                </option>
            </select>
            <textarea class="custom-input" type="text-area" v-model="comment" placeholder="Comment"/>
            <button class="custom-button" @click="PostComment()">Post Comment</button>
        </div>
        <H2 class="h2 m-3">Comments</H2>
        <div class="div-flex">
            <div class="the-box" v-for="comment in comments" :key="comment.id">
                <p class="text-xl"><strong>Rating: </strong>{{ comment.rating }}/5</p>
                <p class="p-2 m-2">{{ comment.description }}</p>
            </div>
        </div>
    </div>
</template>

<script>

    export default {
        name: 'AccomodationsPage',
        data(){
            return{
                accomodations: [],
                bookings: [],
                comments: [],
                Spot: {},
                id: "",
                userId: "",
                comment: "",
                rating: "",
                allow: false,
                ratings: ["1", "2", "3", "4", "5"]
            }
        },
        mounted(){
            this.id = this.$route.params.id;
            this.userId = this.$route.params.userId;
            console.log(this.id + " is the id @ AccomodationsPage");
            this.GetSpotById(this.id);
            this.GetAccomodationBySpotId(this.id);
            this.GetCommentsBySpotId(this.id);
            console.log(this.comments + " is the comments");
            this.GetBookingsBySpotId(this.id);

            

        },
        methods:{
            GoToBooking(id){
                this.$router.push({name: "AddBookingPage", params: {id: id, userId: this.userId, spotId: this.id}});
                this.$emit("changeActivePage", "addbooking");
            },
            GetBookingsBySpotId(id){
                fetch("https://localhost:5162/Booking/campingSpotId/" + id, {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => {
                    if(!response.ok){
                        throw new Error("Network response was not ok at GetBookingsBySpotId");
                    }
                    else{
                        return response.json();
                    }
                })
                .then(data => {
                    console.log(data);
                    this.bookings = data;
                    console.log(this.bookings + " is the bookings");
                    this.bookings.find(booking => {
                        console.log(booking.userId + " is the booking userId");
                        console.log(this.userId + " is the userId");
                        if(booking.userId == this.userId){
                            this.allow = true;
                        }
                    });
                    this.CheckIfUserHasCommented();
                })
                .catch(error => {
                    console.error("There was a problem with your fetch operation:", error);
                });
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
            },
            PostComment() {
                console.log(this.comment + " is the comment");
                console.log(this.rating + " is the rating");
                console.log(this.id + " is the spotId");
                console.log(this.userId + " is the userId");
                
                fetch("https://localhost:5162/Comment", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify({
                        description: this.comment,
                        rating: this.rating,
                        spotId: this.id,
                        userId: this.userId
                    })
                })
                .then(response => {
                    if (!response.ok) {
                        throw new Error("Network response was not ok at PostComment");
                    }
                    return response;
                })
                .then(data => {
                    console.log(data);
                    alert("Comment added");
                    this.CheckIfUserHasCommented();
                    this.GetCommentsBySpotId(this.id);
                })
                .catch(error => {
                    console.error("There was a problem with your fetch operation:", error);
                });
            },
            GetCommentsBySpotId(id){
                fetch("https://localhost:5162/Comment/campingSpot/" + id, {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => {
                    if(!response.ok){
                        throw new Error("Network response was not ok at GetCommentsBySpotId");
                    }
                    else{
                        return response.json();
                    }
                })
                .then(data => {
                    console.log(data);
                    this.comments = data;
                })
                .catch(error => {
                    console.error("There was a problem with your fetch operation:", error);
                });
            },
            CheckIfUserHasCommented(){
                this.comments.find(comment => {
                    if(comment.userId == this.userId){
                        this.allow = false;
                    }
                });
            }
        }
    }
</script>