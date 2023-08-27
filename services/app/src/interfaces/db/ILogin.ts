export interface ISingIn {
    Name?: string;
    Password?: string;
    EnterpriseId?: number;
    Code?: string;
}

export interface ISingInResult {
    expire?: string;
    token?: string;
    codeSended: boolean;
}