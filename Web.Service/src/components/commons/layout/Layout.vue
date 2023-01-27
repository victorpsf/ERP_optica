<template>
    <div class="layout-common noselect">
        <div class="layout-view-modes flex flex-row">
            <div 
                v-for="(mode, index) in viewModes"
                :key="index"
                @click="ChangeViewMode($event, mode)"
                :class="ClassName(mode)"
            >
                {{ mode.label }}
            </div>
        </div>

        <div class="layout-content">
            <slot />
        </div>
    </div>
</template>

<script src="@/components/commons/layout/Layout.js" lang="js"></script>
<style src="@/components/commons/layout/Layout.scss" lang="scss"></style>