import { Options, Vue } from 'vue-class-component'
import { IBaseFieldData } from '@/interfaces/components/IField';

@Options({
    props: {
        label: {
            type: String,
            required: true
        },

        modelValue: {
            type: String,
            required: false
        },

        field: {
            type: String,
            required: true
        },

        error: {
            type: String,
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

            this.$emit('change', { field: this.field, value: newValue });
            this.$emit('update:modelValue', newValue);
        }
    },

    computed: {
        labelClass(): string[] {
            const classList: string[] = ['ui-m1-label', 'ui-locktext']
            if (typeof this.isEmptyValue === 'function') classList.push((this.isEmptyValue()) ? 'ui-m1-label-center': 'ui-m1-label-top')
            return classList;
        },

        fieldOptions(): { [key: string]: any } {
            return this.options || {  };
        },

        hasIcon(): boolean {
            return !!this.fieldOptions.icon;
        },

        icon(): string {
            return this.fieldOptions.icon;
        },

        fieldInputClass(): string[] {
            const classList: string[] = ['ui-m1-field-input']
            if (this.hasIcon) classList.push('ui-m1-field-input-icon', 'ui-flex', 'ui-flex-row', 'ui-flex-item-center')
            return classList;
        }
    },

    mounted(): void {
        try {
            if (typeof this.overrideMounted === 'function') this.overrideMounted();
            else if (this.modelValue) this.value = (typeof this.parseValue === 'function') ? this.parseValue(this.modelValue) : '';
        }

        catch(ex) { console.error(ex) }
    },

    data: (): IBaseFieldData<string, HTMLInputElement> => ({
        value: '',
        inputEl: undefined
    })
})

export default class StringMixin extends Vue { }