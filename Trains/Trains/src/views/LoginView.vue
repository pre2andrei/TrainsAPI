<script setup>
import { reactive } from "vue";

const loginUser = reactive({ username: "", password: "" });

async function getToken() {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(loginUser),
  };
  const res = await fetch(
    `https://localhost:7063/api/authentication/login`,
    requestOptions
  );
  const finalRes = await res.json();

  if (finalRes.token != null) {
    this.$cookie.set("refreshToken", finalRes.token, 1);
    alert("login succesful");
  }
}
</script>

<template>
  <main>
    <input type="text" v-model="loginUser.username" /><input
      type="password"
      v-model="loginUser.password"
    />
    <button @click="getToken">Login</button>
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
