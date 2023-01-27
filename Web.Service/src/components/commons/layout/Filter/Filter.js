const FilterCommon = {
    props: {
        actions: {
            type: Array,
            required: true
        }
    },

    methods: {
        ActionClick: function (event = new MouseEvent(), action) {
            this.$emit('action-click', { event, action })
        }
    },
}

export default FilterCommon;