<template>
    <div class="ui-m1-field">
        <div :class="fieldInputClass">
            <span @click="setFocus($event)" :class="labelClass">{{ label }}</span>
            <input v-ref:inpt="setRef" class="ui-text-normal" type="text" v-model="value">
            <svg-img 
                v-if="hasIcon"
                width="20"
                height="20"
                :name="icon"
            />
        </div>
    </div>
</template>

<script src="@/components/Fields/StringField/String.ts" lang="ts"></script>
