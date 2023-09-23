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
                        <svg-img :name="screen.image" width="25" height="25" />
                        <div class="name">
                            <router-link :to="screen.route">{{ screen.name }}</router-link>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script src="@/components/Views/Main/Menu/Menu.ts" lang="ts"></script>
