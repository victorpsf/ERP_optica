import { ICode } from "../../interfaces/entity/ICode";
import { IForgotten, IForgottenResult } from "../../interfaces/entity/IForgotten";
import { IHttpResponse } from "../../interfaces/entity/IHttp";
import { IEnterprise, ILogin, IResendCodeResult, ISignInResult, IValidateTokenResult } from "../../interfaces/entity/ILogin";
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

export const SignInGetEnterprises = async function (): Promise<IHttpResponse<IEnterprise[]>> {
    const request = HttpClient();
    try {
        const { data } = await request.get<IHttpResponse<IEnterprise[]>>('/auth/SignIn/GetEnterprises');
        return data;
    }

    catch(ex) 
    { throw ex; }
}

export const Forgotten = async function (values: IForgotten): Promise<IHttpResponse<IForgottenResult>> {
    const request = HttpClient();

    try {
        const { data } = await request.get<IHttpResponse<IForgottenResult>>('/auth/Forgotten');
        return data;
    }

    catch(ex) 
    { throw ex; }
}

export const ForgottenResendCode = async function (): Promise<IHttpResponse<IResendCodeResult>> {
    const request = HttpClient();

    try {
        const { data } = await request.get<IHttpResponse<IResendCodeResult>>('/auth/Forgotten/ResendCode');
        return data;
    }

    catch(ex) 
    { throw ex; }
}

export const ForgottenValidateCode = async function (values: IForgotten): Promise<IHttpResponse<IForgottenResult>> {
    const request = HttpClient();

    try {
        const { data } = await request.get<IHttpResponse<IForgottenResult>>('/auth/Forgotten/ValidateCode');
        return data;
    }

    catch(ex) 
    { throw ex; }
}