import { App } from "vue";

import Menu from '@/components/Menu/Menu.vue'
import Svg from '@/components/Svg/Svg.vue'

import PasswordField from "@/components/Fields/PasswordField/Password.vue";
import StringField from "@/components/Fields/StringField/String.vue";
import SelectField from '@/components/Fields/SelectField/Select.vue'

export default {
    install: function (app: App) {
        app.component('main-menu', Menu);
        app.component('svg-img', Svg);

        app.component('string-field', StringField);
        app.component('password-field', PasswordField);
        app.component('select-field', SelectField);
    }
}