<template>
  <div class="main">
    <login-page 
      v-if="!auth" 
      @singIn="SingIn"
    />

    <router-view v-else />
  </div>
</template>

<script src="@/views/Main/Main.ts" lang="ts"></script>