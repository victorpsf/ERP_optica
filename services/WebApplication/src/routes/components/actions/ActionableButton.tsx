import { ActionableButtonProps } from '../../../interfaces/components/actions/IActionableButton'
import React from 'react'

export default function ActionableButton (props: ActionableButtonProps): JSX.Element {
    return (
        <button 
            disabled={props.disabled}
            style={{ opacity: props.disabled? 0.5: 1 }}
            className='p-2 text-xs w-full hover:!opacity-50'
            onClick={(event: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
                if (!props.disabled) props.onPress(event);
            }}
        >{ props.label }</button>
    )
}