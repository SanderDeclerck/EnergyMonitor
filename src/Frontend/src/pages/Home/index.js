import React, { useEffect, useState } from "react";
import { BuildingForm } from "./components/BuildingForm";
import { BuildingList } from "./components/BuildingList";

export function Home() {
  var [buildings, setBuildings] = useState([]);

  function loadBuildings() {
    fetch("https://localhost:5001/api/building")
      .then((response) => response.json())
      .then((data) => setBuildings(data.buildings));
  }

  useEffect(loadBuildings, []);

  return (
    <>
      <h1>List of buildings:</h1>
      <BuildingList buildings={buildings} />
      <BuildingForm onBuildingCreated={loadBuildings} />
    </>
  );
}
