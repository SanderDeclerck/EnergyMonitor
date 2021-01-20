import React from "react";
import { render } from "react-dom";
import { Router } from "@reach/router";
import { Home } from "./pages/Home";
import { BuildingDetail } from "./pages/BuildingDetail";
import { BuildingDetailProvider } from "./pages/BuildingDetail/state/BuildingDetailContext";

window.addEventListener("DOMContentLoaded", function initApplication() {
  render(
    <Router>
      <Home path="/" />
      <BuildingDetailProvider path="/building/:buildingId">
        <BuildingDetail default />
      </BuildingDetailProvider>
    </Router>,
    document.getElementById("app")
  );
});
