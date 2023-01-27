import { createApp } from 'vue'
import App from './pages/app/App.vue'

import router from './lib/router'
import store from './lib/store'
import components from './components/index'
import directives from './directives'
import services from './services'

import './lib/registerServiceWorker'
import './main.scss'

const application = createApp(App)

application.use(store)
application.use(router)
application.use(directives)
application.use(components)
application.use(services)

application.mount('#app')
