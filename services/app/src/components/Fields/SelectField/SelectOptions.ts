import { Options, Vue } from 'vue-class-component'

import { ISelectFieldOption, ISelectOptionsFieldData, ISelectedFieldOption } from '@/interfaces/components/IField';

@Options({
    props: {
        options: {
            type: Array,
            required: true
        },

        selected: {
            type: Array,
            required: true
        },

        search: {
            type: Boolean,
            required: false
        }
    },

    emits: ['close', 'unset', 'set'],

    computed: {
        listOptions(): ISelectedFieldOption<unknown>[] {
            return this.applyFilter();
        },

        listSelected(): ISelectFieldOption<unknown>[] {
            return this.selected || [];
        },

        optionsClass(): string[] {
            const classList: string[] = [];
            if (this.search) classList.push('ui-mtb-10px')
            return classList;
        }
    },

    data: (): ISelectOptionsFieldData => ({
        filter: ''
    }),

    methods: {
        applyFilter(): ISelectedFieldOption<unknown>[] {
            const   filter = this.filter.toUpperCase().replace(/\s/g, ''), 
                    regexp = new RegExp(filter, 'g'),
                    values: ISelectedFieldOption<unknown>[] = this.options.map(
                (a: ISelectFieldOption<unknown>) => ({ 
                    label: a.label, 
                    value: a.value, 
                    selected: !!this.listSelected.find(
                        (b: ISelectFieldOption<unknown>) => b.value === a.value)
                })
            );

            return !this.search ? values: values.filter((a: ISelectedFieldOption<unknown>) => regexp.test(a.label.toUpperCase().replace(/\s/g, '')));
        },

        close(event: MouseEvent): void {
            const className = (event.target as HTMLDivElement).className || '';
            if (className.replace(/\s/g, '_').toUpperCase() === 'UI-PAGE_UI-MODAL_UI-FLEX_UI-FLEX-CONTENT-CENTER_UI-FLEX-ITEM-CENTER')
                this.$emit('close', event);
        },

        addValue(event: MouseEvent, option: ISelectedFieldOption<unknown>) {
            this.$emit(option.selected ? 'unset': 'set', event, { label: option.label, value: option.value });
        }
    }
})

export default class SelectOptionsField extends Vue { }