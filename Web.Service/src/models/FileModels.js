export class FileData {
    lastModified = 0;
    success = 0;
    name = "";
    data = {
        encoding: 'base64',
        value: null,
        size: 0
    }
    type = '';

    constructor(file = new File([], ''), data = null, encoding = 'base64', success = 0) {
        this.lastModified = file.lastModified;
        this.name = file.name; 
        this.type = file.type; 
        this.data = {
            size: file.size,
            encoding,
            value: data
        };
        this.success = success; 
    }
}