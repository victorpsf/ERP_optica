import React from 'react'

import Nav from './Nav'
import { IPageProps } from '../../interfaces/components/IPage'

export default function Page({ children }: IPageProps): JSX.Element {
    return (
        <div className='p-0 m-0 w-full h-full flex flew-row justify-center items-center'>
            <div className='h-full' style={{ backgroundColor: '#525252', width: 30 }}>
                <Nav />
            </div>

            <div className='h-full' style={{ backgroundColor: '#323232', width: 'calc(100% - 30px)' }}>
                {children}
            </div>
        </div>
    )
}