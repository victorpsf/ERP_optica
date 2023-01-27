require('dotenv').config();

const Authentication = require('./tests/authentication');
const fs = require('fs');
const date = new Date();

const stream = fs.createWriteStream(`./log/${date.getTime()}.log`, { mode: 0666, encoding: 'utf8' , autoClose: true });

const getArrayInt = function (message = '') {
    const bytes = [];
    for (let x = 0; x < message.length; x++) bytes.push(message.charCodeAt(x));
    return new Uint8Array(bytes);
}

const saveLog = async (message) => {
    stream.write(getArrayInt(message), (error) => { if (error) console.log('stream error :: ' + error)});
}

const HandleTests = async () => {
    await Authentication(saveLog);
    stream.end();
}

HandleTests();