<template>
    <div>
        <h1>Make a new Account</h1>
        <input type="text" v-model="fn" placeholder="First Name">
        <input type="text" v-model="ln" placeholder="Last Name">
        <input type="text" v-model="email" placeholder="Email">
        <input type="text" v-model="username" placeholder="Username">
        <input type="password" v-model="password" placeholder="Password">
        <input type="checkbox" v-model="checked">
        <label for="checked">owner</label>
        <button @click="Add()">
            ADD USER
        </button>
        <button @click="ChangePage('login')">
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
                })
                .catch(error => {
                    console.error("There has been a problem with your fetch operation: ADDUSER", error);
                })
            }
        }
    }
</script>