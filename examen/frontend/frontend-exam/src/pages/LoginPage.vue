<template>
    <div class="div-login">
      <div class="w-full max-w-md text-center">
        <h1 class="text-5xl font-bold mb-4">Ducks & Dragons</h1>
        <p class="text-2xl mb-4">Please login to continue your Grand Adventure!</p>
          <input class="custom-input"
            type="text"
            v-model="username"
            placeholder="Username"
          />
          <input
            class="custom-input"
            type="password"
            v-model="password"
            placeholder="Password"
          />

        <div class="mb-4 flex items-center justify-center">
          <input type="checkbox" v-model="checked" class="mr-2" />
          <label class="text-black text-center" for="checked">Login as an owner</label>
        </div>
        <div class="mb-4">
          <button class="custom-button" @click="ButtonClick()">
            Login
          </button>
        </div>
        <div class="mb-4">
          <button class="custom-button" @click="GoToAddUser()">
            Make New Account
          </button>
        </div>
        <div class="mb-4">
          <button class="custom-button" @click="GoToAdminLogin()">
            Admin
          </button>
        </div>
      </div>
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
                    this.OwnerLogin(this.username, this.password);
                }
                else{
                    
                    this.UserLogin(this.username, this.password);
                }
            },
            ChangePage(page) {
                this.$emit("changeActivePage", page);
            },
            GoToAddUser(){
                this.$router.push({name: "AddUserPage"});
                this.$emit("changeActivePage", "adduser");
            },
            GoToAdminLogin(){
                this.$router.push({name: "AdminLoginPage"});
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
                    if(data.id > 0){
                        this.$router.push({name: 'UserMainPage', params: {id: data.id}});
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
                        throw new Error("user not found at OwnerLogin");
                    }
                    else if(response.status === 400){
                        alert("Please fill in both username and password!");
                        throw new Error("username or password empty");
                    }
                    else if(!response.ok){
                         throw new Error("Network response was not ok at OwnerLogin");
                    }
                    else{
                        return response.json();
                    }
                })
                .then(data => {
                    if(data.id > 0){
                        this.$router.push({name: 'OwnerMainPage', params: {id: data.id}});
                        this.ChangePage("ownermain");
                    }
                    else{
                        alert("Go and suck dick");
                    }
                })
                .catch(error => {
                    console.error("There has been a problem with your fetch operation: OwnerLogin", error);
                })
                

            }
            
        }
    }
</script>
