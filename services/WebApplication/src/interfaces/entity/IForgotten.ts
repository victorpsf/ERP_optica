export interface IForgotten {
    Name?: string;
    Email?: string;
}

export interface IForgottenCode {
    Code: string;
}

export interface IForgottenCodeResult {
    success: boolean;
}

export interface IForgottenResult {
    codeSended: boolean;
}

export interface IForgottenChangePass {
    Passphrase: string;
    Confirm: string;
}

export interface IForgottenChangePassResult {
    success: boolean;
}

export interface IForgottenState extends IForgotten { }