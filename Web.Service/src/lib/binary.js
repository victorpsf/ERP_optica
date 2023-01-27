export const BufferToIntArray = function (buffer = new ArrayBuffer()) {
    const bytes = new Uint8Array(buffer);
    const intArray = [];
    for (const byte of bytes) intArray.push(byte);
    return intArray;
}