import React from 'react'
import StringInput from '../../components/inputs/StringInput'
import { IForgottenState } from '../../../interfaces/entity/IForgotten'
import ActionableButton from '../../components/actions/ActionableButton'
import { Forgotten as ForgottenPassword } from '../../../db/external/AccountDb'
import { useNavigate } from 'react-router-dom'
import { codeConstants } from '../../../constants'

export default function Forgotten (): JSX.Element {
    const [state, setState] = React.useState<IForgottenState>({ })
    const navigate = useNavigate();

    const forgotten = async () => {
        // const { result } = await ForgottenPassword({
        //     userName: state.userName,
        //     email: state.email
        // });

        // if (result && result.codeSended)
        navigate(`/code?ref=${codeConstants.forgotten}`)
    }

    return (
        <div className='w-full h-full flex justify-center items-center' style={{ backgroundColor: '#323232' }}>
            <div className='p-3' style={{ backgroundColor: '#fff', borderRadius: 3 }}>
                <StringInput 
                    label='Usuário'
                    value={state.userName ?? ''}
                    type='text'
                    onTextChange={(text) => setState((values) => ({ ...values, userName: text }))}
                />
                <StringInput 
                    label='E-mail'
                    value={state.email ?? ''}
                    type='text'
                    onTextChange={(text) => setState((values) => ({ ...values, email: text }))}
                />
                <ActionableButton 
                    label='Solicitar'
                    disabled={(!state.email && !state.userName)}
                    onPress={() => forgotten()}
                />
            </div>
        </div>
    )
}