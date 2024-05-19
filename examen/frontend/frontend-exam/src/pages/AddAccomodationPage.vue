<template>
    <div>
        <h1>Add Accomodation</h1>
        <button @click="ToOwner()">
            return to main page
        </button>
        <h2>SpotDetails</h2>

            <h3>{{spot.name}}</h3>
            <p>{{spot.description}}</p>
            <p>€{{spot.price}}</p>
            <p>{{spot.availability}}</p>
            <img :src="require('@/assets/campingspots/' + spot.image)" alt="campingspot image"
            contain
            height="200px"
            width="300px"/>
            <br>
            <H2>add Tags</H2>
            <div v-for="tag in tags" :key="tag.id">
                <input type="checkbox" v-model="tagsId" :value="tag.id">
                <label>{{tag.name}}</label>
            </div>
            <button @click="addAllTags()">
                add Tags
            </button>
            <h2>Add Accomodation</h2>
            <input type="text" v-model="name" placeholder="Name">
            <br>
            <input type="text" v-model="description" placeholder="Description">
            <br>
            <button @click="AddAccomodation()">
                Add Accomodation
            </button>
    </div>
</template>

<script>
    export default {
        name: 'AddAccomodationPage',
        data(){
            return{
                spot: {},
                accomodations: [],
                tags: [],
                name: "",
                description: "",
                campingSpotId: "",
                tagsId: []

            }
        },
        mounted(){
            this.campingSpotId = this.$route.params.id;
            console.log(this.campingSpotId + " is the id @ AddAccomodationPage");
        -   this.GetSpot(this.campingSpotId);
            this.GetTags();
        },
        methods:{
            GetTags(){
                fetch("http://localhost:5162/Tag", {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => {
                    if(!response.ok){
                        throw new Error("Network response was not ok at GetTags");
                    }
                    else{
                        return response.json();
                    }
                })
                .then(data => {
                    console.log(data);
                    this.tags = data;
                })
                .catch(error => {
                    console.error("There was a problem with your fetch operation:", error);
                });
            },
            addAllTags(){
                this.tagsId.forEach(tagId => {
                    this.AddSpotTag(tagId);
                });
            },
            AddSpotTag(id){
                fetch("http://localhost:5162/SpotTag", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify({
                        spotId: this.spot.id,
                        tagId: id
                    })
                })
                .then(response => {
                    if(!response.ok){
                        throw new Error("Network response was not ok at AddSpotTag");
                    }
                    else{
                        return response.json();
                    }
                })
                .then(data => {
                    console.log(data);
                    this.GetSpotTags(this.campingSpotId);
                })
                .catch(error => {
                    console.error("There was a problem with your fetch operation:", error);
                });
            },
            changePage(page){
                this.$emit("changeActivePage", page);
            },
            ToOwner(){
                this.$router.push({name: "OwnerMainPage", params: {id: this.spot.ownerId}});
                this.$emit("changeActivePage", "ownermain");
            },
            GetSpot(id){
                fetch("http://localhost:5162/CampingSpot/" + id, {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => {
                    if(response.status === 404){
                        alert("CampingSpot not found!");
                        throw new Error("CampingSpot not found");
                    }
                    else if(!response.ok){
                        throw new Error("Network response was not ok at GetSpot");
                    }
                    else{
                        return response.json();
                    }
                })
                .then(data => {
                    console.log(data);
                    this.spot = data;
                })
                .catch(error => {
                    console.error("There was a problem with your fetch operation:", error);
                });
            },
        }
    }

</script>