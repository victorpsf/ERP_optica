import { createRouter, createWebHistory } from 'vue-router'

export default createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes: [
        { path: '/', name: 'home', component: () => import('./Home/Home.vue') }
    ]
})