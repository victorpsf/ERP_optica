import Db from "@/db/base/Db";
import { LoginPaths } from "@/db/base/Paths";

import { IHttpResponse, IServerResponse } from "@/interfaces/db/IHttp";
import { ISingIn, ISingInResult } from "@/interfaces/db/ILogin";
import { AxiosError } from "axios";

export const SingIn = async (value: ISingIn): Promise<IServerResponse<ISingInResult>> => {
    try {
        const { data } = await Db.post<ISingIn, IHttpResponse<IServerResponse<ISingInResult>>>(LoginPaths.SingIn, value);
        return data;
    }

    catch (ex) 
    { return ex instanceof AxiosError? ex.response?.data: { failed: true, errors: [] }; }
};