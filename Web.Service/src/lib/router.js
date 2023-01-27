import { createRouter, createWebHistory } from 'vue-router'

const routes = [
  {
    path: '/',
    name: 'home',
    component: () => import('@/pages/home/Home.vue')
  },

  {
    path: '/person/physical',
    name: 'Cad-Person-Physical',
    component: () => import('@/pages/register/PersonPhysical/PersonPhysical.vue')
  },
  {
    path: '/person/juridical',
    name: 'Cad-Person-Juridical',
    component: () => import('@/pages/register/PersonJuridical/PersonJuridical.vue')
  },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
