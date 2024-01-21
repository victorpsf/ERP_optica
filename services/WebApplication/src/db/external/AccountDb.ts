import { ICode } from "../../interfaces/entity/ICode";
import { IHttpResponse } from "../../interfaces/entity/IHttp";
import { ILogin, IResendCodeResult, ISignInResult, IValidateTokenResult } from "../../interfaces/entity/ILogin";
import HttpClient from "../http-client";

export const SigIn = async function (values: ILogin): Promise<IHttpResponse<ISignInResult>> {
    const request = HttpClient();
    try {
        const { data } = await request.post<IHttpResponse<ISignInResult>>('/auth/SignIn', values);
        return data;
    }

    catch(ex) 
    { throw ex; }
}

export const SignInValidateCode = async function (values: ICode): Promise<IHttpResponse<IValidateTokenResult>> {
    const request = HttpClient();
    try {
        const { data } = await request.post<IHttpResponse<IValidateTokenResult>>('/auth/SignIn/ValidateCode', values);
        return data;
    }

    catch(ex) 
    { throw ex; }
}

export const SignInResendCode = async function (): Promise<IHttpResponse<IResendCodeResult>> {
    const request = HttpClient();
    try {
        const { data } = await request.post<IHttpResponse<IResendCodeResult>>('/auth/SignIn/ResendCode');
        return data;
    }

    catch(ex) 
    { throw ex; }
}