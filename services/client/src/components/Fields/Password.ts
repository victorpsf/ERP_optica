import { Options, Vue } from 'vue-class-component'

@Options({
    props: {
        modelValue: {
            type: String,
            required: false
        },

        label: {
            type: String,
            required: true
        },

        options: {
            type: Object,
            required: false
        }
    },

    emits: ['update:modelValue'],

    mounted() {
        this.value = (this.modelValue || '');
    },

    watch: {
        value: function (newValue, oldValue): void { this.$emit('update:modelValue', newValue); }
    },

    computed: {
        isEmpty(): boolean { return !this.value; },

        labelClass(): string[] {
            const classList = ['field-m1-label', 'noselect'];

            if (this.isEmpty)   classList.push('center');
            else                classList.push('top');

            return classList;
        },

        fieldOptions(): object {
            return (this.options || {});
        },

        fieldMaxLength(): number {
            return (this.fieldOptions.maxLength || 1000);
        }
    },

    data: () => ({
        value: '',
    })
})

export default class PasswordField extends Vue { }