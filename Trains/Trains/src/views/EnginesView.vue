<script setup>
import { ref, reactive } from "vue";

const engines = ref([]);
const engineTypes = ref([]);
const selectedEngineId = ref(0);
const newEngine = reactive({ Code: null, EngineTypeId: 0 });

const isSelected = (engine) => engine.id === selectedEngineId.value;
const isLoading = () => engines.value.length === 0;

async function getEngineTypes() {
  const res = await fetch("https://localhost:7063/api/EngineTypes");
  const finalRes = await res.json();
  engineTypes.value = finalRes;
}

async function getEngines() {
  engines.value = [];
  const res = await fetch("https://localhost:7063/api/Engines");
  const finalRes = await res.json();
  engines.value = finalRes;
}

async function deleteEngine(id) {
  const requestOptions = {
    method: "DELETE",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ id }),
  };
  await fetch(`https://localhost:7063/api/Engines/${id}`, requestOptions);
  getEngines();
}
async function create() {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(newEngine),
  };
  await fetch(`https://localhost:7063/api/Engines`, requestOptions);
  newEngine.Code = "";
  newEngine.EngineTypeId = 0;
  getEngines();
}
async function save(engine) {
  const requestOptions = {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(engine),
  };
  await fetch(
    `https://localhost:7063/api/Engines/${engine.id}`,
    requestOptions
  );
  selectedEngineId.value = 0;
  getEngines();
}
getEngines();
getEngineTypes();
function select(engine) {
  selectedEngineId.value = engine.id;
}
function unselect() {
  selectedEngineId.value = 0;
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
          <th><strong>Code</strong></th>
          <th><strong>Engine Type</strong></th>
          <th><strong>Actions</strong></th>
        </thead>
        <tbody>
          <tr v-for="item in engines" :key="item.id">
            <template v-if="isSelected(item)">
              <td><input v-model="item.code" /></td>
              <td>
                <select v-model="item.engineTypeId">
                  <option disabled value="">Please select one</option>
                  <option
                    v-for="engine in engineTypes"
                    :key="engine.id"
                    :value="engine.id"
                  >
                    {{ engine.name }} - {{ engine.speed }} km/h
                  </option>
                </select>
              </td>
            </template>

            <template v-else>
              <td>{{ item.code }}</td>
              <td>{{ item.engineType.name }}</td>
            </template>

            <td>
              <button v-if="isSelected(item)" @click="save(item)">Save</button>
              <button v-else @click="select(item)">Select</button>
              <button @click="deleteEngine(item.id)">Delete</button>
              <button v-if="isSelected(item)" @click="unselect()">X</button>
            </td>
          </tr>
          <tr>
            <td><input v-model="newEngine.Code" type="text" /></td>
            <td>
              <select v-model="newEngine.EngineTypeId">
                <option disabled value="">Please select one</option>
                <option
                  v-for="item in engineTypes"
                  :key="item.id"
                  :value="item.id"
                >
                  {{ item.name }} - {{ item.speed }} km/h
                </option>
              </select>
            </td>
            <td>
              <button @click="create">Create</button>
            </td>
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
}
</style>
