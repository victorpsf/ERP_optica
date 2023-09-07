<template>
    <div class="ui-part-center ui-bg-white ui-flex ui-flex-column ui-p">
        <div class=""></div>

        <component 
            v-for="(element, index) in fields"
            :key="index"
            :label="element.label"
            :error="getFailure(element)"
            @change="(...args) => {}"
            :field="element.field"
            :options="element.options"
            v-model="element.value"
            :is="element.component"
        />

        <div class="ui-w-full ui-flex ui-flex-content-center ui-mtb-5px">
            <button 
                class="ui-btn"
                v-for="(action, index) in actions"
                :key="index"
                @click="handleAction($event, action)"
            >{{ action.label }}</button>
        </div>
    </div>
</template>

<script src="@/components/Views/Form/Form.ts" lang="ts"></script>