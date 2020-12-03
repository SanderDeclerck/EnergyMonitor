import React, { useEffect, useState } from "react";
import { Link } from "@reach/router";
import { MeterForm } from "./components/MeterForm";
import { MeterList } from "./components/MeterList";
import { BuildingSummary } from "./components/BuildingSummary";

export function BuildingDetail({ buildingId }) {
  var [state, setState] = useState({ isLoading: true, building: {} });

  useEffect(fetchBuilding, []);

  function fetchBuilding() {
    fetch(`https://localhost:5001/api/building/${buildingId}`)
      .then((response) => response.json())
      .then((data) => setState({ ...state, isLoading: false, building: data }));
  }

  return (
    <>
      <Link to="/">{"<< back"}</Link>
      <h1>Building</h1>
      {state.isLoading ? (
        "loading..."
      ) : (
        <BuildingSummary building={state.building} />
      )}
      <h2>Meters</h2>
      {state.isLoading ? (
        "loading..."
      ) : (
        <MeterList
          buildingId={buildingId}
          meters={state.building.meters}
          onMeterUpdated={fetchBuilding}
        />
      )}
      <h3>Add meter</h3>
      <MeterForm
        buildingId={state.building.id}
        onMeterCreated={fetchBuilding}
      />
    </>
  );
}

