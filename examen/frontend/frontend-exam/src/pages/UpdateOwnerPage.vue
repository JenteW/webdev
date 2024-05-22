<template>
    <div>
        <button @click="ChangePage('ownermain')">
            back to main page
        </button>
        <H2>Welcome {{ owner.username }}</H2>

        <input type="text" v-model="fn" value="owner.fn">
        <input type="text" v-model="ln" placeholder = "ln">
        <input type="text" v-model="email">
        <input type="text" v-model="username">
        <input type="password" v-model="password" placeholder="Password">
        <button @click="UpdateOwner(owner.id)">
            Update Account
        </button>

    </div>
</template>

<script>
    export default {
        name: 'UpdateOwnerPage',
        mounted(){
            this.id = this.$route.params.id;
            console.log(this.id + " is the id @ UpdateOwnerPage");
            this.GetOwner(this.id);
        },
        data(){
            return{
                owner: {},
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
            GetOwner(id){
                fetch("https://localhost:5162/Owner/" + id, {
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
                    this.owner = data;
                    this.fn = this.owner.fn;
                    this.ln = this.owner.ln;
                    this.username = this.owner.username;
                    this.email = this.owner.email;
                    this.isDeletedByAdmin = this.owner.isDeletedByAdmin;
                })
                .catch(error => {
                    console.error("There was an error!", error);
                });
            },
            UpdateOwner(){
                fetch("https://localhost:5162/Owner/" + this.id, {
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
                            isDeletedByAdmin: this.isDeletedByAdmin
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
                .then(data => {
                    this.GetOwner(this.id);
                    console.log(data + "changed user");
                                })
                .catch(error => {
                    console.error("There was an error!", error);
                });
            }
            
        }
        
    }
</script>