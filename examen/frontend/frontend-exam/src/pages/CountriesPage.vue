<template>
    <div class="flex flex-col items-center">
        <button class="custom-button" @click="ChangePage('admin')">
            return to main page
        </button>
        <h1>Countries</h1>
        <h2>Add Country</h2>
        <input class="custom-input" type="text" v-model="countryName" placeholder="Country Name">
        <button class="custom-button" @click="AddCountry()">
            ADD COUNTRY
        </button>
        <br>
        <h2>Add City</h2>
        <select v-model="countryId">
            <option v-for="country in countries" :key=" 'country-' + country.id" :value="country.id">
                {{country.name}}
            </option>
        </select>
        <input class="custom-input" type="text" v-model="cityName" placeholder="City Name">
        <input class="custom-input" type="text" v-model="PostalCode" placeholder="Postal Code">
        <button class="custom-button" @click="AddCity()">
            ADD CITY
        </button>


        <h2>COUNTRY LIST</h2>


        <button class="custom-button" @click="GetCountries()">
            Get Country List
        </button>
        <br>
        <ul>
            <li v-for="country in countries" :key=" 'country-' + country.id">
                <strong>name :</strong> {{country.name}} <br>
            </li>
        </ul>
        <button class="custom-button" @click="GetCities()">
            Get City List
        </button>
        <ul>
            <li v-for="city in cities" :key=" 'city-' + city.id">
                <strong>name :</strong> {{city.name}} <br>
                <strong>country :</strong> {{GetCountryById(city.countryId)}} <br>
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
                cities: [],
                countryName: "",
                cityName: "",
                countryId: "",
                PostalCode: "",
            }
        },
        mounted(){
            this.GetCountries();
            this.GetCities();
        },
        methods:{
            ChangePage(page) {
                this.$emit("changeActivePage", page);
            },
            GetCountryById(id){
                const country = this.countries.find(country => country.id == id);
                return country ? country.name : "Unknown Country";
            },
            GetCountries(){
                fetch("https://localhost:5162/Country", {
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
                fetch("https://localhost:5162/Country", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify({
                        name: this.countryname
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
                    alert("Country added");
                })
                .catch(error => {
                    console.error("There was an error!", error);
                });
            
            },
            AddCity(){
                fetch("https://localhost:5162/City", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify({
                        name: this.cityName,
                        countryId: this.countryId,
                        postalCode: this.PostalCode
                    })
                })
                .then(response => {
                    if(!response.ok){
                        throw new Error("Network response was not ok at ADDCITY");
                    }
                    else{
                        alert("City added");
                        return response;
                    }
                })
                .then(data => {
                    console.log(data);
                })
                .catch(error => {
                    console.error("There was an error!", error);
                });
            },
            GetCities(){
                fetch("https://localhost:5162/City", {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => response.json())
                .then(data => {
                    console.log(data);
                    this.cities = data;
                })
                .catch(error => {
                    console.error("There was an error!", error);
                });
            },
        },
        
    }
</script>