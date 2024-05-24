<template>
    <div>
        <button @click="ChangePage('usermain')">
            back to main page
        </button>
        <H2>Welcome {{ user.username }}</H2>

        <input type="text" v-model="fn" value="Firstname">
        <input type="text" v-model="ln" placeholder = "Lastname">
        <input type="text" v-model="email">
        <input type="text" v-model="username">
        <input type="password" v-model="password" placeholder="Password">
        <button @click="UpdateUser(user.id)">
            Update Account
        </button>

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