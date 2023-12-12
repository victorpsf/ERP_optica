import { IRoute } from "../interfaces/IRoute";
import Client from "./pages/Client/Client";
import Code from "./pages/Code/Code";
import Forgotten from "./pages/Forgotten/Forgotten";

import Home from './pages/Home/Home'
import Login from './pages/Login/Login'

export const RouteList: IRoute[] = [
    { path: '/', element: Home, name: 'Pagina Inicial', index: true },
    { path: '/login', element: Login, name: 'Sign-in', logged: false },
    { path: '/forgottem', element: Forgotten, name: 'Forgottem-Password', logged: false },
    { path: '/code', element: Code, name: 'code', logged: false },
    { path: '/client', element: Client, name: 'Clientes', logged: true },
]

export const getFiltredRoutes = (logged?: boolean): IRoute[] => RouteList.filter(a => (a.logged === undefined || logged === a.logged));