const { IsNullOrUndefined } = require('./Util')

class Cache {
    #cache = {}
    #prefix = '.'

    constructor(prefix = '.') {
        this.#prefix = prefix;
    }

    #pathInfo(path = '') {
        const pathArrayString = path.split(this.#prefix);
        const [currentPath, nextPath, ...anotherPaths] = pathArrayString;
        return { currentPath, nextPath, anotherPaths, length: pathArrayString.length };
    }

    #pathJoin(nextPath = '', pathArrayString = []) {
        return [nextPath].concat(pathArrayString).join(this.#prefix);
    }

    #recursiveGet(path = '', defaultValue = null, currentObject = {}) {
        const { length, currentPath, nextPath, anotherPaths } = this.#pathInfo(path);

        if (
            !(length > 0) || 
            IsNullOrUndefined(currentPath)
        ) return defaultValue;

        if (typeof nextPath === 'string')
            return (!IsNullOrUndefined(currentObject[currentPath]) && currentObject[currentPath].constructor === currentObject.constructor) ?
                this.#recursiveGet(this.#pathJoin(nextPath, anotherPaths), defaultValue, currentObject[currentPath]):
                defaultValue;
        
        return currentObject[currentPath] || defaultValue;
    }

    #recursiveSet(path = '', value = null, currentObject = {}) {
        const { length, currentPath, nextPath, anotherPaths } = this.#pathInfo(path);

        if (!(length > 0) || IsNullOrUndefined(currentPath)) throw new Error("PATH NOT EXISTS");
        
        if (typeof nextPath === 'string') {
            currentObject[currentPath] = (IsNullOrUndefined(currentObject[currentPath]) || currentObject[currentPath].constructor !== currentObject.constructor) ?
                {}:
                currentObject[currentPath];
            currentObject[currentPath] = this.#recursiveSet(this.#pathJoin(nextPath, anotherPaths), value, currentObject[currentPath]);
            return currentObject;
        }

        else {
            currentObject[currentPath] = value;
            return currentObject;
        }
    }

    Get(path = '', defaultValue = null) {
        return this.#recursiveGet(path, defaultValue, this.#cache);
    }

    Set(path = '', value = null) {
        this.#recursiveSet(path, value, this.#cache);
    }

    static Create(prefix) {
        return new Cache(prefix);
    }
}

module.exports = Cache;