import React, { CSSProperties, useState } from 'react'

import Nav from './Nav'
import { IPageProps } from '../../interfaces/components/IPage'

export default function Page({ children }: IPageProps): JSX.Element {
    const [open, setOpen] = useState<boolean>(false);

    const getStyle = function (name: 'nav' | 'content'): CSSProperties {
        return (name === 'content') ?
            { backgroundColor: '#525252', width: open? 'calc(100% - 140px)': 'calc(100% - 30px)' }:
            { backgroundColor: '#323232', width: open? 140: 30, padding: 5, height: '100%' };
    }

    const onNav = function (listen: 'open' | 'close') {
        setOpen(listen === 'open' ? true: false)
    }

    return (
        <div className='p-0 m-0 w-full h-full flex flew-row justify-center items-center'>
            <div className='h-full' style={getStyle('nav')}>
                <Nav
                    open={open}
                    menuOpen={() => onNav('open')}
                    menuClose={() => onNav('close')}
                />
            </div>

            <div style={getStyle('content')} className='h-full'>
                {children}
            </div>
        </div>
    )
}