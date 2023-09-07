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
            @onListen="formLoginSubmit"
        />

        <!-- <div  class="ui-part-center ui-bg-white ui-flex ui-flex-column ui-p">
            <string-field label="Usuário" v-model="Inputs.Name" />
            <password-field label="Senha" v-model="Inputs.Password" />
            <select-field label="Empresa" v-model="Enterprises" :options="{ multiple: false, path: enterprisePath }" />

            <div class="ui-w-full ui-flex ui-flex-content-center ui-mtb-5px">
                <button class="ui-btn" @click="SingIn($event)">Verificar</button>
            </div>
        </div> -->
    </div>
</template>

<script src="@/views/Login/Login.ts" lang="ts"></script>
