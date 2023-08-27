import { Options, Vue } from 'vue-class-component'

import Login from '@/views/Login/Login.vue'
import { IMainPageData } from '@/interfaces/views/IMain'

@Options({
    components: { LoginPage: Login },

    data: (): IMainPageData => ({
        auth: undefined
    })
})

export default class Main extends Vue { }