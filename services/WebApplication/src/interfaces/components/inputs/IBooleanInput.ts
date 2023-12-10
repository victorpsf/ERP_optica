export interface IBooleanInputProps {
    value: boolean;
    label: string;
    labelClassName?: string;
    direction?: 'left' | 'right';
    onValueChange: (value: boolean) => void;
}