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
    }).then(onBuildingCreated);
  }

  return (
    <div>
      <h1>Create new building</h1>
      <label htmlFor="name">Name:</label>
      <input
        type="text"
        name="name"
        value={formState.name}
        onChange={handleInputChange}
        required
      ></input>
      <br />
      <label htmlFor="postalcode">Postal code:</label>
      <input
        type="text"
        name="postalcode"
        value={formState.postalCode}
        onChange={handleInputChange}
        required
      ></input>
      <br />
      <label htmlFor="city">City:</label>
      <input
        type="text"
        name="city"
        value={formState.city}
        onChange={handleInputChange}
        required
      ></input>
      <br />
      <label htmlFor="country">Country:</label>
      <input
        type="text"
        name="country"
        value={formState.country}
        onChange={handleInputChange}
        required
      ></input>
      <br />
      <button onClick={createBuilding}>Add</button>
    </div>
  );
}
