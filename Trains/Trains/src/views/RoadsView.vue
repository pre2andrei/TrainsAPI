<script setup>
import { ref, reactive } from "vue";

const newRoad = reactive({ toId: 0, fromId: 0, length: 0 });
const selectedRoadId = ref(0);
const stations = ref([]);
const roads = ref([]);

const isSelected = (road) => road.id == selectedRoadId.value;
const isLoading = () => roads.value.lendth === 0;

const filteredStations = () =>
  stations.value.filter((x) => x.id != newRoad.fromId);

async function getRoads() {
  roads.value = [];
  const res = await fetch("https://localhost:7063/api/Roads");
  const finalRes = await res.json();
  roads.value = finalRes;
  console.log(roads.value);
}
async function getStations() {
  stations.value = [];
  const res = await fetch("https://localhost:7063/api/Stations");
  const finalRes = await res.json();
  stations.value = finalRes;
}
async function deleteRoad(id) {
  const requestOptions = {
    method: "DELETE",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ id }),
  };
  await fetch(`https://localhost:7063/api/Roads/${id}`, requestOptions);
  getRoads();
}
async function create() {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(newRoad),
  };
  await fetch(`https://localhost:7063/api/Roads`, requestOptions);
  getRoads();
}
async function save(road) {
  const requestOptions = {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(road),
  };
  await fetch(`https://localhost:7063/api/Roads/${road.id}`, requestOptions);
  selectedRoadId.value = 0;
  getRoads();
}
getRoads();
getStations();

function select(item) {
  selectedRoadId.value = item.id;
}
</script>

<template>
  <main>
    <div v-if="isLoading()">
      <h1>Loading...</h1>
    </div>
    <div v-else>
      <table>
        <thead>
          <th>
            <strong>From</strong>
          </th>
          <th>
            <strong>To</strong>
          </th>
          <th>
            <strong>Length(km)</strong>
          </th>
          <th>
            <strong>Actions</strong>
          </th>
        </thead>
        <tbody>
          <tr v-for="item in roads" :key="item.id">
            <template v-if="isSelected(item)">
              <td>
                <select v-model="item.fromId">
                  <option disabled value="">Please select one</option>
                  <option
                    v-for="item in stations"
                    :key="item.id"
                    :value="item.id"
                  >
                    {{ item.name }}
                  </option>
                </select>
              </td>
              <td>
                <select v-model="item.toId">
                  <option disabled value="">Please select one</option>
                  <option
                    v-for="item in filteredStations()"
                    :key="item.id"
                    :value="item.id"
                  >
                    {{ item.name }}
                  </option>
                </select>
              </td>
              <td><input v-model="item.length" type="number" /></td>
            </template>
            <template v-else>
              <td>{{ item.from.name }}</td>
              <td>{{ item.to.name }}</td>
              <td>{{ item.length }}</td>
            </template>
            <td>
              <button v-if="isSelected(item)" @click="save(item)">Save</button>
              <button v-else @click="select(item)">Select</button>
              <button @click="deleteRoad(item.id)">Delete</button>
              <button v-if="isSelected(item)" @click="selectedRoadId = 0">
                X
              </button>
            </td>
          </tr>
          <tr>
            <td>
              <select v-model="newRoad.fromId">
                <option disabled value="">Please select one</option>
                <option
                  v-for="item in stations"
                  :key="item.id"
                  :value="item.id"
                >
                  {{ item.name }}
                </option>
              </select>
            </td>
            <td>
              <select v-model="newRoad.toId">
                <option disabled value="">Please select one</option>
                <option
                  v-for="item in filteredStations()"
                  :key="item.id"
                  :value="item.id"
                >
                  {{ item.name }}
                </option>
              </select>
            </td>
            <td><input v-model="newRoad.length" type="number" /></td>
            <td><button @click="create">Create</button></td>
          </tr>
        </tbody>
      </table>
    </div>
  </main>
</template>
<style>
table {
  width: 100%;
}

td,
th {
  border: 1px solid black;
  color: black;
}
</style>
