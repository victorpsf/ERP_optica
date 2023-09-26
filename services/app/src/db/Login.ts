import Db from "@/db/base/Db";
import { LoginPaths } from "@/db/base/Paths";

import { IHttpResponse, IServerResponse } from "@/interfaces/db/IHttp";
import { ISingIn, ISingInAuthenticated, ISingInResult, IValidateCode } from "@/interfaces/db/ILogin";
import { AxiosError } from "axios";

export const SingIn = async (value: ISingIn): Promise<IServerResponse<ISingInResult>> => {
    try {
        const { data } = await Db.post<ISingIn, IHttpResponse<IServerResponse<ISingInResult>>>(LoginPaths.SingIn, value);
        return data;
    }

    catch (ex) 
    { return ex instanceof AxiosError? ex.response?.data: { failed: true, errors: [] }; }
};

export const ValidateCode = async (value: IValidateCode): Promise<IServerResponse<ISingInAuthenticated>> => {
    try {
        const { data } = await Db.post<IValidateCode, IHttpResponse<IServerResponse<ISingInAuthenticated>>>(LoginPaths.ValidateCode, value);
        return data;
    }

    catch (ex) 
    { return ex instanceof AxiosError? ex.response?.data: { failed: true, errors: [] }; }
}