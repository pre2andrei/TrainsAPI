<script setup>
import { ref, reactive, toRaw } from "vue";

const trains = ref([]);
const cars = ref([]);
const engines = ref([]);

function select(train) {
  console.log(train);
  selectedTrain.Train.Id = train.id;
  selectedTrain.Train.Code = train.code;
  selectedTrain.Train.EngineId = train.engineId;
  selectedTrain.CarIds = train.cars.map((x) => x.id);
}
function deselect() {
  selectedTrain.Train.Id = 0;
  selectedTrain.Train.Code = "";
  selectedTrain.Train.EngineId = 0;
  selectedTrain.CarIds = [];
}
function resetCreation() {
  selectedTrain.Train.Id = 0;
  selectedTrain.Train.Code = "";
  selectedTrain.Train.EngineId = 0;
  selectedTrain.CarIds = [];
}

const newTrain = reactive({
  Train: { Code: null, EngineId: null },
  CarIds: [],
});

const selectedTrain = reactive({
  Train: { Id: 0, Code: null, EngineId: null },
  CarIds: [],
});
const isLoading = () => trains.value.length === 0;
const isSelected = (train) => train.id == selectedTrain.Train.Id;

async function getTrains() {
  const res = await fetch("https://localhost:7063/api/Trains");
  const finalRes = await res.json();
  trains.value = finalRes;
}
async function getCars() {
  const res = await fetch("https://localhost:7063/api/Cars/notraincars");
  const finalRes = await res.json();
  cars.value = finalRes;
}
async function getEngines() {
  const res = await fetch("https://localhost:7063/api/Engines/notrainengines");
  const finalRes = await res.json();
  engines.value = finalRes;
}

function newTrainToggleCar(carId) {
  if (!newTrain.CarIds.includes(carId)) {
    newTrain.CarIds.push(carId);
  } else {
    newTrain.CarIds.splice(newTrain.CarIds.indexOf(carId), 1); //deleting
  }
}

function selectedTrainToggleCar(carId) {
  if (!selectedTrain.CarIds.includes(carId)) {
    selectedTrain.CarIds.push(carId);
  } else {
    selectedTrain.CarIds.splice(selectedTrain.CarIds.indexOf(carId), 1); //deleting
  }
}

async function deleteTrain(id) {
  const requestOptions = {
    method: "DELETE",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ id }),
  };
  await fetch(`https://localhost:7063/api/Trains/${id}`, requestOptions);

  getTrains();
  getEngines();
  getCars();
}
async function create() {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(toRaw(newTrain)),
  };
  await fetch(`https://localhost:7063/api/Trains`, requestOptions);
  resetCreation();
  getTrains();
  getEngines();
  getCars();
}
async function save() {
  const requestOptions = {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(selectedTrain),
  };
  await fetch(
    `https://localhost:7063/api/Trains/${selectedTrain.Train.Id}`,
    requestOptions
  );
  getTrains();
  getEngines();
  getCars();
  deselect();
}
getTrains();
getEngines();
getCars();
</script>

<template>
  <main>
    <div v-if="isLoading()">
      <h1>Loading...</h1>
    </div>
    <div v-else>
      <template v-for="item in trains" :key="item.id">
        <table>
          <tbody>
            <tr>
              <td v-if="isSelected(item)" colspan="3">
                <input v-model="selectedTrain.Train.Code" />
              </td>
              <td v-else colspan="3">
                <strong>{{ item.code }}</strong>
              </td>
              <td rowspan="3">
                <button v-if="isSelected(item)" @click="save">Save</button>
                <button v-else @click="select(item)">Select</button>
                <button @click="deleteTrain">Delete</button>
                <button v-if="isSelected(item)" @click="deselect">X</button>
              </td>
            </tr>
            <tr>
              <td colspan="1">
                <strong>Engine</strong>
              </td>
              <td colspan="2">
                <strong>Cars</strong>
              </td>
            </tr>
            <tr>
              <td v-if="isSelected(item)" colspan="1">
                <select v-model="selectedTrain.Train.EngineId">
                  <option disabled value="">Please select one</option>
                  <option
                    v-for="item in engines"
                    :key="item.id"
                    :value="item.id"
                  >
                    {{ item.code }} - {{ item.engineType.name }}
                  </option>
                </select>
              </td>
              <td v-else colspan="1">
                {{ item.engine.code }}
              </td>

              <td v-if="isSelected(item)" colspan="2">
                <span
                  v-for="item in [...item.cars, ...cars]"
                  :key="item.id"
                  :value="item.id"
                  @click="selectedTrainToggleCar(item.id)"
                >
                  &nbsp;<span
                    :style="{
                      color: selectedTrain.CarIds.includes(item.id)
                        ? 'green'
                        : 'yellow',
                    }"
                  >
                    {{ item.code }}
                  </span>
                </span>
              </td>

              <td v-else colspan="2">
                <template v-for="car in item.cars">
                  &nbsp;{{ car.code }},
                </template>
              </td>
            </tr>
          </tbody>
        </table>
        <br />
      </template>
    </div>

    <h1>Create</h1>

    <table>
      <tbody>
        <tr>
          <td colspan="3">
            <input v-model="newTrain.Train.Code" type="text" />
          </td>
          <td rowspan="3">
            <button @click="create">Create</button>
          </td>
        </tr>
        <tr>
          <td colspan="1">
            <strong>Engine</strong>
          </td>
          <td colspan="2">
            <strong>Cars</strong>
          </td>
        </tr>
        <tr>
          <td colspan="1">
            <select v-model="newTrain.Train.EngineId">
              <option disabled value="">Please select one</option>
              <option v-for="item in engines" :key="item.id" :value="item.id">
                {{ item.code }} - {{ item.engineType.name }}
              </option>
            </select>
          </td>
          <td
            v-if="cars.length == 0"
            style="color: red; font-weight: bold"
            colspan="2"
          >
            No cars available for a new train!
          </td>

          <td v-else colspan="2">
            <span
              v-for="item in cars"
              :key="item.id"
              :value="item.id"
              @click="newTrainToggleCar(item.id)"
            >
              &nbsp;<span
                :style="{
                  color: newTrain.CarIds.includes(item.id) ? 'green' : 'yellow',
                }"
              >
                {{ item.code }}
              </span>
            </span>
          </td>
        </tr>
      </tbody>
    </table>
  </main>
</template>
<style scoped>
table {
  width: 100%;
  table-layout: fixed;
}

td,
th {
  border: 1px solid black;
  color: black;
  text-align: center;
}

strong {
  font-weight: bold;
}
</style>
