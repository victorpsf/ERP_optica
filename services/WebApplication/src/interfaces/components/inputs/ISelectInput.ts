export interface ISelectInputOption {
    value: number;
    label: string;
}

export interface ISelectInputProps {
    label: string;
    value: ISelectInputOption[];
    multiple?: boolean;
    options: ISelectInputOption[];
    onValueChange: (values: ISelectInputOption[]) => void;
}