<template>
    <div>
        <h1>User Main Page</h1>
        <button @click="ChangePage('login')">
            back to login
        </button>
        <H2>Welcome {{ user.username }}</H2>
        <button @click="GoToUpdate()">
            Update Account
        </button>
        <br>
        <h2>Available Camping Spots</h2>
        <h3>Filter by:</h3>

        <div v-for="campingspot in CampingSpots" :key="campingspot.id">
            <h3>{{campingspot.name}}</h3>
            <p>{{campingspot.description}}</p>
            <p>€{{campingspot.price}}</p>
            <img :src="require('@/assets/campingspots/' + campingspot.image)" alt="campingspot image"
            contain
            height="200px"
            width="300px"/>
            <br>
            <button @click="GoToAccomodations(campingspot.id)">
                see more
            </button>
            <br>
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
        },
        data(){
            return{
                user: {},
                id: "",
                CampingSpots: [],

            }
        },
        
        methods:{

            ChangePage(page) {
                this.$emit("changeActivePage", page);
            },
            GoToUpdate(){
                this.$router.push({name: "UpdateUserPage", params: {id: this.id}});
                this.$emit("changeActivePage", "updateuser");
            },
            GetAllCampingSpots(){
                fetch("http://localhost:5162/CampingSpot", {
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
                fetch("http://localhost:5162/User/" + id, {
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