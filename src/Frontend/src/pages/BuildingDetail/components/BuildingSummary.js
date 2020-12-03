import React from "react";

export function BuildingSummary({ building }) {
  return (
    <div className="building-card">
      <h3>{building.name}</h3>
      <div className="address">
        {building.postalCode} {building.city}, {building.country}
      </div>
    </div>
  );
}
