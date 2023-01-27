import InputMixin from '@/components/fields/inputs/mixins/index'

const SearchField = {
    mixins: [InputMixin],

    methods: {
        checkRule: function () {
            this.errorMessages = []

            if (this.value.length < this.minLength) this.errorMessages.push(`${this.field}: require min ${this.minLength} characters`);
            if (this.value.length > this.maxLength) this.errorMessages.push(`${this.field}: require max ${this.minLength} caharacters`);
        }
    },
}

export default SearchField