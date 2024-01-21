import { IForgotten, IForgottenChangePass, IForgottenChangePassResult, IForgottenCode, IForgottenCodeResult, IForgottenResult } from "../../interfaces/entity/IForgotten";
import { IHttpResponse } from "../../interfaces/entity/IHttp";
import { IResendCodeResult } from "../../interfaces/entity/ILogin";
import HttpClient from "../http-client";

export const Forgotten = async function (values: IForgotten): Promise<IHttpResponse<IForgottenResult>> {
    const request = HttpClient();

    try {
        const { data } = await request.post<IHttpResponse<IForgottenResult>>('/auth/Forgotten', values);
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

export const ForgottenValidateCode = async function (values: IForgottenCode): Promise<IHttpResponse<IForgottenCodeResult>> {
    const request = HttpClient();

    try {
        const { data } = await request.post<IHttpResponse<IForgottenCodeResult>>('/auth/Forgotten/ValidateCode', values);
        return data;
    }

    catch(ex) 
    { throw ex; }
}

export const ForgottenChangePassword = async function (values: IForgottenChangePass): Promise<IHttpResponse<IForgottenChangePassResult>> {
    const request = HttpClient();

    try {
        const { data } = await request.post<IHttpResponse<IForgottenChangePassResult>>('/auth/Forgotten/ChangePassword', values);
        return data;
    }

    catch(ex) 
    { throw ex; }
}