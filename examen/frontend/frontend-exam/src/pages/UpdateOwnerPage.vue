<template>
    <div class="div-login">
        <div class="div-flex w-3/4">
            <div class="top-left-button">
                <button class="custom-button" @click="ChangePage('ownermain')">
                    Back to main page
                </button>
            </div>
            <H2 class="h2">Welcome {{ owner.username }}</H2>

            <input class="custom-input" type="text" v-model="fn" value="owner.fn">
            <input class="custom-input" type="text" v-model="ln" placeholder = "ln">
            <input class="custom-input" type="text" v-model="email">
            <input class="custom-input" type="text" v-model="username">
            <input class="custom-input" type="password" v-model="password" placeholder="Password">
            <button class="half-button" @click="UpdateOwner(owner.id)">
                Update Account
            </button>
        </div>

    </div>
</template>

<script>
    export default {
        name: 'UpdateOwnerPage',
        mounted(){
            this.id = this.$route.params.id;
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
                        return response;
                    }
                })
                .then(
                    this.GetOwner(this.id),
                    alert("Owner updated!")
                )
                .catch(error => {
                    console.error("There was an error!", error);
                });
            }
            
        }
        
    }
</script>