import { IStringInputProps } from "../../../interfaces/components/inputs/IStringInput";
import React, { CSSProperties, useState } from "react";

export default function StringInput (props: IStringInputProps): JSX.Element {
    const getLabelStyle = function (): CSSProperties {
        if (props.value)
            return { position: 'absolute', padding: 1, fontSize: 7, top: 0, transition: 'ease', transitionDuration: '0.5s' }
        else 
            return { position: 'absolute', padding: 1, fontSize: 12, top: 8, left: 8, transition: 'ease', transitionDuration: '0.5s' }
    }

    return (
        <div className="relative p-1 m-1 locktext">
            <div style={getLabelStyle()}>{props.label}</div>
            <div className="w-full">
                <input 
                    type={props.type}
                    className="px-1 py-0.5"
                    style={{ outline: 0, borderBottomWidth: 1, borderBottomColor: '#000', borderRadius: 3 }}
                    minLength={props.rule?.length?.min}
                    maxLength={props.rule?.length?.max}
                    value={props.value} 
                    onChange={(event) => props.onTextChange(event.target.value)}
                />
            </div>
        </div>
    )
}