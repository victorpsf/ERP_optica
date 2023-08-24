import { App } from "vue";

import Input from './Input.vue'
import Password from './Password.vue'
import Select from './Select/Select.vue'

export default {
    install(app: App) {
        app.component('input-field', Input)
        app.component('password-field', Password)
        app.component('selected-field', Select)
    }
}