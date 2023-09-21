import { get, set } from '@/lib/tree-navigation'
import { IsNullOrUndefined } from './type-utils';

interface IStorage {
    getValue: <T>(path: string, defaultValue: T) => T | null;
    setValue: <T>(path: string, value: T) => T | null;
}

let appName = "oOoQYEt+6XU4vTEGCFc0jthBgBJ3VjtnnN2B0eSep+nRO2a/53e2KtCXrlWrWjixzeHvP0drk+ERrNUKCOBeBQ==";

const getData = (): object => {
const value = window.sessionStorage.getItem(appName ?? '');

try 
{ return JSON.parse(value ?? '{}'); }

catch (error) 
{ return {}; }
}

const setData = (value: object): void => {
    try 
    { window.sessionStorage.setItem(appName ?? '', JSON.stringify(value)); }

    catch (error)
    { }
}

export const getValue = <T> (path: string, defaultValue: T | null): T | null => get<T>({ path, prefix: '.', defaultValue, currentObject: getData() });
export const setValue = <T> (path: string, value: T): T | null => {
    setData(set<T>({ path, prefix: '.', value, currentObject: getData() }));
    return getValue<T>(path, null);
};

export default (_appName: string): IStorage => {
    appName = _appName;

    return { getValue, setValue }
};