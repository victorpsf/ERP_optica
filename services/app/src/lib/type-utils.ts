export const IsNull = (value: any) => value === null;
export const IsUndefined = (value: any) => value === undefined;

export const IsNullOrUndefined = (value: any) => (IsNull(value) || IsUndefined(value));
export const Equal = (v1: any, v2: any): boolean => {
    if (IsNullOrUndefined(v1) || IsNullOrUndefined(v2)) 
        return false;

    return (v1.constructor === v2.constructor);
}