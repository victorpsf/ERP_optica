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
                <svg className={`${props.onPress ? 'cursor-pointer hover:!opacity-50': ''}  ${props.className ?? ''}`} onClick={(event: any) => onClick(event)} width={props.width ?? 24} height={props.height ?? 24} viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M12 17L17 12L12 7" stroke={props.stroke ?? 'black'} />
                    <path d="M15 12L3 12" stroke={props.stroke ?? 'black'} />
                </svg>
            )
        case 'arrow-left':
            return (
                <svg className={`${props.onPress ? 'cursor-pointer hover:!opacity-50': ''}  ${props.className ?? ''}`} onClick={(event: any) => onClick(event)} width={props.width ?? 24} height={props.height ?? 24} viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M8 7L3 12L8 17" stroke={props.stroke ?? 'black'} />
                    <path d="M4 12H16" stroke={props.stroke ?? 'black'} />
                </svg>
            )
        case 'close':
            return (
                <svg className={`${props.onPress ? 'cursor-pointer hover:!opacity-50': ''}  ${props.className ?? ''}`} onClick={(event: any) => onClick(event)} width={props.width ?? 25} height={props.height ?? 25} viewBox="0 0 25 25" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M18.6818 6.3106L6.68181 18.3106" stroke={props.stroke ?? 'black'} />
                    <path d="M6.68181 6.3106L18.6818 18.3106" stroke={props.stroke ?? 'black'} />
                </svg>
            )
        case 'menu':
            return (
                <svg className={`${props.onPress ? 'cursor-pointer hover:!opacity-50': ''}  ${props.className ?? ''}`} onClick={(event: any) => onClick(event)} width={props.width ?? 25} height={props.height ?? 25} viewBox="0 0 25 25" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M10.6818 3.52272H3.68181V10.5227H10.6818V3.52272Z" stroke={props.stroke ?? 'black'} />
                    <path d="M21.6818 3.52272H14.6818V10.5227H21.6818V3.52272Z" stroke={props.stroke ?? 'black'} />
                    <path d="M21.6818 14.5227H14.6818V21.5227H21.6818V14.5227Z" stroke={props.stroke ?? 'black'} />
                    <path d="M10.6818 14.5227H3.68181V21.5227H10.6818V14.5227Z" stroke={props.stroke ?? 'black'} />
                </svg>
            )
        case 'search':
            return (
                <svg className={`${props.onPress ? 'cursor-pointer hover:!opacity-50': ''}  ${props.className ?? ''}`} onClick={(event: any) => onClick(event)} width={props.width ?? 25} height={props.height ?? 25} viewBox="0 0 25 25" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M11.2778 19.0142C15.696 19.0142 19.2778 15.4325 19.2778 11.0142C19.2778 6.59591 15.696 3.01419 11.2778 3.01419C6.85949 3.01419 3.27777 6.59591 3.27777 11.0142C3.27777 15.4325 6.85949 19.0142 11.2778 19.0142Z" stroke={props.stroke ?? 'black'} strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"/>
                    <path d="M21.2778 21.0142L16.9278 16.6642"                                                                                                                                                                                stroke={props.stroke ?? 'black'} strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"/>
                </svg>
            )
        case 'person':
            return (
                <svg className={`${props.onPress ? 'cursor-pointer hover:!opacity-50': ''}  ${props.className ?? ''}`} onClick={(event: any) => onClick(event)} width={props.width ?? 25} height={props.height ?? 25} viewBox="0 0 25 25" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M20.8565 21.5095V19.5095C20.8565 18.4487 20.4351 17.4312 19.6849 16.6811C18.9348 15.931 17.9174 15.5095 16.8565 15.5095H8.8565C7.79564 15.5095 6.77822 15.931 6.02808 16.6811C5.27793 17.4312 4.8565 18.4487 4.8565 19.5095V21.5095"   stroke={props.stroke ?? 'black'} strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"/>
                    <path d="M12.8565 11.5095C15.0656 11.5095 16.8565 9.71867 16.8565 7.50953C16.8565 5.30039 15.0656 3.50953 12.8565 3.50953C10.6474 3.50953 8.8565 5.30039 8.8565 7.50953C8.8565 9.71867 10.6474 11.5095 12.8565 11.5095Z"                        stroke={props.stroke ?? 'black'} strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"/>
                </svg>
            )
        default:
            return <></>
    }
} 