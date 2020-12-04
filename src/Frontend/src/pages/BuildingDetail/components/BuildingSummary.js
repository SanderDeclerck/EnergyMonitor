import React, { useContext } from "react";
import { BuildingDetailContext } from "../state/BuildingDetailContext";

export function BuildingSummary() {
  var { buildingDetail } = useContext(BuildingDetailContext);
  return (
    <div className="building-card">
      <h3>{buildingDetail.building.name}</h3>
      <div className="address">
        {buildingDetail.building.postalCode} {buildingDetail.building.city},{" "}
        {buildingDetail.building.country}
      </div>
    </div>
  );
}
