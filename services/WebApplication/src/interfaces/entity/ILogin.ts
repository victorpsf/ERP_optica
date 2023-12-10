import { ISelectInputOption } from "../components/inputs/ISelectInput";

export interface ILogin {
    Name: string;
    Password: string;
    EnterpriseId: number;
}

export interface ISignInResult {
    codeSended: boolean;
}

export interface IValidateTokenResult {
    expire: string;
    token: string;
}

export interface IEnterprise extends ISelectInputOption { }
export interface ILoginState {
    input: ILogin;
    remenber: boolean;
    enterprises: ISelectInputOption[];
}
