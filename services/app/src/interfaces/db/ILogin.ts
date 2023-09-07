export interface ISingInCode {
    Code?: string;
}

export interface ISingInInput extends ISingInCode {
    Name?: string;
    Password?: string;
    EnterpriseId?: number;
}

export interface ISingIn extends ISingInInput {
}

export interface ISingInResult {
    expire?: string;
    token?: string;
    codeSended: boolean;
}