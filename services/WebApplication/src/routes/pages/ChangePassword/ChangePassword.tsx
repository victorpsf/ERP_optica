import React from 'react'
import { IChangePasswordProps, IChangePasswordState } from './IChangePassword'
import StringInput from '../../components/inputs/StringInput'
import ActionableButton from '../../components/actions/ActionableButton'
import { ForgottenChangePassword } from '../../../db/external/ForgottenDb'
import { useNavigate } from 'react-router-dom'

const ChangePassword = function (props: IChangePasswordProps): JSX.Element {
    const navigate = useNavigate();
    const [state, setState] = React.useState<IChangePasswordState>({
        Passphrase: '',
        Confirm: ''
    })

    const changePassphrase = async function (): Promise<void> {
        try {
            const { result } = await ForgottenChangePassword({
                Passphrase: state.Passphrase,
                Confirm: state.Confirm
            })

            if (result?.success) {
                navigate('/')
                window.location.reload();
            };
        }

        catch (ex) { console.error(ex); }
    }

    return (
        <div className='w-full h-full flex justify-center items-center' style={{ backgroundColor: '#323232' }}>
            <div className='p-3' style={{ backgroundColor: '#fff', borderRadius: 3 }}>
                <StringInput 
                    label='Senha'
                    value={state.Passphrase ?? ''}
                    type='password'
                    onTextChange={(text) => setState((values) => ({ ...values, Passphrase: text }))}
                />
                <StringInput 
                    label='Confirmar Senha'
                    value={state.Confirm ?? ''}
                    type='password'
                    onTextChange={(text) => setState((values) => ({ ...values, Confirm: text }))}
                />
                <ActionableButton 
                    label='Alterar'
                    disabled={(!state.Passphrase && !state.Confirm)}
                    onPress={() => changePassphrase()}
                />
            </div>
        </div>
    )
}

export default ChangePassword