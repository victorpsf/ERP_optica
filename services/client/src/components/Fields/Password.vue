<template>
    <div class="field-m1">
        <div class="field-m1-content">
            <div :class="labelClass">{{ label }}</div>
            
            <div class="field-m1-input">
                <input 
                    type="password" 
                    v-model="value"
                    :maxlength="fieldMaxLength"
                >
            </div>
        </div>
    </div>
</template>

<script src="@/components/Fields/Password.ts" lang="ts"></script>
