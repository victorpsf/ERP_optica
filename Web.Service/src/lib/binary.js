export const BASE64KEYS = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=';

export const BufferToIntArray = function (buffer = new ArrayBuffer()) {
    const bytes = new Uint8Array(buffer);
    const intArray = [];
    for (const byte of bytes) intArray.push(byte);
    return intArray;
}

export const IntArrayToBuffer = function (array = []) {
    let bytes = new Uint8Array(array.length)

    for (let index in array)
        bytes[index] = array[index]

    return bytes.buffer;
}

export const Utf8Decode = function (utftext) {
    let string = "", i = 0, c2 = i, c1 = c2, c = c1;

    while (i < utftext.length) {
        c = utftext.charCodeAt(i);

        if (c < 128) {
            string += String.fromCharCode(c);
            i++;
        }

        else if ((c > 191) && (c < 224)) {
            c2 = utftext.charCodeAt(i + 1);
            string += String.fromCharCode(((c & 31) << 6) | (c2 & 63));
            i += 2;
        }

        else {
            c2 = utftext.charCodeAt(i + 1);
            c3 = utftext.charCodeAt(i + 2);
            string += String.fromCharCode(((c & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63));
            i += 3;
        }
    }

    return string;
}

export const Utf8Encode = function (string) {
    var utftext = "";
    string = string.replace(/\r\n/g, "\n");

    for (var n = 0; n < string.length; n++) {
        var c = string.charCodeAt(n);

        if (c < 128) {
            utftext += String.fromCharCode(c);
        }
        else if ((c > 127) && (c < 2048)) {
            utftext += String.fromCharCode((c >> 6) | 192);
            utftext += String.fromCharCode((c & 63) | 128);
        }
        else {
            utftext += String.fromCharCode((c >> 12) | 224);
            utftext += String.fromCharCode(((c >> 6) & 63) | 128);
            utftext += String.fromCharCode((c & 63) | 128);
        }
    }

    return utftext;
}

export const BufferToBase64 = function (buffer = new ArrayBuffer(0)) {
    var array = BufferToIntArray(buffer),
        output = "",
        chr1, chr2, chr3, enc1, enc2, enc3, enc4,
        i = 0

    while (i < array.length) {
        chr1 = array[i++];
        chr2 = array[i++];
        chr3 = array[i++];

        enc1 = chr1 >> 2;
        enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
        enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
        enc4 = chr3 & 63;

        if (isNaN(chr2)) { enc3 = enc4 = 64; }
        else if (isNaN(chr3)) { enc4 = 64; }

        output = output +
            BASE64KEYS.charAt(enc1) +
            BASE64KEYS.charAt(enc2) +
            BASE64KEYS.charAt(enc3) +
            BASE64KEYS.charAt(enc4);
    }

    return output;
}

export const Base64ToBuffer = function (base64 = '') {
    var bytes = [],
        chr1, chr2, chr3,
        enc1, enc2, enc3, enc4,
        i = 0;
    base64 = base64.replace(/[^A-Za-z0-9\+\/\=]/g, "");

    while (i < base64.length) {
        enc1 = BASE64KEYS.indexOf(base64.charAt(i++));
        enc2 = BASE64KEYS.indexOf(base64.charAt(i++));
        enc3 = BASE64KEYS.indexOf(base64.charAt(i++));
        enc4 = BASE64KEYS.indexOf(base64.charAt(i++));

        chr1 = (enc1 << 2) | (enc2 >> 4);
        chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
        chr3 = ((enc3 & 3) << 6) | enc4;

        bytes.push(chr1)
        if (enc3 != 64) bytes.push(chr2)
        if (enc4 != 64) bytes.push(chr3)
    }

    return IntArrayToBuffer(bytes)
}

export const HexadecimalToBuffer = function (hexadecimal = '') {
    let bytes = new Uint8Array(hexadecimal.length / 2)

    for (let index in bytes) {
        let start = index * 2,
            end = start + 2,
            hex = hexadecimal.substring(start, end)

        bytes[index] = parseInt(hex, 16)
    }

    return bytes.buffer
}

export const BufferToHexadecimal = function (buffer = new ArrayBuffer(0)) {
    let bytes = new Uint8Array(buffer),
        hexadecimal = ''

    for (let byte of bytes) {
        let hex = byte.toString(16)
        hexadecimal += ((`000${hex}`).slice(-2)).toUpperCase()
    }

    return hexadecimal
}

export const BinaryToBuffer = function (string = '') {
    let characters = Utf8Encode(string)
    let bytes = new Uint8Array(characters.length)

    for (let index = 0; index < characters.length; index++)
        bytes[index] = characters.charCodeAt(index)

    return bytes.buffer
}

export const BufferToBinary = function (buffer = new ArrayBuffer(0)) {
    let bytes = new Uint8Array(buffer),
        string = ''

    for (let byte of bytes) string += String.fromCharCode(byte)

    return Utf8Decode(string);
}

export const BufferTo = function (buffer = new ArrayBuffer(0), encoding = 'base64') {
    switch (encoding) {
        case 'hex':
            return BufferToHexadecimal(buffer);
        case 'bin':
            return BufferToBinary(buffer);
        case 'bytes':
            return BufferToIntArray(buffer);
        case 'base64':
        default:
            return BufferToBase64(buffer);
    }
}