<template>
    <div class="ui-page ui-flex ui-flex-content-center ui-flex-item-center ui-bg-grey">
        <form-view 
            v-if="isPageLogin"
            :fields="Login.fields"
            :failures="Failures"
            :actions="Login.actions"
            @onListen="formLoginSubmit"
        />

        <form-view
            v-else-if="isPageCode"
            :fields="Code.fields"
            :failures="Failures"
            :actions="Code.actions"
            @onListen="formCodeSubmit"
        />
    </div>
</template>

<script src="@/views/Login/Login.ts" lang="ts"></script>
