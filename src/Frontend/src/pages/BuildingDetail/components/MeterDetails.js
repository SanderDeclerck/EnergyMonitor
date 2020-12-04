import React from "react";

export function MeterDetails({ meter }) {
  var formatter = new Intl.NumberFormat(undefined, {
    minimumFractionDigits: 1,
    maximumFractionDigits: 1,
    useGrouping: false,
  });

  return (
    <>
      {meter.registers.map((register) => (
        <div key={register.tariff.code}>
          <div className="meter-reading">
            <span className="reading">
              {formatter.format(register.lastReading)}
            </span>
            <span className="tariff">{register.tariff.name}</span>
          </div>
        </div>
      ))}
    </>
  );
}
