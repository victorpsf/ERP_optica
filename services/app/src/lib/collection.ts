export interface ICollectionInfo { 
    start: number;
    final: number;
}

export class Collection<T> {
    values: T[];

    constructor(values: T[]) {
        this.values = values;
    }

    get count(): number { return this.values.length; }
    get isEmpty(): boolean { return this.count == 0; }
    get info(): ICollectionInfo { return { start: 0, final: (this.isEmpty) ? 0: (this.count - 1) } }

    first(): T | null { return this.isEmpty ? null: this.values[0]; }
    last(): T | null { return this.isEmpty ? null: this.values[this.info.final]; }
    get(index: number): T | null { return (this.isEmpty || this.info.final < index)? null: this.values[index]; }
    add(...data: T[]): void { this.values.push.apply(null, data); }
    put(index: number, value: T): void { this.values[index] = value; }
    slice(start?: number, end?: number) { return this.values.slice(start, end); }

    static byArray<T>(values: T[]) {
        return new Collection<T>(values);
    }
}