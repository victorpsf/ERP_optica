import { App } from "vue";
import Fields from './Fields';

export default {
    install(app: App) {
        app.use(Fields);
    }
}