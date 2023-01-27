<template>
    <div class="application-common noselect">
        <div class="application-menu flex flex-row flex-justify-end flex-items-center">
            <div @click="ShowOptions" class="menu-icon flex flex-justify-center flex-items-center UI-border">
                <svg-image width="30px" height="30px" name="option" />
            </div>
        </div>

        <!-- to-do: adicionar animação abertura e de fechamento -->
        <div v-if="option.open" class="menu-options">
            <div class="option-search">
                <search-field label="Buscar" field="menuSearch" v-model="search" />
            </div>

            <div
                class="option-group UI-border"
                v-for="(group, indexG) in optionToView"
                :key="indexG"
            >
                <div class="group-info flex flex-justify-evenly flex-items-center">
                    <svg-image :name="group.icon" width="25px" height="25px" />
                    <span>{{ group.name }}</span>
                </div>

                <div class="group-options UI-border flex flex-column">
                    <div
                        class="UI-border"
                        v-for="(children, indexP) in group.pattern"
                        :key="`${indexG}:${indexP}`"
                    >
                        <router-link :to="children.route">{{ children.name }}</router-link>
                    </div>
                </div>
            </div>
        </div>

        <div class="application-content">
            <slot />
        </div>
    </div>
</template>

<script src="@/components/commons/application/Application.js" lang="js"></script>
<style src="@/components/commons/application/Application.scss" lang="scss"></style>