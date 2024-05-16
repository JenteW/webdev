<template>
    <div>
        <h1>Owner Main Page</h1>
        <button @click="ChangePage('login')">
            back to login
        </button>
        <H2>Welcome {{ owner.username }}</H2>
        
    </div>
</template>

<script>

    export default {
        name: 'OwnerMainPage',
        mounted(){
            this.id = this.$route.params.id;
            console.log(this.id + " is the id @ OwnerMainPage");
            this.GetOwner(this.id);
        },
        data(){
            return{
                owner: {},
                id: "",

            }
        },
        methods:{
            ChangePage(page) {
                this.$emit("changeActivePage", page);
            },
            GetOwner(id){
                fetch("http://localhost:5162/Owner/" + id, {
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
                })
                .catch(error => {
                    console.error("There was an error!", error);
                });
            }
            
        }
        
    }
</script>