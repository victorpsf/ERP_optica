<template>
    <div class="main">
        <div v-if="authenticated">
            
        </div>
        <login-view v-else />
    </div>
</template>

<script src="./Main.ts" lang="ts"></script>
<style src="./Main.scss" lang="scss"></style>