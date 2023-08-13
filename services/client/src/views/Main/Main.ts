import { Options, Vue } from 'vue-class-component'
import Login from '@/views/Login/Login.vue'

@Options({
    components: {
        LoginView: Login
    },

    data: () => ({
        auth: null
    }),

    computed: {
        authenticated(): boolean {
            return !!this.auth;
        }
    }
})

export default class Main extends Vue { }