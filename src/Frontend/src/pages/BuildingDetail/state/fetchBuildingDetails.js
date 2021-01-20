import {
  ERROR,
  LOADING_ACTION,
  RESPONSE_COMPLETE,
} from "./buildingDetailReducer";

export function fetchBuildingDetail(dispatch, buildingId) {
  dispatch({ type: LOADING_ACTION });

  fetch(`https://localhost:5001/api/building/${buildingId}`)
    .then((response) => response.json())
    .then((data) => dispatch({ type: RESPONSE_COMPLETE, payload: data }))
    .catch(() => dispatch({ type: ERROR }));
}
