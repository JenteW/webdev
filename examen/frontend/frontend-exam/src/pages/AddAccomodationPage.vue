<template>
    <div class="flex flex-col items-center">
        <h1 class="h1">Detailed View</h1>
        <div class="top-left-button">
            <button class="custom-button" @click="ToOwner()">
            Return to main page
            </button>
        </div>
        <br>
        <div class="flex flex-col items-center w-3/4">
            <h2 class="h2 mr-2">SpotDetails</h2>
            <h3 class="h3 text-center mr-2 p-4">Name: {{spot.name}}</h3>
            <p class=" mr-2 p-2">Description: {{spot.description}}</p>
            <p class="mr-2 p-2">Price per night: €{{spot.price}}</p>
            <div class="flex justify-center w-full">
                <img :src="require('@/assets/campingspots/' + spot.image)" alt="campingspot image"
                class="mr-4 p-2"
                contain
                height="200px"
                width="300px"/>
            </div>
            <div class="text-center">
                <input type="checkbox" v-model="available" name="checkavailability" id="check">
                <label for="check">The spot is available on the site: {{ this.available }}</label>
            </div>
            <button class="edit-button" @click="UpdateSpot()">
                    Update Availability Spot
            </button>
            <button @click="toggleEdit" class="edit-button">Edit</button>
            <div class="div-flex w-2/3" v-if="editing">
                <input v-model="spot.name" type="text" class="custom-input" placeholder="Name">
                <textarea v-model="spot.description" class="custom-input" placeholder="Description"></textarea>
                <input v-model="spot.price" type="number" class="custom-input" placeholder="Price per night">
                <div class="flex justify-center w-full">
                    <button @click="UpdateSpot()" class="custom-button-green">Save</button>

                </div>
            </div>
        </div>
        <h2 class="h2">Comments</h2>
        <button v-if="comment == false" class="half-button" @click="GetCommentsBySpotId(campingSpotId)">Get Comments</button>
        <button v-else class="half-button" @click="CloseComments()">Close Comments</button>
        <div class="div-flex">
            <div class="the-box" v-for="comment in comments" :key="comment.id">
                <p class="text-xl"><strong>Rating: </strong>{{comment.rating}}/5</p>
                <p class="p-2 m-2">{{comment.description}}</p>
            </div>
        </div>

        <h2 class="h2">Accomodations</h2>
        <button class="half-button" @click="GetAccomodationBySpotId(campingSpotId)">Get accommodation</button>    
        <div class="div-flex">
           <div v-for="accomodation in accomodations" :key="accomodation.id">
                <h3 class="h3 text-center">{{accomodation.name}}</h3>
                <p class="text-center">{{accomodation.description}}</p>
                <div class="flex justify-center w-full">
                    <button class="half-button" @click="toggleEditAccomodation(accomodation.id)">Edit</button>
                    <button class="half-button-red" @click="DeleteAccomodation(accomodation.id)">Delete</button>
                </div>
                <div v-if="editingAccomodationId == accomodation.id" class="div-flex">
                    <input v-model="AccName" type="text" class="custom-input" placeholder="Name">
                    <textarea v-model="AccDescription" class="custom-input" placeholder="Description"></textarea>
                        <button class="custom-button" @click="UpdateAccomodation(accomodation.id)">
                        Update Accommodation
                        </button>
                    
                </div>
            </div>
        </div>

            <H2 class="h2">Tags</H2>
            <button class="half-button" @click="GetSpotTags(campingSpotId)">Get Tags</button>
            <div class="flex flex-wrap -mr-4 mb-2">
                <div v-for="tag in SpotTags" :key="tag.id" class="bg-green-300 p-4 rounded-md flex flex-wrap items-center mr-4 mb-2">
                    <p>{{getTagName(tag.tagId)}}</p>
                </div>
            </div>
        <H2 class="h2">Add tags</H2>
        <div class="flex flex-wrap">
            <div v-for="tag in tags" :key="tag.id" class="bg-blue-200 p-4 rounded-md flex items-center mr-4 mb-2">
                <input type="checkbox" v-model="tagsId" :value="tag.id">
                <label>{{tag.name}}</label>
            </div>
        </div>
        <button class="half-button" @click="addAllTags()">
            add Tags
        </button>
        <h2 class="h2">Add Accomodation</h2>
        <input class="custom-input" type="text" v-model="name" placeholder="Name">
        <textarea class="custom-input" type="text-area" v-model="description" placeholder="Description"/>
        <button class="half-button" @click="AddAccomodation()">
            Add Accomodation
        </button>
        <button class="half-button-red" @click="DeleteSpot()">
            Delete Spot
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
                AccName: "",
                AccDescription: "",
                name: "",
                description: "",
                campingSpotId: "",
                tagsId: [],
                SpotTags: [],
                SpotTagsIds: [],
                available: true,
                editing: false,
                editingAccomodation: false,
                editingAccomodationId: null,
                comments: [],
                comment: true

            }
        },
        mounted(){
            this.campingSpotId = this.$route.params.id;
            console.log(this.campingSpotId + " is the id @ AddAccomodationPage");
        -   this.GetSpot(this.campingSpotId);
            this.GetTags();
            this.GetSpotTags(this.campingSpotId);
            this.GetAccomodationBySpotId(this.campingSpotId);
            this.GetCommentsBySpotId(this.campingSpotId);


        },
        methods:{
            toggleEdit() {
            this.editing = !this.editing;
            },
            toggleEditAccomodation(accomodationId) {
            this.editingAccomodation = !this.editingAccomodation;
            this.editingAccomodationId = accomodationId;
            },
            getTagName(tagId) {
                const tag = this.tags.find(tag => tag.id == tagId);
                return tag ? tag.name : "Unknown Tag";
            },
            GetTags(){
                fetch("https://localhost:5162/Tag", {
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
            GetSpotTags(id){
                fetch("https://localhost:5162/SpotTag/Spot/" + id, {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => {
                    if(response.status === 404){
                        alert("No tags found!");
                        throw new Error("No tags found");
                    }
                    else if(!response.ok){
                        throw new Error("Network response was not ok at GetSpotTags");
                    }
                    else{
                        return response.json();
                    }
                })
                .then(data => {
                    console.log(data);
                    this.SpotTags = data;
                })
                .catch(error => {
                    console.error("There was a problem with your fetch operation:", error);
                });
            },
            AddSpotTag(id){
                fetch("https://localhost:5162/SpotTag", {
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
                        return response;
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
            addAllTags(){
                this.tagsId.forEach(tagId => {
                    if(!this.SpotTags.find(spotTag => spotTag.tagId == tagId))
                    this.AddSpotTag(tagId);
                    else{
                        alert("Tag already added!");
                    }
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
                fetch("https://localhost:5162/CampingSpot/" + id, {
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
                    this.available = this.spot.availability;

                })
                .catch(error => {
                    console.error("There was a problem with your fetch operation:", error);
                });
            },
            AddAccomodation(){
                fetch("https://localhost:5162/Accomodation", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify({
                        name: this.name,
                        description: this.description,
                        campingSpotId: this.campingSpotId
                    })
                })
                .then(response => {
                    if(!response.ok){
                        throw new Error("Network response was not ok at AddAccomodation");
                    }
                    else{
                        this.name = "";
                        this.description = "";
                        alert("Accomodation added!");
                        return response;

                    }
                })

                .catch(error => {
                    console.error("There was a problem with your fetch operation:", error);
                });
            },
            GetAccomodationBySpotId(id){
                console.log(id + " is the id @ GetAccomodationBySpotId")
                fetch("https://localhost:5162/Accomodation/CampingSpotId/" + id, {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => {
                    if(response.status === 404){
                        alert("No accomodations found!");
                        throw new Error("No accomodations found");
                    }
                    else if(!response.ok){
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
            UpdateSpot(){
                fetch("https://localhost:5162/CampingSpot/" + this.spot.id, {
                    method: "PUT",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify({
                        id: this.spot.id,
                        name: this.spot.name,
                        description: this.spot.description,
                        price: this.spot.price,
                        image: this.spot.image,
                        ownerId: this.spot.ownerId,
                        availability: this.available
                    })
                })
                .then(response => {
                    if(!response.ok){
                        throw new Error("Network response was not ok at UpdateSpot");
                    }
                    else{
                        return response;
                    }
                })
                .then(data => {
                    console.log(data);
                    alert("Spot updated!");
                })
                .catch(error => {
                    console.error("There was a problem with your fetch operation:", error);
                });
            },
            DeleteSpot(){
                fetch("https://localhost:5162/CampingSpot/" + this.spot.id, {
                    method: "DELETE",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => {
                    if(!response.ok){
                        throw new Error("Network response was not ok at DeleteSpot");
                    }
                    else{
                        return response;
                    }
                })
                .then(data => {
                    console.log(data);
                    alert("Spot deleted!");
                    this.ToOwner();
                })
                .catch(error => {
                    console.error("There was a problem with your fetch operation:", error);
                });

            },
            UpdateAccomodation(id){
                console.log(id + " is the id @ UpdateAccomodation");
                console.log(this.AccName+ " is the name @ UpdateAccomodation");
                console.log(this.AccDescription + " is the description @ UpdateAccomodation");
                console.log(this.spot.id + " is the campingSpotId @ UpdateAccomodation");
                fetch("https://localhost:5162/Accomodation/" + id, {
                    method: "PUT",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify({
                        id: id,
                        name: this.AccName,
                        description: this.AccDescription,
                        campingSpotId: this.spot.id
                    })
                })
                .then(response => {
                    if(!response.ok){
                        throw new Error("Network response was not ok at UpdateAccomodation");
                    }
                    else{
                        return response;
                    }
                })
                .then(data => {
                    console.log(data);
                    this.toggleEditAccomodation(null);
                    alert("Accomodation updated!");
                    this.GetAccomodationBySpotId(this.campingSpotId);
                })
                .catch(error => {
                    console.error("There was a problem with your fetch operation:", error);
                });
            },
            DeleteAccomodation(id){
                fetch("https://localhost:5162/Accomodation/" + id, {
                    method: "DELETE",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => {
                    if(!response.ok){
                        throw new Error("Network response was not ok at DeleteAccomodation");
                    }
                    else{
                        return response;
                    }
                })
                .then(data => {
                    console.log(data);
                    alert("Accomodation deleted!");
                    this.GetAccomodationBySpotId(this.campingSpotId);
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
                    if(response.status === 404){
                        alert("No comments found!");
                        throw new Error("No comments found");
                    }
                    else if(!response.ok){
                        throw new Error("Network response was not ok at GetCommentsBySpotId");
                    }
                    else{
                        return response.json();
                    }
                })
                .then(data => {
                    console.log(data);
                    this.comments = data;
                    this.comment = true;
                })
                .catch(error => {
                    console.error("There was a problem with your fetch operation:", error);
                });
            },
            CloseComments(){
                this.comments = [];
                this.comment = false;
            }
        }
    }

</script>