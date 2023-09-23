import { Options, Vue } from 'vue-class-component'

import Login from '@/views/Login/Login.vue'
import { IMainPageData } from '@/interfaces/views/IMain'
import { getValue, setValue } from '@/lib/storage'

@Options({
    components: { LoginPage: Login },

    data: (): IMainPageData => ({
        auth: undefined
    }),

    mounted(): void {
        this.auth = getValue('auth.token', undefined);
    },

    methods: {
        SingIn(token: string): void {
            setValue<string>('auth.token', token);
            this.auth = token;
        }
    }
})

export default class Main extends Vue { }