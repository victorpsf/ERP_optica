<template>
    <div class="field-group">
        <div 
            class="fields"
        >
            <component 
                v-for="(field, index) in fieldsArray"
                :key="index"
                :label="field.label"
                :field="field.field"
                :rule="field.rule"
                :options="field.options"
                :is="field.name" 
                v-model="field.value"
            />
        </div>

        <div
            v-if="actions && actions.length > 0"
            class="actions"
        >
            <button
                v-for="(action, index) in actions" 
                :key="index"
                :class="action.type == 'submit' ? 'btn-submit' : 'btn-normal'" @click="ApplyAction($event, action)"
            >{{ action.label }}</button>
        </div>
    </div>
</template>

<script src="@/components/fields/group/Fields.js" lang="js"></script>
<style src="@/components/fields/group/Fields.scss" lang="scss">

</style>