<template>
    <div>
        <button  @click="ChangePage('login')">
            back to login
        </button>
        <h1>Admin Page</h1>
        <p>Welcome {{ admin.username }}</p> 
        <p>
            This is the admin page. Only authenticated users can see this page.
        </p>
        <button @click="ChangePage('countries')">
            countries page
        </button>
        <h2>Add User</h2>
        <input type="text" v-model="fn" placeholder="First Name">
        <input type="text" v-model="ln" placeholder="Last Name">
        <input type="text" v-model="email" placeholder="Email">
        <input type="text" v-model="username" placeholder="Username">
        <input type="password" v-model="password" placeholder="Password">
        <button @click="AddUser()">
            ADD USER
        </button>

        <ul>
            <h2>USER LIST</h2>
            <button @click="getUsers()">
                Get User List
            </button>
            <li v-for="user in users" :key=" 'user-' + user.id">
                <strong>name :</strong> {{user.fn}} {{ user.ln }} <br>
                mail: {{user.email}} <br>
                username: {{user.username}}
                <button @click="DeleteUser(user.id)">
                    Delete
                </button>
            </li>
            <h2>OWNER LIST</h2>
            <button @click="getOwners()">
                Get Owner List
            </button>
            <li v-for="owner in owners" :key=" 'owner-' +owner.id">
                <strong>name :</strong> {{owner.fn}} {{ owner.ln }} <br>
                mail: {{owner.email}} <br>
                username: {{owner.username}}
                <button @click="DeleteOwner(owner.id)">
                    Delete
                </button>
            </li>
        </ul>
        <h2>Add Tag</h2>
        <input type="text" v-model="tagName" placeholder="Tag Name">
        <input type="text" v-model="tagDescription" placeholder="Tag Description">
        <button @click="AddTag()">
            ADD TAG
        </button>
        <ul>
            <h2>TAG LIST</h2>
            <button @click="getTags()">
                Get Tag List
            </button>
            <li v-for="tag in tags" :key=" 'tag-' + tag.id">
                <strong>name :</strong> {{tag.name}} <br>
                description: {{tag.description}} <br>
            </li>
        </ul>

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
            GetAdmin(id){
                fetch("https://localhost:5162/Admin/" + id, {
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
                    this.admin = data;
                })
                .catch(error => {
                    console.error("There has been a problem with your fetch operation: GETADMIN", error);
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
                        throw new Error("Network response was not ok at DELETEUSER");
                    }
                    return response;
                })
                .then(data => {
                    console.log("succes in deleting the user", data, id);
                    this.getUsers();
                })
                .catch(error => {
                    console.error("There has been a problem with your fetch operation: DELETEUSER", error);
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
                    this.getUsers();
                })
                .catch(error => {
                    console.error("There has been a problem with your fetch operation: ADDUSER", error);
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
                        throw new Error("Network response was not ok at GETUSER");
                    }
                    return response.json();
                })
                .then(data => {
                    console.log(data);
                    this.users = data;
                })
                .catch(error => {
                    console.error("There has been a problem with your fetch operation: GETUSER", error);
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
                    console.log(data);
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
                        throw new Error("Network response was not ok at DELETEUSER");
                    }
                    return response;
                })
                .then(data => {
                    console.log("succes in deleting the user", data, id);
                    this.getOwners();
                })
                .catch(error => {
                    console.error("There has been a problem with your fetch operation: DELETEUSER", error);
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
                        throw new Error("Network response was not ok at ADDTAG");
                    }
                    return response;
                })
                .then(data => {
                    console.log(data);
                    this.getTags();
                })
                .catch(error => {
                    console.error("There has been a problem with your fetch operation: ADDTAG", error);
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
                    console.log(data);
                    this.tags = data;
                })
            },
        },
    }

</script>