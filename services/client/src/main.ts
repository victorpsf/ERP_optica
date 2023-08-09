import { createApp } from 'vue'
import App from './App.vue'
import './registerServiceWorker'
import router from './router'

const application = createApp(App)

application.use(router)


application.mount('#app')
