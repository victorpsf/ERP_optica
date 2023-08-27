import { ISingIn } from "@/interfaces/db/ILogin";
import { ISelectFieldOption } from '@/interfaces/components/IField'

export interface ILoginPageData {
    Enterprises: ISelectFieldOption<number>[];
    Inputs: ISingIn;
}