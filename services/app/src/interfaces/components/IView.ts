export interface IFormViewAction {
    label: string;
    name: string;
}

export interface IFormViewData {
    values: { [key: string]: unknown }
}

export interface IFieldChangeEvent {
    field: string;
    value: unknown;
}

export interface IFormListenEvent {
    event?: MouseEvent;
    action?: IFormViewAction;
    values?: { [key: string]: unknown };
}

export interface IScreenPermission {
    name: string[];
}

export interface IScreens {
    route: string;
    name: string;
    image: string;
    permission?: string;
}

export interface IMainAppMenuData {
    visible: boolean;
    screens: IScreens[];
    filter: string;
}