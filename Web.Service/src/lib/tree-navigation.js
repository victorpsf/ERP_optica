import { IsNullOrUndefined } from './Util'

export const pathInfo = function (path = '', prefix = '.') {
    const pathArrayString = path.split(prefix);
    const [currentPath, nextPath, ...anotherPaths] = pathArrayString;
    return { currentPath, nextPath, anotherPaths, length: pathArrayString.length };
}

export const pathJoin = function (nextPath = '', pathArrayString = '', prefix = '.') {
    return [nextPath].concat(pathArrayString).join(prefix);
}

export const get = function ({ path = '', defaultValue = null, currentObject = {}, prefix = '.' }) {
    const { length, currentPath, nextPath, anotherPaths } = pathInfo(path, prefix);

    if (
        !(length > 0) ||
        IsNullOrUndefined(currentPath)
    ) return defaultValue;

    if (typeof nextPath === 'string')
        return (!IsNullOrUndefined(currentObject[currentPath]) && currentObject[currentPath].constructor === currentObject.constructor) ?
            this.get({ path: pathJoin(nextPath, anotherPaths), defaultValue, currentObject: currentObject[currentPath], prefix }) :
            defaultValue;

    return (currentObject[currentPath] || defaultValue);
}

export const set = function ({ path = '', value = null, currentObject = {}, prefix = '.' }) {
    const { length, currentPath, nextPath, anotherPaths } = pathInfo(path, prefix);

    if (!(length > 0) || IsNullOrUndefined(currentPath)) throw new Error("PATH NOT EXISTS");
    
    if (typeof nextPath === 'string') {
        currentObject[currentPath] = (IsNullOrUndefined(currentObject[currentPath]) || currentObject[currentPath].constructor !== currentObject.constructor) ?
            {}:
            currentObject[currentPath];
        currentObject[currentPath] = set({ path: pathJoin(nextPath, anotherPaths), value, currentObject: currentObject[currentPath], prefix });
        return currentObject;
    }

    else {
        currentObject[currentPath] = value;
        return currentObject;
    }
}