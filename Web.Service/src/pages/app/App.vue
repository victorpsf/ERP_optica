<template>
  <application-common v-if="auth.value">
    <router-view  />
  </application-common>
  <login-page v-else />
</template>

<script src="@/pages/app/App.js" lang="js"></script>
<style src="@/pages/app/App.scss" lang="scss"></style>
