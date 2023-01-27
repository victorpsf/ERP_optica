import { IsNullOrUndefined } from '../lib/Util';
import { get, set } from '../lib/tree-navigation'

class AppStorage {
    #storage;
    #name;
    #prefix;

    constructor(name = '', prefix = '.') {
        this.#name = name;
        this.#storage = window.localStorage;
        this.#prefix = prefix;
    }

    #getStorage () {
        try {
            const value = JSON.parse(this.#storage.getItem(this.#name))
            return IsNullOrUndefined(value)? {}: value;
        }

        catch { return {} }
    }

    #setStorage (value) {
        try {
            this.#storage.setItem(this.#name, JSON.stringify(value));
        }

        catch (error) { console.error(error); }
    }

    Get (path = '', defaultValue) {
        return get({ path, defaultValue, currentObject: this.#getStorage(), prefix: this.#prefix });
    }

    Set (path = '', value) {
        const storage = this.#getStorage();
        set({ path, value, currentObject: storage, prefix: this.#prefix });
        this.#setStorage(storage);
    }
}

export default new AppStorage('ERP_OPTICA')