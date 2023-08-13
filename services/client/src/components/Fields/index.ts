import { App } from "vue";

import Inputs from './Input/Input.vue'

export default {
    install(app: App) {
        app.component('input-field', Inputs)
    }
}