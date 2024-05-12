<template>
    <div>
        <h1>User Main Page</h1>
        <button @click="ChangePage('login')">
            back to login
        </button>
        <H2>Welcome {{ username }}</H2>
        
    </div>
</template>

<script>
    export default {
        name: 'UserMainPage',
        methods:{
            ChangePage(page) {
                this.$emit("changeActivePage", page);
            }
        }
    }
</script>