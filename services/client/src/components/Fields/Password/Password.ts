import { Options, Vue } from 'vue-class-component'

@Options({
    props: {
        modelValue: {
            type: String,
            required: false
        }
    },

    emits: ['update:modelValue'],

    mounted() {
        this.value = this.modelValue;
    },

    watch: {
        value(newValue, oldValue) { this.$emit('update:modelValue', newValue); }
    },

    data: () => ({
        value: '',
    }),

    methods: {
        
    }
})

export default class PasswordField extends Vue { }