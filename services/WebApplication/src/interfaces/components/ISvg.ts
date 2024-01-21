export type ISvgName = 'arrow-right' | 'arrow-left' | 'close' | 'menu' | 'search' | 'person' | '';

export interface ISvgProps {
    name: ISvgName;
    stroke?: string;
    width?: number;
    height?: number;
    className?: string;

    onPress?: (event: MouseEvent) => void;
}