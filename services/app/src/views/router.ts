import { createRouter, createWebHistory } from 'vue-router'

export default createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes: [
        { path: '/', name: 'home', component: () => import('./Home/Home.vue') },
        { path: '/form/person/physical', name: 'PersonPhysical', component: () => import('./Forms/PersonPhysical/PersonPhysical.vue') },
        { path: '/form/person/juridical', name: 'PersonJuridical', component: () => import('./Forms/PersonJuridical/PersonJuridical.vue') }
    ]
})