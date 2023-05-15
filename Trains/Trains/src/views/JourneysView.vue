<script setup>
import { ref, reactive, toRaw } from "vue";
const trains = ref([]);
const roads = ref([]);
const journeys = ref([]);
const stations = ref([]);
// const newJourneyRoads = reactive({ roads: [] });
// const newJourneyAuxRoad = reactive({ value: null });
// const newJourneyFirstStation = reactive({ value: null });

const newJourney = reactive({
  Code: null,
  TrainId: null,
  Departure: null,
  RoadsIds: [],
});
const isLoading = () => journeys.value.length === 0;

async function getTrains() {
  const res = await fetch("https://localhost:7063/api/Trains");
  const finalRes = await res.json();
  trains.value = finalRes;
}
async function getRoads() {
  const res = await fetch("https://localhost:7063/api/Roads");
  const finalRes = await res.json();
  roads.value = finalRes;
}
async function getJourneys() {
  const res = await fetch("https://localhost:7063/api/Journeys");
  const finalRes = await res.json();
  journeys.value = finalRes;
}
async function getStations() {
  stations.value = [];
  const res = await fetch("https://localhost:7063/api/Stations");
  const finalRes = await res.json();
  stations.value = finalRes;
}

function toggleRoad(roadId) {
  if (!newJourney.RoadsIds.includes(roadId)) {
    newJourney.RoadsIds.push(roadId);
  } else {
    const index = newJourney.RoadsIds.indexOf(roadId);
    newJourney.RoadsIds.length = index;
  }
}

function filteredRoads() {
  let fromId = 0;
  if (newJourney.RoadsIds.length == 0) return roads.value;
  const notGoodIds = [];

  for (let i = 0; i < newJourney.RoadsIds.length - 1; i++) {
    const newJourneyRoadId = newJourney.RoadsIds[i];
    for (let j = 0; j < roads.value.length; j++) {
      const newJourneyRoad = roads.value[j];
      if (newJourneyRoad.id == newJourneyRoadId) {
        for (let k = 0; k < roads.value.length; k++) {
          const element = roads.value[k];
          if (
            element.fromId == newJourneyRoad.fromId ||
            element.toId == newJourneyRoad.fromId ||
            element.fromId == newJourneyRoad.toId ||
            element.toId == newJourneyRoad.toId
          )
            if (!notGoodIds.includes(element.id)) notGoodIds.push(element.id);
        }
        break;
      }
    }
  }

  const roadId = newJourney.RoadsIds[newJourney.RoadsIds.length - 1];

  for (let i = 0; i < roads.value.length; i++) {
    const element = roads.value[i];
    if (element.id == roadId) {
      fromId = element.toId;
      break;
    }
  }

  const filteredRoads = [];
  for (let i = 0; i < roads.value.length; i++) {
    const element = roads.value[i];
    if (notGoodIds.includes(element.id)) continue;
    if (element.fromId == fromId) {
      filteredRoads.push(element);
    }
  }
  return filteredRoads;
}

async function deleteJourney(id) {
  const requestOptions = {
    method: "DELETE",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ id }),
  };
  await fetch(`https://localhost:7063/api/Journeys/${id}`, requestOptions);
  getTrains();
}
async function create() {
  console.log(toRaw(newJourney));
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(toRaw(newJourney)),
  };
  await fetch(`https://localhost:7063/api/Journeys`, requestOptions);
  getJourneys();
}

function roadWithId(id) {
  for (let i = 0; i < roads.value.length; i++) {
    const road = roads.value[i];
    if (road.id == id) {
      return road;
    }
  }
}

function toRoads() {
  return newJourney.RoadsIds.map((x) => roadWithId(x));
}

getTrains();
getJourneys();
getRoads();
getStations();
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
            <strong>Engine</strong>
          </th>
          <th>
            <strong>Departure</strong>
          </th>
          <th>
            <strong>Roads</strong>
          </th>
          <th>
            <strong>Actions</strong>
          </th>
        </thead>

        <tbody>
          <tr v-for="item in journeys" :key="item.id">
            <td>{{ item.code }}</td>
            <td>{{ item.train.code }}</td>
            <td>{{ item.departure }}</td>
            <td>
              <ul>
                <li v-for="road in item.roads" :key="road.id">
                  {{ road.from.name }} -> {{ road.to.name }}
                </li>
              </ul>
            </td>
            <td>
              <button @click="deleteJourney(item.id)">Delete</button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <h1>Create</h1>
    <label for="newCode">Code:</label>
    <input name="newCode" v-model="newJourney.Code" type="text" />&nbsp;
    <label>Train:</label>
    <select v-model="newJourney.TrainId">
      <option disabled value="">Please select one</option>
      <option v-for="item in trains" :key="item.id" :value="item.id">
        {{ item.code }}
      </option></select
    >&nbsp;
    <label>Departure:</label>
    <input type="datetime-local" v-model="newJourney.Departure" />
    <div style="display: flex; justify-content: start; align-items: center">
      <template v-if="newJourney.RoadsIds.length > 0">
        <span>{{ roadWithId(newJourney.RoadsIds[0]).from.name }}</span>

        <span
          v-for="item in toRoads()"
          :key="item.id"
          @click="toggleRoad(item.id)"
        >
          &nbsp;->&nbsp;{{ item.to.name }}
        </span>
      </template>
      <ul>
        <li
          v-for="item in filteredRoads()"
          :key="item.id"
          :value="item.id"
          @click="toggleRoad(item.id)"
        >
          {{ `${item.from.name} -> ${item.to.name}` }}
        </li>
      </ul>
    </div>
    <button @click="create">Create</button>
    <div class="createZone"></div>
  </main>
</template>
<style scoped>
table {
  width: 100%;
}

ul,
li {
  list-style-type: none;
  list-style: none;
}

div > ul > li {
  border: 2px solid cyan;
  border-radius: 15px;
}

span {
  font-size: large;
  font-weight: bolder;
}

td,
th {
  border: 1px solid black;
  color: black;
  text-align: center;
}

select,
input,
button {
  max-width: 200px;
}
</style>
