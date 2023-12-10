import React, { useEffect, useState } from 'react'
import { INavProps, INavState } from '../../interfaces/components/INav'
import Svg from './Svg'
import { useNavigate } from 'react-router-dom'

import { RouteList } from '../index'
import Storage from '../../db/app-storage'

export default function Nav(props: INavProps): JSX.Element {
    const navigate = useNavigate();
    const [state, setState] = useState<INavState>({ logged: false })
    const storage = Storage();

    useEffect(() => {
        const token = storage.get('auth.token', null);
        setState(() => ({ logged: !!token }));
    }, [state.logged]);

    return (
        <div className='w-full h-full'>
            {props.open && (
                <div className='w-full flex flex-col justify-center'>
                    <div className='w-full flex justify-end items-center'>
                        <Svg 
                            name='arrow-left' 
                            onPress={() => props.menuClose()} 
                            stroke='#ddd'
                        />
                    </div>

                    {
                        RouteList.filter(a => (a.logged === undefined || a.logged === state.logged)).map(a => (
                                <div 
                                    className='w-full cursor-pointer hover:!opacity-50 my-0.5 p-1 text-white text-xs'
                                    style={{ borderRadius: 3, backgroundColor: '#525252' }}
                                    onClick={() => navigate(a.path)} 
                                    key={a.name}
                                >{a.name}</div>
                            )
                        )
                    }
                </div>
            )}

            {!props.open && (
                <div className='w-full h-full'>
                    <div className='w-full'>
                        <Svg 
                            name='arrow-right' 
                            onPress={() => props.menuOpen()} 
                            stroke='#ddd'
                        />
                    </div>
                </div>
            )}
        </div>
    )
}