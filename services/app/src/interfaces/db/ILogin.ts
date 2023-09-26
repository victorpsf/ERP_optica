export interface IValidateCode {
    Code?: string;
}

export interface ISingIn {
    Name?: string;
    Password?: string;
    EnterpriseId?: number;
}

export interface ISingInResult {
    codeSended: boolean;
}

export interface ISingInAuthenticated {
    expire?: string;
    token?: string;
}