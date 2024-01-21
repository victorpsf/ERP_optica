import { IEnterprise } from "../../interfaces/entity/IEnterprise";
import { IHttpResponse } from "../../interfaces/entity/IHttp";
import HttpClient from "../http-client";

export const GetEnterprises = async function (): Promise<IHttpResponse<IEnterprise[]>> {
    const request = HttpClient();
    try {
        const { data } = await request.get<IHttpResponse<IEnterprise[]>>('/auth/Enterprise');
        return data;
    }

    catch(ex) 
    { throw ex; }
}