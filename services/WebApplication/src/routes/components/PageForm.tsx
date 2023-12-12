import React from "react";
import { IPageFormProps, IPageFormState } from "../../interfaces/components/IPageForm";
import Nav from "./Nav";

export default function PageForm ({ children }: IPageFormProps): JSX.Element {
    // const [state, setState] = React.useState<IPageFormState>({
    //     open: false,
    //     viewMode: 'table'
    // });

    // const getStyle = function (name: 'nav' | 'content'): React.CSSProperties {
    //     return (name === 'content') ?
    //         { backgroundColor: '#525252', width: state.open? 'calc(100% - 140px)': 'calc(100% - 30px)' }:
    //         { backgroundColor: '#323232', width: state.open? 140: 30, padding: 5, height: '100%' };
    // }

    // const onNav = function (listen: 'open' | 'close') {
    //     setState((values) => ({ ...values, open: listen === 'open' ? true: false }))
    // }

    // return (
    //     <div className='p-0 m-0 w-full h-full flex flew-row justify-center items-center'>
    //         <div className='h-full' style={getStyle('nav')}>
    //             <Nav
    //                 open={state.open}
    //                 menuOpen={() => onNav('open')}
    //                 menuClose={() => onNav('close')}
    //             />
    //         </div>

    //         <div style={getStyle('content')} className='h-full'>
    //             {/* {children} */}
    //             <div className="w-full h-full flex-1">
    //                 <div className="flex-2 flex pt-1">
    //                     <div className="mx-2 px-2 py-1 text-xs" style={{ color: '#fff' }}>{'Lista'}</div>
    //                     <div className="mx-1 px-2 py-1 text-xs" style={{ color: '#fff' }}>{'Edição'}</div>
    //                     <div className="mx-1 px-2 py-1 text-xs" style={{ color: '#fff' }}>{'Criação'}</div>
    //                 </div>

    //                 <div className="flex-1">

    //                 </div>
    //             </div>
    //         </div>
    //     </div>
    // )
    return (<></>)
}