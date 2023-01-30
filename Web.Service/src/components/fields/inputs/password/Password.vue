<template>
    <div class="inputs-field noselect">
        <div :class="inputClass">
            <div class="label UI-border">{{ label }}</div>
            <div class="input icon input-border">
                <input class="input icon" :type="typeName" v-model="value" maxlength="">
                <svg-image :name="svgName" @click="ChangeModeView($event)" width="15px" height="15px" />
            </div>
        </div>
        <div class="error-messages" v-if="errorMessages.length > 0">
            <ul>
                <li v-for="(message, index) in errorMessages" :key="index">
                    {{ message }}
                </li>
            </ul>
        </div>
    </div>
</template>

<script src="@/components/fields/inputs/password/Password.js" lang="js"></script>
<style src="@/components/fields/inputs/password/Password.scss" lang="scss"></style>