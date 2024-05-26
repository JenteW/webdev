<template>
    <div class="div-login">
        <div class="div-flex w-3/4">
            <div class="top-left-button">
                <button class="custom-button" @click="ChangePage('usermain')">
                    Back to main page
                </button>
            </div>
            <H2 class="h2">Welcome {{ user.username }}, below you can change your user information</H2>

            <input class="custom-input" type="text" v-model="fn" value="Firstname">
            <input class="custom-input" type="text" v-model="ln" placeholder = "Lastname">
            <input class="custom-input" type="text" v-model="email">
            <input class="custom-input" type="text" v-model="username">
            <input class="custom-input" type="password" v-model="password" placeholder="Password">
            <button class="half-button" @click="UpdateUser(user.id)">
                Update Account
            </button>
        </div>
    </div>
</template>

<script>
    export default {
        name: 'UpdateUserPage',
        mounted(){
            this.id = this.$route.params.id;
            console.log(this.id + " is the id @ UpdateUserPage");
            this.GetUser(this.id);
        },
        data(){
            return{
                user: {},
                id: "",
                fn: "",
                ln: "",
                username: "",
                password: "",
                email: "",
                isDeletedByAdmin: false
            }
        },
        methods:{
            ChangePage(page) {
                this.$emit("changeActivePage", page);
            },
            GetUser(id){
                fetch("https://localhost:5162/User/" + id, {
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
                    this.fn = this.user.fn;
                    this.ln = this.user.ln;
                    this.username = this.user.username;
                    this.email = this.user.email;
                })
                .catch(error => {
                    console.error("There was an error!", error);
                });
            },
            UpdateUser(){
                fetch("https://localhost:5162/User/" + this.id, {
                    method: "PUT",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify({
                            id: this.id,
                            fn: this.fn,
                            ln: this.ln,
                            username: this.username,
                            password: this.password,
                            email: this.email,
                            })
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
                        console.log("changed user @ response.json");
                        return response;
                    }
                })
                .then(() =>{
                    this.GetUser(this.id);
                    alert("User updated!");
                                })
                .catch(error => {
                    console.error("There was an error!", error);
                });
            }
            
        }
        
    }
</script>