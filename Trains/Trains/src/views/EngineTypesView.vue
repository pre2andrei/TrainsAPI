<script setup>
import { ref, reactive } from "vue";

const engineTypes = ref([]);
const selectedEngineTypeId = ref(0);
const newEngineType = reactive({ Name: null, Speed: 0 });
const isLoading = () => engineTypes.value.lendth === 0;

function isSelected(engineType) {
  return engineType.id === selectedEngineTypeId.value;
}

async function getEngineTypes() {
  engineTypes.value = [];
  const res = await fetch("https://localhost:7063/api/EngineTypes");
  const finalRes = await res.json();
  engineTypes.value = finalRes;
}

async function deleteEngineType(id) {
  const requestOptions = {
    method: "DELETE",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ id }),
  };
  await fetch(`https://localhost:7063/api/EngineTypes/${id}`, requestOptions);
  getEngineTypes();
}
async function create() {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(newEngineType),
  };
  await fetch(`https://localhost:7063/api/EngineTypes`, requestOptions);
  getEngineTypes();
}
async function save(engineType) {
  const requestOptions = {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(engineType),
  };
  await fetch(
    `https://localhost:7063/api/EngineTypes/${engineType.id}`,
    requestOptions
  );
  selectedEngineTypeId.value = 0;
  getEngineTypes();
}
getEngineTypes();

function Select(item) {
  selectedEngineTypeId.value = item.id;
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
            <strong>Name</strong>
          </th>
          <th>
            <strong>Speed</strong>
          </th>
          <th>
            <strong>Actions</strong>
          </th>
        </thead>
        <tbody>
          <tr v-for="item in engineTypes" :key="item.id">
            <td v-if="isSelected(item)"><input v-model="item.name" /></td>
            <td v-else>{{ item.name }}</td>
            <td v-if="isSelected(item)"><input v-model="item.speed" /></td>
            <td v-else>{{ item.speed }}</td>
            <td>
              <button v-if="isSelected(item)" @click="save(item)">Save</button>
              <button v-else @click="Select(item)">Select</button>
              <button @click="deleteEngineType(item.id)">Delete</button>&nbsp;
            </td>
          </tr>
          <tr>
            <td>
              <input name="newName" v-model="newEngineType.Name" type="text" />
            </td>
            <td>
              <input
                name="newSpeed"
                v-model="newEngineType.Speed"
                type="number"
              />
            </td>
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
  text-align: center;
}
</style>
