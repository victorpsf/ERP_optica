<template>
    <layout-common
        :mode="viewMode"
        @change-view-mode="ChangeViewMode"
    >
        <filter-common 
            v-if="viewMode == 'filter'"
            :actions="controllers.actions.filter"
        >

        </filter-common>

        <grid-common v-if="viewMode == 'grid'">
        </grid-common>

        <form-common v-if="viewMode == 'form'">
        </form-common>
    </layout-common>
</template>

<script src="@/pages/register/PersonPhysical/PersonPhysical.js" lang="js"></script>
<style  src="@/pages/register/PersonPhysical/PersonPhysical.scss" lang="scss"></style>