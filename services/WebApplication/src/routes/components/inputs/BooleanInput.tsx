import { IBooleanInputProps } from "../../../interfaces/components/inputs/IBooleanInput";
import React from "react";

export default function BooleanInput ({ label, value, direction = 'left', labelClassName, onValueChange }: IBooleanInputProps): JSX.Element {
    return (
        <div className="p-1 m-1 locktext w-full flex">
            {direction === 'left' && (<div className={labelClassName ?? ''}>{label}</div>)}
            <input 
                type={'checkbox'}
                className="px-1 py-0.5"
                style={{ outline: 0, borderBottomWidth: 1, borderBottomColor: '#000', borderRadius: 3 }}
                value={(value ?? false).toString()}
                onClick={() => onValueChange(!value)}
            />
            {direction === 'right' && (<div className={labelClassName ?? ''}>{label}</div>)}
        </div>
    )
}