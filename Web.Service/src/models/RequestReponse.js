import ResponseData from './ResponseData'

class RequestResponse {
    request = {
        url: '',
        method: '',
        timeout: 0,
        data: null,
        headers: {}
    };

    response = {
        code: 0,
        status: '',
        data: new ResponseData(null),
        headers: {}
    }

    constructor(req, res) {
        for (const key in req)
            this.request[key] = req[key];
        for (const key in res)
            if (key == 'data')
                this.response.data = new ResponseData(res[key]);
            else 
                this.response[key] = res[key];
    }

    getData(defaultValue) {
        return this.response.data.GetData(defaultValue);
    }

    failed() {
        if (this.code < 200 || this.code > 299) return true;
        return this.response.data.Failed();
    }

    getResCode(defaultValue) {
        return this.response.data.GetCode(defaultValue);
    }

    getResError(defaultValue) {
        return this.response.data.GetResError(defaultValue);
    }
}

export default RequestResponse;