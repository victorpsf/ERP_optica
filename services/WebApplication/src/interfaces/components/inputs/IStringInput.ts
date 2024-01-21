import { ISvgName } from "../ISvg";

export interface IStringInputRuleLength {
    min?: number;
    minText?: string;
    max?: number;
    maxText?: string;
}

export interface IStringInputRule {
    length?: IStringInputRuleLength;
    required?: string;
}

export interface IStringInputProps {
    value: string;
    type: 'text' | 'password';
    label: string;
    icon?: ISvgName;
    onTextChange: (value: string) => void;
    rule?: IStringInputRule;
}