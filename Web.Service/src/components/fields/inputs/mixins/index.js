const InputMixin = {
    props: {
        label: {
            type: String,
            required: true
        },
        modelValue: {
            type: String,
            required: true
        },
        rule: {
            type: Object,
            required: false
        },
        options: {
            type: Object,
            required: false
        },
        field: {
            type: String,
            required: true
        },
        failure: {
            type: Function,
            required: false
        }
    },

    mounted() {
        this.value = this.modelValue || '';
        if (typeof this.failure === 'function')
            this.failure(this.field, this.SetErrors)
    },

    watch: {
        value: function (value) {
            if (typeof this.checkRule === 'function') this.checkRule()
            if (typeof this.ApplyRule === 'function') value = this.ApplyRule(value)
            this.value = value;
            this.$emit('update:modelValue', value);
        }
    },

    computed: {
        labelPosition: function () {
            return ['label', 'UI-border', (this.value && this.value.length > 0) ? 'top': 'center'];
        },

        inputClass: function () {
            return ['input-field', 'UI-border', (this.errorMessages.length) ? 'error': '']
        },

        minLength: function () {
            const { min } = this.rule || { min: 0, max: 1000 };
            return min;
        },

        maxLength: function () {
            const { max } = this.rule || { min: 0, max: 1000 };
            return max;
        }
    },

    data: function () {
        return {
            value: null,
            errorMessages: []
        }
    },

    methods: {
        SetErrors: function (value) {
            this.errorMessages.push(value);
        }
    },
}

export default InputMixin