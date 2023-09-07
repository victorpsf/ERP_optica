import { IFormViewFields, ISelectFieldOption } from '@/interfaces/components/IField'
import { IFailure } from "../db/IHttp";
import { IFormViewAction } from '../components/IView';
import { ISingIn } from '../db/ILogin';

export type PageView = 'login' | 'code'

export interface ILoginPageDataLogin<T> {
    fields: IFormViewFields<unknown>[];
    data: T;
    actions: IFormViewAction[];
}

export interface ILoginPageData<T, B> {
    Failures: IFailure[];
    Login: ILoginPageDataLogin<T>;
    Code: ILoginPageDataLogin<B>;
    Page: PageView;
    Values: ISingIn;
}