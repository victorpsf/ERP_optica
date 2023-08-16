import { Options, Vue } from 'vue-class-component'

@Options({
    props: {
        actionAlign: {
            type: String,
            required: false
        }
    },

    computed: {
        actionStyle(): string[] {
            const stylesClass: string[] = ['actions'];

            if (this.props.actionAlign)
                stylesClass.push(this.props.actionAlign);
            else
                stylesClass.push('right');

            console.log(stylesClass)
            return stylesClass;
        }
    }
})

export default class Form extends Vue { }