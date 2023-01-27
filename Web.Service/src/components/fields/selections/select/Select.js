const SelectField = {
    props: {
        label: {
            type: String,
            required: true
        },
        modelValue: {
            type: Array,
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

    data: function () {
        return {
            values: [],
            errorMessages: [],
            search: '',
            view: false
        }
    },

    mounted: function () {
        this.Load()
        if (typeof this.failure === 'function')
            this.failure(this.field, this.SetErrors)
    },

    computed: {
        labelPosition: function () {
            return ['label', 'UI-border', (this.values && this.values.length > 0) ? 'top': 'center']
        },

        optionValue: function () {
            const { multiple = false } = (this.rule || { multiple: false });
            const { values = [], initial = [] } = (this.options || { values: [], initial: [] });

            if (!multiple && this.values.length > 0)
                return []

            return this.ApplySearch(
                values.slice()
                    .filter(a => this.values.find(b => a.id == b.id) == undefined), 
                this.search
            )
        },

        className: function () {
            return ['select-input', 'UI-border', this.errorMessages.length > 0 ? 'error': '']
        }
    },

    methods: {
        SetErrors: function (value) {
            this.errorMessages.push(value);
        },

        Load: function () {
            const { values = [], initial = [] } = (this.options || { values: [], initial: [] });

            const intialValues = values.filter(a => initial.find(b => b == a.id) !== undefined);
            for (const value of intialValues)
                this.values.push(value)
        },

        ApplySearch: function (values = [], search = '') {
            if (!search) return values
            const reg = new RegExp(search.toUpperCase(), 'g')
            return values.filter(b => {
                return reg.test(b.Name.toUpperCase())
            })
        },

        ChangeView: function (event = new MouseEvent()) {
            this.view = !this.view;
        },

        SelectValue: function (event = new MouseEvent(), option) {
            this.values.push(option)
            this.errorMessages = []
            this.$emit('update:modelValue', this.values)
        },

        RemoveValue: function (event = new MouseEvent(), option, index) {
            this.values.splice(index, 1)
            this.errorMessages = []
            this.$emit('update:modelValue', this.values)
        }
    },
}

export default SelectField