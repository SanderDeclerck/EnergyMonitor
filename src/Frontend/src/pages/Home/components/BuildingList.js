import React from "react";
import { Link } from "@reach/router";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faBolt,
  faTint,
  faBurn,
  faCaretRight,
} from "@fortawesome/free-solid-svg-icons";

function getIcon(meterType) {
  switch (meterType) {
    case 1:
      return faBolt;
    case 2:
      return faTint;
    case 3:
      return faBurn;
  }
}

export function BuildingList({ buildings }) {
  return (
    <>
      {buildings.length == 0 ? "No buildings created yet" : ""}
      {buildings.map((building) => (
        <div className="building-card" key={building.id}>
          <h3>{building.name}</h3>
          <div className="icons">
            {building.meters.map((meter) => (
              <FontAwesomeIcon
                key={meter.eanCode}
                icon={getIcon(meter.meterType)}
              />
            ))}
          </div>
          <Link to={`/building/${building.id}`}>
            Details <FontAwesomeIcon icon={faCaretRight} />
          </Link>
        </div>
      ))}
    </>
  );
}
