<template>
    <div class="div-login">
        <div class="flex flex-col items-center">
            <div class="top-left-button"> 
                <button class="custom-button" @click="ChangePage('ownermain')">
                    return to main page
                </button>
            </div>
            <h1 class="h1">Add Campingspot</h1>
                <label class="label-strong" for="name">Name:</label>
                <input class="custom-input" type="text" id="name" v-model="name" required>
                <label class="label-strong" for="price">Price:</label>
                <input class="custom-input" type="number" id="price" v-model="price" required>
                <label class="label-strong" for="description">Description:</label>
                <textarea class="custom-input" type="text" id="description" v-model="description" required></textarea>
                <label class="label-strong" for="image">Image:</label>
                <input class="custom-input" type="file" id="image" @change="handleImageUpload" required>
                <img :src="imagePreview" alt="Selected image" v-if="imagePreview" />

                <label class="label-strong" for="country">Country:</label>
                <select v-model="country">
                    <option v-for="country in countries" :key=" 'country-' + country.id" :value="country.id">
                        {{country.name}}
                    </option>
                </select>
                <label class="label-strong" for="city">City:</label>
                <select v-model="city">
                    <option v-for="city in cities" :key=" 'city-' + city.id" :value="city.id">
                        {{city.name}}
                    </option>
                </select>

                <label class="label-strong" for="street">Street:</label>
                <input class="custom-input" type="text" id="street" v-model="street" required>

                <label class="label-strong" for="number">Number:</label>
                <input class="custom-input" type="text" id="number" v-model="number" required>
                
                <!-- <select name="tagsId" multiple>
                    <option v-for="tag in tags" :key=" 'tag-' + tag.id" :value="tag.id">
                        {{tag.name}}
                    </option>
                </select> -->
                <button class="custom-button" type="submit" @click="handleAddCampingspot()">Add Campingspot</button>
        </div>
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
                imagePreview: null
            }
        },
        methods:{
            previewImage(event) {
                const selectedFile = event.target.files[0];
                if (selectedFile) {
                    const reader = new FileReader();
                    reader.onload = () => {
                        this.imagePreview = reader.result;
                    };
                    reader.readAsDataURL(selectedFile);
                }
            },
            handleImageUpload(event){
                const file = event.target.files[0];
                //log the file name
                console.log(file.name + " is the file name");
                console.log(file + " is the file");
                console.log(this.image + " is the image");
                this.UploadImage(file);
                this.image = file.name;
                this.previewImage(event);
            },
            UploadImage(file){
                const formData = new FormData();
                formData.append("file", file);
                fetch("https://localhost:5162/api/UploadImage", {
                    method: "POST",
                    body: formData
                })
                .then(response => {
                    if(!response.ok){
                        throw new Error("Network response was not ok at UPLOADIMAGE");
                    }
                    return response.json();
                })
                .then(data => {
                    console.log(data);
                    
                })
                .catch(error => {
                    console.error("There has been a problem with your fetch operation: UPLOADIMAGE", error);
                })
            },
            GetTags(){
                fetch("https://localhost:5162/Tag", {
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
                this.AddLocation();
                // this.locationId = locationData.id;
                // this.AddCampingSpot();
                // //for each id in tagsId, add a tag to the campingspot
                // this.tagsId.forEach(id => {
                //     this.AddSpotTag(id);
                // });
                //     this.push({name: "OwnerMainPage", params: {id: this.ownerId}});
                //     this.ChangePage("ownermain");
                    
                } catch (error) {
                console.error("Error:", error);
                }
            },
            AddSpotTag(id){
                fetch("https://localhost:5162/SpotTag", {
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
                fetch("https://localhost:5162/Campingspot", {
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
                    return response.json();
                })
                .then(data => {
                    console.log(data + " is the data");
                    
                    alert("Campingspot added!");
                    this.$router.push({name:"AddAccomodationPage", params: {id: data}});
                    this.$emit("changeActivePage", "addaccomodation");

                })
                .catch(error => {
                    console.error("There has been a problem with your fetch operation: ADDUSER", error);
                })
            },
            AddLocation(){
                console.log(this.country, this.city, this.street, this.number)
                fetch("https://localhost:5162/Location", {
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
                fetch("https://localhost:5162/City/country/" + id, {
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
                fetch("https://localhost:5162/Country", {
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