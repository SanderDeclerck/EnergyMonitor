import {
  faCheck,
  faPencilAlt,
  faTimes,
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import React, { useState } from "react";
import { mapMeterType, mapRegisterTariff } from "./MeterList";

export function MeterListItem({ buildingId, meter, onUpdated }) {
  var [isEditMode, setEditMode] = useState(false);

  function toggleEditMode(ev) {
    ev && ev.preventDefault();
    setEditMode(!isEditMode);
  }

  function meterReadingRegistered() {
    toggleEditMode();
    onUpdated();
  }

  var meterType = mapMeterType(meter.meterType);
  return (
    <div className={`meter-card ${meterType.className}`} key={meter.eanCode}>
      <div className="icon-container">
        <FontAwesomeIcon icon={meterType.icon} size="1x" />
      </div>
      <div className="meter-details">
        {isEditMode ? (
          <MeterReadingForm
            buildingId={buildingId}
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

function MeterDetails({ meter }) {
  var formatter = new Intl.NumberFormat(undefined, { minimumFractionDigits: 1, maximumFractionDigits: 1, useGrouping: false });

  return (
    <>
      {meter.registers.map((register) => (
        <div key={register.tariff}>
          <div className="meter-reading">
            <span className="reading">{formatter.format(register.lastReading)}</span>
            <span className="tariff">
              {mapRegisterTariff(register.tariff).name}
            </span>
          </div>
        </div>
      ))}
    </>
  );
}

function MeterReadingForm({ buildingId, meter, onReadingRegistered }) {
  var initialState = {
    registers: meter.registers.map((register) => {
      return { tariff: register.tariff, value: register.lastReading || 0 };
    }),
  };

  var [formState, setFormState] = useState(initialState);

  function handleInputChange(event) {
    var tariff = event.target.name;
    var newRegisters = formState.registers.map(function updateValue(register) {
      if (register.tariff == tariff) {
        return { tariff: register.tariff, value: Number(event.target.value) };
      }
      return register;
    });
    setFormState({ ...formState, registers: newRegisters });
  }

  function registerReading() {
    var data = {
      readings: formState.registers.map(mapRegisterToRestData),
    };

    fetch(
      `https://localhost:5001/api/building/${buildingId}/meter/${meter.eanCode}/readings`,
      {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data),
      }
    ).then(onReadingRegistered);

    function mapRegisterToRestData(register) {
      return { tariff: register.tariff, value: register.value };
    }
  }

  return (
    <>
      {formState.registers.map((register) => (
        <>
          <label htmlFor={register.tariff}>
            {mapRegisterTariff(register.tariff).name}
          </label>
          <input
            type="number"
            name={register.tariff}
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
