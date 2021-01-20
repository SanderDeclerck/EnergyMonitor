import {
  LOADING_ACTION,
  SET_BUILDING,
  GET_BUILDING_FAILED
} from "./overviewPageReducer";

export function fetchBuildings(dispatch) {
  dispatch({ type: LOADING_ACTION });
  
  fetch("https://localhost:5001/api/building")
    .then((response) => response.json())
    .then((data) => dispatch({ type: SET_BUILDING, payload: data.buildings }))
    .catch(() => dispatch({ type: GET_BUILDING_FAILED }));
}
