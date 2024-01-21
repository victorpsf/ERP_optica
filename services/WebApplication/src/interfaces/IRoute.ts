export interface IRoute {
    name: string;
    logged?: boolean;
    icon: string;
    group?: IRoute[];
    path?: string;
}

export interface IRoutePath {
    main?: boolean;
    element: any;
    path: string;
}