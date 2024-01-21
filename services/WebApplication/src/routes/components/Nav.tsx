import React from 'react'
import { INavProps, INavState } from '../../interfaces/components/INav'
import Svg from './Svg'
import { useNavigate } from 'react-router-dom'

import { RouteList } from '../index'
import Storage from '../../db/app-storage'
import { IRoute } from '../../interfaces/IRoute'

export default function Nav(props: INavProps): JSX.Element {
    const navigate = useNavigate();
    const [state, setState] = React.useState<INavState>({ logged: false, opened: false })
    const storage = Storage();

    React.useEffect(() => {
        const token = storage.get('auth.token', null);
        setState((values) => ({ ...values, logged: !!token }));
    }, [state.logged]);

    const onMenuButtonClick = function (event: MouseEvent): void {
        setState((values) => ({ ...values, opened: !values.opened }));
    }

    const getRoutes = function (): JSX.Element[] {
        const routes = RouteList.filter(a => a.logged === undefined || a.logged === state.logged);

        return routes.map(a => <div key={a.name}></div>);
    }

    const getOpenedMenuContent = function (): JSX.Element {
        return (
            <div className='absolute inset-x-0 inset-y-0' style={{ backgroundColor: 'rgba(0,0,0,0.7)' }}>
                <div >
                    <div className='w-full p-3 flex items-center justify-center'>
                        <Svg name='close' stroke='white' onPress={onMenuButtonClick} />
                    </div>
                    <div>

                    </div>
                </div>
                <div>

                </div>
            </div>
        )
    }

    return (
        <div className='w-full h-full flex flex-column justify-center py-1'>
            <Svg 
                className='mb-1'
                width={20}
                height={20}
                name='menu'
                stroke='white'
                onPress={onMenuButtonClick}
            />

            {state.opened && (getOpenedMenuContent())}
        </div>
    )
}