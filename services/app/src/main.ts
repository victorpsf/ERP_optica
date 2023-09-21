import { createApp } from 'vue'
import Main from '@/views/Main/Main.vue'

import router from '@/views/router'
import store from '@/views/store'
import components from '@/components/components'
import directives from '@/directives/directives'

import '@/registerServiceWorker'
import '@/main.scss'
import '@/component.scss'

const main = createApp(Main)

main.use(store)
main.use(router)
main.use(directives)
main.use(components)

main.mount('#app')
