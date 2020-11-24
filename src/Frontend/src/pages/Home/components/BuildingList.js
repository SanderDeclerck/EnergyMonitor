import React from "react";
import { Link } from "@reach/router";

export function BuildingList({ buildings }) {
  return (
    <>
      {buildings.length == 0 ? "No buildings created yet" : ""}
      <ul>
        {buildings.map((building) => (
          <li key={building.id}>
            {building.name} -{" "}
            <Link to={`/building/${building.id}`}>details</Link>
          </li>
        ))}
      </ul>
    </>
  );
}
