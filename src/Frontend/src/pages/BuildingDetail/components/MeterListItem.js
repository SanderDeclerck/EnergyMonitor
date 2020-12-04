import { faPencilAlt, faTimes } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import React, { useContext, useState } from "react";
import { BuildingDetailContext } from "../state/BuildingDetailContext";
import { MeterDetails } from "./MeterDetails";
import { MeterReadingForm } from "./MeterReadingForm";

export function MeterListItem({ meter }) {
  var { buildingDetail, reload } = useContext(BuildingDetailContext);
  var [isEditMode, setEditMode] = useState(false);

  function toggleEditMode() {
    setEditMode(!isEditMode);
    return false;
  }

  function meterReadingRegistered() {
    toggleEditMode();
    reload();
  }

  return (
    <div
      className={`meter-card ${meter.meterType.className}`}
      key={meter.eanCode}
    >
      <div className="icon-container">
        <FontAwesomeIcon icon={meter.meterType.icon} size="1x" />
      </div>
      <div className="meter-details">
        {isEditMode ? (
          <MeterReadingForm
            buildingId={buildingDetail.building.id}
            meter={meter}
            onReadingRegistered={meterReadingRegistered}
          />
        ) : (
          <MeterDetails meter={meter} />
        )}
      </div>
      <a href="#" className="button" onClick={toggleEditMode}>
        <FontAwesomeIcon icon={isEditMode ? faTimes : faPencilAlt} />
      </a>
    </div>
  );
}
