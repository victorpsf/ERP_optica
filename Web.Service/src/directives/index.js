export default {
    install: function (vue) {
        const ThisHandler = function (cicle, el, binding, vnode, prevVnode) {
            if (typeof binding.value !== 'function') throw new Error('v-this: expected funtion')
            binding.value({ el, cicle, name: binding.arg });
        }

        vue.directive('this', {
            created         (...args) { ThisHandler.apply(null, ['created'].concat(args)); },
            beforeMount     (...args) { ThisHandler.apply(null, ['beforeMount'].concat(args)) },
            mounted         (...args) { ThisHandler.apply(null, ['mounted'].concat(args)) },
            beforeUpdate    (...args) { ThisHandler.apply(null, ['beforeUpdate'].concat(args)) },
            updated         (...args) { ThisHandler.apply(null, ['updated'].concat(args)) },
            beforeUnmount   (...args) { ThisHandler.apply(null, ['beforeUnmount'].concat(args)) },
            unmounted       (...args) { ThisHandler.apply(null, ['unmounted'].concat(args)) }
        })
    }
}