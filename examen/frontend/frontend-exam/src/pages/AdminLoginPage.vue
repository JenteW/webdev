<template>
    <div class="div-login">
        <div class="w-full max-w-md text-center">
            <h1 class="h1">Login</h1>
            <br>
            <p>This is the admin login. You're not supposed to be here, are you?</p>
            <br>
            <input class="custom-input" type="text" v-model="username" placeholder="Username">
            <input class="custom-input" type="password" v-model="password" placeholder="Password">
            <button class="custom-button" @click="ButtonClick()">
                Login
            </button>

        
            <button class="custom-button" @click="GoToLogin()">
                User login
            </button>
        </div>
        
    </div>
</template>

<script>
    export default {
        name: 'AdminLoginPage',
        data(){
            return{
                username: "",
                password: "",
            }
        },
        methods:{
            ButtonClick(){
                this.AdminLogin(this.username, this.password);
            },
            ChangePage(page) {
                this.$emit("changeActivePage", page);
            },
            GoToLogin(){
                this.$router.push({name: "LoginPage"});
                this.$emit("changeActivePage", "login");
            },
            AdminLogin(username, password){
                fetch("https://localhost:5162/Admin/login/" + username +"?password=" + password, {
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
                         throw new Error("Network response was not ok at AdminLogin");
                    }
                    else{
                        return response.json();
                    }
                })
                .then(data => {
                    // route to admin page, while giving the id of the admin
                    this.$router.push({name: 'AdminPage', params: {id: data.id}});
                    if(data.id > 0)
                        this.ChangePage("admin");
                    
                    else{
                        alert("Username or password is not correct! Please leave and go to the user login page.");
                    }
                })
                .catch(error => {
                    console.error("There has been a problem with your fetch operation: AdminLogin", error);
                })
                

            },
            
        }
    }
</script>