import { IStringInputProps } from "../../../interfaces/components/inputs/IStringInput";
import React from "react";
import Svg from '../Svg'

export default function StringInput (props: IStringInputProps): JSX.Element {
    const getLabelStyle = function (): React.CSSProperties {
        if (props.value)
            return { position: 'absolute', padding: 1, fontSize: 7, top: 0, transition: 'ease', transitionDuration: '0.5s' }
        else 
            return { position: 'absolute', padding: 1, fontSize: 12, top: 12, left: 12, transition: 'ease', transitionDuration: '0.5s' }
    }

    const getIconStyle = function (): React.CSSProperties {
        return { position: 'absolute', padding: 1, fontSize: 12, top: 10, right: 16, transition: 'ease', transitionDuration: '0.5s' }
    }

    const getInputElement = function (element: HTMLDivElement): HTMLInputElement | null {
        const elements: ChildNode[] = Array.from(element.childNodes ?? [], (a) => a);
        for (const el of elements)
            if (el.nodeName === 'INPUT') return el as HTMLInputElement
        return null;
    }

    const onLabelClick = function (event: React.MouseEvent<HTMLDivElement>) {
        const target = event.target as HTMLDivElement

        const elements: HTMLDivElement[] = Array.from(target.parentNode?.childNodes ?? [], (a) => a as HTMLDivElement);
        for (const el of elements) {
            const input = getInputElement(el);
            if (input) input.select();
        }
    }

    const getStyle = function (): React.CSSProperties {
        if (!props.icon)
            return {
                padding: 2.5,
                width: 'calc(100% - 5px)',
                outline: 0, 
                borderBottomWidth: 1, 
                borderBottomColor: '#000', 
                borderRadius: 3
            }
        else 
            return {
                padding: 2.5,
                width: 'calc(100% - 5px)',
                outline: 0, 
                borderBottomWidth: 1, 
                borderBottomColor: '#000', 
                borderRadius: 3
            }
    }

    return (
        <div className="relative w-full p-1 m-1 locktext">
            <div className="cursor-text" style={getLabelStyle()} onClick={(event) => onLabelClick(event)}>{props.label}</div>
            <div className="w-full p-1">
                <input 
                    type={props.type}
                    style={getStyle()}
                    minLength={props.rule?.length?.min}
                    maxLength={props.rule?.length?.max}
                    value={props.value} 
                    onChange={(event) => props.onTextChange(event.target.value)}
                />
                {props.icon && <div className="absolute" style={getIconStyle()}><Svg name={props.icon ?? ''} /></div>}
            </div>
        </div>
    )
}