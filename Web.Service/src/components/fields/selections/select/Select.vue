<template>
    <div class="selection-select-field noselect">
        <div
            :class="className"
        >
            <div :class="labelPosition">{{ label }}</div>
            <div class="select-click" @click="ChangeView($event)">
                <svg-image
                    name="option"
                    width="15px"
                    height="15px"
                />
            </div>
            <div class="select-values">
                <div
                    v-for="(value, index) in values"
                    class="select-value UI-border"
                    :key="index"
                >
                    <div 
                        class="select-value-action"
                        @click="RemoveValue($event, value, index)"
                    >
                        <svg-image
                            name="close"
                            width="10px"
                            height="10px"
                        />
                    </div>

                    <div class="select-value-data">
                        {{ value.name }}
                    </div>
                </div>
            </div>
        </div>
        <div
            v-if="view"
            class="option-input UI-border"
        >
            <div class="option-search">
                <search-field v-model="search" label="Filtrar" field="search" />
            </div>
            <div class="options">
                <div
                    v-if="optionValue.length > 0"
                    v-for="(option, index) in optionValue"
                    :key="index"
                    class="option"
                    @click="SelectValue($event, option)"
                >
                    {{ option.name }}
                </div>
                <div v-else class="option unset">
                    No found Itens
                </div>
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

<script src="@/components/fields/selections/select/Select.js" lang="js"></script>
<style src="@/components/fields/selections/select/Select.scss" lang="scss"></style>