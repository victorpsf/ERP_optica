import EventEmitter from '@/lib/EventEmitter'
import storage from '@/services/storage'
import { IsNullOrUndefined } from '@/lib/Util';

const AuthenticationService = function () {
    const event = new EventEmitter();

    setInterval(() => {
        const { token, expirationTime } = this.get()
        const args = { auth: false };

        if (IsNullOrUndefined(expirationTime)) {
            event.emit('auth',  args)
            return;
        }

        const expire = new Date(expirationTime)
        const current = new Date();
        
        if (current > expire) {
            this.unset()
            return;
        }
        
        args.auth = !IsNullOrUndefined(token);
        event.emit('auth',  args);
    }, 1000)

    this.get = function () {
        return storage.Get('user', { token: null, expirationTime: null, Name: '', EnterpriseId: null });
    }

    this.set = function (value) {
        storage.Set('user', value);
        event.emit('auth', { auth: true })
    }

    this.unset = function () {
        const { Name, EnterpriseId } = this.get();
        storage.Set('user', { token: null, expirationTime: null, Name: Name, EnterpriseId: EnterpriseId })
        event.emit('auth', { auth: false })
    }

    this.listen = function (callback, register) {
        event.on('auth', callback, register)
    }

    return this
}

export default new AuthenticationService()