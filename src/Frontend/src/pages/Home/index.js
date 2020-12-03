import React, { useEffect, useReducer } from "react";
import { BuildingForm } from "./components/BuildingForm";
import { BuildingList } from "./components/BuildingList";
import { fetchBuildings } from "./state/fetchBuildings";
import { overviewPageReducer } from "./state/overviewPageReducer";

export function Home() {
  var [overviewState, dispatch] = useReducer(overviewPageReducer, {
    isLoading: true,
    isError: false,
    buildings: [],
  });

  useEffect(function initialize() {
    fetchBuildings(dispatch);
  }, []);

  return (
    <>
      <h1>List of buildings</h1>
      <BuildingList buildings={overviewState.buildings} />
      <h2>Create new building</h2>
      <BuildingForm onBuildingCreated={() => fetchBuildings(dispatch)} />
    </>
  );
}
