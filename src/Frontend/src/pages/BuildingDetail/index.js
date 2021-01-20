import React, { useContext } from "react";
import { Link } from "@reach/router";
import { MeterForm } from "./components/MeterForm";
import { MeterList } from "./components/MeterList";
import { BuildingSummary } from "./components/BuildingSummary";
import { BuildingDetailContext } from "./state/BuildingDetailContext";

export function BuildingDetail() {
  var { buildingDetail } = useContext(BuildingDetailContext);

  return (
    <>
      <Link to="/">{"<< back"}</Link>
      <h1>Building</h1>
      {buildingDetail.isLoading ? "loading..." : <BuildingSummary />}
      <h2>Meters</h2>
      {buildingDetail.isLoading ? "loading..." : <MeterList />}
      <h3>Add meter</h3>
      <MeterForm />
    </>
  );
}
