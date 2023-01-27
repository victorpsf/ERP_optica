import ApplicationCommon from './application/Application.vue'

import LayoutCommon from './layout/Layout.vue'
import GridCommon from './layout/Grid/Grid.vue'
import FormCommon from './layout/Form/Form.vue'
import FilterCommon from './layout/Filter/Filter.vue'

export default {
    install: function (vue) {
        vue.component('application-common', ApplicationCommon);

        vue.component('layout-common', LayoutCommon);
        vue.component('grid-common', GridCommon);
        vue.component('filter-common', FilterCommon);
        vue.component('form-common', FormCommon);
    }
}