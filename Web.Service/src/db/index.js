import Axios from 'axios'
import RequestResponse from '@/models/RequestReponse';
const server = 'http://localhost:3000'
const timeout = 12000;

const callServer = async function (args) {
    try {
        const result = await Axios(args);
        return new RequestResponse(args, { code: result.status, status: result.statusText, data: result.data, headers: result.headers })
    } catch (error) {
        console.error(args.url, error);
        return new RequestResponse(args, { data: null, code: isNaN(parseInt(error.code)) ? 500: error.code, status: error.message? error.message: 'INTERNAL SERVER ERROR', headers: {} })
    }
}

const getUrl = (url) => (`${server}${url}`);

export default {
    GET: (url, data, headers) => callServer({ url: getUrl(url), data, headers, method: 'GET', timeout: timeout }),
    POST: (url, data, headers) => callServer({ url: getUrl(url), data, headers, method: 'POST', timeout: timeout }),
    PUT: (url, data, headers) => callServer({ url: getUrl(url), data, headers, method: 'PUT', timeout: timeout }),
    DELETE: (url, data, headers) => callServer({ url: getUrl(url), data, headers, method: 'DELETE', timeout: timeout }),
}