<template>
    <div class="UI-modal-page">
        <div v-if="currentView == 'login'" class="UI-modal UI-border">
            <div class="UI-modal-header">
                <div class="modal-title">{{ labels.login.title }}</div>
            </div>

            <div class="UI-modal-content">
                <component 
                    :label="fields.Name.label"
                    v-model="models.login.Name"
                    :rule="fields.Name.rule"
                    :field="fields.Name.field"
                    :failure="SetError"
                    :is="fields.Name.name"
                />
                <component 
                    :label="fields.Key.label"
                    v-model="models.login.Key"
                    :rule="fields.Key.rule"
                    :field="fields.Key.field"
                    :failure="SetError"
                    :is="fields.Key.name"
                />
                <component 
                    :label="fields.Enterprises.label"
                    v-model="models.login.Enterprises"
                    :rule="fields.Enterprises.rule"
                    :field="fields.Enterprises.field"
                    :failure="SetError"
                    :options="fields.Enterprises.options"
                    :is="fields.Enterprises.name"
                />
            </div>

            <div class="errors">
                <ul v-if="errors.length > 0">
                    <li v-for="(error, index) in errors" :key="index">{{ error.message }}</li>
                </ul>
            </div>

            <div class="UI-modal-action-center">
                <button class="UI-border" @click="SingIn">{{ labels.login.action }}</button>
            </div>
        </div>

        <div v-else-if="currentView == 'code'" class="UI-modal UI-border">
            <div class="UI-modal-header">
                <div class="modal-title">{{ labels.code.title }}</div>
            </div>

            <div class="UI-modal-content">
                <component 
                    :label="fields.Code.label"
                    v-model="models.login.Code"
                    :rule="fields.Code.rule"
                    :field="fields.Code.field"
                    :failure="SetError"
                    :options="fields.Code.options"
                    :is="fields.Code.name"
                />
            </div>

            <div class="UI-modal-action-center">
                <button class="UI-border" @click="SendCode">{{ labels.code.action }}</button>
            </div>
        </div>
    </div>
</template>

<script src="@/pages/login/Login.js" lang="js"></script>
<style src="@/pages/login/Login.scss" lang="scss"></style>