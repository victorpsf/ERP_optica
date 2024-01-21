import React from 'react'
import { INavProps, INavState } from '../../interfaces/components/INav'
import { useNavigate } from 'react-router-dom'

import Svg from './Svg'
import StringInput from './inputs/StringInput'
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

    const handleItemClick = function (event: React.MouseEvent<HTMLDivElement, MouseEvent>, route: IRoute): void {
        if (route.path) navigate(route.path);
    }

    const getRoutes = function (): JSX.Element[] {
        const routes = RouteList.filter(a => a.logged === undefined || a.logged === state.logged);

        return routes.map(a => (
            <div className='p-3' key={a.name}>
                <h2 style={{ color: '#fff' }}>{a.name}</h2>

                {a.group && a.group.length > 0 && (
                    <div key={`${a.name}-${a.group.length}`} className='p-3 grid grid-cols-3 gap-3'>
                        {a.group.map(b => (
                            <div 
                                key={b.name}
                                className='p-3 cursor-pointer hover:!opacity-50 flex justify-center items-center flex-col' 
                                style={{ backgroundColor: '#323232', color: '#fff', borderRadius: 3 }}
                                onClick={(event) => handleItemClick(event, b)}
                            >
                                <div>
                                    {b.icon && <Svg name={b.icon ?? ''} />}
                                </div>
                                <div>
                                    {b.name}
                                </div>
                            </div>
                        ))}
                    </div>
                )}
            </div>
        ));
    }

    const getOpenedMenuContent = function (): JSX.Element {
        return (
            <div className='absolute inset-x-0 inset-y-0' style={{ backgroundColor: 'rgba(0,0,0,0.7)' }}>
                <div className='p-3'>
                    <div className='w-full p-3 flex items-center justify-center'>
                        <Svg name='close' stroke='white' onPress={onMenuButtonClick} />
                    </div>
                    <div className='w-full p-3'>
                        <div>
                            <div>
                                <StringInput 
                                    type='text'
                                    value=''
                                    label='Grupo'
                                    icon='search'
                                    onTextChange={() => {}}
                                />
                            </div>
                            <div>
                                <StringInput 
                                    type='text'
                                    value=''
                                    label='Nome'
                                    icon='search'
                                    onTextChange={() => {}}
                                />
                            </div>
                        </div>
                    </div>
                </div>
                <div className='p-3 grid grid-rows-none auto-rows-max gap-3' style={{ borderTopColor: '#fff', borderTopWidth: 1 }}>
                    {getRoutes()}
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