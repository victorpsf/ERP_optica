import AuthenticationService from './authentication'
import storage from './storage'
import cache from './cache'

export default {
    install: function (vue) {
        vue.config.globalProperties.$storage = storage;
        vue.config.globalProperties.$cache = cache;

        vue.config.globalProperties.$service = {
            authentication: AuthenticationService
        }
    }
}