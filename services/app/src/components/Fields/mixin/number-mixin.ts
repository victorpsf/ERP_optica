import { Options, Vue } from 'vue-class-component'
import { IBaseFieldData } from '@/interfaces/components/IField';

@Options({
    props: {
        label: {
            type: String,
            required: true
        },

        modelValue: {
            type: Number,
            required: false
        },

        options: {
            type: Object,
            required: false,
        }
    },

    emits: ['update:modelvalue'],

    watch: {
        async value(newValue: string, oldValue?: string): Promise<void> {
            if (typeof this.parseValue === 'function') 
                newValue = await this.parseValue(newValue);
            if (typeof this.applyRules === 'function')
                newValue = await this.applyRules(newValue);

            this.$emit('update:modelValue', newValue);
        }
    },

    computed: {
        labelClass(): string[] {
            const classList: string[] = ['ui-m1-label', 'ui-locktext']
            if (typeof this.isEmptyValue === 'function') classList.push((this.isEmptyValue()) ? 'ui-label-center': 'ui-label-top')
            return classList;
        },

        fieldOptions(): { [key: string]: any } {
            return this.options || {  };
        }
    },

    mounted(): void {
        try {
            if (typeof this.overrideMounted === 'function') this.overrideMounted();
            else if (this.modelValue) this.value = (typeof this.parseValue === 'function') ? this.parseValue(this.modelValue) : '0';
        }

        catch(ex) { console.error(ex) }
    },

    data: (): IBaseFieldData<string, HTMLInputElement> => ({
        value: '0',
        inputEl: undefined
    })
})

export default class NumberMixin extends Vue { }