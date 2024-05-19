<template>
    <div>
        <button @click="ChangePage('admin')">
            return to admin page
        </button>
        <h1>Add Campingspot</h1>
        <form @submit.prevent="AddCampingspot">
            <label for="name">Name:</label>
            <input type="text" id="name" v-model="name" required>
            <br>
            <label for="price">Price:</label>
            <input type="number" id="price" v-model="price" required>
            <br>
            <label for="description">Description:</label>
            <input type="text" id="description" v-model="description" required>
            <br>
            
            <label for="location">Location:</label>
            <input type="text" id="location" v-model="location" required>
            <br>
            <label for="image">Image:</label>
            <input type="text" id="image" v-model="image" required>
            <br>
            <button type="submit">Add Campingspot</button>
        </form>
    </div>
</template>

<script>
    export default {
        name: 'MyCampingspotPage',
        data(){
            return{
                
                name: "",
                locationId: "",
                description: "",
                ownerId: "",
                price: "",
                image: "",
                country: "",
                city: "",
                street: "",
                number: "",
                

                availability: true,
            }
        },
        methods:{
            ChangePage(page) {
                this.$emit("changeActivePage", page);
            },
            AddCampingspot(){
                fetch("http://localhost:5162/Campingspot", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify({
                        name: this.name,
                        price: this.price,
                        description: this.description,
                        ownerId: this.ownerId,
                        location: this.location,
                        rating: this.rating,
                        image: this.image
                    })
                })
                .then(response => {
                    if(response.status === 400){
                        alert("Please fill in all fields!");
                        throw new Error("fields empty at ADDCAMPINGSPOT");
                    }
                    else if(!response.ok){
                         throw new Error("Network response was not ok at ADDCAMPINGSPOT");
                    }
                    else{
                        return response.json();
                    }
                })
                .then(data => {
                    console.log(data);
                    console.log("is fecking data");
                    this.$router.push({name: "AdminPage", params: {id: 1}});
                })
                .catch(error => {
                    console.error("Error:", error);
                });
            }
        }
    }
</script>