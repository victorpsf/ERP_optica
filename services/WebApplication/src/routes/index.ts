import { IRoute, IRoutePath } from "../interfaces/IRoute";
import Person from "./pages/Person/Person";
import Code from "./pages/Code/Code";
import Forgotten from "./pages/Forgotten/Forgotten";

import Home from './pages/Home/Home'
import Login from './pages/Login/Login'

export const registredRoutes: IRoutePath[] = [
    {
        path: "/",
        main: true,
        element: Home
    },
    {
        path: "/login",
        element: Login
    },
    {
        path: "/Forgotten",
        element: Forgotten
    },
    {
        path: "/code",
        element: Code
    },
    {
        path: "/person",
        element: Person
    },
]

export const RouteList: IRoute[] = [
    {
        name: "APP_MENU_PERSON",
        icon: "PERSON-ICON",
        logged: true,
        group: [
            {
                name: "APP_MENU_PERSON_PHYSICAL",
                icon: "PERSON-PHYSICAL-ICON",
                path: "/person?mode=physical"
            },
            {
                name: "APP_MENU_PERSON_JURIDICAL",
                icon: "PERSON-JURIDICAL-ICON",
                path: "/person?mode=juridical"
            }
        ]
    }
]

export const getFiltredRoutes = (logged?: boolean): IRoute[] => RouteList.filter(a => (a.logged === undefined || logged === a.logged));