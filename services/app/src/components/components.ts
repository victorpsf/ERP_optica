import { App } from "vue";

import Svg from '@/components/Svg/Svg.vue'

import MainApp from '@/components/Views/Main/Main.vue'
import Folder from '@/components/Views/Folder/Folder.vue'

import PasswordField from "@/components/Fields/PasswordField/Password.vue";
import StringField from "@/components/Fields/StringField/String.vue";
import SelectField from '@/components/Fields/SelectField/Select.vue';

import Form from '@/components/Views/Form/Form.vue';

export default {
    install: function (app: App) {
        app.component('main-application', MainApp);
        app.component('folder-application', Folder);
        app.component('svg-img', Svg);

        app.component('form-view', Form);

        app.component('string-field', StringField);
        app.component('password-field', PasswordField);
        app.component('select-field', SelectField);
    }
}