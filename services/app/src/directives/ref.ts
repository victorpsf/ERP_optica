import { DirectiveBinding, ObjectDirective, VNode } from "vue";

export const RefDirective: ObjectDirective<HTMLElement, unknown> = {
    mounted: function(
        el: HTMLElement,
        binding: DirectiveBinding<unknown>
    ): void {
        if (binding.arg && typeof binding.value == 'function')
            binding.value.apply(null, [{ name: binding.arg, el, cicle: 'mounted' }])
    }
}