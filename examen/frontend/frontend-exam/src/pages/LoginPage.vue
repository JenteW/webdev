<template>
    <div>
        <h1>Login</h1>
        <input type="text" v-model="username" placeholder="Username">
        <input type="password" v-model="password" placeholder="Password">
        <input type="checkbox" v-model="checked">
        <label for="checked">owner</label>
        <button @click="ButtonClick()">
            Login
        </button>

        <button @click="GoToAddUser()">
            Make New Account
        </button>
        <button @click="GoToAdminLogin()">
            admin
        </button>

        
    </div>
</template>

<script>
    export default {
        name: 'LoginPage',
        data(){
            return{
                username: "",
                password: "",
                checked: false
            }
        },
        methods:{
            // depending on the checkbox, it will either log in as an owner or a user
            ButtonClick(){
                if(this.checked == true){
                    console.log("Owner");
                    this.OwnerLogin(this.username, this.password);
                }
                else{
                    console.log(this.username + " is the username");
                    console.log(this.password + " is the password");
                    this.UserLogin(this.username, this.password);
                    console.log("User");
                }
                console.log("Button Clicked");
            },
            ChangePage(page) {
                this.$emit("changeActivePage", page);
            },
            GoToAddUser(){
                this.$router.push({name: "AddUserPage"});
                this.$emit("changeActivePage", "adduser");
            },
            GoToAdminLogin(){
                this.$router.push({name: "AdminPage"});
                this.$emit("changeActivePage", "adminlogin");
            },
            UserLogin(username, password){
                fetch("https://localhost:5162/User/login/" + username +"?password=" + password, {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => {
                    if(response.status === 404){
                        alert("Username or password is not correct!");
                        throw new Error("user not found at LOGIN");
                    }
                    else if(response.status === 400){
                        alert("Please fill in both username and password!");
                        throw new Error("username or password empty");
                    }
                    else if(!response.ok){
                         throw new Error("Network response was not ok at LOGIN");
                    }
                    else{
                        return response.json();
                    }
                })
                .then(data => {
                    console.log(data);
                    if(data.id > 0){
                        this.$router.push({name: 'UserMainPage', params: {id: data.id}});
                        console.log(data.id + " is id");
                        this.ChangePage("usermain");
                    }
                    else{
                        alert("Go and suck dick");
                    }
                })
                .catch(error => {
                    console.error("There has been a problem with your fetch operation: LOGIN", error);
                })
                

            },
            OwnerLogin(username, password){
                fetch("https://localhost:5162/Owner/login/" + username +"?password=" + password, {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => {
                    if(response.status === 404){
                        alert("Username or password is not correct!");
                        throw new Error("user not found at LOGIN");
                    }
                    else if(response.status === 400){
                        alert("Please fill in both username and password!");
                        throw new Error("username or password empty");
                    }
                    else if(!response.ok){
                         throw new Error("Network response was not ok at LOGIN");
                    }
                    else{
                        return response.json();
                    }
                })
                .then(data => {
                    console.log(data);
                    if(data.id > 0){
                        this.$router.push({name: 'OwnerMainPage', params: {id: data.id}});
                        console.log(data.id + " is id");
                        this.ChangePage("ownermain");
                    }
                    else{
                        alert("Go and suck dick");
                    }
                })
                .catch(error => {
                    console.error("There has been a problem with your fetch operation: LOGIN", error);
                })
                

            }
            
        }
    }
</script>