import { ICode } from "../../interfaces/entity/ICode";
import { IHttpResponse } from "../../interfaces/entity/IHttp";
import { IEnterprise, ILogin, IResendCodeResult, ISignInResult, IValidateTokenResult } from "../../interfaces/entity/ILogin";
import HttpClient from "../http-client";

export const SigIn = async function (values: ILogin): Promise<IHttpResponse<ISignInResult>> {
    const request = HttpClient();
    try {
        const { data } = await request.post<IHttpResponse<ISignInResult>>('/auth/Account/SingIn', values);
        return data;
    }

    catch(ex) 
    { throw ex; }
}

export const ValidateCode = async function (values: ICode): Promise<IHttpResponse<IValidateTokenResult>> {
    const request = HttpClient();
    try {
        const { data } = await request.post<IHttpResponse<IValidateTokenResult>>('/auth/Account/ValidateCode', values);
        return data;
    }

    catch(ex) 
    { throw ex; }
}

export const ResendCode = async function (): Promise<IHttpResponse<IResendCodeResult>> {
    const request = HttpClient();
    try {
        const { data } = await request.post<IHttpResponse<IResendCodeResult>>('/auth/Account/ResendCode');
        return data;
    }

    catch(ex) 
    { throw ex; }
}

export const GetEnterprises = async function (): Promise<IHttpResponse<IEnterprise[]>> {
    const request = HttpClient();
    try {
        const { data } = await request.get<IHttpResponse<IEnterprise[]>>('/auth/Account/GetEnterprises');
        return data;
    }

    catch(ex) 
    { throw ex; }
}