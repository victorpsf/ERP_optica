import { get, set } from '../lib/tree-navigation'

class AppCache {
    #cache = {};
    #prefix = '.'

    constructor(prefix = '.', data = {}) {
        this.#prefix = prefix;
        this.#cache = data;
    }

    Get(path = '', defaultValue = null) {
        return get({ path, defaultValue, currentObject: this.#cache, prefix: this.#prefix });
    }

    Set(path = '', value = null) {
        set({ path, value, currentObject: this.#cache, prefix: this.#prefix });
    }
}

export default new AppCache()