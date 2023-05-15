<script setup>
import { RouterLink, RouterView } from "vue-router";
</script>

<template>
  <div class="appwrap">
    <header>
      <div class="nav">
        <RouterLink to="/">Home</RouterLink>
        <!-- <RouterLink to="/login">Login</RouterLink>
        <RouterLink to="/register">Register</RouterLink> -->
        <RouterLink to="/cars">Cars</RouterLink>
        <RouterLink to="/about">About</RouterLink>
        <RouterLink to="/roads">Roads</RouterLink>
        <RouterLink to="/trains">Trains</RouterLink>
        <RouterLink to="/engines">Engines</RouterLink>
        <RouterLink to="/stations">Stations</RouterLink>
        <RouterLink to="/journeys">Journeys</RouterLink>
        <RouterLink to="/carTypes">Car Types</RouterLink>
        <RouterLink to="/engineTypes">Engine Types</RouterLink>
        <RouterLink to="/map">Map</RouterLink>
      </div>
    </header>
    <RouterView />
  </div>
</template>

<style scoped>
.appwrap {
  display: flex;
  height: 100vh;
}
.nav {
  position: fixed;
  background-color: rgb(29, 126, 126);
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: center;
  height: 100vh;
  width: 200px;
}
.nav > a:hover {
  transform: scale(1.5);
}

.nav > a {
  width: 66.6%;
  text-align: center;
  border: 3px solid rgb(31, 175, 211);
  border-radius: 20px;
  font-weight: 500;
  font-size: 20px;
  transition: 0.5s;
}

.nav > a:link,
.nav > a:visited,
.nav > a:active,
.nav > a:active {
  color: black;
  text-decoration: none;
}
</style>
