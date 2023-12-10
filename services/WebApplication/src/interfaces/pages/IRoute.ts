import React from "react";

export interface IRoute {
    index?: boolean;
    element: any;
    path: string;
    icon?: string;
    name: string;
    logged?: boolean;
}