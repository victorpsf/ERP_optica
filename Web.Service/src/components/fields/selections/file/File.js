import File from "@/lib/file"

const FileField = {
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
            file: null,
            files: [],
            errorMessages: []
        }
    },

    mounted() {
        if (typeof this.failure === 'function')
            this.failure(this.field, this.SetErrors)
        const { multiple = false } = (this.rule || { multiple: false })
        var input = document.createElement('input')

        input.type = "file"
        input.onchange = (event) => this.ChangeField(event)
        if (multiple)
            input.multiple =  "true"

        this.file = new File(input)
    },

    watch: {
        files: function (value) {
            this.$emit('update:modelValue', value)
        }
    },

    computed: {
        minLength: function () {
            const { min } = this.rule || { min: 0, max: 1000 }
            return min
        },

        maxLength: function () {
            const { max } = this.rule || { min: 0, max: 1000 }
            return max
        },

        labelPosition: function () {
            return ['label', 'UI-border', (this.files && this.files.length > 0) ? 'top': 'center']
        }
    },

    methods: {
        SetErrors: function (value) {
            this.errorMessages.push(value);
        },

        AddFile: function (value) {
            if (value.success)
                this.files.push(value)
            
            else
                this.errorMessages.push(`upload error: ${a.name}`)
        },

        ChangeField: async function (event = new InputEvent()) {
            this.errorMessages = []
            await this.file.ReadFiles(this.AddFile)
            this.$emit('update:modelValue', this.files)
        },

        InputFiles: function () {
            this.file.OpenDialog()
        },

        RemoveFile: function (event = new MouseEvent(), file = {}, index = -1) {
            this.files.splice(index, 1)
            this.$emit('update:modelValue', this.files)
        }
    },
}

export default FileField