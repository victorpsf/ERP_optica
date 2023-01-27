const PersonPhysicalRegister = {
    data: () => ({
        viewMode: 'filter',

        controllers: {
            actions: {
                filter: [
                    { label: 'Aplicar', value: 1 },
                    { label: 'Limpar', value: 2 }
                ]
            }
        }
    }),

    methods: {
        ChangeViewMode: function ({ original = new MouseEvent(), name = '' }) {
            this.viewMode = name;
        }
    },
}

export default PersonPhysicalRegister