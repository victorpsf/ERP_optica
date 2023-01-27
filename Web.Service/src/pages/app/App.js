import { IsNullOrUndefined } from "@/lib/Util";
import LoginPage from "@/pages/login/Login.vue";

const AppPage = {
    components: { LoginPage },

    data: function () {
        return {
            auth: {
                guid: null,
                value: true
            }
        }
    },

    mounted: function () {
        this.$service.authentication.listen(this.authCallback, this.registerCallback)
    },

    unmounted: function () {
    },

    methods: {
        authCallback: function (info, { auth }) {
            this.auth.value = auth;
        },

        registerCallback: function ({ guid }) {
            this.auth.guid = guid;
        }
    }
}

export default AppPage