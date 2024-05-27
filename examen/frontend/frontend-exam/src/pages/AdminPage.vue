<template>
    <div class="flex flex-col items-center">
        <div class="top-left-button">
            <button class=" custom-button" @click="GoToLogin()">
            Back to login
        </button>
        </div>
        <div class="div-flex w-full">
        <h1 class="h1 text-center p-2 m-2">Admin Page</h1>
        <p class=" text-xl font-bold p-2 m-2">Welcome {{ admin.username }}</p> 
        <p>
            This is the admin page. Only authenticated users can see this page.
        </p>
        <button class="button-static-size" @click="ChangePage('countries')">
            Countries page
        </button>
        <!-- <h2>Add User</h2>
        <input class="custom-input" type="text" v-model="ln" placeholder="Last Name">
        <input class="custom-input" type="text" v-model="fn" placeholder="First Name">
        <input class="custom-input" type="text" v-model="email" placeholder="Email">
        <input class="custom-input" type="text" v-model="username" placeholder="Username">
        <input class="custom-input" type="password" v-model="password" placeholder="Password">
        <button class="custom-button" @click="AddUser()">
            ADD USER
        </button> -->
        </div>
            <div class="div-flex w-full">
                <h2 class="h2">User list</h2>
                <button class="button-static-size" v-if="users.length == 0" @click="getUsers()">
                    Get User List
                </button>
                <button class="button-static-size" v-else @click="CloseUserList()">
                    Close User List
                </button>
                <ul>
                    <li v-for="user in users" :key=" 'user-' + user.id">
                        <strong>Name :</strong> {{user.fn}} {{ user.ln }} <br>
                        <p>Mail: {{user.email}}</p>
                        <p>Username: {{user.username}}</p>
                        <button class="custom-button" @click="DeleteUser(user.id)">
                            Delete
                        </button>
                    </li>
                </ul>   
            </div>
            <h2 class="h2">Owner list</h2>
            <div class="div-flex w-full">
                <button class="button-static-size" v-if="owners.length == 0" @click="getOwners()">
                Get Owner List
            </button>
            <button class="button-static-size" v-else @click="CloseOwnerList() ">
                Close Owner List
            </button>
            </div>
            <ul>
                <li v-for="owner in owners" :key=" 'owner-' +owner.id">
                    <strong>Name :</strong> {{owner.fn}} {{ owner.ln }} <br>
                    Mail: {{owner.email}} <br>
                    Username: {{owner.username}}
                    <button class="custom-button" @click="DeleteOwner(owner.id)">
                        Delete
                    </button>
                </li>
            </ul>
        <h2 class="h2">Add tag</h2>
        <input class="custom-input" type="text" v-model="tagName" placeholder="Tag Name">
        <input class="custom-input" type="text" v-model="tagDescription" placeholder="Tag Description">
        <button class="button-static-size" @click="AddTag()">
            Add Tag
        </button>
        <div class="div-flex w-full">
                <h2 class="h2 text-center">Tag list</h2>
                <button class="button-static-size" v-if="tags == 0" @click="getTags()">
                    Get Tag List
                </button>
                <button class="button-static-size" v-else @click="CloseTagList()">
                    Close Tag List
                </button>
                <li v-for="tag in tags" :key=" 'tag-' + tag.id">
                    <strong>name :</strong> {{tag.name}} <br>
                    description: {{tag.description}} <br>
                    <button class="custom-button" @click="DeleteTag(tag.id)">
                        Delete
                    </button>
                </li>    
        </div>

    </div>
</template>

<script>
    export default {
        name: 'AdminPage',
       
        mounted(){
            this.adminId = this.$route.params.id;
            this.GetAdmin(this.adminId);
        },
        data(){
            return{
                admin: [],
                adminId: "",
                users: [],
                fn: "",
                ln: "",
                email: "",
                username: "",
                password: "",
                owners: [],
                tags: [],
                tagId: "",
                tagName: "",
                tagDescription: "",

            }
        },
        methods:{
            ChangePage(page) {
                this.$emit("changeActivePage", page);
            },
            CloseOwnerList(){
                this.owners = [];
            },
            CloseUserList(){
                this.users = [];
            },
            CloseTagList(){
                this.tags = [];
            },
            GoToLogin(){
                this.$router.push({name: "AdminLoginPage"});
                this.ChangePage("adminlogin");

            },
            GetAdmin(id){
                fetch("https://localhost:5162/Admin/" + id, {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => {
                    if(!response.ok){
                        throw new Error("Network response was not ok at GetAdmin");
                    }
                    return response.json();
                })
                .then(data => {
                    this.admin = data;
                })
                .catch(error => {
                    console.error("There has been a problem with your fetch operation: GetAdmin", error);
                })
            },
            DeleteUser(id){
                fetch("https://localhost:5162/User/" + id, {
                    method: "DELETE",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => {
                    if(!response.ok){
                        throw new Error("Network response was not ok at DeleteUser");
                    }
                    return response;
                })
                .then(
                    alert("User deleted"),
                    this.getUsers()
                )
                .catch(error => {
                    console.error("There has been a problem with your fetch operation: DeleteUser", error);
                })
            },
            getUsers(){
                fetch("https://localhost:5162/User", {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => {
                    if(!response.ok){
                        throw new Error("Network response was not ok at getUsers");
                    }
                    return response.json();
                })
                .then(data => {
                    this.users = data;
                })
                .catch(error => {
                    console.error("There has been a problem with your fetch operation: getUsers", error);
                })
            },
            getOwners(){
                fetch("https://localhost:5162/Owner", {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => response.json())
                .then(data => {
                    this.owners = data;
                })
            },
            DeleteOwner(id){
                fetch("https://localhost:5162/Owner/" + id, {
                    method: "DELETE",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => {
                    if(!response.ok){
                        throw new Error("Network response was not ok at DeleteOwner");
                    }
                    return response;
                })
                .then(
                    alert("Owner deleted"),
                    this.getOwners()
                )
                .catch(error => {
                    console.error("There has been a problem with your fetch operation: DeleteOwner", error);
                })
            },
            AddTag(){
                fetch("https://localhost:5162/Tag", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify({
                        name: this.tagName,
                        description: this.tagDescription
                    })

                })  
                .then(response => {
                    if(!response.ok){
                        throw new Error("Network response was not ok at AddTag");
                    }
                    return response;
                })
                .then(
                    this.getTags(),
                    alert("Tag added")
                )
                .catch(error => {
                    console.error("There has been a problem with your fetch operation: AddTag", error);
                })
            },
            getTags(){
                fetch("https://localhost:5162/Tag", {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => response.json())
                .then(data => {
                    this.tags = data;
                })
            },
            DeleteTag(id){
                fetch("https://localhost:5162/Tag/" + id, {
                    method: "DELETE",
                    headers: {
                        "Content-Type": "application/json",
                    }
                })
                .then(response => {
                    if(!response.ok){
                        throw new Error("Network response was not ok at DeleteTag");
                    }
                    return response;
                })
                .then(
                    alert("Tag deleted"),
                    this.getTags(),
                )
                .catch(error => {
                    console.error("There has been a problem with your fetch operation: DeleteTag", error);
                })
            },  
        },
    }

</script>