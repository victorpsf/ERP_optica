import db from './index'

export const SingIn = function (data) {
    return db.POST('/api/au/Account/SingIn', data);
}