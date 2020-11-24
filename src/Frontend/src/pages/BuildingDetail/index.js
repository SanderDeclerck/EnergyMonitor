import React, { useEffect, useState } from "react";
import { Link } from "@reach/router";

export function BuildingDetail({ buildingId }) {
  var [state, setState] = useState({ isLoading: true, building: {} });

  useEffect(function fetchBuilding() {
    fetch(`https://localhost:5001/api/building/${buildingId}`)
      .then((response) => response.json())
      .then((data) => setState({ ...state, isLoading: false, building: data }));
  }, []);

  return (
    <>
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
      <Link to="/">{"<< back"}</Link>
    </>
  );
}
