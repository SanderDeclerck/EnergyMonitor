import React, { createContext, useEffect, useReducer } from "react";
import { buildingDetailReducer } from "./buildingDetailReducer";
import { fetchBuildingDetail } from "./fetchBuildingDetails";

export const BuildingDetailContext = createContext();

export function BuildingDetailProvider({ buildingId, children }) {
  var [buildingDetail, dispatch] = useReducer(buildingDetailReducer, {
    isLoading: true,
    isError: false,
    building: {},
  });

  useEffect(reload, [buildingId]);

  function reload() {
    fetchBuildingDetail(dispatch, buildingId);
  }

  var value = { buildingDetail, reload };

  return (
    <BuildingDetailContext.Provider value={value}>
      {children}
    </BuildingDetailContext.Provider>
  );
}
