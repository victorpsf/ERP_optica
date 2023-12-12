import React from 'react'
import ActionableButton from '../../components/actions/ActionableButton'
import { ICodeState } from '../../../interfaces/entity/ICode'
import { useNavigate, useSearchParams } from 'react-router-dom'
import { codeConstants } from '../../../constants';
import { ResendCode, ValidateCode } from '../../../db/external/AccountDb';
import AppStorage from '../../../db/app-storage';

export default function Code (): JSX.Element {
    const [searchParams] = useSearchParams();
    const navigate = useNavigate();
    const storage = AppStorage();
    const ref = searchParams.get('ref');
    const [state, setState] = React.useState<ICodeState>({
        Code: ['','','','','','']
    })

    React.useEffect(() => {
        if (ref !== codeConstants.login && ref !== codeConstants.forgotten) {
            navigate('/');
            window.location.reload();
        }
    }, [])


    const getCodeArray = (): string[] => state.Code.filter(a => !!a).filter(a => /\d/g.test(a))
    const getValueIndex = (index: number) => getCodeArray()[index] ?? '';

    const SignIn = async function() {
        try {
            const { result } = await ValidateCode({ Code: state.Code.join('') });

            if (result) {
                storage.set('auth.expire', result.expire)
                storage.set('auth.token', result.token)
                navigate('/')
                window.location.reload()
            }
        }

        catch(ex) { console.error(ex); }
    }

    const resendCode = async function () {
        try {
            await ResendCode();
        }

        catch(ex) { console.error(ex); }
    }

    const elementArray = function<T extends ParentNode | HTMLElement>(element: T | null | undefined, next: boolean): HTMLElement[] {
        if (!element) return [];
        const elements: HTMLElement[] = [];
        for (const el of Array.from(element.childNodes, (a) => a))
            if (next)   elements.push(elementArray(el as HTMLElement, false)[0]);
            else        elements.push(el as HTMLElement);
        return elements;
    }

    const nextElement = function (event: React.ChangeEvent<HTMLInputElement>, index: number): void {
        const data = getCodeArray();
        const value = event.target.value.replace(/[^0-9]/g, '');
        data[index] = value;
        setState((values) => ({ ...values, Code: data }))

        if (!value) return;
        if (index === 5)
            return;

        const elements = elementArray(event.target.parentNode?.parentNode, true);
        const el: HTMLInputElement = elements[index + 1] as HTMLInputElement;
        el.select();
    }

    return (
        <div className='w-full h-full flex justify-center items-center' style={{ backgroundColor: '#323232' }}>
            <div className='p-3' style={{ backgroundColor: '#fff', borderRadius: 3 }}>
                <div className='flex p-2 justify-center items-center' id="code">
                    <div className='px-2'><input className='p-1 text-center' value={getValueIndex(0)} style={{ fontSize: 8, width: 20, height: 20, borderWidth: 1, borderColor: '#000', borderRadius: 3 }} type='text' maxLength={1} onChange={(event) => nextElement(event, 0)} /></div>
                    <div className='px-2'><input className='p-1 text-center' value={getValueIndex(1)} style={{ fontSize: 8, width: 20, height: 20, borderWidth: 1, borderColor: '#000', borderRadius: 3 }} type='text' maxLength={1} onChange={(event) => nextElement(event, 1)} /></div>
                    <div className='px-2'><input className='p-1 text-center' value={getValueIndex(2)} style={{ fontSize: 8, width: 20, height: 20, borderWidth: 1, borderColor: '#000', borderRadius: 3 }} type='text' maxLength={1} onChange={(event) => nextElement(event, 2)} /></div>
                    <div className='px-2'><input className='p-1 text-center' value={getValueIndex(3)} style={{ fontSize: 8, width: 20, height: 20, borderWidth: 1, borderColor: '#000', borderRadius: 3 }} type='text' maxLength={1} onChange={(event) => nextElement(event, 3)} /></div>
                    <div className='px-2'><input className='p-1 text-center' value={getValueIndex(4)} style={{ fontSize: 8, width: 20, height: 20, borderWidth: 1, borderColor: '#000', borderRadius: 3 }} type='text' maxLength={1} onChange={(event) => nextElement(event, 4)} /></div>
                    <div className='px-2'><input className='p-1 text-center' value={getValueIndex(5)} style={{ fontSize: 8, width: 20, height: 20, borderWidth: 1, borderColor: '#000', borderRadius: 3 }} type='text' maxLength={1} onChange={(event) => nextElement(event, 5)} /></div>
                </div>

                {ref === codeConstants.login && (
                    <div className='px-2 py-0 m-0 w-full text-left break-words'>
                        <p className='p-2 break-words w-full' style={{ fontSize: 8, maxWidth: 300 }}>
                            {'E-mail contendo código de acesso foi enviado, acesse seu e-mail para obter e utilizar. caso não tenha recebido'}
                            <span className='ml-1 underline cursor-pointer' style={{ fontSize: 8 }} onClick={(event) => resendCode()}>{'clique aqui para reenviar código'}</span>
                            {'.'}
                        </p>
                    </div>
                    
                )}

                {ref === codeConstants.login && (
                    <ActionableButton 
                        label={'Logar'} 
                        disabled={(getCodeArray().length === 0)}
                        onPress={() => SignIn()} 
                    />
                )}
            </div>
        </div>
    )
}