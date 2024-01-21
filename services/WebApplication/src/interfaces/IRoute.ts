import { ISvgName } from "./components/ISvg";

export interface IRoute {
    name: string;
    logged?: boolean;
    icon?: ISvgName;
    group?: IRoute[];
    path?: string;
}

export interface IRoutePath {
    main?: boolean;
    element: any;
    path: string;
}