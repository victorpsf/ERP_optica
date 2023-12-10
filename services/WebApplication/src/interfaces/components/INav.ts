import { CSSProperties } from "react";

export interface INavProps {
    open: boolean;
    menuOpen: () => void;
    menuClose: () => void;
}

export interface INavState {
    logged: boolean;
}