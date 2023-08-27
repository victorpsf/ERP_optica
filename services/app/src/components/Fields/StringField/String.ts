import { Options, mixins } from 'vue-class-component'

import StringMixin from '@/components/Fields/mixin/string-mixin'
import { IRefDirectiveArguments } from '@/interfaces/IDirectives';

@Options({
    emits: ['update:modelValue'],

    methods: {
        isEmptyValue(): boolean {
            return !this.value;
        },

        setRef(arg: IRefDirectiveArguments<HTMLInputElement, string>): void{
            if (arg.cicle != 'mounted') return;
            this.inputEl = arg.el;
        },

        setFocus(event: MouseEvent): void {
            try { (this.inputEl as HTMLInputElement).focus(); }
            catch (ex) {  }
        }
    }
})

export default class StringField extends mixins(StringMixin) { }