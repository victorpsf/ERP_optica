import { IMainAppMenuData } from '@/interfaces/components/IView';
import { Options, Vue } from 'vue-class-component'

import { Screens } from './Menu.screen'

@Options({
    data: (): IMainAppMenuData => ({
        visible: false,
        screens: Screens,
        filter: ''
    }),

    methods: {
        changeView(event: MouseEvent): void {
            this.visible = !this.visible;
        }
    }
})

export default class MenuApp extends Vue { }