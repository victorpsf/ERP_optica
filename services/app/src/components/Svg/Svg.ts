import { Options, Vue } from 'vue-class-component'

@Options({
    props: {
        name: {
            type: String,
            required: false
        },

        width: {
            type: String,
            required: false
        },

        height: {
            type: String,
            required: false
        }
    },

    computed: {
        isOpenEye(): boolean { return (this.name || '').toUpperCase() == 'OPEN-EYE'; },
        isCloseEye(): boolean { return (this.name || '').toUpperCase() == 'CLOSE-EYE'; },
        isSearch(): boolean { return (this.name || '').toUpperCase() == 'SEARCH'; }
    }
})

export default class SvgImg extends Vue { }