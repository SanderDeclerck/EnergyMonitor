import React, { useEffect, useState } from "react";
import { Link } from "@reach/router";
import { MeterForm } from "./components/MeterForm";
import { MeterList } from "./components/MeterList";

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
      <div>
        {state.isLoading ? (
          "loading..."
        ) : (
          <ul>
            <li>Name: {state.building.name}</li>
            <li>Postal code: {state.building.postalCode}</li>
            <li>City: {state.building.city}</li>
            <li>Country: {state.building.country}</li>
          </ul>
        )}
      </div>
      <h2>Meters</h2>
      {state.isLoading ? (
        "loading..."
      ) : (
        <MeterList meters={state.building.meters} />
      )}
      <h3>Add meter</h3>
      <MeterForm buildingId={state.building.id} />
    </>
  );
}
