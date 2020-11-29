import React from "react";

export function MeterList({ meters }) {
  return (
    <ul>
      {meters.map((meter) => (
        <MeterListItem meter={meter} />
      ))}
    </ul>
  );
}

function MeterListItem({ meter }) {
  var meterType = mapMeterType(meter.meterType);
  return (
    <li key={meter.eanCode}>
      {meterType.name}
      <ul>
        {meter.registers.map((register) => (
          <li>{mapRegisterTariff(register.tariff).name}: --</li>
        ))}
      </ul>
    </li>
  );
}

function mapMeterType(meterType) {
  switch (meterType) {
    case 1:
      return { code: "E", name: "Electricity", icon: "fa-bolt" };
    case 2:
      return { code: "W", name: "Water", icon: "fa-tint" };
    case 3:
      return { code: "G", name: "Gas", icon: "fa-burn" };
  }
}

function mapRegisterTariff(tariff) {
  console.log("hi");
  switch (tariff) {
    case 1:
      return { name: "Peek" };
    case 2:
      return { name: "Off-peek" };
    case 3:
      return { name: "Day" };
  }
}
