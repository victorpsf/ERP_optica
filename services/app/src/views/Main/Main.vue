<template>
  <div class="main">
    <login-page 
      v-if="!auth" 
      @singIn="(token) => { this.auth = token }"
    />

    <router-view v-else />
  </div>
</template>

<script src="@/views/Main/Main.ts" lang="ts"></script>