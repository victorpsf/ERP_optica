import StringField from '@/components/fields/inputs/string/String.vue'
import NumberField from '@/components/fields/inputs/number/Number.vue'
import SearchField from '@/components/fields/inputs/search/Search.vue'
import PasswordField from '@/components/fields/inputs/password/Password.vue'

import FileField from '@/components/fields/selections/file/File.vue'
import SelectField from '@/components/fields/selections/select/Select.vue'

import SvgImage from '@/components/svg.vue'
import FieldsGroup from '@/components/fields/group/Fields.vue'

export default {
    install: function (vue) {
        vue.component('file-field', FileField)
        vue.component('string-field', StringField)
        vue.component('number-field', NumberField)
        vue.component('search-field', SearchField)
        vue.component('select-field', SelectField)
        vue.component('password-field', PasswordField)

        vue.component('svg-image', SvgImage)
        vue.component('field-group', FieldsGroup)
    }
}