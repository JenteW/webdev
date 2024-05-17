<template>
    <div>
        <button @click="ChangePage('admin')">
            return to main page
        </button>
        <h1>Countries</h1>
        
        <input type="text" v-model="name" placeholder="Country Name">
        <button @click="AddCountry()">
            ADD COUNTRY
        </button>
        <h2>COUNTRY LIST</h2>
        <button @click="GetCountries()">
            Get Country List
        </button>
        <ul>
            <li v-for="country in countries" :key=" 'country-' + country.id">
                <strong>name :</strong> {{country.name}} <br>
            </li>
        </ul>

    </div>
</template>

<script>
    export default {
        name: 'CountriesPage',
        data(){
            return{
                countries: [],
                name: "",
            }
        },
        mounted(){
            this.GetCountries();
        },
        methods:{
            ChangePage(page) {
                this.$emit("changeActivePage", page);
            },
            GetCountries(){
                fetch("http://localhost:5162/Country", {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => {
                    if(!response.ok){
                         throw new Error("Network response was not ok at GETCOUNTRIES");
                    }
                    else{
                        return response.json();
                    }
                })
                .then(data => {
                    console.log(data);
                    this.countries = data;
                })
                .catch(error => {
                    console.error("There was an error!", error);
                });
            },
            AddCountry(){
            fetch("http://localhost:5162/Country", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({
                    name: this.name
                })
            })
            .then(response => {
                if(!response.ok){
                     throw new Error("Network response was not ok at ADDCOUNTRY");
                }
                else{
                    return response;
                }
            })
            .then(data => {
                console.log(data);
                this.GetCountries();
            })
            .catch(error => {
                console.error("There was an error!", error);
            });
            
        }
        },
        
    }
</script>