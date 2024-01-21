import AppStorage from "./app-storage";
import Axios, { AxiosError, AxiosRequestConfig, AxiosResponse } from 'axios'

type SimpleCaller = <T = any, R = AxiosResponse<T>, D = any>(url: string, config?: AxiosRequestConfig<D>) => Promise<R>;
type CompleteCaller = <T = any, R = AxiosResponse<T>, D = any>(url: string, data?: D, config?: AxiosRequestConfig<D>) => Promise<R>;

const HttpClient = function () {
    const storage = AppStorage();
    const client = Axios.create({ baseURL: `http://localhost:5297` });
    var token: string | undefined = undefined;

    const tokenPrefix = (): string | undefined => token ? `Bearer ${token}`: undefined;
    const getConfig = <D>(config?: AxiosRequestConfig<D>) => config ? { ...config, headers: { ...config.headers, Authorization: tokenPrefix() } }: { headers: { Authorization: tokenPrefix() } };

    const middleware = async function<T = any, R = AxiosResponse<T>, D = any>(caller: SimpleCaller | CompleteCaller, url: string, data?: D, config?: AxiosRequestConfig<D>): Promise<R> {
        try {
            if (token === undefined)
                token = storage.get('auth.token', undefined) as string | undefined;

            if (data) return await (caller as CompleteCaller)(url, data, getConfig(config));
            else return await (caller as SimpleCaller)(url, getConfig(config));
        }

        catch(ex) {
            if (ex instanceof AxiosError && ex.code && parseInt(ex.code) === 401) {
                storage.clear();
                window.location.reload();
            }

            throw ex;
        }
    }

    return {
        client,
        get: <T = any, R = AxiosResponse<T>, D = any>(url: string, config?: AxiosRequestConfig<D>): Promise<R> => middleware(client.get, url, undefined, config),
        post: <T = any, R = AxiosResponse<T>, D = any>(url: string, data?: D, config?: AxiosRequestConfig<D>): Promise<R> => middleware(client.post, url, data, config),
        put: <T = any, R = AxiosResponse<T>, D = any>(url: string, data?: D, config?: AxiosRequestConfig<D>): Promise<R> => middleware(client.put, url, data, config),
        delete: <T = any, R = AxiosResponse<T>, D = any>(url: string, config?: AxiosRequestConfig<D>): Promise<R> => middleware(client.delete, url, undefined, config)
    }
}

export default HttpClient;