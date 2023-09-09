import { Collection } from './collection';
import { IsNullOrUndefined, Equal } from './type-utils'

export interface IPathInfo {
    currentPath: string;
    nextPath: string;
    anotherPaths: string[];
    length: number;
}

export interface IGetParameter<T> {
    path: string;
    defaultValue: T | null;
    currentObject: { [key: string]: any; };
    prefix: string;
}

export interface ISetParameter<T> {
    path: string;
    value: T | null;
    currentObject: { [key: string]: any; };
    prefix: string;
}

export const pathInfo = (path: string, prefix: string = '.'): IPathInfo => {
    const collection = new Collection(path.split(prefix));
    const [currentPath, nextPath, ...anotherPaths] = collection.values;
    return { currentPath, nextPath, anotherPaths, length: collection.count };
}

export const pathJoin = (nextPath: string, pathArrayString: string[], prefix: string = '.'): string => [nextPath].concat(pathArrayString).join(prefix);

export const get = <T> ({ path, defaultValue = null, currentObject = {}, prefix = '.' }: IGetParameter<T>): T | null => {
    const info = pathInfo(path, prefix);

    if ( (info.length == 0) || IsNullOrUndefined(info.currentPath) ) 
        return defaultValue;

    if (typeof info.nextPath !== 'string')
        return ( (currentObject[info.currentPath] as T) || defaultValue );
    
    if (IsNullOrUndefined(currentObject[info.currentPath]))
        return defaultValue;

    if (Equal(currentObject[info.currentPath], currentObject.constructor))
        return get<T>({ path: pathJoin(info.nextPath, info.anotherPaths), defaultValue, currentObject: currentObject[info.currentPath], prefix });

    return defaultValue;
}

export const set = <T> ({ path, value = null, currentObject = {}, prefix = '.' }: ISetParameter<T>): { [key: string]: unknown } => {
    const info = pathInfo(path, prefix);

    if (info.length == 0 || IsNullOrUndefined(info.currentPath))
        return { };

    if (typeof info.nextPath !== 'string') {
        currentObject[info.currentPath] = value;
        return currentObject;
    }

    if (!Equal(currentObject[info.currentPath], currentObject))
        currentObject[info.currentPath] = { };

    currentObject[info.currentPath] = set({ path: pathJoin(info.nextPath, info.anotherPaths), value, currentObject: currentObject[info.currentPath], prefix });
    return currentObject;
}