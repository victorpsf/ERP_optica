<template>
    <div class="ui-m2-field">
        <div class="ui-m2-field-input">
            <span @click="setVisible($event)" :class="labelClass">{{ label }}</span>
            <div @click="setVisible($event)" class="ui-m2-field-values ui-pointer">
                <div class="ui-w-full" v-for="(v, index) in value" :key="index" @click="unsetOption($event, v)">
                    <span class="ui-font-14">{{ v.label }}</span>
                </div>
            </div>
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
