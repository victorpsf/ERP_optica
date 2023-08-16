import { App } from "vue";

import Input from './Input.vue'
import Password from './Password.vue'

export default {
    install(app: App) {
        app.component('input-field', Input)
        app.component('password-field', Password)
    }
}