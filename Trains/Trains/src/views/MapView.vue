<script setup>
import { ref, onMounted, nextTick, onBeforeUnmount } from "vue";

const windowHeight = ref(0);
const windowWidth = ref(0);
const roads = ref([]);
const stations = ref([]);

async function getRoads() {
  roads.value = [];
  const res = await fetch("https://localhost:7063/api/Roads");
  const finalRes = await res.json();
  roads.value = finalRes;
}
async function getStations() {
  stations.value = [];
  const res = await fetch("https://localhost:7063/api/Stations");
  const finalRes = await res.json();
  stations.value = finalRes;
}

onMounted(() => {
  resize();
  nextTick(() => {
    window.addEventListener("resize", resize);
  });

  onBeforeUnmount(() => {
    window.removeEventListener("resize", resize);
  });

  function resize() {
    windowHeight.value = window.innerHeight;
    windowWidth.value = window.innerWidth - 200;
  }
});
function reduceY(value) {
  return (value * windowWidth.value) / 562.5;
}
function reduceX(value) {
  return (value * windowWidth.value) / 1000;
}
function dotSize() {
  return windowWidth.value / 150;
}
getStations();
getRoads();
</script>
<template>
  <main>
    <img src="../assets/map.png" alt="" />
    <svg>
      <line
        v-for="item in roads"
        :key="item.id"
        :x1="reduceX(item.from.x)"
        :y1="reduceY(item.from.y)"
        :x2="reduceX(item.to.x)"
        :y2="reduceY(item.to.y)"
        :stroke-width="dotSize()"
      />
      <circle
        v-for="item in stations"
        :key="item.id"
        :cx="reduceX(item.x)"
        :cy="reduceY(item.y)"
        :r="dotSize()"
      />
      <text
        v-for="item in stations"
        :key="item.id"
        :x="reduceX(item.x)"
        :y="reduceY(item.y - dotSize() / 2)"
      >
        {{ item.name }}
      </text>

      <text
        v-for="item in roads"
        :key="item.id"
        :x="reduceX((item.from.x + item.to.x) / 2)"
        :y="reduceY((item.from.y + item.to.y) / 2)"
      >
        {{ item.length + " " }}km
      </text>
    </svg>
  </main>
</template>

<style scoped>
main {
  padding: 0px;
  position: relative;
}

text,
circle,
line {
  font-weight: bold;
  font-size: medium;
  stroke: black;
  fill: white;
}

img {
  display: block;
  width: calc(100vw - 200px);
  aspect-ratio: 16/9;
  user-select: none;
  z-index: -1;
}

svg {
  border: 2px solid blue;
  position: absolute;
  top: 0;
  left: 0;
  width: calc(100vw - 200px);
  aspect-ratio: 16/9;
  z-index: 1;
}

circle:hover {
  fill: aqua;
}
</style>
