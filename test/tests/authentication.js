const { CallApi, GetServiceUrl, CallMessage, AppendUri } = require('./Request');

const Handler = async (log) => {
    const service = GetServiceUrl(1);
    let url, result;

    try {
        url = AppendUri({service, url: '/Account/SingIn' })
        result = await CallApi({ 
            service, 
            url, 
            method: 'POST', 
            data: { 
                Name: process.env.TEST_USER_NAME,
                Key: process.env.TEST_USER_KEY,
                EnterpriseId: parseInt(process.env.TEST_USER_ENTERPRISEID),
                Code: process.env.TEST_USER_CODE
            }
        });
        log(CallMessage({method: 'POST', url}, result));
    }

    catch (error) {
        console.log('teste authentication error :: ', error);
    }

    try {
        url = AppendUri({service, url: '/Account/ResendEmail' })
        result = await CallApi({ 
            service, 
            url, 
            method: 'POST', 
            data: { 
                Name: process.env.TEST_USER_NAME,
                Key: process.env.TEST_USER_KEY,
                EnterpriseId: parseInt(process.env.TEST_USER_ENTERPRISEID),
                Code: process.env.TEST_USER_CODE
            }
        });
        log(CallMessage({method: 'POST', url}, result));
    }

    catch (error) {
        console.log('teste authentication error :: ', error);
    }
}

module.exports = Handler;