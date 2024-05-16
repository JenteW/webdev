import Vue from 'vue';
import App from './App.vue';
import VueRouter from 'vue-router';
import AdminLoginPage from './pages/AdminLoginPage.vue';
import AdminPage from './pages/AdminPage.vue';
import LoginPage from './pages/LoginPage.vue';
import UserMainPage from './pages/UserMainPage.vue';
import OwnerMainPage from './pages/OwnerMainPage.vue';
import UpdateOwnerPage from './pages/UpdateOwnerPage.vue';
Vue.use(VueRouter);
Vue.config.productionTip = false

const router = new VueRouter({
  routes: [
    { path: '/AdminLoginPage', component: AdminLoginPage },
    { path: '/AdminPage', name: 'AdminPage', component: AdminPage, props: true },
    { path: '/LoginPage', component: LoginPage},
    { path: '/UserMainPage', name: 'UserMainPage', component: UserMainPage, props: true},
    { path: '/OwnerMainPage', name: 'OwnerMainPage', component: OwnerMainPage, props: true},
    { path: '/UpdateOwnerPage', name: 'UpdateOwnerPage', component: UpdateOwnerPage, props: true},
  ]
});


new Vue({
  render: h => h(App),
  router,
}).$mount('#app')
