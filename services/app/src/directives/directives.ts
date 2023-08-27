import { App } from "vue";
import { RefDirective } from "./ref";

export default {
    install: function (app: App) {
        app.directive('ref', RefDirective);
    }
}