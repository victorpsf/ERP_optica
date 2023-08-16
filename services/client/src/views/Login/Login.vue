<template>
    <div class="login">
        <div class="login-content">
            <div v-if="stepOne" class="first-page">
                <div class="login-content-header">

                </div>
                <div class="login-content-fields">
                    <input-field label="Usuário" v-model="inputs.Name" />
                    <password-field label="Senha" v-model="inputs.Password" />
                </div>
                <div class="login-content-actions">
                    <button
                        :disabled="stepOneEmpty"
                        @click="handle"
                    >
                        Submit
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Options, Vue } from 'vue-class-component'

@Options({
    data: () => ({
        inputs: {
            Name: '',
            Password: '',
            EnterpriseId: null
        },

        step: 0
    }),

    computed: {
        stepOne() {
            return this.step == 0;
        },

        stepOneEmpty() {
            return (
                !this.inputs.Name ||
                !this.inputs.Password ||
                !this.inputs.EnterpriseId
            )
        }
    },

    methods: {
        previousStep(): void {
            if (this.step == 0) return;
            this.step--;
        },

        nextStep(): void {
            this.step++;
        },

        async handle (): Promise<void> {

        }
    }
})

export default class Login extends Vue { }
</script>

<style src="./Login.scss" lang="scss"></style>