import { Guid, IsNullOrUndefined } from './Util'

const listeners = {
    on: { },
    once: { }
}

const getGuid = function (listener, mode)  {
    let guids = [];
    let guid = '';

    do {
        guid = Guid();
        if (IsNullOrUndefined(listeners[mode][listener])) break;
        guids = Object.keys(listeners[mode][listener]);
    }

    while (guids.filter(a => a == guid).length > 0);

    return guid;
}

const getAllEvents = function (mode, listener) {
    if (!listeners[mode][listener]) return []

    return Object.keys(listeners[mode][listener])
        .map(a => {
            return {
                guid: a,
                callback: listeners[mode][listener][a]
            }
        });
}

const setEvent = function (mode, listener, callback) {
    const guid = getGuid(listener, mode);
    if (IsNullOrUndefined(listeners[mode][listener]))
        listeners[mode][listener] = {};
    listeners[mode][listener][guid] = callback;
    return guid;
}

const unsetEvent = function (mode, listener) {
    if (listeners[mode][listener]) delete listeners[mode][listener];
}

const on = function (listener, callback, register) {
    const guid = setEvent('on', listener, callback)
    if (typeof register === 'function')
        register({ listener, guid })
}

const once = function (listener, callback) {
    setEvent('once', listener, callback)
}

const off = function (listener) {
    unsetEvent('on', listener)
    unsetEvent('once', listener)
}

const emit = function (listener, ...args) {
    const events = getAllEvents('on', listener)
        .concat(
            getAllEvents('once', listener)
        );

    for (const { guid, callback } of events)
        try {
            callback.apply(null, [{ guid, caller: new Date() }].concat(args))
        }

        catch (error) 
        { console.error(error) }

    unsetEvent('once', listener)
}

class EventEmitter {
    constructor() {}

    on(listener = '', callback, register) { on(listener, callback, register) }
    once(listener = '', callback, register) { once(listener, callback, register) }
    off (listener) { off(listener) }
    emit(listener, ...args) { emit.apply(null, [listener].concat(args)) }
}

export default EventEmitter