<template>
    <div class="div-login">
        <div class="div-flex">
            <h1 class="h1">Make a new Account</h1>
            <input class="custom-input" type="text" v-model="fn" placeholder="First Name">
            
            <input class="custom-input" type="text" v-model="ln" placeholder="Last Name">
            
            <input class="custom-input" type="text" v-model="email" placeholder="Email">
           
            <input class="custom-input" type="text" v-model="username" placeholder="Username">
         
            <input class="custom-input" type="password" v-model="password" placeholder="Password">
            <br>

            <input type="checkbox" v-model="checked">
            <label for="checked">Are you an owner?</label>

            <div class="div-flex">
                <button class="custom-button" @click="Add()">
                Add Account
                </button>
                <button class="custom-button" @click="GoToLogin()">
                Back to login
            </button>
            </div>
        </div>
    </div>
</template>

<script>

    export default {
        name: 'AddUserPage',
        data(){
            return{
                fn: "",
                ln: "",
                email: "",
                username: "",
                password: "",
                taken: true,
            };
        },
        methods:{
            ChangePage(page) {
                this.$emit("changeActivePage", page);
            },
            GoToLogin(){
                this.$router.push({name: "LoginPage"});
                this.$emit("changeActivePage", "login");
            },
            async Add(){
                if(this.email.includes('@') == false || this.email.includes('.') == false){
                    alert("Email is not valid");
                    return;
                }
                if(this.checked == true){
                    await this.OwnernameAvailable(this.username);
                    this.GoToLogin();

                }
                else{
                    await this.UsernameAvailable(this.username);
                    this.GoToLogin();
                }

            },
            OwnernameAvailable(username){
                fetch("https://localhost:5162/Owner/username/" + username, {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => {
                    if(!response.ok){
                        throw new Error("Network response was not ok at GETADMIN");
                    }
                    return response.json();
                })
                .then(data => {
                    if(data == false){
                        this.AddOwner();
                    }
                    else{
                        alert("Username is already taken");
                    }
                })
                .catch(error => {
                    console.error("There has been a problem with your fetch operation: GETADMIN", error);
                })
            },

            UsernameAvailable(username){
                fetch("https://localhost:5162/User/username/" + username, {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => {
                    if(!response.ok){
                        throw new Error("Network response was not ok at GETADMIN");
                    }
                    return response.json();
                })
                .then(data => {
                    console.log(data);
                    this.taken = data;
                    if(data == false){
                        this.AddUser();
                    }
                    else{
                        alert("Username is already taken");
                    }
                })
                .catch(error => {
                    console.error("There has been a problem with your fetch operation: GETADMIN", error);
                })
            },
            AddOwner(){
                fetch("https://localhost:5162/Owner",{
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify({
                        fn: this.fn,
                        ln: this.ln,
                        username: this.username,
                        password: this.password,
                        email: this.email,
                        isDeletedByAdmin: false
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
                    alert("Owner added!");
                })
                .catch(error => {
                    console.error("There has been a problem with your fetch operation: ADDUSER", error);
                })
            },      
            AddUser(){
                fetch("https://localhost:5162/User", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify({
                        fn: this.fn,
                        ln: this.ln,
                        email: this.email,
                        username: this.username,
                        password: this.password
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
                    alert("User added!");
                })
                .catch(error => {
                    console.error("There has been a problem with your fetch operation: ADDUSER", error);
                })
            }
        }
    }
</script>