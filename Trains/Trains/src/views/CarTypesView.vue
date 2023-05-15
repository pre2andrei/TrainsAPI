<script setup>
import { ref, reactive } from "vue";

const carTypes = ref([]);
const selectedCarTypeId = ref(0);
const newCarType = reactive({ Name: null, Price: 0 });

const isSelected = (carType) => carType.id === selectedCarTypeId.value;
const isLoading = () => carTypes.value.lendth === 0;

async function getCarTypes() {
  carTypes.value = [];
  const res = await fetch("https://localhost:7063/api/CarTypes");
  const finalRes = await res.json();
  carTypes.value = finalRes;
}

async function deleteEngineType(id) {
  const requestOptions = {
    method: "DELETE",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ id }),
  };
  await fetch(`https://localhost:7063/api/CarTypes/${id}`, requestOptions);
  getCarTypes();
}
async function create() {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(newCarType),
  };
  await fetch(`https://localhost:7063/api/CarTypes`, requestOptions);
  getCarTypes();
}
async function save(carType) {
  const requestOptions = {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(carType),
  };
  await fetch(
    `https://localhost:7063/api/CarTypes/${carType.id}`,
    requestOptions
  );
  getCarTypes();
}
getCarTypes();

function Select(item) {
  selectedCarTypeId.value = item.id;
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
            <strong>Price</strong>
          </th>
          <th>
            <strong>Actions</strong>
          </th>
        </thead>
        <tbody>
          <tr v-for="item in carTypes" :key="item.id">
            <td v-if="isSelected(item)">
              <input v-model="item.name" type="text" />
            </td>
            <td v-else>{{ item.name }}</td>
            <td v-if="isSelected(item)">
              <input v-model="item.price" type="text" />
            </td>
            <td v-else>{{ item.price }}</td>
            <td>
              <button v-if="isSelected(item)" @click="save(item)">Save</button>
              <button v-else @click="Select(item)">Select</button>
              <button @click="deleteEngineType(item.id)">Delete</button>
              <button
                v-if="isSelected(item)"
                @click="
                  () => {
                    selectedCarTypeId = 0;
                  }
                "
              >
                X
              </button>
            </td>
          </tr>
          <tr>
            <td><input v-model="newCarType.Name" type="text" /></td>
            <td><input v-model="newCarType.Price" type="number" /></td>
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
