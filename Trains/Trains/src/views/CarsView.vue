<script setup>
import { ref, reactive } from "vue";

const cars = ref([]);
const carTypes = ref([]);
const selectedCarId = ref(0);
const newCar = reactive({
  Code: "",
  FirstSeat: 0,
  LastSeat: 0,
  CarTypeId: null,
  TrainId: null,
});

const isSelected = (car) => car.id == selectedCarId.value;
const isLoading = () => cars.value.length == 0;

async function getCarTypes() {
  const res = await fetch("https://localhost:7063/api/CarTypes");
  const finalRes = await res.json();
  carTypes.value = finalRes;
}
async function getCars() {
  const res = await fetch("https://localhost:7063/api/Cars");
  const finalRes = await res.json();
  cars.value = finalRes;
  selectedCarId.value = 0;
}

async function deleteCar(id) {
  const requestOptions = {
    method: "DELETE",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ id }),
  };
  await fetch(`https://localhost:7063/api/Cars/${id}`, requestOptions);
  getCars();
}
async function create() {
  console.log(newCar);
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(newCar),
  };
  await fetch(`https://localhost:7063/api/Cars`, requestOptions);
  getCars();
}
async function save(car) {
  const requestOptions = {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(car),
  };
  await fetch(`https://localhost:7063/api/Cars/${car.id}`, requestOptions);
  getCars();
}
getCars();
getCarTypes();
function select(item) {
  selectedCarId.value = item.id;
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
            <strong>Code</strong>
          </th>
          <th>
            <strong>car Type</strong>
          </th>
          <th>
            <strong>First Seat</strong>
          </th>
          <th>
            <strong>Last Seat</strong>
          </th>
          <th>
            <strong>Train</strong>
          </th>
          <th>
            <strong>Actions</strong>
          </th>
        </thead>
        <tbody>
          <tr v-for="item in cars" :key="item.id">
            <template v-if="isSelected(item)">
              <td><input v-model="item.code" /></td>
              <td>
                <select v-model="item.carTypeId">
                  <option disabled value="">Please select one</option>
                  <option
                    v-for="item in carTypes"
                    :key="item.id"
                    :value="item.id"
                  >
                    {{ item.name }} - {{ item.price }} RON
                  </option>
                </select>
              </td>
              <td><input v-model="item.firstSeat" type="number" /></td>
              <td><input v-model="item.lastSeat" type="number" /></td>

              <td>{{ item.train == null ? "No Train" : item.train.code }}</td>
            </template>

            <template v-else>
              <td>{{ item.code }}</td>
              <td>{{ item.carType.name }}</td>
              <td>{{ item.firstSeat }}</td>
              <td>{{ item.lastSeat }}</td>
              <td>
                {{ item.train == null ? "No Train" : item.train.code }}
              </td>
            </template>

            <td>
              <button v-if="isSelected(item)" @click="save(item)">Save</button>
              <button v-else @click="select(item)">Select</button>
              <button @click="deleteCar(item.id)">Delete</button>
              <button v-if="isSelected(item)" @click="selectedCarId = 0">
                X
              </button>
            </td>
          </tr>
          <tr>
            <td><input v-model="newCar.Code" /></td>
            <td>
              <select v-model="newCar.CarTypeId">
                <option disabled value="">Please select one</option>
                <option
                  v-for="item in carTypes"
                  :key="item.id"
                  :value="item.id"
                >
                  {{ item.name }} - {{ item.price }} RON
                </option>
              </select>
            </td>
            <td><input v-model="newCar.FirstSeat" /></td>
            <td>
              <input v-model="newCar.LastSeat" type="text" />
            </td>
            <td>No Train</td>
            <td><button @click="create()">Create</button></td>
          </tr>
        </tbody>
      </table>
    </div>
  </main>
</template>
<style scoped>
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
