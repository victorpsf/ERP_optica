import { ISelectFieldData, ISelectFieldOption } from '@/interfaces/components/IField';
import { Options, Vue } from 'vue-class-component'

import SelectOption from '@/components/Fields/SelectField/SelectOptions.vue'
import Db from '@/db/base/Db'

@Options({
    props: {
        label: {
            type: String,
            required: true
        },

        modelValue: {
            type: Array,
            required: false
        },

        options: {
            type: Object,
            required: false,
        }
    },

    components: { SelectOption },

    emits: ['update:modelvalue'],

    watch: {
        async value(newValue: ISelectFieldOption<unknown>[], oldValue?: ISelectFieldOption<unknown>[]): Promise<void> {
            this.$emit('update:modelValue', newValue);
        }
    },

    computed: {
        labelClass(): string[] {
            const classList: string[] = ['ui-m2-label', 'ui-locktext']
            if (typeof this.isEmptyValue === 'function') classList.push((this.isEmptyValue()) ? 'ui-m2-label-center': 'ui-m2-label-top')
            return classList;
        },

        fieldOptions(): { [key: string]: any } {
            return this.options || {  };
        },

        isMultiple(): boolean {
            return this.fieldOptions.multiple || false;
        },

        searchPath(): string {
            return this.fieldOptions.path || undefined;
        }
    },

    mounted(): void {
        try { 
            this.value = (typeof this.modelValue === 'object' && Array.isArray(this.modelValue)) ? this.modelValue: []; 
            this.loadOptions();
        }
        catch(ex) { console.error(ex) }
    },

    data: (): ISelectFieldData => ({
        selectOptions: [],
        value: [],
        inputEl: undefined,
        visible: false
    }),

    methods: {
        isEmptyValue(): boolean { return this.value.length == 0; },
        async loadOptions(): Promise<void> {
            try {
                const { data: { failed, result } } = await Db.get(this.searchPath);

                if (!failed) this.selectOptions = result;
            }

            catch (ex) { console.error(ex); }
        },
        applyRule(): boolean {
            return !this.isMultiple && this.value.length > 0;
        },
        setVisible(event: MouseEvent): void {
            if (((event.target as HTMLDivElement).className || '').replace(/\s/g, '_').toUpperCase() !== 'UI-M2-FIELD-VALUES_UI-POINTER') return;
            if (this.applyRule()) return;
            this.visible = !this.visible;
        },
        setOption(event: MouseEvent, option: ISelectFieldOption<unknown>): void {
            this.value.push(option);
            if (!this.isMultiple) this.visible = false;
        },
        unsetOption(event: MouseEvent, option: ISelectFieldOption<unknown>): void {
            const index = (this.value as ISelectFieldOption<unknown>[]).findIndex(
                (a: ISelectFieldOption<unknown>) => a.value === option.value
            );

            if (index < 0) return;
            this.value.splice(index, 1);
        }
    }
})

export default class SelectField extends Vue { }