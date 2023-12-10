import { IRoute } from "../interfaces/IRoute";
import Code from "./pages/Code/Code";
import Forgotten from "./pages/Forgotten/Forgotten";

import Home from './pages/Home/Home'
import Login from './pages/Login/Login'

export const RouteList: IRoute[] = [
    { index: true, path: '/', element: Home, name: 'Pagina Inicial' },
    { path: '/login', element: Login, name: 'Sign-in', logged: false },
    { path: '/forgottem', element: Forgotten, name: 'Forgottem-Password', logged: false },
    { path: '/code', element: Code, name: 'code', logged: false },
]

export const getFiltredRoutes = (logged?: boolean): IRoute[] => RouteList.filter(a => (a.logged === undefined || logged === a.logged));