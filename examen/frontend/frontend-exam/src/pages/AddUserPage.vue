<template>
    <div class="flex flex-col items-center">
        <h1>Make a new Account</h1>
        <input class="custom-input" type="text" v-model="fn" placeholder="First Name">
        <input class="custom-input" type="text" v-model="ln" placeholder="Last Name">
        <input class="custom-input" type="text" v-model="email" placeholder="Email">
        <input class="custom-input" type="text" v-model="username" placeholder="Username">
        <input class="custom-input" type="password" v-model="password" placeholder="Password">
        <input class="custom-input" type="checkbox" v-model="checked">
        <label for="checked">owner</label>
        <button class="custom-button" @click="Add()">
            ADD USER
        </button>
        <button class="custom-button" @click="ChangePage('login')">
            back to login
        </button>
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
                password: ""
            };
        },
        methods:{
            ChangePage(page) {
                this.$emit("changeActivePage", page);
            },
            Add(){
                if(this.checked == true){
                    console.log("Owner");
                    this.AddOwner();
                }
                else{
                    console.log("User");
                    this.AddUser();
                }

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