export type ISvgName = 'arrow-right' | 'arrow-left' | 'close'

export interface ISvgProps {
    name: ISvgName;
    stroke?: string;
    width?: number;
    height?: number;

    onPress?: (event: MouseEvent) => void;
}