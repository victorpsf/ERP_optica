type IFieldPaths = 'enterprises';
type ILoginPaths = 'SingIn' | 'ValidateCode';

export const FieldPaths: { [ key in IFieldPaths]: string; } = {
    enterprises: '/auth/Account/GetEnterprises'
}

export const LoginPaths: { [ key in ILoginPaths]: string; } = {
    SingIn: '/auth/Account/SingIn',
    ValidateCode: '/auth/Account/ValidateCode'
}