<template>
    <div class="inputs-field noselect">
        <div :class="inputClass">
            <div :class="labelPosition">{{ label }}</div>
            <input class="input" type="text" v-model="value" maxlength="">
        </div>
        <div class="error-messages" v-if="errorMessages.length > 0">
            <ul>
                <li
                    v-for="(message, index) in errorMessages"
                    :key="index"
                >{{ message }}</li>
            </ul>
        </div>
    </div>
</template>

<script src="@/components/fields/inputs/number/Number.js" lang="js"></script>
<style src="@/components/fields/inputs/number/Number.scss" lang="scss"></style>