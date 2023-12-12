export type ISvgName = 'arrow-right' | 'arrow-left' | 'close' | 'menu'

export interface ISvgProps {
    name: ISvgName;
    stroke?: string;
    width?: number;
    height?: number;
    className?: string;

    onPress?: (event: MouseEvent) => void;
}