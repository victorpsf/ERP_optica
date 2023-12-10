export interface IJSON {
    [key: string]: IJSON | any;
} 

export default function AppStorage (appname: string = '0BuBEqlWBM4KJ8qj8KUcEZWyqDIVJJfOXXnuTL1kKS6MzGJA04yeME9bn7Wh8lqyTyeqn3RG6rKkrknncBtYpA==', separator: string = '.') {
    const getCache = function (): IJSON {
        try { return JSON.parse(window.localStorage.getItem(appname) || '{}') }
        catch (ex) { console.error(ex); return {}; }
    }
    const setCache = function (value: IJSON): void {
        try { window.localStorage.setItem(appname, JSON.stringify(value)); }
        catch (ex) { console.error(ex); }
    }

    const setTreeNavigation = function <T>(path: string[], json: IJSON, value: T): IJSON | T {
        const [current, next, others] = path,
            base = {};

        if (!current)
            return value;

        if (next && (!json[current] || json[current].constructor !== base.constructor))
            json[current] = {};

        if (next) {
            json[current] = setTreeNavigation([next].concat(others), json[current], value) as IJSON;
            return json;
        }

        else {
            json[current] = value;
            return json;
        }
    }

    const getTreeNavigation = function <T>(path: string[], json: IJSON, value: T): IJSON | T {
        const [current, next, others] = path,
            base = {};

        if (!current || (next && (!json[current] || json[current].constructor !== base.constructor)))
            return value;

        if (next)
            return getTreeNavigation([next].concat(others), json[current], value);

        return json[current] ?? value;
    }

    const get = function <T>(name: string, defaultValue: T): IJSON | T {
        const path = name.split(separator).filter(a => !!a.trim());

        if (path.length === 0) throw new Error('[ERROR] Storage.get: Argument 1 not accepted');

        return getTreeNavigation(name.split(separator), getCache(), defaultValue);
    }

    const set = function <T>(name: string, value: T): T {
        const path = name.split(separator).filter(a => !!a.trim());

        if (path.length === 0) throw new Error('[ERROR] Storage.set: Argument 1 not accepted');

        const result = setTreeNavigation(name.split(separator), getCache(), value) as IJSON;
        setCache(result);
        return value;
    }

    const clear = (): void => setCache({});

    return {
        get, set, clear
    }
}