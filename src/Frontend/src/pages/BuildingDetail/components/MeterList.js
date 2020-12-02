import { faBolt, faBurn, faTint } from "@fortawesome/free-solid-svg-icons";
import React from "react";
import { MeterListItem } from "./MeterListItem";

export function MeterList({ buildingId, meters, onMeterUpdated }) {
  return (
    <>
      {meters.map((meter) => (
        <MeterListItem
          buildingId={buildingId}
          meter={meter}
          onUpdated={onMeterUpdated}
        />
      ))}
    </>
  );
}

export function mapMeterType(meterType) {
  switch (meterType) {
    case 1:
      return { code: "E", className: "electricity", icon: faBolt };
    case 2:
      return { code: "W", className: "water", icon: faTint };
    case 3:
      return { code: "G", className: "gas", icon: faBurn };
  }
}

export function mapRegisterTariff(tariff) {
  switch (tariff) {
    case 1:
      return { name: "Peek" };
    case 2:
      return { name: "Off-peek" };
    case 3:
      return { name: "Day" };
  }
}
