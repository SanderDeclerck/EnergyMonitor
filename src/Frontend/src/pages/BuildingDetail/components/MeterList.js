import { faBolt, faBurn, faTint } from "@fortawesome/free-solid-svg-icons";
import React, { useContext } from "react";
import { BuildingDetailContext } from "../state/BuildingDetailContext";
import { MeterListItem } from "./MeterListItem";

export function MeterList() {
  var { buildingDetail } = useContext(BuildingDetailContext);

  return (
    <>
      {buildingDetail.building.meters.map((meter) => (
        <MeterListItem key={meter.eanCode} meter={meter} />
      ))}
    </>
  );
}
