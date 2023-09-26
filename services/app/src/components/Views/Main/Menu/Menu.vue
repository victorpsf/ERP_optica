<template>
    <div class="main-application-menu">
        <svg-img name="menu" width="20" height="20" @click="changeView" />

        <div v-if="visible" class="main-application-menu-content">
            <div class="main-application-menu-content-header">
                <svg-img class="close" name="close" width="20" height="20" @click="changeView" />
                <div class="search">
                    <div class="search-input">
                        <string-field label="Busca" field="search" :options="{ icon: 'search' }" v-model="filter" />
                    </div>
                </div>
            </div>

            <div class="main-application-menu-content-applications">
                <div
                    class="main-application-menu-itens"
                >
                    <div 
                        v-for="(screen, index) in screens"
                        :key="index"
                        class="main-application-menu-item"
                    >
                        <svg-img class="main-application-menu-item-icon" :name="screen.image" width="50" height="50" />
                        <div class="main-application-menu-item-name">
                            <router-link :to="screen.route">{{ screen.name }}</router-link>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script src="@/components/Views/Main/Menu/Menu.ts" lang="ts"></script>
<style src="@/components/Views/Main/Menu/Menu.scss" lang="scss"></style>
