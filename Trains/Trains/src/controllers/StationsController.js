import { API_ROOT } from "../utils/APIutils";

export async function getStations() {
    const res = await fetch(`${API_ROOT}api/Stations`);
    const data = await res.json();
    return data
}
export async function postStation(station) {
    const requestOptions = {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            Name: station.Name,
            station,
        }),
    };
    await fetch(`${API_ROOT}api/Stations`, requestOptions)
}
export async function putStation(id, station) {
    const requestOptions = {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            id,
            Name: selectedStationName.value,
            station,
        }),
    };
    await fetch(
        `${API_ROOT}api/Stations/${id}`,
        requestOptions
    );
}
export async function deleteStation(id){
    const requestOptions = {
        method: "DELETE",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ id }),
      };
      await fetch(`${API_ROOT}api/Stations/${id}`, requestOptions);
}