import React, { useContext, useState } from "react";
import { BuildingDetailContext } from "../state/BuildingDetailContext";

export function MeterForm() {
  var { buildingDetail, reload } = useContext(BuildingDetailContext);
  var [formState, setFormState] = useState({
    eanCode: "",
    meterType: undefined,
    hasOffPeakRegister: false,
  });

  function handleInputChange(event) {
    var name = event.target.name;
    var value =
      event.target.type == "checkbox"
        ? event.target.checked
        : event.target.value;
    setFormState({ ...formState, [name]: value });
  }

  function createMeter() {
    var data = {
      ...formState,
      hasOffPeakRegister:
        formState.meterType == 1 && formState.hasOffPeakRegister,
    };

    fetch(
      `https://localhost:5001/api/building/${buildingDetail.building.id}/meter`,
      {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data),
      }
    ).then(meterCreated);
  }

  function meterCreated() {
    reload();
    setFormState(initialState);
  }

  return (
    <div>
      <label htmlFor="eanCode">Ean code</label>
      <input
        type="text"
        name="eanCode"
        value={formState.eanCode}
        onChange={handleInputChange}
      />
      <label htmlFor="meterType">Type</label>
      <select
        name="meterType"
        value={formState.meterType}
        onChange={handleInputChange}
      >
        <option value="">---</option>
        <option value="1">Electricity</option>
        <option value="2">Water</option>
        <option value="3">Gas</option>
      </select>
      <input
        hidden={formState.meterType != 1}
        type="checkbox"
        name="hasOffPeakRegister"
        checked={formState.hasOffPeakRegister}
        onChange={handleInputChange}
      />
      <label hidden={formState.meterType != 1} htmlFor="hasOffPeakRegister">
        Has off peak register
      </label>
      <button onClick={createMeter}>Add</button>
    </div>
  );
}
