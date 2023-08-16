import { App } from "vue";
import Fields from './Fields';
import From from './Form/Form.vue'

export default {
    install(app: App) {
        app.use(Fields);

        // app.component('form-field', From);
    }
}