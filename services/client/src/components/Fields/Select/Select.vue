<template>
    <div class="field-m2">
        <div class="field-m2-content">
            <div :class="labelClass">{{ label }}</div>
            
            <div class="field-m2-values">
                <div class="field-m2-selected" @click="showOptions">

                </div>

                <div class="field-m2-select">

                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Vue, Options } from 'vue-class-component'

@Options({
    props: {
        label: {
            type: String,
            required: true
        },

        modelValue: {
            type: Array
        },


    },

    emits: ['update:modelValue'],

    mounted() {
        this.value = (this.modelValue || []);
    },

    watch: {
        value: function (newValue, oldValue): void { this.$emit('update:modelValue', newValue); }
    },

    computed: {
        isEmpty(): boolean { return this.value.length == 0; },

        labelClass(): string[] {
            const classList = ['field-m2-label', 'noselect'];

            if (this.isEmpty)   classList.push('center');
            else                classList.push('top');

            return classList;
        },

        fieldOptions(): object {
            return (this.options || {});
        },

        fieldIsMultiple(): number {
            return (this.fieldOptions.multiple || false);
        },

        fieldOptionsValues(): any[] {
            return (this.fieldOptions.options || []);
        }
    },

    data: () => ({
        value: [],
        visible: false
    }),

    methods: {
        showOptions(event: MouseEvent): void {
            this.visible = !this.visible;
        }
    }
})

export default class SelectField extends Vue { }
</script>
