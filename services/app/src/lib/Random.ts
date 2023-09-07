export const RandomNumber = (min: number, max: number): number => Math.floor(Math.random() * (max - min + 1) + min);

export const Uuid = (): string => {
    const parts: number[][] = [
        [0,0,0,0].map(() => RandomNumber(0,255)),
        [0,0].map(() => RandomNumber(0,255)),
        [0,0].map(() => RandomNumber(0,255)),
        [0,0,0,0,0,0].map(() => RandomNumber(0,255)),
    ]

    return parts.map(
        (a: number[]): string => a.map((b: number): string => (`000${b.toString(16)}`).slice(-2)).join('')
    ).join('-');
}