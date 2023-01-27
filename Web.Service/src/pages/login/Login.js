import { Login } from '@/models/LoginModels'
import { ListEnterprise } from '@/db/api'
import { SingIn } from '@/db/authentication'

const LoginPage = {
    data: () => ({
        models: {
            login: new Login()
        },

        labels: {
            login: {
                title: 'Acessar o sistema',
                action: 'Acessar'
            },
            code: {
                title: 'Verificação de Código',
                action: 'Verificar'
            }
        },

        view: {
            last: 'login',
            current: 'login'
        },

        fields: {
            Name: { name: 'string-field', label: 'Usuário', field: 'Name', rule: { min: 5 } },
            Key: { name: 'password-field', label: 'Senha', field: 'Key', rule: { min: 8 } },
            Enterprises: { name: 'select-field', label: 'Empresa', field: 'Enterprises', rule: { multiple: false }, options: { values: [], initial: [] } },
            Code: { name: 'number-field', label: 'Código Autenticação', field: 'Code', rule: { min: 100000, max: 1000000 } }
        },

        failures: {

        },

        errors: []
    }),

    mounted: function () {
        this.Load();
    },

    computed: {
        currentView: function () {
            return this.view.current;
        }
    },

    methods: {
        Load: async function () {
            const value = await ListEnterprise({});
            if (value.failed()) return
            this.fields.Enterprises.options.values = value.getData([])
        },

        SetError: function (field, callback) {
            this.failures[field] = callback
        },
        
        SetErrors: function (errors = []) {
            for (const error of errors) {
                if (error.field == null) {
                    this.errors.push(error)
                    continue
                }
                
                if (error.field == 'EnterpriseId') {
                    if (typeof this.failures.Enterprises !== 'function') continue;
                    this.failures.Enterprises(error.message)
                }

                else {
                    if (typeof this.failures[error.field] !== 'function') continue;
                    this.failures[error.field](error.message)
                }
            }
        },

        SetViewMode: function (current) {
            this.view.last = this.view.current;
            this.view.current = current;
        },

        SingIn: async function (event = new MouseEvent(), step = 1) {
            this.models.login.setEnterpriseId()
            const result = await SingIn(this.models.login.toJSON())

            if (result.failed())
                return this.SetErrors(result.getResError([]))
            switch (step) {
                case 1:
                    this.SetViewMode('code')
                    break
                case 2:
                    const { token, expirationTime } = result.getData({});
                    this.$service.authentication.set({ token, expirationTime, Name: this.models.login.Name, EnterpriseId: this.models.login.EnterpriseId });
                    break
            }
        },

        SendCode: async function (event = new MouseEvent()) {
            this.SingIn(event, 2);
        }
    },
}

export default LoginPage