const ApplicationCommon = {
    data: function () {
        return {
            search: '',
            option: {
                open: false,
                values: [
                    {
                        name: 'Cadastro Pessoal',
                        icon: 'persons',
                        pattern: [
                            { name: 'Pessoa Fisica', route: '/person/physical' },
                            { name: 'Pessoa Juridica', route: '/person/juridical' },
                        ]
                    }
                ]
            }
        }
    },

    computed: {
        optionToView: function () {
            if (!this.search) return this.option.values;

            const reg = new RegExp(this.search.toUpperCase(), 'g');
            return this.option.values.filter(group => {
                const isGroup = reg.test(group.name.replace(/\s/g, '').toUpperCase());
                if (isGroup) return true;

                const isPattern = group.pattern.filter(children => {
                    return reg.test(children.name.replace(/\s/g, '').toUpperCase())
                }).length > 0

                return isPattern;
            })
        }
    },

    methods: {
        ShowOptions: function (event = new MouseEvent()) {
            this.option.open = !this.option.open
        },

        RouteClick: function (event = new MouseEvent(), { name, route }) {

        }
    },
}

export default ApplicationCommon;