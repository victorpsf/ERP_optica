<template>
    <div class="ui-m2-field">
        <div class="ui-m2-field-input">
            <span @click="setVisible($event)" :class="labelClass">{{ label }}</span>
            <div @click="setVisible($event)" :id="uuid" class="ui-m2-field-values ui-pointer">
                <div class="ui-w-full" v-for="(v, index) in value" :key="index" @click="unsetOption($event, v)">
                    <span class="ui-font-14">{{ v.label }}</span>
                </div>
            </div>
        </div>

        <div class="ui-m1-field-error" v-if="error">
            <span>{{ error }}</span>
        </div>

        <select-option 
            v-if="visible" 
            search
            :selected="value"
            :options="selectOptions" 
            @set="setOption"
            @unset="unsetOption"
            @close="setVisible"
        />
    </div>
</template>

<script src="@/components/Fields/SelectField/Select.ts" lang="ts"></script>
