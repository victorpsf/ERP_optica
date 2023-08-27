import Db from "@/db/base/Db";
import { LoginPaths } from "@/db/base/Paths";

import { IHttpResponse } from "@/interfaces/db/IHttp";
import { ISingIn, ISingInResult } from "@/interfaces/db/ILogin";

export const SingIn = async (value: ISingIn): Promise<ISingInResult> => {
    const { data } = await Db.post<ISingIn, IHttpResponse<ISingInResult>>(LoginPaths.SingIn, value);
    return data;
};