import React, { useState } from "react";

export function BuildingForm({ onBuildingCreated }) {
  var initialState = {
    name: "",
    postalcode: "",
    city: "",
    country: "",
  };

  var [formState, setFormState] = useState(initialState);

  function handleInputChange(event) {
    var name = event.target.name;
    var value = event.target.value;
    setFormState({ ...formState, [name]: value });
  }

  function createBuilding() {
    fetch("https://localhost:5001/api/building", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(formState),
    }).then(buildingCreated);
  }

  function buildingCreated() {
    onBuildingCreated();
    setFormState(initialState);
  }

  return (
    <div>
      <label htmlFor="name">Name</label>
      <input
        type="text"
        name="name"
        value={formState.name}
        onChange={handleInputChange}
        required
      ></input>
      <label htmlFor="postalcode">Postal code</label>
      <input
        type="text"
        name="postalcode"
        value={formState.postalCode}
        onChange={handleInputChange}
        required
      ></input>
      <label htmlFor="city">City</label>
      <input
        type="text"
        name="city"
        value={formState.city}
        onChange={handleInputChange}
        required
      ></input>
      <label htmlFor="country">Country</label>
      <input
        type="text"
        name="country"
        value={formState.country}
        onChange={handleInputChange}
        required
      ></input>
      <button onClick={createBuilding}>Add</button>
    </div>
  );
}
