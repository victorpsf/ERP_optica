const FieldsGroup = {
    props: {
        fields: {
            type: Array,
            required: false
        },

        actions: {
            type: Array,
            required: false
        }
    },

    mounted: function () {
        this.fieldsArray = this.fields || [];
    },

    data: function () {
        return {
            fieldsArray: []
        }
    },

    methods: {
        ApplyAction: function (event = new MouseEvent(), { label, type }) {
            let data = null;

            switch (type) {
                case 'submit':
                    data = Object.assign
                        .apply(
                            null,
                            [{}].concat(
                                this.fieldsArray.map(a => {
                                    const parser = {}
                                    parser[a.field] = a.value;
                                    return parser;
                                })
                            )
                        );
                    break;
                default:
                    break;
            }

            this.$emit('action-click', { event, info: { label, type }, data });
        }
    },
}

export default FieldsGroup