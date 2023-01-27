const Axios = require('axios');

exports.AppendUri = ({service, url}) => (`${service}${url}`);

exports.GetServiceUrl = function (envNumber = 0) {
    switch (envNumber) {
        case 1:  return process.env.AUTHENTICATION_SERVICE;
        default: return '';
    }
}

exports.CallApi = async ({ url, data, method, headers }) => {
    try {
        const result = await Axios({
            url,
            data,
            method,
            headers,
            timeout: 22000
        });

        return { code: result.status, status: result.statusText, data: result.data, headers: result.headers };
    }

    catch (error) {
        console.log(`CALLER ERROR: ${url}\n${error}`);
        return { data: null, code: isNaN(parseInt(error.code)) ? 500 : error.code, status: error.message ? error.message : 'INTERNAL SERVER ERROR', headers: {} };
    }
}

exports.CallMessage = ({url, method}, { code, status, data, headers }) => {
    return`
[${method}] ${url}
status:  ${status}
code:    ${code}
data:    ${JSON.stringify(data)}
headers: ${JSON.stringify(headers)}
`;
}