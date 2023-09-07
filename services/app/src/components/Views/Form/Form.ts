import { IFormViewFields, ISelectFieldOption } from '@/interfaces/components/IField';
import { IFormViewAction, IFormViewData } from '@/interfaces/components/IView';
import { IFailure } from '@/interfaces/db/IHttp';
import { Options, Vue } from 'vue-class-component'

@Options({
    props: {
        fields: {
            type: Array,
            required: true
        },

        failures: {
            type: Array,
            required: true
        },

        actions: {
            type: Array,
            required: true
        }
    },

    emits: ['onListen'],

    data: (): IFormViewData => ({
        values: {}
    }),

    methods: {
        extractValues(): { [key: string]: unknown } {
            const values: { [key: string]: unknown } = {};
            const fields = (this.fields as IFormViewFields<unknown>[]);

            for (const element of fields) {
                if (element.component === 'select-field') {
                    const options = (element.value || []) as ISelectFieldOption<unknown>[];
                    values[element.field] = (!element.options?.multiple) ? (options[0] || { value: undefined }).value: options.map((a: ISelectFieldOption<unknown>) => a.value);
                    continue;
                }

                values[element.field] = element.value;
            }

            return values;
        },

        handleAction(event: MouseEvent, action: IFormViewAction): void {
            this.$emit('onListen', { event, action, values: this.extractValues() });
        },

        getFailure(element: IFormViewFields<unknown>) : string | null {
            const failure = this.failures.find((a: IFailure) => a.propertie === element.field) as IFailure;
            return (failure) ? failure.message: null;
        }
    }
})

export default class FormView extends Vue { }