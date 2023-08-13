import { createApp } from 'vue'
import Main from './views/Main/Main.vue'
import './registerServiceWorker'
import router from './router'

import components from './components'

const application = createApp(Main)

application.use(router)
application.use(components)


application.mount('#app')
