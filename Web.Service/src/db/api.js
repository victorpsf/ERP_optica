import db from './index'

export const ListEnterprise = function (data) {
    return db.POST('/api/ap/Enterprise/List', data);
}