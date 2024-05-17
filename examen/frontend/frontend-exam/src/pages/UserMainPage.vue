<template>
    <div>
        <h1>User Main Page</h1>
        <button @click="ChangePage('login')">
            back to login
        </button>
        <H2>Welcome {{ user.username }}</H2>
        <button @click="GoToUpdate()">
            Update Account
        </button>
        
    </div>
</template>

<script>
    export default {
        name: 'UserMainPage',
        mounted(){
            this.id = this.$route.params.id;
            console.log(this.id + " is the id @ UserMainPage");
            this.GetUser(this.id);
        },
        data(){
            return{
                user: {},
                id: ""
            }
        },
        
        methods:{
            ChangePage(page) {
                this.$emit("changeActivePage", page);
            },
            GoToUpdate(){
                this.$router.push({name: "UpdateUserPage", params: {id: this.id}});
                this.$emit("changeActivePage", "updateuser");
            },
            GetUser(id){
                fetch("http://localhost:5162/User/" + id, {
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
                })
                .catch(error => {
                    console.error("There was an error!", error);
                });
            }
            
        }
        
    }
</script>