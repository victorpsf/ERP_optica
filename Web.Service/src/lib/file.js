import { BufferTo, BufferToIntArray } from './binary'
import { FileData } from '@/models/FileModels'

class File {
    el = document.createElement('input');

    constructor(el = document.createElement('input')) {
        this.el = el;
    }

    OpenDialog() {
        this.el.click()
    }

    upload (file = new Blob([])) {
        return new Promise((resolve, reject) => {
            const fileReader = new FileReader();

            fileReader.onloadend = function (event) {
                resolve(event.target.result);
            }

            fileReader.onerror = function (event) {
                reject();
            }

            fileReader.readAsArrayBuffer(file);
        });
    }

    async ReadFiles (callback, encoding) {
        if (this.el.files == null) return;

        for (const file of this.el.files) {
            try {
                const buffer = await this.upload(file);
                callback(new FileData(file, BufferTo(buffer, encoding), encoding, 1))
            }

            catch {
                callback(new FileData(file, [], 0))
            }
        }
    }
}

export default File