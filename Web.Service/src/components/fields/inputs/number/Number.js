import InputMixin from '@/components/fields/inputs/mixins/index'

const NumberField = {
    mixins: [InputMixin],

    methods: {
        checkRule: function () {
            this.errorMessages = []

            if ((parseInt(this.value) || 0) < this.minLength) this.errorMessages.push(`${this.field}: require min ${this.minLength}`);
            if ((parseInt(this.value) || 0) > this.maxLength) this.errorMessages.push(`${this.field}: require max ${this.maxLength}`);
        },

        ApplyRule: function(value) {
            return value.split('').filter(a => /\d/g.test(a)).join('');
        }
    },
}

export default NumberField