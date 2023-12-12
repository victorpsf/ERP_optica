export interface IPageFormProps {
    children: JSX.Element;
}

export interface IPageFormState {
    open: boolean;
    viewMode: 'table' | 'edit' | 'create';
}