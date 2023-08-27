import { Options, mixins } from 'vue-class-component'

import StringMixin from '@/components/Fields/mixin/string-mixin'
import { IRefDirectiveArguments } from '@/interfaces/IDirectives';
import { IPasswordFieldData } from '@/interfaces/components/IField';

@Options({
    emits: ['update:modelValue'],

    computed: {
        svgName(): string { return this.visible ? 'close-eye': 'open-eye'; },
        inputType(): string { return this.visible ? 'text': 'password'; }
    },

    data: (): IPasswordFieldData => ({
        visible: false,
        value: '',
        inputEl: undefined
    }),

    methods: {
        isEmptyValue(): boolean {
            return !this.value;
        },

        setRef(arg: IRefDirectiveArguments<HTMLInputElement, string>): void {
            if (arg.cicle != 'mounted') return;
            this.inputEl = arg.el;
        },

        setFocus(event: MouseEvent): void {
            try { (this.inputEl as HTMLInputElement).focus(); }
            catch (ex) {  }
        },

        setVisible(event: MouseEvent): void {
            this.visible = !this.visible;
        }
    }
})

export default class PasswordField extends mixins(StringMixin) { }