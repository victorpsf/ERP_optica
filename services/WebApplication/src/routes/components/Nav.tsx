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

    const openMenu = function (event: MouseEvent): void {
        console.log(event)
    }

    return (
        <div className='w-full h-full flex flex-column justify-center py-1'>
            <Svg 
                className='mb-1'
                width={20}
                height={20}
                name='menu'
                stroke='white'
                onPress={openMenu}
            />

            <div>
            </div>
        </div>
    )
}