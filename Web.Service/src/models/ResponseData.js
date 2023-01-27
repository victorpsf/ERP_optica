class ResponseData {
    code = 200;
    result = null;
    errors = null;

    constructor(data) {
        const values = (data || { code: 200, result: null, errors: [] });

        for (const key in values)
            this[key] = values[key];
    }

    GetData(defaultValue) {
        return this.result || defaultValue;
    }

    GetCode(defaultValue) {
        return this.code || defaultValue;
    }

    GetResError(defaultValue) {
        return this.errors || defaultValue;
    }

    Failed() {
        return (this.code < 200 || this.code > 299);
    }
}

export default ResponseData;