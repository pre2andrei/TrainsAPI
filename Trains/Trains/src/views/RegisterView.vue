<script setup>
import { reactive } from "vue";

const regiserUser = reactive({ username: "", password: "", email: "" });
</script>

<template>
  <main>
    <input type="text" v-model="regiserUser.username" /><input
      type="text"
      v-model="regiserUser.password"
    /><input type="text" v-model="regiserUser.email" />
  </main>
</template>
<style scoped>
main {
  width: calc(100vw - 200px);
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}
input {
  margin: 10px;
}
</style>
