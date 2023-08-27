<template>
    <div class="ui-m1-field">
        <div class="ui-m1-field-input ui-m1-field-input-icon ui-flex ui-flex-row ui-flex-item-center">
            <span @click="setFocus($event)" :class="labelClass">{{ label }}</span>
            <input v-ref:inpt="setRef" class="ui-text-normal" :type="inputType" v-model="value">
            <svg-img class="ui-pointer" width="20" height="20" @click="setVisible($event)" :name="svgName" />
        </div>
    </div>
</template>

<script src="@/components/Fields/PasswordField/Password.ts" lang="ts"></script>
