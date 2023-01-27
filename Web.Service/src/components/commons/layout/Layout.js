const LayoutCommon = {
    props: {
        mode: {
            type: String,
            required: true
        }
    },

    data: () => ({
        viewModes: [
            { name: 'filter', label: 'Filtro' }, 
            { name: 'grid', label: 'Listagem'},
            { name: 'form', label: 'Novo' }
        ]
    }),

    methods: {
        ChangeViewMode: function (event = new MouseEvent(), { name, label }) {
            this.$emit('change-view-mode', { original: event, name })
        },

        ClassName: function ({ name, label }) {
            return ['layout-view-mode', (this.mode == name)? 'selected': 'unselected']
        }
    },
}

export default LayoutCommon