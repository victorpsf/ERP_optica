import { IScreens } from "@/interfaces/components/IView";

export const Screens: IScreens[] = [
    { name: 'home', route: '/', image: '' },
    { name: 'Cadastro Pessoa Física', route: '/form/person/physical', image: '', permission: 'ACCESSPERSONPHYSICAL' },
    { name: 'Cadastro Pessoa Jurídica', route: '/form/person/juridical', image: '', permission: 'ACCESSPERSONJURIDICAL' },
]