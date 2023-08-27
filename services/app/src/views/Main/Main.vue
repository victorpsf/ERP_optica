<template>
  <div class="main">
    <login-page v-if="!auth" />
    <router-view v-else />
  </div>
</template>

<script src="@/views/Main/Main.ts" lang="ts"></script>