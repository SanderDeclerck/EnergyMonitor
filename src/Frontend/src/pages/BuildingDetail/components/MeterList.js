import {
  faBolt,
  faBurn,
  faPencilAlt,
  faTint,
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import React from "react";

export function MeterList({ meters }) {
  return (
    <>
      {meters.map((meter) => (
        <MeterListItem meter={meter} />
      ))}
    </>
  );
}

function MeterListItem({ meter }) {
  var meterType = mapMeterType(meter.meterType);
  return (
    <div className={`meter-card ${meterType.className}`} key={meter.eanCode}>
      <div className="icon-container">
        <FontAwesomeIcon icon={meterType.icon} size="1x" />
      </div>
      <div className="meter-details">
        {meter.registers.map((register) => (
          <div>
            <strong>{mapRegisterTariff(register.tariff).name}</strong> 123456
          </div>
        ))}
      </div>
      <a href="#" className="edit-button">
        <FontAwesomeIcon icon={faPencilAlt} />
      </a>
    </div>
  );
}

function mapMeterType(meterType) {
  switch (meterType) {
    case 1:
      return { code: "E", className: "electricity", icon: faBolt };
    case 2:
      return { code: "W", className: "water", icon: faTint };
    case 3:
      return { code: "G", className: "gas", icon: faBurn };
  }
}

function mapRegisterTariff(tariff) {
  switch (tariff) {
    case 1:
      return { name: "Peek" };
    case 2:
      return { name: "Off-peek" };
    case 3:
      return { name: "Day" };
  }
}
