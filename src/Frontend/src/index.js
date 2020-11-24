import React from "react";
import { render } from "react-dom";
import { Router } from "@reach/router";
import { Home } from "./pages/Home";
import { BuildingDetail } from "./pages/BuildingDetail";

window.addEventListener("DOMContentLoaded", function initApplication() {
  render(
    <Router>
      <Home path="/" />
      <BuildingDetail path="/building/:buildingId" />
    </Router>,
    document.getElementById("app")
  );
});
