<template>
    <div>
        <button @click="ChangePage('ownermain')">
            return to main page
        </button>
        <h1>Add Campingspot</h1>
            <label for="name">Name:</label>
            <input type="text" id="name" v-model="name" required>
            <br>
            <label for="price">Price:</label>
            <input type="number" id="price" v-model="price" required>
            <br>
            <label for="description">Description:</label>
            <textarea type="text" id="description" v-model="description" required></textarea>
            <br>
            <label for="image">Image:</label>
            <input type="text" id="image" v-model="image" required>
            <br>
            <img :src="ImagePath" alt="campingspot image" v-if="image"
            contain
            height="100px"
            width = "150px"/>
            <br>
            <label for="country">Country:</label>
            <select v-model="country">
                <option v-for="country in countries" :key=" 'country-' + country.id" :value="country.id">
                    {{country.name}}
                </option>
            </select>
            <br>
            <label for="city">City:</label>
            <select v-model="city">
                <option v-for="city in cities" :key=" 'city-' + city.id" :value="city.id">
                    {{city.name}}
                </option>
            </select>
            <br>
            <label for="street">Street:</label>
            <input type="text" id="street" v-model="street" required>
            <br>
            <label for="number">Number:</label>
            <input type="text" id="number" v-model="number" required>
            <br>
            <select name="tagsId" multiple>
                <option v-for="tag in tags" :key=" 'tag-' + tag.id" :value="tag.id">
                    {{tag.name}}
                </option>
            </select>
            <button type="submit" @click="handleAddCampingspot()">Add Campingspot</button>
    </div>
</template>

<script>
    export default {
        name: 'MyCampingspotPage',
        mounted(){
            this.ownerId = this.$route.params.id;
            console.log(this.ownerId + " is the id @ MyCampingspotPage")
            this.GetCountry();
            this.GetTags();
        },
        watch: {
            country: function(){
                this.GetCities(this.country);
            }
            
        },
        computed: {
            ImagePath(){
                return require("@/assets/campingspots/"+this.image);
            }
        },
        data(){
            return{
                countries: [],
                cities: [],
                tags: [],
                tagsId: [],
                name: "",
                locationId: 0,
                description: "",
                ownerId: 0,
                price: 0,
                image: "",
                country: 0,
                city: "",
                street: "",
                number: "",
                availability: true,
                spotId: "",
            }
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
                    if(response.status === 404){
                        alert("Tag not found!");
                        throw new Error("tag not found at GETTAGS");
                    }
                    else if(!response.ok){
                         throw new Error("Network response was not ok at GETTAGS");
                    }
                    else{
                        return response.json();
                    }
                })
                .then(data => {
                    console.log(data);
                    console.log("is fecking data");
                    this.tags = data;
                })
                .catch(error => {
                    console.error("Error:", error);
                });
            
            },
            handleAddCampingspot() {
                try {
                if (!this.name || !this.price || !this.description || !this.image || !this.country || !this.city || !this.street || !this.number) {
                alert("Please fill in all fields!");
                return;
                }
                const locationData =  this.AddLocation();
                this.locationId = locationData.id;
                this.AddCampingSpot();
                //for each id in tagsId, add a tag to the campingspot
                this.tagsId.forEach(id => {
                    this.AddSpotTag(id);
                });
                    
                } catch (error) {
                console.error("Error:", error);
                }
            },
            AddSpotTag(id){
                fetch("http://localhost:5162/SpotTag", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify({
                        spotId: this.spotId,
                        tagId: id
                    })
                })
                .then(response => {
                    if(!response.ok){
                        throw new Error("Network response was not ok at ADDSPOTTAG");
                    }
                    return response;
                })
                .then(data => {
                    console.log(data);
                })
                .catch(error => {
                    console.error("There has been a problem with your fetch operation: ADDSPOTTAG", error);
                })
            },
            ChangePage(page) {
                this.$emit("changeActivePage", page);
            },
            AddCampingspot(){
                console.log(this.name, this.locationId, this.description, this.ownerId, this.price, this.image, this.availability);
                console.log(this.locationId + " is the location id");
                fetch("http://localhost:5162/Campingspot", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify({
                        name: this.name,
                        locationId: this.locationId,
                        description: this.description,                        
                        ownerId: this.ownerId,
                        price: this.price,
                        image: this.image,
                        availability: true
                    })
                })
                .then(response => {
                    if(!response.ok){
                        throw new Error("Network response was not ok at ADDUSER");
                    }
                    return response;
                })
                .then(data => {
                    console.log(data);
                })
                .catch(error => {
                    console.error("There has been a problem with your fetch operation: ADDUSER", error);
                })
            },
            AddLocation(){
                console.log(this.country, this.city, this.street, this.number)
                fetch("http://localhost:5162/Location", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify({
                        countryId: this.country,
                        cityId: this.city,
                        street: this.street,
                        number: this.number,
                        latitude: "7",
                        longitude: "7"
                    })
                })
                .then(response => {
                    if(response.status === 400){
                        console.log(response.status + " is the status");
                        console.log(response + " is the response");
                        console.log(response.json() + " is the response.json");
                        alert("Please fill in all fields!");
                        throw new Error("fields empty at ADDLOCATION");
                    }
                    else if(!response.ok){
                         throw new Error("Network response was not ok at ADDLOCATION");
                    }
                    else{
                        console.log("first hurdle cleared");
                        return response.json();
                    }
                })
                .then(data => {
                    console.log(data);
                    console.log("is fecking data");
                    this.locationId = data.id;
                    console.log(this.locationId + " is the location id" + data.id);
                    this.AddCampingspot();
                })
                .catch(error => {
                    console.error("Error:", error);
                });
            },
            GetCities(id){
                fetch("http://localhost:5162/City/country/" + id, {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => {
                    if(response.status === 404){
                        alert("City not found!");
                        throw new Error("city not found at GETCITIES");
                    }
                    else if(!response.ok){
                         throw new Error("Network response was not ok at GETCITIES");
                    }
                    else{
                        return response.json();
                    }
                })
                .then(data => {
                    console.log(data);
                    console.log("is fecking data");
                    this.cities = data;
                })
                .catch(error => {
                    console.error("Error:", error);
                });
            },
            GetCountry(){
                fetch("http://localhost:5162/Country", {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => {
                    if(response.status === 404){
                        alert("Country not found!");
                        throw new Error("country not found at GETCOUNTRY");
                    }
                    else if(!response.ok){
                         throw new Error("Network response was not ok at GETCOUNTRY");
                    }
                    else{
                        return response.json();
                    }
                })
                .then(data => {
                    console.log(data);
                    console.log("is fecking data");
                    this.countries = data;
                })
                .catch(error => {
                    console.error("Error:", error);
                });
            },
        }
    }
</script>