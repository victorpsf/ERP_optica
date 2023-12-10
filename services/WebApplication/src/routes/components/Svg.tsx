import { ISvgProps } from "../../interfaces/components/ISvg";
import React from "react";

export default function Svg(props: ISvgProps): JSX.Element {
    const onClick = function (event: MouseEvent): void {
        if (!props.onPress) return;
        props.onPress(event);
    }

    switch (props.name) {
        case 'arrow-right':
            return (
                <svg className={`${props.onPress ? 'cursor-pointer': ''} hover:!opacity-50`} onClick={(event: any) => onClick(event)} width={props.width ?? 24} height={props.height ?? 24} viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M12 17L17 12L12 7" stroke={props.stroke ?? 'black'} />
                    <path d="M15 12L3 12" stroke={props.stroke ?? 'black'} />
                </svg>
            )
        case 'arrow-left':
            return (
                <svg className={`${props.onPress ? 'cursor-pointer': ''} hover:!opacity-50`} onClick={(event: any) => onClick(event)} width={props.width ?? 24} height={props.height ?? 24} viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M8 7L3 12L8 17" stroke={props.stroke ?? 'black'} />
                    <path d="M4 12H16" stroke={props.stroke ?? 'black'} />
                </svg>
            )
        case 'close':
            return (
                <svg className={`${props.onPress ? 'cursor-pointer': ''} hover:!opacity-50`} onClick={(event: any) => onClick(event)} width={props.width ?? 25} height={props.height ?? 25} viewBox="0 0 25 25" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M18.6818 6.3106L6.68181 18.3106" stroke={props.stroke ?? 'black'} />
                    <path d="M6.68181 6.3106L18.6818 18.3106" stroke={props.stroke ?? 'black'} />
                </svg>
            )
        default:
            return <></>
    }
} 