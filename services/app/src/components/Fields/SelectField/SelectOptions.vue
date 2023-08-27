<template>
    <div @click="close($event)" class="ui-page ui-modal ui-flex ui-flex-content-center ui-flex-item-center">
        <div class="ui-part-center ui-bg-white ui-flex ui-flex-column ui-p">
            <div v-if="search">
                <string-field label="Busca" icon="search" v-model="filter" />
            </div>
            <div :class="optionsClass">
                <div 
                    class="ui-locktext ui-pointer" 
                    v-for="(option, index) in listOptions" 
                    :key="index"
                    @click="addValue($event, option)"
                >
                    {{ option.selected ? `${option.label} *`: option.label }}
                </div>
            </div>
        </div>
    </div>
</template>

<script src="@/components/Fields/SelectField/SelectOptions.ts" lang="ts"></script>