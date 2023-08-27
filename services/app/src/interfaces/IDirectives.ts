export interface IRefDirectiveArguments <T, R> {
    el: T;
    name?: R;
    cicle: 'created' | 'beforeMount' | 'mounted' | 'beforeUpdate' | 'updated' | 'beforeUnmount' | 'unmounted';
}