import InputMixin from '@/components/fields/inputs/mixins/index'

const PasswordField = {
    mixins: [InputMixin],

    data: function () {
        return {
            type: 0
        }
    },

    computed: {
        typeName: function () {
            return this.type == 0 ? 'password': 'text';
        },
        svgName: function () {
            return this.type == 0 ? 'open-eye': 'close-eye';
        }
    },

    methods: {
        checkRule: function () {
            this.errorMessages = []

            if (this.value.length < this.minLength) this.errorMessages.push(`${this.field}: require min ${this.minLength} characters`);
            if (this.value.length > this.maxLength) this.errorMessages.push(`${this.field}: require max ${this.minLength} caharacters`);
        },

        ChangeModeView: function (event = new MouseEvent()) {
            this.type = (this.type == 0) ? 1: 0;
        }
    },
}

export default PasswordField