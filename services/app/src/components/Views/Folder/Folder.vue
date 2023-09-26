<template>
    <div class="folder">
        <div class="folder-header">
            <div class="folter-header-ticket active">
                Busca
            </div>
            <div class="folter-header-ticket">
                Informação
            </div>
            <div class="folter-header-ticket">
                Formulario
            </div>
        </div>
        <div class="folder-content">
            <slot />
        </div>
    </div>
</template>

<script src="@/components/Views/Folder/Folder.ts" lang="ts"></script>
<style src="@/components/Views/Folder/Folder.scss" lang="scss"></style>