<template>
    <div class="filter-page flex flex-row">
        <div class="filters">

        </div>
        <div class="actions">
            <button
                class="flex flex-column"
                v-for="(action, index) in actions"
                @click="ActionClick($event, action)"
                :key="index"
            >
                {{ action.label }}
            </button>
        </div>
    </div>
</template>

<script src="@/components/commons/layout/Filter/Filter.js" lang="js"></script>
<style src="@/components/commons/layout/Filter/Filter.scss" lang="scss"></style>