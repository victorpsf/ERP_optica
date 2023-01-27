export class FileData {
    lastModified = 0;
    success = 0;
    name = "";
    size = 0;
    type = '';
    bytes = []

    constructor(file = new File([], ''), bytes = [], success = 0) {
        this.lastModified = file.lastModified;
        this.name = file.name; 
        this.size = file.size; 
        this.type = file.type; 
        this.success = success; 
        this.bytes = bytes;
    }
}