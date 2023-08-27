import { Options, Vue } from 'vue-class-component'
import { ILoginPageData } from '@/interfaces/views/ILogin';
import { FieldPaths } from '@/db/base/Paths';

import { SingIn } from "@/db/Login"
import { ISelectFieldOption } from '@/interfaces/components/IField';

@Options({
    data: (): ILoginPageData => ({
        Enterprises: [],
        Inputs: { }
    }),

    computed: {
        enterprisePath(): string { return FieldPaths.enterprises; }
    },

    methods: {
        async SingIn(event: MouseEvent): Promise<void> {
            const enterprise: ISelectFieldOption<number> = (this.Enterprises[0] || { value: 0, label: '' }) as ISelectFieldOption<number>;
            this.Inputs.EnterpriseId = enterprise.value;

            try {
                const data = await SingIn(this.Inputs);
                console.log(data)
            }

            catch (ex) { console.error(ex) }
        }
    }
})

export default class Login extends Vue { }