import React from 'react'
import { ILoginState } from '../../../interfaces/entity/ILogin'
import StringInput from '../../components/inputs/StringInput'
import ActionableButton from '../../components/actions/ActionableButton'
import BooleanInput from '../../components/inputs/BooleanInput'
import { useNavigate } from 'react-router-dom'
import { codeConstants } from '../../../constants'
import { SigIn } from '../../../db/external/AccountDb'
import SelectInput from '../../components/inputs/SelectInput'
import AppStorage from '../../../db/app-storage'
import { IEnterprise } from '../../../interfaces/entity/IEnterprise'
import { GetEnterprises } from '../../../db/external/EnterpriseDb'

export default function Login (): JSX.Element {
    const navigate = useNavigate();
    const storage = AppStorage();
    const [enterprises, setEnterprises] = React.useState<IEnterprise[]>([]);
    const [state, setState] = React.useState<ILoginState>({
        input: {
            Name: storage.get('auth.name', '') as string,
            Password: '',
            EnterpriseId: 0
        },
        remenber: false,
        enterprises: []
    });

    React.useEffect(() => { 
        let ignore = false;
        GetEnterprises()
            .then(({ result = [] }) => {
                if (ignore) return;
                setEnterprises((result ?? []))
            })
            .catch((e) => console.error(e))
        return () => { ignore = true; }
    }, []);

    const onSingIn = async function (): Promise<void> {
        try {
            if (state.remenber)
                storage.set('auth.name', state.input.Name)
            const { result } = await SigIn({
                Name: state.input.Name,
                Password: state.input.Password,
                EnterpriseId: state.enterprises[0].value
            })

            if (result && result.codeSended)
                navigate(`/code?ref=${codeConstants.login}`)
        }

        catch (ex) { console.error(ex); }
    }

    return (
        <div className='w-full h-full flex justify-center items-center' style={{ backgroundColor: '#323232' }}>
            <div className='p-3 w-full max-w-screen-sm' style={{ backgroundColor: '#fff', borderRadius: 3 }}>
                <StringInput 
                    label={'Usuário'}
                    value={state.input.Name}
                    type={'text'}
                    onTextChange={(text: string) => setState((values) => ({ ...values, input: { ...values.input,  Name: text } }))}
                />
                <StringInput 
                    label={'Password'}
                    value={state.input.Password}
                    type={'password'}
                    onTextChange={(text: string) => setState((values) => ({ ...values, input: { ...values.input, Password: text } }))}
                />
                <SelectInput 
                    label={'Empresa'}
                    value={state.enterprises}
                    options={enterprises}
                    onValueChange={(data) => setState((values) => ({ ...values, enterprises: data }))}
                />
                <BooleanInput
                    label={'lembrar de mim'} 
                    labelClassName='px-1 text-xs'
                    value={state.remenber}
                    direction='right'
                    onValueChange={(value) => setState((values) => ({ ...values, remenber: value }))}
                />
                <div className='px-2 py-0 m-0 w-full text-right' style={{ height: 10 }}>
                    <p className='underline cursor-pointer' style={{ fontSize: 8 }} onClick={(event) => navigate('/forgotten')}>
                        {'Esqueçi a senha'}
                    </p>
                </div>

                <ActionableButton 
                    label={'Continuar'}
                    disabled={state.input.Name.trim().length === 0 || state.input.Password.trim().length === 0 || state.enterprises.length === 0}
                    onPress={() => onSingIn()}
                />
            </div>
        </div>
    )
}