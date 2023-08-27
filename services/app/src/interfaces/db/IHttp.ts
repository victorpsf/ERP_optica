import { AxiosResponse } from "axios";

export interface IFailure {
    propertie: string;
    message: string; 
}

export interface IServerResponse<T> {
    errors?: IFailure[],
    failed: boolean;
    result?: T;
}

export interface IHttpResponse<T> extends AxiosResponse<T> { }