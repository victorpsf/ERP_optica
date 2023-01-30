<template>
    <div class="inputs-field noselect">
        <div :class="inputClass">
            <div class="label UI-border">{{ label }}</div>
            <div class="input icon input-border">
                <input type="search" v-model="value">
                <svg-image name="search" width="15px" height="15px" />
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

<script src="@/components/fields/inputs/search/Search.js" lang="js"></script>
<style src="@/components/fields/inputs/search/Search.scss" lang="scss"></style>