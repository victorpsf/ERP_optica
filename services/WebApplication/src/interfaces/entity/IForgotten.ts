export interface IForgotten {
    userName?: string;
    email?: string;
}

export interface IForgottenResult {
    codeSended: boolean;
}

export interface IForgottenState extends IForgotten { }