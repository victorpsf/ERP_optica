export interface IFailure {
    Propertie?: string;
    Message?: string;
}

export interface IHttpResponse<T> {
    erros: IFailure[] | null;
    result: T | null;
    failed: boolean;
}