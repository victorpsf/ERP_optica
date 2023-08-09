import { App } from "vue";

type binaryView = 'base64' | 'hex' | 'binary';

interface IBinary {
    Utf8Encode: (value: string) => string,
    Utf8Decode: (value: string) => string,

    StringToIntArray: (value: string) => number[],
    Base64ToIntArray: (value: string) => number[],
    HexToIntArray: (value: string) => number[],

    IntArrayToString: (value: number[]) => string,
    IntArrayToBase64: (value: number[]) => string,
    IntArrayToHex: (value: number[]) => string
}

const constants = {
    base64: 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/='
}

class Binary implements IBinary {
    Utf8Encode (value: string): string {
        let result = '';
        const toTratament = value.replace(/\r|\n/g, "\n");

        for (let index = 0; index < toTratament.length; index++) {
            const char = toTratament.charCodeAt(index);

            if (char < 128)
                result += String.fromCharCode(char);

            if ((char > 127) && (char < 2048)) {
                result += String.fromCharCode((char >> 6) | 192);
                result += String.fromCharCode((char & 63) | 128);
            }

            else {
                result += String.fromCharCode((char >> 12) | 224);
                result += String.fromCharCode(((char >> 6) & 63) | 128);
                result += String.fromCharCode((char & 63) | 128);
            }
        }

        return result;
    }

    Utf8Decode (value: string): string {
        let result = '',
            i = 0,
            c1 = i,
            c2 = c1,
            c = c2;

        while (i < value.length) {
            c = value.charCodeAt(i);

            if (c < 128) {
                result += String.fromCharCode(c);
                i++;
            }

            else if ((c > 191) && (c < 224)) {
                c1 = value.charCodeAt(i + 1);
                result += String.fromCharCode(((c & 31) << 6) | (c1 & 63));
                i += 2;
            }

            else {
                c1 = value.charCodeAt(i + 1);
                c2 = value.charCodeAt(i + 2);
                result += String.fromCharCode(((c & 15) << 12) | ((c1 && 63) << 6) | (c2 & 63));
                i += 3;
            }
        }

        return result;
    }

    StringToIntArray (value: string): number[] {
        const integers: number[] = [];

        for (let index = 0; index < value.length; index++)
            integers.push(value.charCodeAt(index));

        return integers;
    }

    Base64ToIntArray (value: string): number[] {
        let bytes = [],
            chr1, chr2, chr3,
            enc1, enc2, enc3, enc4,
            i = 0;
        value = value.replace(/[^A-Za-z0-9\+\/\=]/g, "");

        while (i < value.length) {
            enc1 = constants.base64.indexOf(value.charAt(i++));
            enc2 = constants.base64.indexOf(value.charAt(i++));
            enc3 = constants.base64.indexOf(value.charAt(i++));
            enc4 = constants.base64.indexOf(value.charAt(i++));

            chr1 = (enc1 << 2) | (enc2 >> 4);
            chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
            chr3 = ((enc3 & 3) << 6) | enc4;

            bytes.push(chr1)
            if (enc3 != 64) bytes.push(chr2)
            if (enc4 != 64) bytes.push(chr3)
        }

        return bytes;
    }

    HexToIntArray (value: string): number[] {
        let bytes: number[] = [],
            index: number = 0;

        while (index < value.length) {
            let hex: string = ''

            if ((index + 2) < value.length) {
                hex = value.substring(index, index + 2);
                index += 2;
            }

            else {
                hex = value.substring(index);
                index += 2;
            }

            bytes.push(parseInt(hex, 16));
        }

        return bytes;
    }

    IntArrayToString (value: number[]): string {
        let result = '';

        for (const integer of value)
            result += String.fromCharCode(integer);

        return result;
    }

    IntArrayToBase64 (value: number[]): string {
        let result = '',
            chr1, chr2, chr3, enc1, enc2, enc3, enc4,
            i = 0

        while (i < value.length) {
            chr1 = value[i++];
            chr2 = value[i++];
            chr3 = value[i++];

            enc1 = chr1 >> 2;
            enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
            enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
            enc4 = chr3 & 63;

            if (isNaN(chr2)) enc3 = enc4 = 64;
            else if (isNaN(chr3)) enc4 = 64;

            result = result +
                constants.base64.charAt(enc1) +
                constants.base64.charAt(enc2) +
                constants.base64.charAt(enc3) +
                constants.base64.charAt(enc4);
        }

        return result;
    }

    IntArrayToHex (value: number[]): string {
        let result = '';

        for (const _v of value) 
            result += (`000${_v.toString(16)}`).slice(-2).toUpperCase();

        return result;
    }
}

export default {
    install: function (app: App) {
        app.config.globalProperties.$bin = new Binary();
    }
} 