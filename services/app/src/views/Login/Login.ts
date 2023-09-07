import { Options, Vue } from 'vue-class-component'
import { ILoginPageData } from '@/interfaces/views/ILogin';
import { FieldPaths } from '@/db/base/Paths';

import { SingIn } from "@/db/Login"
import { ISingIn, ISingInCode, ISingInInput } from '@/interfaces/db/ILogin';
import { IFormListenEvent } from '@/interfaces/components/IView';

@Options({
    data: (): ILoginPageData<ISingInInput, ISingInCode> => ({
        Failures: [],
        Login: {
            fields: [
                { component: 'string-field', field: 'Name', value: '', label: 'Usuário', options: { max: 250 } },
                { component: 'password-field', field: 'Password', value: '', label: 'Senha', options: { max: 250 } },
                { component: 'select-field', field: 'EnterpriseId', value: undefined, label: 'Empresa', options: { multiple: false, path: FieldPaths.enterprises } },
            ],
            data: {},
            actions: [{ label: 'Sing-In', name: 'Submit' }]
        },
        Code: {
            fields: [
                { component: 'string-field', field: 'Code', value: '', label: 'Código Acesso', options: { max: 6 } },
            ],
            data: {},
            actions: [{ label: 'Verify', name: 'Submit' }]
        },
        Page: 'login',
        Values: { }
    }),

    computed: {
        isPageLogin(): boolean { return this.Page == 'login'; },
        isPageCode(): boolean { return this.Page == 'code'; }
    },

    methods: {
        formLoginSubmit(argument: IFormListenEvent) {
            if (this.isPageCode) argument.values = { Code: argument.values?.Code || '0' };
            return this.SingIn(argument.values);
        },

        async SingIn(values: ISingIn): Promise<void> {
            this.Failures = [];
            this.Values = { ...this.Values, ...values };

            const { result, errors } = await SingIn(this.Values);

            if (result?.codeSended) this.Page = 'code';
            else if (result?.token) this.$emit('singIn', result?.token);
            else if ((errors || []).length > 0) this.Failures = errors;
        }
    }
})

export default class Login extends Vue { }