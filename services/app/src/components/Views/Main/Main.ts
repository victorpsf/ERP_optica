import { Options, Vue } from 'vue-class-component'

import Menu from '@/components/Views/Main/Menu/Menu.vue'

@Options({
    components: { MenuApp: Menu }
})

export default class MainApp extends Vue { }