export interface IBaseFieldData<T, E> {
    value?: T;
    inputEl?: E;
}

export interface ISelectFieldOption<T> {
    value: T;
    label: string;
}

export interface ISelectedFieldOption<T> extends ISelectFieldOption<T> {
    selected: boolean;
}

export interface ISelectOptionsFieldData {
    filter: string;
}

export interface IStringFieldData extends IBaseFieldData<string, HTMLInputElement> { }
export interface IPasswordFieldData extends IBaseFieldData<string, HTMLInputElement> {
    visible: boolean;
}
export interface INumberFieldData extends IBaseFieldData<number, HTMLInputElement> { }
export interface ISelectFieldData extends IBaseFieldData<ISelectFieldOption<unknown>[], HTMLInputElement> {
    selectOptions: ISelectFieldOption<unknown>[];
    visible: boolean;
}