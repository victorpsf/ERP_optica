import { IField } from './IField'

export interface IInputData extends IField<string> { }

export interface IInputOptions {
    maxLength?: number;
    minLength?: number;
}