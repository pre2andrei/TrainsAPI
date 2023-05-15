<script setup>
import { ref } from "vue";

const stations = ref([]);
const selectedStationId = ref(0);
//const selectedStationName = ref("");
const newStationName = ref("");
const adding = ref(false);
const isLoading = () => {
  return stations.value.length === 0;
};
const isSelected = (station) => {
  return station.id === selectedStationId.value;
};
function Select(item) {
  console.log(item);
  selectedStationId.value = item.id;
}
async function getData() {
  stations.value = [];
  selectedStationId.value = 0;
  const res = await fetch("https://localhost:7063/api/Stations");
  const finalRes = await res.json();
  stations.value = finalRes;
}

async function save(station) {
  const requestOptions = {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(station),
  };
  await fetch(
    `https://localhost:7063/api/Stations/${selectedStationId.value}`,
    requestOptions
  );
  getData();
}

async function create() {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({
      Name: newStationName.value,
      station: { Name: newStationName.value, tos: [], froms: [] },
    }),
  };
  await fetch(`https://localhost:7063/api/Stations/`, requestOptions);
  selectedStationId.value = 0;
  newStationName.value = "";
  adding.value = false;
  getData();
}
async function deleteStation(id) {
  const requestOptions = {
    method: "DELETE",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ id }),
  };
  await fetch(`https://localhost:7063/api/Stations/${id}`, requestOptions);
  getData();
}

getData();
</script>
<template>
  <main>
    <div v-if="isLoading()">
      <h1>Loading....</h1>
    </div>
    <div class="stationsGrid" v-else>
      <div v-for="item in stations" :key="item.id">
        <div>
          <input v-if="isSelected(item)" v-model="item.name" />
          <input v-if="isSelected(item)" v-model="item.x" type="number" />
          <input v-if="isSelected(item)" v-model="item.y" type="number" />
          <div v-else>
            {{ item.name }} <br />
            x: {{ item.x ?? 0 }} y:
            {{ item.y ?? 0 }}
          </div>
        </div>
        <div>
          <button v-if="isSelected(item)" @click="save(item)">Save</button>
          <button v-else @click="Select(item)">Edit</button>
          <button @click="deleteStation(item.id)">Delete</button>
        </div>
      </div>
      <div>
        <div v-if="adding">
          new Station: &nbsp;<input v-model="newStationName" />
          <div>
            <button @click="create()">Create</button>
          </div>
        </div>
        <div v-else class="addBtn">
          <button @click="adding = !adding">+</button>
        </div>
      </div>
    </div>
  </main>
</template>

<style scoped>
input {
  height: 1.5rem;
}

.stationsGrid {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
}

.addBtn {
  margin: 5px;
}

.stationsGrid > div {
  padding: 10px;
  height: 200px;
  border: 1px solid black;
  color: black;
  font-size: 20px;
  display: flex;
  justify-content: space-between;
}

.stationsGrid > div > div:nth-child(2) {
  align-self: flex-end;
}
</style>
