import React from "react";
import { ISelectInputOption, ISelectInputProps } from "../../../interfaces/components/inputs/ISelectInput";
import Svg from "../Svg";

export default function SelectInput ({ label, value, options, onValueChange }: ISelectInputProps): JSX.Element {
    const [open, setOpen] = React.useState<boolean>(false);

    const getLabelStyle = function (): React.CSSProperties {
        if (value.length > 0)
            return { position: 'absolute', padding: 1, fontSize: 7, top: 0, transition: 'ease', transitionDuration: '0.5s' }
        else 
            return { position: 'absolute', padding: 1, fontSize: 12, top: 8, left: 8, transition: 'ease', transitionDuration: '0.5s' }
    }

    const getOptions = (): ISelectInputOption[] => 
        options.filter(a => value.findIndex(b => a.value === b.value) < 0);

    const getSelectedElement = (item: ISelectInputOption): JSX.Element => {
        return (
            <div className="p-1 flex items-center" key={item.value} style={{ background: "#aaa", borderRadius: 3 }}>
                <div className="text-xs" style={{ color: '#fff' }}>{item.label}</div>
                <div className="pl-1"><Svg name={'close'} stroke="white" width={12} height={12} onPress={() => onClickSelectedElement(item)} /></div>
            </div>
        )
    }

    const onClickElement = (item: ISelectInputOption): void => {
        const values = value.slice();
        values.push(item);
        onValueChange(values);
    }

    const onClickSelectedElement = (item: ISelectInputOption): void => {
        const values = value.slice();
        const index = values.findIndex(a => a.value === item.value)

        if (index < 0) return;
        values.splice(index, 1);
        onValueChange(values);
    }

    const getOptionElement = (item: ISelectInputOption): JSX.Element => {
        return (
            <div 
                key={item.value}
                className="p-1 cursor-pointer"
                onClick={() => onClickElement(item)}
            >{item.label}</div>
        )
    }

    return (
        <div className="relative w-full p-1 m-1 locktext">
            <div className="cursor-pointer" onClick={() => setOpen(!open)} style={getLabelStyle()}>{label}</div>

            <div 
                className="w-full px-1 py-0.5 cursor-pointer flex" 
                style={{ height: 28.67, borderBottomWidth: 1, borderBottomColor: '#000', borderRadius: 3, overflowX: 'auto', overflowY: 'hidden' }}
                onClick={() => {
                    if (!open && getOptions().length === 0) return;
                    setOpen(!open)
                }}
            >
                {value.map(a => getSelectedElement(a))}
            </div>

            {open && getOptions().length > 0 && (
                <div 
                    className="p-1 w-full text-xs" 
                    style={{ 
                        marginTop: 0.5,
                        border: '1px solid #aaa',
                        borderRadius: 3
                    }}
                >
                    {getOptions().map(b => getOptionElement(b))}
                </div>
            )}
        </div>
    )
}