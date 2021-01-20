import { faCheck } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import React, { useState } from "react";

export function MeterReadingForm({ buildingId, meter, onReadingRegistered }) {
  var initialState = {
    registers: meter.registers.map((register) => {
      return { tariff: register.tariff, value: register.lastReading || 0 };
    }),
  };

  var [formState, setFormState] = useState(initialState);

  function handleInputChange(event) {
    var tariffCode = event.target.name;
    var newRegisters = formState.registers.map((register) =>
      register.tariff.code == tariffCode
        ? { tariff: register.tariff, value: Number(event.target.value) }
        : register
    );
    setFormState({ ...formState, registers: newRegisters });
  }

  function registerReading() {
    fetch(
      `https://localhost:5001/api/building/${buildingId}/meter/${meter.eanCode}/readings`,
      {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(mapFormStateToResponse()),
      }
    ).then(onReadingRegistered);
  }

  function mapFormStateToResponse() {
    var readings = formState.registers.map((register) => ({
      tariff: register.tariff.code,
      value: register.value,
    }));
    return { readings };
  }

  return (
    <>
      {formState.registers.map((register) => (
        <>
          <label htmlFor={register.tariff.code}>{register.tariff.name}</label>
          <input
            type="number"
            id={register.tariff.code}
            name={register.tariff.code}
            value={register.value}
            onChange={handleInputChange}
          />
        </>
      ))}
      <button onClick={registerReading}>
        <FontAwesomeIcon icon={faCheck} /> Save
      </button>
    </>
  );
}
